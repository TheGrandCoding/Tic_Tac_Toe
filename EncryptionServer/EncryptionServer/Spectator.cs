using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Net.Sockets;

namespace TicTacToeServer
{
    public class Spectator
    {
        public Form1 f1;
        public Thread RD;
        public TcpClient client;
        TTTGame ChosenGame;
        public Tournament SpectatingTournament;
        int Games=0;
        public string myname;
        public bool NotPlaying;
        public void NewSpectator(TcpClient tcpClient,string name,Tournament CurrentTournament , Form1 _f1)
        {
            f1 = _f1;
            NotPlaying = true;
            myname = name;
            SpectatingTournament = CurrentTournament;
            client = tcpClient;
            Thread HS = new Thread(HandleSpectator);
            RD = HS;
            HS.Start();
            Send("Spectator");
            AddGames();
            AddPastMessages();
        }
        public void AddGames()
        {
            try
            {
                foreach (string key in SpectatingTournament.SpectatorsGames.Keys)
                {
                    if ((SpectatingTournament.SpectatorsGames.Keys.ToList().IndexOf(key)) > Games - 1)
                    {
                      //Console.WriteLine("["+myname+"] "+key);
                        Send("Game:~" + key);
                        Games++;
                    }
                }
            }
            catch (Exception) { }
        }
        private void AddPastMessages()
        {
            try
            {
                foreach (string message in SpectatingTournament.SpectatorMessages)
                {
                    Send(message);
                }
            }
            catch (Exception) { }
        }
        public void HandleSpectator()
        {
            string data;
            NetworkStream RecieveDataStream = client.GetStream();
            byte[] bytes = new byte[256];
            int i;
            try
            {
                while (NotPlaying)
                {
                    if ((i = RecieveDataStream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                       //Console.WriteLine("["+NotPlaying.ToString()+"]" + data);
                        if (data.StartsWith("Selected:"))
                        {
                            var parts = data.Split(':');
                            ChangedGame(parts[1]);
                        }
                        else if (data == "BreakSpectator")
                        {
                            break;
                        }
                        else if (data.ToUpper() == "LEFT")
                        {
                            Left();
                        }
                    }
                }
            }
            catch (Exception)
            {
                Left();
            }
        }
        public void Send(string message)
        {
            try
            {
                //Console.WriteLine("Sent "+message);
                //f1.Add(GameIn.gamenumber+1,$"{Name} sent {message}");
                message = $"%{message}`";
                NetworkStream SendDataStream = client.GetStream();
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(message);
                SendDataStream.Write(msg, 0, msg.Length);
            }
            catch (Exception)
            {
                Left();
            }
        }
        private void Left()
        {
            NotPlaying = false;
            Program.AllSpectators.Remove(this);
            if (Program.AllSpectatorsNames.Contains(myname))
            {
                Program.AllSpectatorsNames.Remove(this.myname);
            }
            f1.SpectatorChanged();
            if (ChosenGame != null)
            {
                if (ChosenGame.Spectators.Contains(this))
                {
                    ChosenGame.Spectators.Remove(this);
                }
            }
        }
        private void ChangedGame(string strGameChosen)
        {
            //Console.WriteLine("GameChosen: "+strGameChosen);
            try
            {
                if (ChosenGame.Spectators.Contains(this))
                {
                    ChosenGame.Spectators.Remove(this);
                }
            }
            catch (Exception) {  }
            if (SpectatingTournament.SpectatorsGames.ContainsKey(strGameChosen))
            {
                ChosenGame = SpectatingTournament.SpectatorsGames[strGameChosen];
            }
            ChosenGame.Spectators.Add(this);
            GameUpdate();
        }
        public void GameUpdate()
        {
            Send(ChosenGame.GetInfo());
        }
    }
}
