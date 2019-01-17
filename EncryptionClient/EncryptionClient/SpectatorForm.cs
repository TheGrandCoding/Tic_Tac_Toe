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
    public partial class SpectatorForm : Form
    {
        TcpClient client;
        List<string> Games = new List<string>();
        List<Button> button;
        private Form1 form1;
        public SpectatorForm(TcpClient tcpClient, Form1 _form1)
        {
            form1 = _form1;
            client = tcpClient;
            button = new List<Button>() { A1, A2, A3, B1, B2, B3, C1, C2, C3 };
            InitializeComponent();
        }

        private void SpectatorForm_Load(object sender, EventArgs e)
        {
            //senddata("Spectator");
            Thread RD = new Thread(recievedata);
            RD.Start();
            form1.Invoke((MethodInvoker)(() => form1.Hide()));
        }
        public void senddata(string message)
        {
            try
            {
                NetworkStream stream = client.GetStream();
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                stream.Write(data, 0, data.Length);
                Program.Log("[Sent]: " + message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Server has been closed" + ex.ToString());
                Environment.Exit(0);
            }
        }
        public void recievedata()
        {
            try
            {
                string data;
                NetworkStream stream = client.GetStream();
                Byte[] bytes = new byte[256];
                int i;
                while (true)
                {
                    if ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        String responseData = String.Empty;
                        string _dataBunched = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        string[] messages = _dataBunched.Split('%').Where(x => string.IsNullOrWhiteSpace(x) == false && x != "%").ToArray();
                        foreach (var msg in messages)
                        {
                            data = msg.Substring(0, msg.IndexOf("`"));
                            Program.Log("[Rec(S)]: " + data);
                            if (data.StartsWith("Game:"))
                            {
                                var words = data.Split(' ');
                                Games.Add($"{words[1]} {words[2]} {words[3]} {words[4]}");
                                CBGames.Items.Add($"Round {words[3]} Game {words[4]} - {words[1]} vs {words[2]}");
                            }
                            else
                            {
                                var parts = data.Split('/');
                                char[] Board = parts[0].ToCharArray();
                                UpdateBoard(Board);
                                var Info = parts[1].Split(':');
                                Update(Info[0], Info[1], Info[2]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Server has been closed: " + ex.ToString());
                Environment.Exit(0);
                throw; // You can uncomment this line to show it to the debugger!
            }
        }
        private void Update(string Turn , string CrossName , string NoughtName)
        {
            if(Turn == "C")
            {
                PBTurn.Image = Properties.Resources.cross;
            }
            else
            {
                PBTurn.Image = Properties.Resources.Nought;
            }
            TxtCrossName.Text = CrossName;
            TxtNoughtName.Text = NoughtName;
        }
        private void UpdateBoard(char[] Board)
        {
            foreach(char value in Board)
            {
                if(value == 'O')
                {
                    button[Board.ToList().IndexOf(value)].BackgroundImage = Properties.Resources.Nought;
                }else if(value == 'X')
                {
                    button[Board.ToList().IndexOf(value)].BackgroundImage = Properties.Resources.cross;
                }
                else
                {
                    button[Board.ToList().IndexOf(value)].BackgroundImage = null;
                }
            }
        }
        private void CBGames_SelectedIndexChanged(object sender, EventArgs e)
        {

            senddata("Selected:"+Games[CBGames.SelectedIndex]);
        }
    }
}
