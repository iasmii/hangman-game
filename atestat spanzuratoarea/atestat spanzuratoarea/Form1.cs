using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace atestat_spanzuratoarea
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label1.BackColor = System.Drawing.Color.Transparent;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmJoc form = new frmJoc();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmReguli form = new frmReguli();
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string message = "Doriti sa parasiti jocul?";
            string title = "Parasire joc";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
        
    }
}
