using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;

using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace TicTacToeClient
{
    public partial class Form1 : Form
    {
        public bool SpectatorWaiting = false;
        bool keeprecieving = true;
        public Spectator Sf = null;
        bool gamedone;
        public static int roundcounter = 0;
        Thread wt;
        MenuClass m = new MenuClass();
        public TcpClient client;// set from Form2
        Byte[] bytes = new byte[256];
        string data;
        public string player; // nought or cross // set from Form2
        public string opponentusername;// set from Form2
        int nomessages = 0;
        List<Button> buttons;
        Button[,] wins = null;
        public bool myturn; // set from Form2
        Image cross = Properties.Resources.cross;
        Image nought = Properties.Resources.Nought;
        public static bool win;
        private string username;
        public Thread latest;
        private static readonly Regex validIpV4AddressRegex = new Regex(@"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5]).){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$", RegexOptions.IgnoreCase);
        public Form1()
        {
            InitializeComponent();
            buttons = new List<Button>() { A1, A2, A3, B1, B2, B3, C1, C2, C3 };
            wins = new Button[,] { { A1, A2, A3 }, { B1, B2, B3 }, { C1, C2, C3 }, { A1, B1, C1 }, { A2, B2, C2 }, { A3, B3, C3 }, { A1, B2, C3 }, { A3, B2, C1 } };
        }

        public void AddMessage(string message)
        {
           
            if(this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)(() => AddMessage(message)));
            } else
            {
                messagesLB.Items.Add(message);
                messagesLB.TopIndex = messagesLB.Items.Count - 1;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            roundcounter = 0;
            Waiting.Visible = true;
            Game.Visible = false;
            Waiting.Location = new Point(0, 0);
            latest = null;
            this.Size = new Size(555, 325);
            m.StartUp(this);
        }
        public void startgame(string opponent , string nc , bool turn , string myusername)
        {
            gamedone = false;
            username = myusername;
            opponentusername = opponent;
            player = nc;
            myturn = turn;
            Game.Visible = true;
            Waiting.Visible = false;
            this.Size = new Size(820, 360);
            AddMessage("Oponnent Found");
            checkbutton();
            wt = new Thread(() => whoturn(false)); // while loop, permenant for game
            wt.Start();
            senddata("BreakSpectator"); // used to break out of spectator mode
        }
        private void cleargame()
        {
            for (int ii = 0; ii < buttons.Count(); ii++)
            {
                buttons[ii].BackgroundImage = null;
            }
        }
        private void whoturn(bool draw)
        {
            if (draw == false)
            {
                AddMessage("You are playing against " + opponentusername);
                if (player == "cross")
                {
                    AddMessage("You are crosses");
                    PlayerPB.BackgroundImage = cross;
                }
                else
                {
                    AddMessage("You are noughts");
                    PlayerPB.BackgroundImage = nought;
                }
            }
            if (myturn == true)
            {
                  AddMessage("It is your turn");
            }
            else
            {
                  AddMessage("It is your opponents turn");
            }
            this.Size = new Size(820, 360);
            while (win ==false)
            {
                if (myturn == true && win == false)
                {
                    if (player == "cross")
                    {
                        TurnPB.BackgroundImage = cross;
                    }
                    else
                    {
                        TurnPB.BackgroundImage = nought;
                    }
                }
                else if (myturn == false && win == false)
                {
                    if (player == "cross")
                    {
                        TurnPB.BackgroundImage = nought;
                    }
                    else
                    {
                        TurnPB.BackgroundImage = cross;
                    }
                }
                else
                {
                    TurnPB.BackgroundImage = null;
                }
            }
            TurnPB.BackgroundImage = null;
        }
        public void senddata(string message)
        {
            try
            {
                NetworkStream stream = client.GetStream();
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                stream.Write(data, 0, data.Length);
                //AddMessage($"[SENT] {message}");
                Program.Log("[Sent]: " + message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("[SENT]Server has been closed" + ex.ToString());
                Environment.Exit(0);
            }
        }
        public void recievedata()
        {
            try
            {
                string tempdata;
                NetworkStream stream = client.GetStream();
                Byte[] bytes = new byte[256];
                int i;
                while (keeprecieving)
                {
                    try
                    {
                        if ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            nomessages += 1;
                            String responseData = String.Empty;
                            string _dataBunched = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                            string[] messages = _dataBunched.Split('%').Where(x => string.IsNullOrWhiteSpace(x) == false && x != "%").ToArray();
                            foreach (var msg in messages)
                            {
                                try
                                {
                                    data = msg.Substring(0, msg.IndexOf("`"));

                                }
                                catch (Exception) { }
                                Program.Log("[Rec]: " + data);
                                tempdata = data.ToUpper();
                                //AddMessage($"from {opponentusername}: {data}");
                                if (tempdata == "You win".ToUpper())
                                {
                                    if (latest == null)
                                    {
                                        Thread smb = new Thread(() => winningmessagebox("You won so you move on to the next round"));
                                        latest = smb;
                                        smb.Start();
                                    }
                                }
                                else if (tempdata == "Opponent left".ToUpper())
                                {
                                    if (latest == null)
                                    {
                                        Thread smb = new Thread(() => winningmessagebox("Your opponent left \r\n so you win the game \r\n and move on to the next round"));
                                        latest = smb;
                                        smb.Start();
                                    }

                                }
                                else if (tempdata == "You lose".ToUpper())
                                {
                                    win = true;
                                    MessageBox.Show("You lost so have been kicked out of the tornament \r\n You can still spectate the rest of the games");
                                    Thread lts = new Thread(LostToSpectator);
                                    lts.Start();
                                }
                                else if (tempdata == "draw".ToUpper())
                                {
                                    AddMessage("Draw");
                                    AddMessage("Play again");
                                    cleargame();
                                }
                                else if (tempdata.StartsWith("You win draw:".ToUpper()))
                                {
                                    var parts = tempdata.Split(':');
                                    if(parts[1] == "RND")
                                    {
                                        if (latest == null)
                                        {
                                            Thread smb = new Thread(() => winningmessagebox($"Your number({parts[2]}) was the same distance away from my number({parts[4]}) as your opponents number({parts[3]}) but you were randomly chosen to get a winning draw \r\n so move on to the next round"));
                                            latest = smb;
                                            smb.Start();
                                        }
                                    }
                                    else
                                    {
                                        if (latest == null)
                                        {
                                            Thread smb = new Thread(() => winningmessagebox($"Your number({parts[2]}) was closer then your opponents number({parts[3]}) to my number({parts[4]}) so you get a winning draw \r\n so move on to the next round"));
                                            latest = smb;
                                            smb.Start();
                                        }
                                    }
                                }
                                else if (tempdata.StartsWith("You lose draw:".ToUpper()))
                                {
                                    var parts = tempdata.Split(':');
                                    if (parts[1] == "RND")
                                    {
                                        win = true;
                                        MessageBox.Show($"Your number({parts[2]}) was the same distance away from my number({parts[4]}) as your opponents number({parts[3]}) but you were randomly chosen to get a losing draw and are kicked out of the tournament \r\n You can still spectate the rest of the games");
                                        Thread lts = new Thread(LostToSpectator);
                                        lts.Start();
                                    }
                                    else
                                    {
                                        win = true;
                                        MessageBox.Show($"Your number({parts[2]}) was further away then your opponents number({parts[3]}) to my number({parts[4]}) so you get a losing draw and are kicked out of the tournament \r\n You can still spectate the rest of the games");
                                        Thread lts = new Thread(LostToSpectator);
                                        lts.Start();
                                    }
                                }
                                else if (tempdata == "True".ToUpper())
                                {
                                    myturn = true;
                                    Thread wt = new Thread(() => whoturn(true));
                                    wt.Start();
                                }
                                else if (tempdata == "False".ToUpper())
                                {
                                    myturn = false;
                                    Thread wt = new Thread(() => whoturn(true));
                                    wt.Start();
                                }
                                else if (tempdata == "Last".ToUpper())
                                {
                                    AddMessage("If the game is drawn: closest guess to my random number wins");
                                }
                                else if (tempdata == "Times Up".ToUpper())
                                {
                                    AddMessage("You took too long so we went for you");
                                    timeup(player);
                                }
                                else if (tempdata == "Kicked".ToUpper())
                                {
                                    if (latest != null)
                                    {
                                        latest.Abort();
                                    }
                                    MessageBox.Show("You have been kicked for inactivity.");
                                    Environment.Exit(0);
                                }
                                else if (tempdata == "Closed".ToUpper())
                                {
                                    this.Hide();
                                    MessageBox.Show("Server has closed");
                                    Environment.Exit(0);
                                }
                                else if (tempdata == "You won the tournament".ToUpper())
                                {
                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        this.Size = new Size(555, 325);
                                        this.Text = "You won the Tournament";
                                        Waiting.Visible = false;
                                        Game.Visible = false;
                                        PWinner.Visible = true;
                                        PWinner.Location = new Point(0, 0);
                                        win = true;
                                        keeprecieving = false;
                                    });
                                }
                                else if (tempdata == "CrossTrue".ToUpper())
                                {
                                    roundcounter++;
                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        win = false;
                                        LeaveBTN.Enabled = true;
                                    });
                                    m.StartGame("cross",true);
                                    SpectatorWaiting = false;
                                }
                                else if (tempdata == "NoughtTrue".ToUpper())
                                {
                                    roundcounter++;
                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        win = false;
                                        LeaveBTN.Enabled = true;
                                    });
                                    m.StartGame("nought",true);
                                    SpectatorWaiting = false;
                                }
                                else if (tempdata == "Nought".ToUpper())
                                {
                                    roundcounter++;
                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        win = false;
                                        LeaveBTN.Enabled = true;
                                    });
                                    m.StartGame("nought",false);
                                    SpectatorWaiting = false;
                                }
                                else if (tempdata == "Cross".ToUpper())
                                {
                                    roundcounter++;
                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        win = false;
                                        LeaveBTN.Enabled = true;
                                    });
                                    m.StartGame("cross",false);
                                    SpectatorWaiting = false;
                                }
                                else if (tempdata == "Closed".ToUpper())
                                {
                                    this.Hide();
                                    MessageBox.Show("Server has closed");
                                    Environment.Exit(0);
                                }
                                else if (tempdata == "ONE ONLY")
                                {
                                    MessageBox.Show("One client each please");
                                    Environment.Exit(0);
                                }
                                else if (tempdata.StartsWith("YOU:"))
                                {
                                    string[] s = data.ToLower().Split(':');
                                    m.gotusername(s[1]);

                                }else if (tempdata.StartsWith("OPP:"))
                                {
                                    string[] s = data.ToLower().Split(':');
                                    m.gotopponent(s[1]);
                                }else if (tempdata == "WAIT")
                                {
                                    roundcounter++;
                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        this.Text = m.myusername.ToUpper();
                                        WaitingGame.Visible = false;
                                        WaitingTournament.Visible = false;
                                        WaitingRound.Visible = true;
                                        SpectatorWaiting = true;
                                        MessageBox.Show("There is no pair for you to play against \r\n so your are automatically in the next round \r\n you can spectate while you wait");
                                    });
                                }else if (tempdata == "TIE")
                                {
                                    AddMessage("Waiting For TieBreaker Results");
                                    TieBreaker tb = new TieBreaker(this ,new Thread(recievedata));
                                    tb.Text = $"TieBreaker - {username.ToUpper()} vs {opponentusername.ToLower()}";
                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        LeaveBTN.Enabled = false;
                                    });
                                    tb.BringToFront();
                                    tb.ShowDialog();
                                }else if(tempdata == "SPECTATOR")
                                {
                                    Spectator S_F = new Spectator();
                                    Sf = S_F;
                                    S_F.SetSpectator(client,this,m.myusername,SpectatorWaiting);
                                }else if(tempdata == "Started".ToUpper())
                                {
                                    SpectatorWaiting = false;
                                    var spectator = MessageBox.Show("The Tournament has already started \n\r Would you like to spectate it?", "Spectate?", MessageBoxButtons.YesNo);
                                    if (spectator == DialogResult.Yes)
                                    {
                                        senddata("True");
                                    }
                                    else
                                    {
                                        senddata("false");
                                        Environment.Exit(0);
                                    }
                                }else if (data.StartsWith("Game:"))
                                {
                                    Thread RG = new Thread(() => Sf.RecievedGame(data));
                                    RG.Start();
                                    Thread.Sleep(50);
                                }
                                else if (data.StartsWith("Message#"))
                                {
                                    Thread RM = new Thread(() => Sf.RecievedMessage(data));
                                    RM.Start();
                                    Thread.Sleep(50);
                                }else if (data.StartsWith("Info/"))
                                {
                                    Thread RI = new Thread(()=>Sf.RecievedInfo(data));
                                    RI.Start();
                                    Thread.Sleep(50);
                                }
                                else if(tempdata.StartsWith("POS"))
                                {
                                    string[] s = tempdata.Split(':');
                                    otherturn(s[1]);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("[REC]Server has been closed: "+ex.ToString());
                        Environment.Exit(0);
                    }
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("[REC]Server has been closed: "+er.ToString());
                Environment.Exit(0);
                throw; // You can uncomment this line to show it to the debugger!
            }
        }
        private void winningmessagebox(string message)
        {
            gamedone = true;
            win = true;
            clearhanderlers();
            this.Invoke((MethodInvoker)delegate
            {
                LeaveBTN.Enabled = false;
            });
            MessageBox.Show(message); // Timer
            cleargame();
            senddata("ready");
            nextgame();
        }
        private void LostToSpectator()
        {
        #if DEBUG
            SpectatorWaiting = false;
            var spectator = MessageBox.Show("Do you want to go into spectater mode?", "Spectator?", MessageBoxButtons.YesNo);
            if (spectator == DialogResult.Yes)
            {
                senddata("TSpectator");
            }
            else
            {
                senddata("FSpectator");
                Environment.Exit(0);
            }
        #else
            senddata("TSpectator");
        #endif
        }
        private void nextgame()
        {
            this.Invoke((MethodInvoker)delegate
            {
                messagesLB.Items.Clear();
                wt.Abort();
                Waiting.Visible = true;
                Game.Visible = false;
                this.Size = new Size(555, 325);
                this.Text = username;
                WaitingGame.Visible = true;
                WaitingTournament.Visible = false;
                WaitingRound.Visible = false;
                latest = null;
            });
        }
        private void clearhanderlers()
        {
            for (int ii = 0; ii < buttons.Count(); ii++)
            {
                if (player == "cross")
                {
                    buttons[ii].Click -= new EventHandler(turncross);
                }
                else
                {
                    buttons[ii].Click -= new EventHandler(turnnought);
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void checkbutton()
        {
            for (int ii = 0; ii < buttons.Count(); ii++)
            {
                if (player == "cross")
                {
                    buttons[ii].Click += new EventHandler(turncross);
                }
                else
                {
                    buttons[ii].Click += new EventHandler(turnnought);
                }
                
            }
        }
        private void turncross(object sender , EventArgs e)
        {
            if (myturn == true && win == false)
            {
                Button thesender = (Button)sender;
                if (thesender.BackgroundImage == null)
                {
                    AddMessage(thesender.Name +" = cross");
                    senddata("POS:"+thesender.Name);
                    thesender.BackgroundImage = cross;
                    myturn = false;
                    CheckEndGame("Opponents turn");
                }
            }
        }
        private void turnnought(object sender , EventArgs e)
        {
            if (myturn == true &&  win == false)
            {
                Button thesender = (Button)sender;
                if (thesender.BackgroundImage == null)
                {
                    AddMessage(thesender.Name + " = nought");
                    senddata("POS:"+thesender.Name);
                    thesender.BackgroundImage = nought;
                    myturn = false;
                    CheckEndGame("Opponents turn");
                }
            }
        }
        private void otherturn(string buttontochange)
        {
            if (win == false && myturn == false)
            {
                foreach (Button b in panel2.Controls)
                {
                    if (b.Name == buttontochange)
                    {
                        if (player == "nought")
                        {
                             AddMessage(buttontochange + " = cross");
                            b.BackgroundImage = cross;
                        }
                        else
                        {
                             AddMessage(buttontochange + " = nought");
                            b.BackgroundImage = nought;
                        }
                    }
                }
                myturn = true;
                CheckEndGame("Your turn");
            }
        }
        private void timeup(string cn)
        {
            List<Button> Free = new List<Button>();
            for (int i = 0; i < 8; i++)
            {
                if (buttons[i].BackgroundImage == null)
                {
                    Free.Add(buttons[i]);
                }
            }
            if (Free.Count != 0)
            {
                int indexchose = RandomNumber(0, Free.Count-1);
                Button buttonchose = Free[indexchose];
                senddata("POS:"+buttonchose.Name);
                myturn = false;
                if (cn == "nought")
                {
                    AddMessage(buttonchose.Name + " = nought");
                    buttonchose.BackgroundImage = nought;
                }
                else
                {
                    AddMessage(buttonchose.Name + " = cross");
                    buttonchose.BackgroundImage = cross;
                }
                CheckEndGame("Opponents turn");
            }
        }
        private void CheckEndGame(string message)
        {
            int counter = 0;
            for (int i = 0; i < 9; i++)
            {
                if (buttons[i].BackgroundImage != null)
                {
                    counter += 1;
                }
            }
            if (counter != 9 && gamedone == false)
            {
                AddMessage(message);

            }
            else
            {
                AddMessage("End of Game");
            }
        }

        private void Newgame_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 8; i++)
            {
                buttons[i].BackgroundImage = null;
                win = false;
            }
        }
        public static int RandomNumber(int min , int max)
        {
            max++;
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[4];
            rng.GetBytes(buffer);
            int result = BitConverter.ToInt32(buffer, 0);
            return new Random(result).Next(min, max);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var exit = MessageBox.Show("Are you sure you want to quit?", "Quit?", MessageBoxButtons.YesNo);
            if (exit == DialogResult.Yes)
            {
                try
                {
                    senddata("LEFT");
                }
                catch (Exception) { }
                Environment.Exit(0);
            }
        }

        private void LeaveBTN_Click(object sender, EventArgs e)
        {
            try
            {
                senddata("LEFT");
            }
            catch (Exception) { }
            Environment.Exit(0);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                senddata("LEFT");
            }
            catch (Exception) { }
            Environment.Exit(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                senddata("LEFT");
            }
            catch (Exception) { }
            client.Close();
            Environment.Exit(0);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            senddata("true");
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                senddata("LEFT");
            }
            catch (Exception) { }
            Environment.Exit(0);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var exit = MessageBox.Show("Are you sure you want to quit?", "Quit?", MessageBoxButtons.YesNo);
            if (exit == DialogResult.Yes)
            {
                try
                {
                    senddata("LEFT");
                }
                catch (Exception) { }
                Environment.Exit(0);
            }
        }

        private void button3_Click_2(object sender, EventArgs e)
        {
            var exit = MessageBox.Show("Are you sure you want to quit?", "Quit?", MessageBoxButtons.YesNo);
            if (exit == DialogResult.Yes)
            {
                try
                {
                    senddata("LEFT");
                }
                catch (Exception) { }
                Environment.Exit(0);
            }
        }
    }
}