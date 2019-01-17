namespace TicTacToeServer
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
            this.LBGeneral = new System.Windows.Forms.ListBox();
            this.Players = new System.Windows.Forms.Label();
            this.numberplay = new System.Windows.Forms.Label();
            this.CBMessages = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Border = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TotalPlayers = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SpectatorCount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LBGeneral
            // 
            this.LBGeneral.FormattingEnabled = true;
            this.LBGeneral.Location = new System.Drawing.Point(140, 39);
            this.LBGeneral.Name = "LBGeneral";
            this.LBGeneral.Size = new System.Drawing.Size(347, 238);
            this.LBGeneral.TabIndex = 27;
            this.LBGeneral.Visible = false;
            // 
            // Players
            // 
            this.Players.AutoSize = true;
            this.Players.Location = new System.Drawing.Point(25, 85);
            this.Players.Name = "Players";
            this.Players.Size = new System.Drawing.Size(44, 13);
            this.Players.TabIndex = 29;
            this.Players.Text = "Players:";
            // 
            // numberplay
            // 
            this.numberplay.AutoSize = true;
            this.numberplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numberplay.Location = new System.Drawing.Point(93, 70);
            this.numberplay.Name = "numberplay";
            this.numberplay.Size = new System.Drawing.Size(29, 31);
            this.numberplay.TabIndex = 30;
            this.numberplay.Text = "0";
            // 
            // CBMessages
            // 
            this.CBMessages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBMessages.FormattingEnabled = true;
            this.CBMessages.Location = new System.Drawing.Point(140, 12);
            this.CBMessages.Name = "CBMessages";
            this.CBMessages.Size = new System.Drawing.Size(347, 21);
            this.CBMessages.TabIndex = 35;
            this.CBMessages.SelectedIndexChanged += new System.EventHandler(this.GameNumber_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 36;
            this.label1.Text = "Game Number:";
            // 
            // Border
            // 
            this.Border.AutoSize = true;
            this.Border.Location = new System.Drawing.Point(85, 101);
            this.Border.Name = "Border";
            this.Border.Size = new System.Drawing.Size(46, 13);
            this.Border.TabIndex = 37;
            this.Border.Text = "-------------";
            this.Border.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(96, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 13);
            this.label3.TabIndex = 38;
            // 
            // TotalPlayers
            // 
            this.TotalPlayers.AutoSize = true;
            this.TotalPlayers.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalPlayers.Location = new System.Drawing.Point(93, 114);
            this.TotalPlayers.Name = "TotalPlayers";
            this.TotalPlayers.Size = new System.Drawing.Size(29, 31);
            this.TotalPlayers.TabIndex = 39;
            this.TotalPlayers.Text = "0";
            this.TotalPlayers.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 183);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 40;
            this.label2.Text = "Spectators:";
            // 
            // SpectatorCount
            // 
            this.SpectatorCount.AutoSize = true;
            this.SpectatorCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SpectatorCount.Location = new System.Drawing.Point(84, 183);
            this.SpectatorCount.Name = "SpectatorCount";
            this.SpectatorCount.Size = new System.Drawing.Size(29, 31);
            this.SpectatorCount.TabIndex = 41;
            this.SpectatorCount.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 294);
            this.ControlBox = false;
            this.Controls.Add(this.SpectatorCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TotalPlayers);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Border);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CBMessages);
            this.Controls.Add(this.numberplay);
            this.Controls.Add(this.Players);
            this.Controls.Add(this.LBGeneral);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.ListBox LBGeneral;
        public System.Windows.Forms.Label Players;
        public System.Windows.Forms.Label numberplay;
        private System.Windows.Forms.ComboBox CBMessages;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Border;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label TotalPlayers;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label SpectatorCount;
    }
}