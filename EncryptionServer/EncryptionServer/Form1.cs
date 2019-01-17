using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using System.Net;
using System.Net.Sockets;

namespace TicTacToeServer
{
    public partial class Form1 : Form
    {
        ListBox LBplayersin = new ListBox();
        public int roundnumber;
        public List<TTTGame> games = new List<TTTGame>();
        public List<string> allplayersnames = new List<string>();
        public List<string> General = new List<string>();
        Point point;
        Size size;
        public List<ListBox> listBoxes = new List<ListBox>();
        bool starttournament = false;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            point = LBGeneral.Location;
            size = LBGeneral.Size;
            listBoxes.Add(LBGeneral);
            CBMessages.Items.Add("General");
            LBplayersin.Size = size;
            LBplayersin.Location = point;
            this.Controls.Add(LBplayersin);
            LBplayersin.Visible = false;
            listBoxes.Add(LBplayersin);
            LBplayersin.Sorted = true;
            LBplayersin.Click += LBplayersin_Click;
            CBMessages.Items.Add("Players In");
            ListBox LBplayersout = new ListBox();
            LBplayersout.Size = size;
            LBplayersout.Location = point;
            this.Controls.Add(LBplayersout);
            LBplayersout.Visible = false;
            listBoxes.Add(LBplayersout);
            CBMessages.Items.Add("Players Out");
            CBMessages.SelectedIndex = 0;
            roundnumber = 1;
        }


        public void SpectatorChanged()
        {
            this.Invoke((MethodInvoker)(() => SpectatorCount.Text = Program.AllSpectatorsNames.Count.ToString()));
        }
        private void GameNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowLB();
        }
        private void ShowLB()
        {
            for (int i =0; i<listBoxes.Count; i++)
            {
                if (i == CBMessages.SelectedIndex)
                {
                    listBoxes[i].Visible = true;
                }
                else
                {
                    listBoxes[i].Visible = false;
                }
            }
        }
        public void NewLB(string player1, string player2 , int gameroundnumber)
        {
            this.Invoke((MethodInvoker)delegate
            {
                ListBox newgameLB = new ListBox();
                newgameLB.Location = point;
                newgameLB.Size = size;
                this.Controls.Add(newgameLB);
                newgameLB.Visible = false;
                listBoxes.Add(newgameLB);
                CBMessages.Items.Add($"Round {roundnumber} Game {gameroundnumber.ToString()}: {player1} vs {player2}");
            });
        }
        public void Add(int Index, string message)
        {
            this.Invoke((MethodInvoker)delegate
            {
                listBoxes[Index].Items.Add(message);
                listBoxes[Index].TopIndex = listBoxes[Index].Items.Count - 1;
            });
        }
        public void RemovePlayer(string player , string gamenumber , string roundnumber)
        {
            this.Invoke((MethodInvoker)delegate
            {
                if (listBoxes[1].Items.Contains(player))
                {
                    listBoxes[1].Items.Remove(player);
                    if (starttournament == true)
                    {
                        listBoxes[2].Items.Add($"{player} (Round {roundnumber} Game {gamenumber})");
                    }
                    numberplay.Text = Program.AllPlayers.Count.ToString();
                } 
            });
        }
        public void FinishedGame(int index)
        {
            this.Invoke((MethodInvoker)delegate
            {
                CBMessages.Items[index] = "[F]" + CBMessages.Items[index];
            });
        }
        public void starttournamet()
        {
            this.Invoke((MethodInvoker)delegate
            {
                starttournament = true;
                Border.Visible = true;
                TotalPlayers.Visible = true;
                TotalPlayers.Text = numberplay.Text;
            });
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }
        private void CMButtonClick(Object sender, System.EventArgs e)
        {
            
        }
        private void LBplayersin_Click(object sender, EventArgs e)
        {
        }
    }
}
