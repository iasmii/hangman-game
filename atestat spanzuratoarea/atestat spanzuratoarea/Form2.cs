using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace atestat_spanzuratoarea
{
    public partial class frmReguli : Form
    {
        public frmReguli()
        {
            InitializeComponent();
            label1.BackColor = System.Drawing.Color.Transparent;

            ReadOnlyAttribute readOnly = new ReadOnlyAttribute(true);
            //StreamReader fisier = new StreamReader("C:\\Users\\USER\\Documents\\aaa\\atestat spanzuratoarea\\reguli.txt");
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(baseDirectory, "TXT", "reguli.txt");
            StreamReader fisier = new StreamReader(filePath);
            string line = fisier.ReadLine();
            while(line != null)
            {
                richTextBox1.Text += line + "\r\n";
                line = fisier.ReadLine();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
