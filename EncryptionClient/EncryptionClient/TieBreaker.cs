using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TicTacToeClient
{
    public partial class TieBreaker : Form
    {
        Form1 f1;
        Thread trd;
        public TieBreaker(Form1 form1 , Thread rd)
        {
            f1 = form1;
            trd = rd;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            f1.senddata("Num:" + NumberChose.Value.ToString());
            this.Close();
        }

        private void TieBreaker_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void TieBreaker_Load(object sender, EventArgs e)
        {
            trd.Start();
        }
    }
}
