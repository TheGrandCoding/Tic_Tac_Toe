namespace TicTacToeClient
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Waiting = new System.Windows.Forms.Panel();
            this.WaitingRound = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.WaitingGame = new System.Windows.Forms.Label();
            this.WaitingTournament = new System.Windows.Forms.Label();
            this.Game = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.PlayerPB = new System.Windows.Forms.PictureBox();
            this.TurnPB = new System.Windows.Forms.PictureBox();
            this.Newgame = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.C3 = new System.Windows.Forms.Button();
            this.C2 = new System.Windows.Forms.Button();
            this.C1 = new System.Windows.Forms.Button();
            this.B3 = new System.Windows.Forms.Button();
            this.B2 = new System.Windows.Forms.Button();
            this.B1 = new System.Windows.Forms.Button();
            this.A3 = new System.Windows.Forms.Button();
            this.A2 = new System.Windows.Forms.Button();
            this.A1 = new System.Windows.Forms.Button();
            this.LeaveBTN = new System.Windows.Forms.Button();
            this.messagesLB = new System.Windows.Forms.ListBox();
            this.PWinner = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.WaitingSpectatorPanel = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.SpectatorPanel = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.TxtInfo = new System.Windows.Forms.Label();
            this.SLBmessages = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.CBGames = new System.Windows.Forms.ComboBox();
            this.TxtTurn = new System.Windows.Forms.Label();
            this.PBTurn = new System.Windows.Forms.PictureBox();
            this.TxtNoughtName = new System.Windows.Forms.Label();
            this.p = new System.Windows.Forms.PictureBox();
            this.TxtCrossName = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.Board = new System.Windows.Forms.Panel();
            this.SC3 = new System.Windows.Forms.Button();
            this.SC2 = new System.Windows.Forms.Button();
            this.SC1 = new System.Windows.Forms.Button();
            this.SB3 = new System.Windows.Forms.Button();
            this.SB2 = new System.Windows.Forms.Button();
            this.SB1 = new System.Windows.Forms.Button();
            this.SA3 = new System.Windows.Forms.Button();
            this.SA2 = new System.Windows.Forms.Button();
            this.SA1 = new System.Windows.Forms.Button();
            this.Waiting.SuspendLayout();
            this.Game.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PlayerPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TurnPB)).BeginInit();
            this.panel2.SuspendLayout();
            this.PWinner.SuspendLayout();
            this.WaitingSpectatorPanel.SuspendLayout();
            this.SpectatorPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PBTurn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.p)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.Board.SuspendLayout();
            this.SuspendLayout();
            // 
            // Waiting
            // 
            this.Waiting.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Waiting.Controls.Add(this.WaitingRound);
            this.Waiting.Controls.Add(this.label5);
            this.Waiting.Controls.Add(this.button1);
            this.Waiting.Controls.Add(this.label4);
            this.Waiting.Controls.Add(this.label3);
            this.Waiting.Controls.Add(this.WaitingGame);
            this.Waiting.Controls.Add(this.WaitingTournament);
            this.Waiting.Location = new System.Drawing.Point(12, 325);
            this.Waiting.Name = "Waiting";
            this.Waiting.Size = new System.Drawing.Size(555, 285);
            this.Waiting.TabIndex = 16;
            // 
            // WaitingRound
            // 
            this.WaitingRound.AutoSize = true;
            this.WaitingRound.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WaitingRound.Location = new System.Drawing.Point(67, 31);
            this.WaitingRound.Name = "WaitingRound";
            this.WaitingRound.Size = new System.Drawing.Size(410, 72);
            this.WaitingRound.TabIndex = 19;
            this.WaitingRound.Text = "You are automatically through to the next round \r\n because there is no pair for y" +
    "ou to play against \r\nso please wait for the round to finish";
            this.WaitingRound.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.WaitingRound.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(205, 183);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "Infomation";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(215, 151);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 24;
            this.button1.Text = "Quit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(132, 250);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(272, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Please check for message boxes throughtout the games";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(132, 215);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(255, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Sever may be laggy so please allow time for it to load";
            // 
            // WaitingGame
            // 
            this.WaitingGame.AutoSize = true;
            this.WaitingGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WaitingGame.Location = new System.Drawing.Point(39, 39);
            this.WaitingGame.Name = "WaitingGame";
            this.WaitingGame.Size = new System.Drawing.Size(446, 42);
            this.WaitingGame.TabIndex = 0;
            this.WaitingGame.Text = "Waiting For Next Game....";
            this.WaitingGame.Visible = false;
            // 
            // WaitingTournament
            // 
            this.WaitingTournament.AutoSize = true;
            this.WaitingTournament.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WaitingTournament.Location = new System.Drawing.Point(11, 46);
            this.WaitingTournament.Name = "WaitingTournament";
            this.WaitingTournament.Size = new System.Drawing.Size(526, 37);
            this.WaitingTournament.TabIndex = 18;
            this.WaitingTournament.Text = "Waiting For Tournament To Start....";
            // 
            // Game
            // 
            this.Game.Controls.Add(this.label2);
            this.Game.Controls.Add(this.label1);
            this.Game.Controls.Add(this.PlayerPB);
            this.Game.Controls.Add(this.TurnPB);
            this.Game.Controls.Add(this.Newgame);
            this.Game.Controls.Add(this.panel2);
            this.Game.Controls.Add(this.LeaveBTN);
            this.Game.Controls.Add(this.messagesLB);
            this.Game.Location = new System.Drawing.Point(0, 0);
            this.Game.Name = "Game";
            this.Game.Size = new System.Drawing.Size(820, 319);
            this.Game.TabIndex = 18;
            this.Game.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(599, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "You are ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(414, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Turn";
            // 
            // PlayerPB
            // 
            this.PlayerPB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PlayerPB.Location = new System.Drawing.Point(577, 47);
            this.PlayerPB.Name = "PlayerPB";
            this.PlayerPB.Size = new System.Drawing.Size(95, 95);
            this.PlayerPB.TabIndex = 21;
            this.PlayerPB.TabStop = false;
            // 
            // TurnPB
            // 
            this.TurnPB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.TurnPB.Location = new System.Drawing.Point(395, 47);
            this.TurnPB.Name = "TurnPB";
            this.TurnPB.Size = new System.Drawing.Size(95, 95);
            this.TurnPB.TabIndex = 20;
            this.TurnPB.TabStop = false;
            // 
            // Newgame
            // 
            this.Newgame.Location = new System.Drawing.Point(374, 262);
            this.Newgame.Name = "Newgame";
            this.Newgame.Size = new System.Drawing.Size(75, 23);
            this.Newgame.TabIndex = 19;
            this.Newgame.Text = "New game";
            this.Newgame.UseVisualStyleBackColor = true;
            this.Newgame.Visible = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Controls.Add(this.C3);
            this.panel2.Controls.Add(this.C2);
            this.panel2.Controls.Add(this.C1);
            this.panel2.Controls.Add(this.B3);
            this.panel2.Controls.Add(this.B2);
            this.panel2.Controls.Add(this.B1);
            this.panel2.Controls.Add(this.A3);
            this.panel2.Controls.Add(this.A2);
            this.panel2.Controls.Add(this.A1);
            this.panel2.Location = new System.Drawing.Point(31, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(295, 295);
            this.panel2.TabIndex = 18;
            // 
            // C3
            // 
            this.C3.BackColor = System.Drawing.Color.White;
            this.C3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.C3.Location = new System.Drawing.Point(200, 200);
            this.C3.Name = "C3";
            this.C3.Size = new System.Drawing.Size(95, 95);
            this.C3.TabIndex = 18;
            this.C3.UseVisualStyleBackColor = false;
            // 
            // C2
            // 
            this.C2.BackColor = System.Drawing.Color.White;
            this.C2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.C2.Location = new System.Drawing.Point(100, 200);
            this.C2.Name = "C2";
            this.C2.Size = new System.Drawing.Size(95, 95);
            this.C2.TabIndex = 17;
            this.C2.UseVisualStyleBackColor = false;
            // 
            // C1
            // 
            this.C1.BackColor = System.Drawing.Color.White;
            this.C1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.C1.Location = new System.Drawing.Point(0, 200);
            this.C1.Name = "C1";
            this.C1.Size = new System.Drawing.Size(95, 95);
            this.C1.TabIndex = 16;
            this.C1.UseVisualStyleBackColor = false;
            // 
            // B3
            // 
            this.B3.BackColor = System.Drawing.Color.White;
            this.B3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.B3.Location = new System.Drawing.Point(200, 100);
            this.B3.Name = "B3";
            this.B3.Size = new System.Drawing.Size(95, 95);
            this.B3.TabIndex = 15;
            this.B3.UseVisualStyleBackColor = false;
            // 
            // B2
            // 
            this.B2.BackColor = System.Drawing.Color.White;
            this.B2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.B2.Location = new System.Drawing.Point(100, 100);
            this.B2.Name = "B2";
            this.B2.Size = new System.Drawing.Size(95, 95);
            this.B2.TabIndex = 14;
            this.B2.UseVisualStyleBackColor = false;
            // 
            // B1
            // 
            this.B1.BackColor = System.Drawing.Color.White;
            this.B1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.B1.Location = new System.Drawing.Point(0, 100);
            this.B1.Name = "B1";
            this.B1.Size = new System.Drawing.Size(95, 95);
            this.B1.TabIndex = 13;
            this.B1.UseVisualStyleBackColor = false;
            // 
            // A3
            // 
            this.A3.BackColor = System.Drawing.Color.White;
            this.A3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.A3.Location = new System.Drawing.Point(200, 0);
            this.A3.Name = "A3";
            this.A3.Size = new System.Drawing.Size(95, 95);
            this.A3.TabIndex = 12;
            this.A3.UseVisualStyleBackColor = false;
            // 
            // A2
            // 
            this.A2.BackColor = System.Drawing.Color.White;
            this.A2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.A2.Location = new System.Drawing.Point(100, 0);
            this.A2.Name = "A2";
            this.A2.Size = new System.Drawing.Size(95, 95);
            this.A2.TabIndex = 11;
            this.A2.UseVisualStyleBackColor = false;
            // 
            // A1
            // 
            this.A1.BackColor = System.Drawing.Color.White;
            this.A1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.A1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.A1.Location = new System.Drawing.Point(0, 0);
            this.A1.Name = "A1";
            this.A1.Size = new System.Drawing.Size(95, 95);
            this.A1.TabIndex = 10;
            this.A1.UseVisualStyleBackColor = false;
            // 
            // LeaveBTN
            // 
            this.LeaveBTN.Location = new System.Drawing.Point(375, 291);
            this.LeaveBTN.Name = "LeaveBTN";
            this.LeaveBTN.Size = new System.Drawing.Size(75, 23);
            this.LeaveBTN.TabIndex = 17;
            this.LeaveBTN.Text = "leave";
            this.LeaveBTN.UseVisualStyleBackColor = true;
            this.LeaveBTN.Click += new System.EventHandler(this.LeaveBTN_Click);
            // 
            // messagesLB
            // 
            this.messagesLB.Location = new System.Drawing.Point(456, 167);
            this.messagesLB.Name = "messagesLB";
            this.messagesLB.Size = new System.Drawing.Size(361, 147);
            this.messagesLB.TabIndex = 16;
            // 
            // PWinner
            // 
            this.PWinner.BackgroundImage = global::TicTacToeClient.Properties.Resources.VictoryRoyale;
            this.PWinner.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PWinner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PWinner.Controls.Add(this.button2);
            this.PWinner.Location = new System.Drawing.Point(586, 325);
            this.PWinner.Name = "PWinner";
            this.PWinner.Size = new System.Drawing.Size(555, 285);
            this.PWinner.TabIndex = 25;
            this.PWinner.Visible = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(226, 232);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "Quit";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // WaitingSpectatorPanel
            // 
            this.WaitingSpectatorPanel.Controls.Add(this.button3);
            this.WaitingSpectatorPanel.Controls.Add(this.label6);
            this.WaitingSpectatorPanel.Location = new System.Drawing.Point(1173, 12);
            this.WaitingSpectatorPanel.Name = "WaitingSpectatorPanel";
            this.WaitingSpectatorPanel.Size = new System.Drawing.Size(800, 353);
            this.WaitingSpectatorPanel.TabIndex = 32;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(366, 193);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 25;
            this.button3.Text = "Quit";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_2);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(172, 140);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(409, 31);
            this.label6.TabIndex = 0;
            this.label6.Text = "Waiting For Tournament To Start";
            // 
            // SpectatorPanel
            // 
            this.SpectatorPanel.Controls.Add(this.button4);
            this.SpectatorPanel.Controls.Add(this.TxtInfo);
            this.SpectatorPanel.Controls.Add(this.SLBmessages);
            this.SpectatorPanel.Controls.Add(this.label7);
            this.SpectatorPanel.Controls.Add(this.label8);
            this.SpectatorPanel.Controls.Add(this.CBGames);
            this.SpectatorPanel.Controls.Add(this.TxtTurn);
            this.SpectatorPanel.Controls.Add(this.PBTurn);
            this.SpectatorPanel.Controls.Add(this.TxtNoughtName);
            this.SpectatorPanel.Controls.Add(this.p);
            this.SpectatorPanel.Controls.Add(this.TxtCrossName);
            this.SpectatorPanel.Controls.Add(this.pictureBox2);
            this.SpectatorPanel.Controls.Add(this.Board);
            this.SpectatorPanel.Location = new System.Drawing.Point(1173, 382);
            this.SpectatorPanel.Name = "SpectatorPanel";
            this.SpectatorPanel.Size = new System.Drawing.Size(909, 339);
            this.SpectatorPanel.TabIndex = 33;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(325, 288);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 55;
            this.button4.Text = "Quit";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // TxtInfo
            // 
            this.TxtInfo.AutoSize = true;
            this.TxtInfo.Location = new System.Drawing.Point(21, 11);
            this.TxtInfo.Name = "TxtInfo";
            this.TxtInfo.Size = new System.Drawing.Size(35, 13);
            this.TxtInfo.TabIndex = 43;
            this.TxtInfo.Text = "label4";
            this.TxtInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.TxtInfo.Visible = false;
            // 
            // SLBmessages
            // 
            this.SLBmessages.Location = new System.Drawing.Point(405, 176);
            this.SLBmessages.Name = "SLBmessages";
            this.SLBmessages.Size = new System.Drawing.Size(361, 147);
            this.SLBmessages.TabIndex = 54;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(647, 100);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(18, 13);
            this.label7.TabIndex = 53;
            this.label7.Text = "vs";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(338, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 13);
            this.label8.TabIndex = 52;
            this.label8.Text = "Game:";
            // 
            // CBGames
            // 
            this.CBGames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBGames.FormattingEnabled = true;
            this.CBGames.Location = new System.Drawing.Point(379, 17);
            this.CBGames.Name = "CBGames";
            this.CBGames.Size = new System.Drawing.Size(387, 21);
            this.CBGames.TabIndex = 51;
            // 
            // TxtTurn
            // 
            this.TxtTurn.AutoSize = true;
            this.TxtTurn.Location = new System.Drawing.Point(412, 51);
            this.TxtTurn.Name = "TxtTurn";
            this.TxtTurn.Size = new System.Drawing.Size(29, 13);
            this.TxtTurn.TabIndex = 50;
            this.TxtTurn.Text = "Turn";
            // 
            // PBTurn
            // 
            this.PBTurn.BackColor = System.Drawing.Color.White;
            this.PBTurn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PBTurn.Location = new System.Drawing.Point(379, 67);
            this.PBTurn.Name = "PBTurn";
            this.PBTurn.Size = new System.Drawing.Size(95, 95);
            this.PBTurn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PBTurn.TabIndex = 49;
            this.PBTurn.TabStop = false;
            // 
            // TxtNoughtName
            // 
            this.TxtNoughtName.AutoSize = true;
            this.TxtNoughtName.Location = new System.Drawing.Point(551, 51);
            this.TxtNoughtName.Name = "TxtNoughtName";
            this.TxtNoughtName.Size = new System.Drawing.Size(70, 13);
            this.TxtNoughtName.TabIndex = 48;
            this.TxtNoughtName.Text = "NoughtName";
            // 
            // p
            // 
            this.p.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.p.Image = global::TicTacToeClient.Properties.Resources.Nought;
            this.p.Location = new System.Drawing.Point(537, 67);
            this.p.Name = "p";
            this.p.Size = new System.Drawing.Size(95, 95);
            this.p.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.p.TabIndex = 47;
            this.p.TabStop = false;
            // 
            // TxtCrossName
            // 
            this.TxtCrossName.AutoSize = true;
            this.TxtCrossName.Location = new System.Drawing.Point(690, 51);
            this.TxtCrossName.Name = "TxtCrossName";
            this.TxtCrossName.Size = new System.Drawing.Size(61, 13);
            this.TxtCrossName.TabIndex = 46;
            this.TxtCrossName.Text = "CrossName";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.White;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Image = global::TicTacToeClient.Properties.Resources.cross;
            this.pictureBox2.Location = new System.Drawing.Point(671, 67);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(95, 95);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 45;
            this.pictureBox2.TabStop = false;
            // 
            // Board
            // 
            this.Board.BackColor = System.Drawing.Color.Black;
            this.Board.Controls.Add(this.SC3);
            this.Board.Controls.Add(this.SC2);
            this.Board.Controls.Add(this.SC1);
            this.Board.Controls.Add(this.SB3);
            this.Board.Controls.Add(this.SB2);
            this.Board.Controls.Add(this.SB1);
            this.Board.Controls.Add(this.SA3);
            this.Board.Controls.Add(this.SA2);
            this.Board.Controls.Add(this.SA1);
            this.Board.Location = new System.Drawing.Point(24, 27);
            this.Board.Name = "Board";
            this.Board.Size = new System.Drawing.Size(295, 295);
            this.Board.TabIndex = 44;
            // 
            // SC3
            // 
            this.SC3.BackColor = System.Drawing.Color.White;
            this.SC3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SC3.Enabled = false;
            this.SC3.Location = new System.Drawing.Point(200, 200);
            this.SC3.Name = "SC3";
            this.SC3.Size = new System.Drawing.Size(95, 95);
            this.SC3.TabIndex = 18;
            this.SC3.UseVisualStyleBackColor = false;
            // 
            // SC2
            // 
            this.SC2.BackColor = System.Drawing.Color.White;
            this.SC2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SC2.Enabled = false;
            this.SC2.Location = new System.Drawing.Point(100, 200);
            this.SC2.Name = "SC2";
            this.SC2.Size = new System.Drawing.Size(95, 95);
            this.SC2.TabIndex = 17;
            this.SC2.UseVisualStyleBackColor = false;
            // 
            // SC1
            // 
            this.SC1.BackColor = System.Drawing.Color.White;
            this.SC1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SC1.Enabled = false;
            this.SC1.Location = new System.Drawing.Point(0, 200);
            this.SC1.Name = "SC1";
            this.SC1.Size = new System.Drawing.Size(95, 95);
            this.SC1.TabIndex = 16;
            this.SC1.UseVisualStyleBackColor = false;
            // 
            // SB3
            // 
            this.SB3.BackColor = System.Drawing.Color.White;
            this.SB3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SB3.Enabled = false;
            this.SB3.Location = new System.Drawing.Point(200, 100);
            this.SB3.Name = "SB3";
            this.SB3.Size = new System.Drawing.Size(95, 95);
            this.SB3.TabIndex = 15;
            this.SB3.UseVisualStyleBackColor = false;
            // 
            // SB2
            // 
            this.SB2.BackColor = System.Drawing.Color.White;
            this.SB2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SB2.Enabled = false;
            this.SB2.Location = new System.Drawing.Point(100, 100);
            this.SB2.Name = "SB2";
            this.SB2.Size = new System.Drawing.Size(95, 95);
            this.SB2.TabIndex = 14;
            this.SB2.UseVisualStyleBackColor = false;
            // 
            // SB1
            // 
            this.SB1.BackColor = System.Drawing.Color.White;
            this.SB1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SB1.Enabled = false;
            this.SB1.Location = new System.Drawing.Point(0, 100);
            this.SB1.Name = "SB1";
            this.SB1.Size = new System.Drawing.Size(95, 95);
            this.SB1.TabIndex = 13;
            this.SB1.UseVisualStyleBackColor = false;
            // 
            // SA3
            // 
            this.SA3.BackColor = System.Drawing.Color.White;
            this.SA3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SA3.Enabled = false;
            this.SA3.Location = new System.Drawing.Point(200, 0);
            this.SA3.Name = "SA3";
            this.SA3.Size = new System.Drawing.Size(95, 95);
            this.SA3.TabIndex = 12;
            this.SA3.UseVisualStyleBackColor = false;
            // 
            // SA2
            // 
            this.SA2.BackColor = System.Drawing.Color.White;
            this.SA2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SA2.Enabled = false;
            this.SA2.Location = new System.Drawing.Point(100, 0);
            this.SA2.Name = "SA2";
            this.SA2.Size = new System.Drawing.Size(95, 95);
            this.SA2.TabIndex = 11;
            this.SA2.UseVisualStyleBackColor = false;
            // 
            // SA1
            // 
            this.SA1.BackColor = System.Drawing.Color.White;
            this.SA1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SA1.Enabled = false;
            this.SA1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.SA1.Location = new System.Drawing.Point(0, 0);
            this.SA1.Name = "SA1";
            this.SA1.Size = new System.Drawing.Size(95, 95);
            this.SA1.TabIndex = 10;
            this.SA1.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2732, 745);
            this.ControlBox = false;
            this.Controls.Add(this.SpectatorPanel);
            this.Controls.Add(this.WaitingSpectatorPanel);
            this.Controls.Add(this.PWinner);
            this.Controls.Add(this.Waiting);
            this.Controls.Add(this.Game);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = " Connected";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Waiting.ResumeLayout(false);
            this.Waiting.PerformLayout();
            this.Game.ResumeLayout(false);
            this.Game.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PlayerPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TurnPB)).EndInit();
            this.panel2.ResumeLayout(false);
            this.PWinner.ResumeLayout(false);
            this.WaitingSpectatorPanel.ResumeLayout(false);
            this.WaitingSpectatorPanel.PerformLayout();
            this.SpectatorPanel.ResumeLayout(false);
            this.SpectatorPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PBTurn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.p)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.Board.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.Panel Waiting;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label WaitingGame;
        public System.Windows.Forms.Panel Game;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.PictureBox PlayerPB;
        public System.Windows.Forms.PictureBox TurnPB;
        public System.Windows.Forms.Button Newgame;
        public System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button C3;
        private System.Windows.Forms.Button C2;
        private System.Windows.Forms.Button C1;
        private System.Windows.Forms.Button B3;
        private System.Windows.Forms.Button B2;
        private System.Windows.Forms.Button B1;
        private System.Windows.Forms.Button A3;
        private System.Windows.Forms.Button A2;
        private System.Windows.Forms.Button A1;
        public System.Windows.Forms.Button LeaveBTN;
        public System.Windows.Forms.ListBox messagesLB;
        private System.Windows.Forms.Label WaitingTournament;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label WaitingRound;
        public System.Windows.Forms.Panel PWinner;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.ListBox SLBmessages;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.PictureBox PBTurn;
        public System.Windows.Forms.PictureBox p;
        public System.Windows.Forms.PictureBox pictureBox2;
        public System.Windows.Forms.Panel Board;
        public System.Windows.Forms.Panel WaitingSpectatorPanel;
        public System.Windows.Forms.Panel SpectatorPanel;
        public System.Windows.Forms.Label TxtInfo;
        public System.Windows.Forms.Label label8;
        public System.Windows.Forms.ComboBox CBGames;
        public System.Windows.Forms.Label TxtTurn;
        public System.Windows.Forms.Label TxtNoughtName;
        public System.Windows.Forms.Label TxtCrossName;
        public System.Windows.Forms.Button SC3;
        public System.Windows.Forms.Button SC2;
        public System.Windows.Forms.Button SC1;
        public System.Windows.Forms.Button SB3;
        public System.Windows.Forms.Button SB2;
        public System.Windows.Forms.Button SB1;
        public System.Windows.Forms.Button SA3;
        public System.Windows.Forms.Button SA2;
        public System.Windows.Forms.Button SA1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}

