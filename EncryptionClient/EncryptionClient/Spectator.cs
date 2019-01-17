using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Net.Sockets;

namespace TicTacToeClient
{
    public class Spectator
    {
        public bool SpectatorLive;
        TcpClient client = null;
        List<string> Games = new List<string>();
        List<Button> buttons = null;
        Form1 f1 = null;
        string username = null;
        bool waiting;
        public void SetSpectator(TcpClient tcpClient,Form1 form1 ,string name , bool _waiting)
        {
            waiting = _waiting;
            f1 = form1;
            SpectatorLive = true;
            f1.Invoke((MethodInvoker)delegate
            {
                f1.SA1.BackgroundImage = null;
                f1.SA2.BackgroundImage = null;
                f1.SA3.BackgroundImage = null;
                f1.SB1.BackgroundImage = null;
                f1.SB2.BackgroundImage = null;
                f1.SB3.BackgroundImage = null;
                f1.SC1.BackgroundImage = null;
                f1.SC2.BackgroundImage = null;
                f1.SC3.BackgroundImage = null;
                f1.TxtInfo.Visible = false;
                f1.TxtInfo.Text = "";
                f1.TxtCrossName.Text = "CrossName";
                f1.TxtNoughtName.Text = "NoughtName";
                f1.PBTurn.Image = null;
                f1.CBGames.Items.Clear();
                f1.SLBmessages.Items.Clear();
            });
            f1.CBGames.SelectedIndexChanged += CBGames_SelectedIndexChanged1;
            username = name.ToUpper();
            client = tcpClient;
            Spectator_Load();
        }

        private void Spectator_Load()
        {
            buttons = new List<Button>() { f1.SA1, f1.SA2, f1.SA3, f1.SB1, f1.SB2, f1.SB3, f1.SC1, f1.SC2, f1.SC3 };
            if(waiting == true)
            {
                f1.Invoke((MethodInvoker)(() => f1.Text = "[Waiting For Next Game]" + username));
            }
            else
            {
                f1.Invoke((MethodInvoker)(() => f1.Text = "[Spectator]" + username));
            }
            f1.Invoke((MethodInvoker)delegate
            {
                f1.WaitingSpectatorPanel.Location = new Point(0, 0);
                f1.WaitingSpectatorPanel.BringToFront();
                f1.Size = new Size(801, 376);
            });
        }
        public void senddata(string message)
        {
            try
            {
                NetworkStream stream = client.GetStream();
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                stream.Write(data, 0, data.Length);
                Program.Log("[Sent(S)]: " + message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("[SENT(S)]Server has been Closed: "+ex.ToString());
                Environment.Exit(0);
            }
        }
        public void RecievedGame(string data)
        {
            f1.Invoke((MethodInvoker)(() => f1.SpectatorPanel.Location = new Point(0,0)));
            f1.Invoke((MethodInvoker)(() => f1.SpectatorPanel.BringToFront()));
            f1.Invoke((MethodInvoker)(() => f1.Size = new Size(801,376)));
            var words = data.Split('~');
            Games.Add($"{words[1]}~{words[2]}~{words[3]}~{words[4]}");
            f1.Invoke((MethodInvoker)(() => f1.CBGames.Items.Add($"Round {words[3]} Game {words[4]} - {words[1]} vs {words[2]}")));
        }
        public void RecievedMessage(string data)
        {
            var words = data.Split('#');
            if (words.Length == 2)
            {
                f1.Invoke((MethodInvoker)(() => f1.SLBmessages.Items.Add(words[1])));
            }
        }
        public void RecievedInfo(string data)
        {
            var parts = data.Split('/');
            char[] Board = parts[1].ToCharArray();
            UpdateBoard(Board);
            var Info = parts[2].Split(':');
            Update(Info[0], Info[1], Info[2], Info[3]);
        }
        private void Update(string Turn, string CrossName, string NoughtName, string Info)
        {
            f1.Invoke((MethodInvoker)delegate
            {
                if (Turn == "C")
                {
                    f1.PBTurn.Image = Properties.Resources.cross;
                }
                else
                {
                    f1.PBTurn.Image = Properties.Resources.Nought;
                }
                f1.TxtCrossName.Text = CrossName;
                f1.TxtNoughtName.Text = NoughtName;
                if (Info != "F")
                {
                    f1.PBTurn.Image = null;
                    f1.TxtInfo.Visible = true;
                }
                if (Info == "N")
                {
                    f1.TxtInfo.Text = NoughtName + " won";
                }
                else if (Info == "C")
                {
                    f1.TxtInfo.Text = CrossName + " won";
                }
                else if (Info == "D")
                {
                    f1.TxtInfo.Text = "Tiebreaker in progress";
                }
                else if (Info == "NT")
                {
                    f1.TxtInfo.Text = NoughtName + " won(TieBreaker)";
                }
                else if (Info == "CT")
                {
                    f1.TxtInfo.Text = CrossName + " won(TieBreaker)";
                }
            });
        }
        private void UpdateBoard(char[] Board)
        {
            for (int i = 0; i < Board.Length; i++)
            {
                f1.Invoke((MethodInvoker)delegate
                {
                    if (Board[i] == 'O')
                    {
                        buttons[i].BackgroundImage = Properties.Resources.Nought;
                    }
                    else if (Board[i] == 'X')
                    {
                        buttons[i].BackgroundImage = Properties.Resources.cross;
                    }
                    else
                    {
                        buttons[i].BackgroundImage = null;
                    }
                });
            }
        }
        private void CBGames_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if(f1.CBGames.SelectedIndex >-1 && SpectatorLive == true)
            {
                f1.TxtInfo.Visible = false;
                senddata("Selected:" + Games[f1.CBGames.SelectedIndex]);
            }
        }
    }
}
