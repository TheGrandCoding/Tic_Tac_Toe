using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using System.Net;
using System.Net.Sockets;

using System.IO;
using System.Windows.Forms;
using System.Reflection;
using Microsoft.Win32;
using System.Security.Cryptography;
using System.Timers;


namespace TicTacToeServer
{
    class Program
    {
        public static bool Found;
        public static List<Spectator> AllSpectators = new List<Spectator>();
        public static List<string> AllSpectatorsNames = new List<string>();
        public static List<string> Lcommands = new List<string>() { "/start q [MatchesPerGame] [BotLevel] ", "/start n [BotLevel]","/Kick [name]", "/admin add [name]","/admin remove [name]","/users add [username] [name]","/end" };
        public static bool runningPortForward = false; // true = clients can connect globally: false = clients can only connect from within network
        public static string Server_Name = ">>JOIN HERE<<";//http://masterlist.uk.to:8889/ website 
        public static Tournament CurrentTournament = null; // set from Form1
        public static bool Quick;
        public static bool masterlist;
        public static MasterlistDLL.MasterlistServer MASTERLIST;
        public static List<string> AllPlayersnames = new List<string>();
        public static Int32 port = 1234; // server port
        public static IPAddress V4iplocal; // server ip
        public static IPAddress V6iplocal;
        public static TcpListener VfourServer;
        public static TcpListener VsixServer;
        public static bool startgame = false; // presume this is used in a while-loop-waiting 
        public static Form1 f1 = new Form1();
        public static List<Player> AllPlayers = new List<Player>();
        public static List<Thread> ServerThreads = new List<Thread>();
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            GetLocalIPAddress(); // sets localip variable.
            Thread st = new Thread(start);
            st.Start();
            Thread CD = new Thread(Commands);
            CD.Start();
            f1.ShowDialog();
        }

        /// <summary>
        /// Loads the Masterlist/other assemblies from resources in the case they are not present in folder
        /// </summary>
        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        { // THIS WILL NOT CAUSE ERRORS
            // UNDER NO CIRCUMSTANCES WILL THIS CRASH!
            // NONE!
            var desiredASsembly = new AssemblyName(args.Name);
            if (desiredASsembly.Name == "MasterlistDLL")
            {
                return Assembly.Load(Properties.Resources.MasterlistDLL);
            }
            return null;
        }
        static void Commands()
        {
            string command;
            while (true)
            {
                command = Console.ReadLine();
                if ((command.ToLower()).StartsWith("/start q".ToLower()))
                {
                    string[] s = command.ToLower().Split(' ');
                    if (s.Length != 4)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Can not complete command : Please enter - /start q [MatchesPerGame] [Bot Level]");
                    }
                    else
                    {
                        try
                        {
                            int drawsallowed = Convert.ToInt16(s[2]);
                            if (drawsallowed == 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Can not complete command : Matches Per Game cannot be 0");
                            }
                            else
                            {
                                int BotLevel = Convert.ToInt16(s[3]);
                                if(BotLevel < 0 || BotLevel > 3)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Can not complete command : Bot Level can only be between 0-3");
                                }
                                else
                                {
                                    if (AllPlayers.Count >= 1)
                                    {
                                        if(!(BotLevel==0 && AllPlayers.Count == 1))
                                        {
                                            endconnect();
                                            Console.ForegroundColor = ConsoleColor.White;
                                            Console.WriteLine($"Started quick tournament: {drawsallowed.ToString()} match[es] allowed per game and Bot Level- {BotLevel.ToString()}");
                                            Quick = true;
                                            CurrentTournament = new Tournament();
                                            CurrentTournament.StartTournament(drawsallowed, BotLevel);
                                            break;
                                        }
                                        else
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("Can not complete command : not enough players");
                                        }
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Can not complete command : not enough players");
                                    }
                                }
                            }
                        }
                        catch
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Can not complete command : Please enter - /start q [MatchesPerGame] [Bot Level]");
                        }
                    }

                } else if ((command.ToLower()).StartsWith("/start n".ToLower()))
                {
                    try
                    {
                        string[] s = command.ToLower().Split(' ');
                        if (s.Length != 3)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Can not complete command : Please enter - /start n [Bot Level]");
                        }
                        else
                        {
                            int BotLevel = Convert.ToInt16(s[2]);
                            if (BotLevel < 0 || BotLevel > 3)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Can not complete command : Bot Level can only be between 0-3");
                            }
                            else
                            {
                                if (AllPlayers.Count >= 1 )
                                {
                                    if(!(BotLevel ==0 && AllPlayers.Count == 1))
                                    {
                                        endconnect();
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Console.WriteLine("Started normal tournament");
                                        Quick = false;
                                        CurrentTournament = new Tournament();
                                        CurrentTournament.StartTournament(0, BotLevel);
                                        break;
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Can not complete command : not enough players");
                                    }

                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Can not complete command : not enough players");
                                }
                            }
                        }
                    }
                    catch
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Can not complete command : Please enter - /start n [Bot Level]");
                    }
                }else if (command.ToLower().StartsWith("/kick"))
                {
                    string[] s = command.ToLower().Split(' ');
                    if (s.Length != 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Can not complete command : /kick [name] (spaces = underscore)");
                    }
                    else
                    {
                        Found = false;
                        foreach(Player p in AllPlayers)
                        {
                            if (p.Name.ToLower() == s[1].ToLower())
                            {
                                Found = true;
                                p.Active = false;
                                p.Connected = false;
                                p.Client.Close();
                                AllPlayers.Remove(p);
                                AllPlayersnames.Remove(p.Name);
                                f1.RemovePlayer(p.Name,"","");
                                f1.Add(0, $"{p.Name} has been kicked");
                                break;
                            }
                        }
                        if(Found == false)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Player not found");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("Kicked " + s[1]);
                        }
                    }
                }
                else if (command.ToLower() == "/end".ToLower())
                {
                    Environment.Exit(0);
                } else if (command.ToLower() == "/start".ToLower())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Command - start [q/n] [number(if q)] [Bot Level]");
                } else if (command.ToLower().StartsWith("/admin add"))
                {
                    string[] s = command.ToLower().Split(' ');
                    if (s.Length != 3)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid Command - /admin add [name]");
                    }
                    else
                    {
                        if (!CheckContains(s[2]))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("User does not exist");
                        }
                        else
                        {
                            if (admins.Contains(s[2]))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("User is already an admin");
                            }
                            else
                            {
                                admins.Add(Names(s[2]));
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.WriteLine($"{Names(s[2])} is now an admin");
                                log($"{s[2]} - make admin");
                            }
                        }
                    }
                }else if (command.ToLower().StartsWith("/admin remove"))
                {
                    string[] s = command.ToLower().Split(' ');
                    if (s.Length != 3)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid Command - /admin remove [name]");
                    }
                    else
                    {
                        s[2] = Names(s[2]);
                        if (admins.Contains(s[2]))
                        {
                            admins.Remove(s[2]);
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine($"{s[2]} is no longer an admin");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"{s[2]} is not admin");
                        }
                    }
                }
                else if (command.ToLower().StartsWith("/users add"))
                {
                    string[] s = command.ToLower().Split(' ');
                    if (s.Length != 4)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid Command - /users add [username] [name]");
                    }
                    else
                    {
                        if (DictNames.ContainsKey(s[2]))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("User already exists - name = "+DictNames[s[2]]);
                        }
                        else
                        {
                            DictNames.Add(s[2].ToLower(), Names(s[3]));
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine($"name-{Names(s[3])} username-{s[2].ToLower()} : added to user dictionary");
                            log($"{s[2].ToLower()}(username) {Names(s[3])}(name) - Add to dictionary");
                        }
                    }
                }else if (command.ToLower() == "/help")
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    for (int i = 0; i< Lcommands.Count(); i++)
                    {
                        Console.WriteLine(Lcommands[i]);
                    }
                }else if (command.ToLower() == "/restart")
                {
                    ServerThreads[0].Abort();
                    ServerThreads[1].Abort();
                    VfourServer.Stop();
                    VsixServer.Stop();
                    Application.Restart();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Command");
                }
            }
            while (true)
            {
                command = Console.ReadLine();
                if (command.ToLower() == "/end".ToLower())
                {
                    Environment.Exit(0);
                }
                else if (command.ToLower() == "/restart")
                {
                    ServerThreads[0].Abort();
                    ServerThreads[1].Abort();
                    VfourServer.Stop();
                    VsixServer.Stop();
                    Application.Restart();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("The tournament has started so only /end or /restart command can be used");
                }
            }
        }
        static void endconnect()
        {
            startgame = true;
            //VfourServer.Stop();
            //VsixServer.Stop();
        }
        static void start()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("IPV4 : " + V4iplocal);
            Console.WriteLine("IPV6 : " + V6iplocal);
            VfourServer = new TcpListener(IPAddress.Any,port);
            VsixServer = new TcpListener(IPAddress.IPv6Any, port);
            VfourServer.Start();
            VsixServer.Start();
            Thread ip4 = new Thread(SetupIp4);
            ip4.Start();
            Thread ip6 = new Thread(SetupIp6);
            ip6.Start();
            ServerThreads.Add(ip4);
            ServerThreads.Add(ip6);
            Console.ForegroundColor = ConsoleColor.Green;
            MASTERLIST = new MasterlistDLL.MasterlistServer(runningPortForward, MasterlistDLL.MasterList_GameType.Abdul_TicTacToe);
            if(string.IsNullOrWhiteSpace(MASTERLIST.MASTERLIST_IP))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("MASTERLIST: Masterlist offline.");
                masterlist = false;
            } else
            {
                MASTERLIST.LogMessage += (object e, string args) =>
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("MASTERLIST: " + args);
                };
                MASTERLIST.RecieveMessage += (object e, MasterlistDLL.ReceiveMessageEventArgs args) =>
                {  //typically would be 'GetDetails' to ensur e the server is still online.
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("ML, from '" + args.LastOperation + "' -> " + args.Message);
                };

                var regkey = Registry.CurrentUser.CreateSubKey("Abdul-TicTacToe");
                regkey = regkey.CreateSubKey("Server");
                string item = (string)regkey.GetValue("GUID", Guid.NewGuid().ToString());
                regkey.SetValue("GUID", item);
                Guid parsed = Guid.Parse(item);
                MASTERLIST.HostServer(Server_Name, parsed, password_protected: false);
                masterlist = true;
            }
        }
        static void GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    V4iplocal = ip;//ipv4
                }
            }
            IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());
            List<IPAddress> mylocalIPs = new List<IPAddress>(); ;
            foreach (IPAddress addr in localIPs)
            {
                if (addr.AddressFamily == AddressFamily.InterNetworkV6)
                {
                    mylocalIPs.Add(addr);
                }
            }
            if(mylocalIPs.Count > 2)
            {
                V6iplocal = mylocalIPs[3];
            }
            else
            {
                V6iplocal = mylocalIPs.FirstOrDefault();
            }
        }
        static void SetupIp4()
        {
            try
            {
                int masterlistcount = 0;
                while (true)
                {
                    TcpClient newCon = VfourServer.AcceptTcpClient();
                    if (((IPEndPoint)newCon.Client.RemoteEndPoint).Address.ToString() == "195.147.75.156" && masterlistcount == 0 && masterlist == true)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Masterlist connected");
                        masterlistcount += 1;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Accepted V4 connection from " + ((IPEndPoint)newCon.Client.RemoteEndPoint).Address.ToString());
                    }
                    Thread ply = new Thread(() => NewUser(newCon));
                    ply.Start();
                }
            }
            catch (SocketException) {}

        }
        static void SetupIp6()
        {
            try
            {
                while (true)
                {
                    TcpClient newCon = VsixServer.AcceptTcpClient();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Accepted V6 connection from " + ((IPEndPoint)newCon.Client.RemoteEndPoint).Address.ToString());
                    Thread ply = new Thread(() => NewUser(newCon));
                    ply.Start();
                }
            }
            catch (SocketException) {}
        }
        static void CheckConnected(object source , EventArgs e ,Player p , System.Timers.Timer T)
        {
            if(p.Active && p.Client.Connected)
            {
                p.Send("Test");
            }
            else
            {
                T.Stop();
            }
        }
        public static System.Timers.Timer Timer1;
        public static void InitTimer(Player p)
        {
            Timer1 = new System.Timers.Timer(5000);
            Timer1.Elapsed += (sender, e) => CheckConnected(sender, e, p, Timer1);
            Timer1.AutoReset = true;
            Timer1.Start();
        }
        static void TournamentStarted(Player user)
        {
            user.Send("Started");
            string s = receivestartingdata(user.Client);
            if(s == "True")
            {
                NewSpectator(user);
            }
        }
        static string NormalName;
        static void NewUser(TcpClient user)
        {
            Player temp = new Player();
            temp.Client = user;
            temp.Active = true;
            temp.Connected = true;
            NormalName = getUsername(temp);
            if(NormalName == "")
            {
                return;//should never happen but jic
            }
            NormalName = Names(NormalName);
            if (AllPlayersnames.Contains(NormalName)|| AllSpectatorsNames.Contains(NormalName))
            {
                if (admins.Contains(NormalName))
                {
                    string copy = NormalName;
                    int ii = 2;
                    while (AllPlayersnames.Contains(copy))
                    {
                        copy = NormalName + ii;
                        ii += 1;
                    }
                    temp.Name = copy;
                }
                else
                {
                    temp.Send("ONE ONLY");
                    temp.Client.Close();
                    return;
                }
            }
            else
            {
                temp.Name = NormalName;
            }
            string spectatorORplayer = receivestartingdata(temp.Client);
            InitTimer(temp);
            if (spectatorORplayer == "S")
            {
                temp.Name = NormalName;
                NewSpectator(temp);
            }
            else if (startgame == false)
            {
                if (spectatorORplayer == "P")
                {
                    NewPlayer(temp);
                }
            }
            else
            {
                TournamentStarted(temp);
            }
        }
        static void NewPlayer(Player user)
        {
            user.f1 = f1;
            AllPlayersnames.Add(user.Name);
            if (string.IsNullOrWhiteSpace(user.Name))
            { // masterlist will test that the port is empty with an empty connection
                return;
            }
            f1.Add(1, user.Name);
            f1.allplayersnames.Add(user.Name);
            user.user = user;
            user.Send("YOU:" + user.Name);
            AllPlayers.Add(user);
            f1.Add(0, $"{user.Name} has joined");
            f1.Invoke((MethodInvoker)delegate
            {
                f1.numberplay.Text = Program.AllPlayers.Count.ToString();
            });
            try
            {
                if (Program.MASTERLIST?.Enabled ?? false)// defaults to false if null                           
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Program.MASTERLIST.Update(Program.Server_Name, Program.AllPlayers.Count, false);
                }
            }
            catch (Exception)
            {
                //Console.WriteLine("ML error: " + ex.ToString());
            }
        }
        public static void NewSpectator(Player user)
        {
            user.Send("You:" + user.Name);
            Spectator spectator = new Spectator();
            spectator.NewSpectator(user.Client,user.Name,CurrentTournament,f1);
            AllSpectators.Add(spectator);
            AllSpectatorsNames.Add(user.Name);
            f1.SpectatorChanged();
        }
        static string getUsername(Player NewPlayer)
        {
            string CheckName = receivestartingdata(NewPlayer.Client);
            CheckName = CheckName.ToLower();
            if (DictNames.ContainsKey(CheckName))
            {
                CheckName = DictNames[CheckName];
            }
            else
            {
                CheckName = CheckName.Replace(' ', '_');
                log(CheckName+" Add to user dictioniary");
            }
            return CheckName;
        }
        public static string receivestartingdata(TcpClient clt)
        {
            string data = "";
            NetworkStream RecieveDataStream = clt.GetStream();
            byte[] bytes = new byte[256];
            int i;
            if ((i = RecieveDataStream.Read(bytes, 0, bytes.Length)) != 0)
            {
                data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
            }
            return data;
        }
        public static void log(string message)
        {
            if (!File.Exists("Infomation.txt"))
            {
                StreamWriter swNew = File.CreateText("Infomation.txt");
                swNew.WriteLine(message);
                swNew.Close();
            }
            else
            {
                StreamWriter swAppend = File.AppendText("Infomation");
                swAppend.WriteLine(message);
                swAppend.Close();
            }
        }
        public static bool CheckContains(string str)
        {
            foreach(string value in DictNames.Values)
            {
                if (value.ToLower() == str.ToLower())
                {
                    return true;
                }
            }
            return false;
        }
        public static string Names(string N)
        {
            try
            {
                char first = char.ToUpper(N[0]);
                List<Char> rest = new List<char>() { first };
                for (int i = 1; i < N.Length; i++)
                {
                    rest.Add(char.ToLower(N[i]));
                }
                string rtn = "";
                foreach (char E in rest)
                {
                    rtn = rtn + E.ToString();
                }
                return rtn;
            }catch (Exception) { return ""; }

        }

        public static int RandomNumber(int min, int max)
        {
            max++;
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[4];
            rng.GetBytes(buffer);
            int result = BitConverter.ToInt32(buffer, 0);
            return new Random(result).Next(min, max);
        }
        public static List<string> admins = new List<string>() {"Abdul"};
        public static Dictionary<string, string> DictNames = new Dictionary<string, string>()
        {
            {"Abdul","Abdul"},//home
            { "tomal","Tom"},
            {"alexchester","Alex"},
            {"anush" , "Anushan"},
            {"shaabd14", "Abdul"},//school
            {"cheale14","Alex" },
            {"manami14","Amir"},
            {"mukanu14","Anushan"},
            {"pavben14","Ben"},
            {"bakmoh14","Borhan"},
            {"sedcha14","Charlie"},
            {"loacur14","Curtis"},
            {"baldie14","Danesh"},
            {"vigfin14","Finley"},
            {"lefgeo14","George"},
            {"hydhar14","Harry_H"},
            {"wilhar14","Harry_W"},
            {"fleiia14","Iian"},
            {"miljak14","Jake"},
            {"seyjar14","Jared"},
            {"blojon14","Jon"},
            {"edgjos14","Josh"},
            {"odjlil14","Liliana"},
            {"broree14","Reece"},
            {"grirya14","Ryan_G"},
            {"benrya14","Ryan_B"},
            {"mohsoh14","Sohail"},
            {"burtho14","Tom"},
            {"thodanst","Mr_Thomson"},
            {"teacher","Mr_Thomson"}
        };
    }
}


