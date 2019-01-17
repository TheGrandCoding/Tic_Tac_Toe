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
using System.IO;
using System.Net.Sockets;

namespace TicTacToeClient
{
    public class MenuClass
    {
        public static Form1 f1;
        public static TcpClient client = new TcpClient();
        public string myusername;
        public static string opponentusername;
        public void gotusername(string username)
        {
            myusername = username;
            f1.Invoke((MethodInvoker)delegate
            {
                f1.Text = username.ToUpper();
            });
        }
        public void gotopponent(string ousername)
        {
            opponentusername = ousername;
        }
        public void StartUp(Form1 form1)
        {
            f1 = form1;
            connectBTN_Click(null, EventArgs.Empty);
            Thread rd = new Thread(f1.recievedata); // permenant
            rd.Start();
            SpectatorOrPlayer();
        }
        private void SpectatorOrPlayer()
        {
        #if DEBUG
            var input = MessageBox.Show("Do you want to Play(Yes) or Spectate(No)?", "SpectateorPlay?", MessageBoxButtons.YesNo);
            if (input == DialogResult.No)
            {
                f1.senddata("S");
            }
            else
            {
                f1.senddata("P");
            }
        #else
            f1.senddata("P");
        #endif
        }
        public void StartGame(string player , bool turn)
        {
            f1.Invoke((MethodInvoker)delegate
            {
                if (f1.Sf != null)
                {
                    f1.Sf.SpectatorLive = false;
                }
                f1.Text = $"Round {(Form1.roundcounter).ToString()}: {myusername.ToUpper()} vs {opponentusername.ToLower()}";
                f1.startgame(opponentusername, player, turn , myusername);
                f1.Game.Location = new Point(0, 0);
                f1.SpectatorPanel.SendToBack();
                f1.Game.BringToFront();
            });
        }
        private void connectBTN_Click(object sender, EventArgs e)
        {
            try
            {
                if (client.Connected)
                {
                    return;
                }
                client.NoDelay = true;
                string ipstringsettings = "127.0.0.1";
                bool valid = IPAddress.TryParse(ipstringsettings, out IPAddress address2);
                client = new TcpClient(address2.AddressFamily);
                if (client.Client.AddressFamily == AddressFamily.InterNetworkV6)
                {
                    client.Client.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.IPv6Only, false);
                }
                client.Connect(address2, 1234);
                Program.Log("Connected to " + ipstringsettings);
                Program.SocketConnection.Client = client;
                f1.client = client;
                f1.senddata(Environment.UserName);
            }
            catch (Exception er)
            {
                Program.Log("[Error] Connection Failed(127.0.0.1): " + er.ToString());
                try
                {
                    if (client.Connected)
                    {
                        return;
                    }
                    client.NoDelay = false;
                    string ipstringsettings = Properties.Settings.Default.thepublicip;
                    bool valid = IPAddress.TryParse(ipstringsettings, out IPAddress address2);
                    client = new TcpClient(address2.AddressFamily);
                    if (client.Client.AddressFamily == AddressFamily.InterNetworkV6)
                    {
                        client.Client.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.IPv6Only, false);
                    }
                    client.Connect(address2, 1234);
                    Program.Log("Connected to " + ipstringsettings);
                    Program.SocketConnection.Client = client;
                    f1.client = client;
                    f1.senddata(Environment.UserName);
                }
                catch (Exception ex)
                {
                    if (client.Connected == false)
                    {
                        MessageBox.Show("Connection Failed");
                        Program.Log("[Error] Connection Failed("+Properties.Settings.Default.thepublicip+"): " + ex);
                        Environment.Exit(0);
                    }
                }
            }
        }
    }
}
