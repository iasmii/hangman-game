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
    public partial class frmJoc : Form
    {
        private int _nrrunda = 0;
        public int Nrrunda { 
            get { return _nrrunda; } 
            set { _nrrunda = value; } 
        }

        private string _cuvCitit = string.Empty;
        public string CuvCitit
        { 
            get { return _cuvCitit; } 
            set { _cuvCitit = value; } 
        } 

        List<string> litereGhicite=new List<string>();
        private int _nrGreseli;
        public int nrGreseli
        {
            get { return _nrGreseli; }
            set { _nrGreseli = value; }
        }

        private int _scorJ1;
        public int scorJ1
        {
            get { return _scorJ1; } 
            set { _scorJ1 = value; }    
        }

        private int _scorJ2;
        public int scorJ2
        {
            get { return _scorJ2; }
            set { _scorJ2 = value; }
        }

        public frmJoc()
        {
            InitializeComponent();
            lblRunda.BackColor = System.Drawing.Color.Transparent;
            lblGreseli.BackColor = System.Drawing.Color.Transparent;
            lblCuvAscuns.BackColor = System.Drawing.Color.Transparent;
            lblJ1.BackColor = System.Drawing.Color.Transparent;
            lblJ2.BackColor = System.Drawing.Color.Transparent;
            lblMesaj.BackColor = System.Drawing.Color.Transparent;
            lblCineScrie.BackColor = System.Drawing.Color.Transparent;
        }

        private void frmJoc_Load(object sender, EventArgs e)
        {
            foreach (Control control in this.Controls)
            {
                if (control.GetType() == typeof(Button) && control.Text!="Ok")
                {
                    control.Width = 40;
                    control.Height = 40;
                    control.Enabled = false;
                }
            }
            nrGreseli = 0;
            //afisare nr runda
            lblRunda.Text =  "Runda: 1";
            lblCineScrie.Text = "Jucator 1, introdu un cuvant\nde la tastatura!";
            scorJ1 = 0;
            scorJ2 = 0;
            lblJ1.Text = "Scor Jucator 1: " + scorJ1.ToString();
            lblJ2.Text = "Scor Jucator 2: " + scorJ2.ToString();
        }

        private bool PosibilGhicit(string cuvant)
        {
            char litera0 = cuvant[0];
            char literan = cuvant[cuvant.Length - 1];
            int nrap0 = 0, nrapn = 0;
            for (int i = 0; i < cuvant.Length; i++)
            {
                if (cuvant[i] == litera0) nrap0++;
                if (cuvant[i] == literan) nrapn++;
            }
            if(nrapn+nrap0==cuvant.Length) { return false; }
            else { return true; }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            CuvCitit = txtCuvCitit.Text.Trim(); //preluam cuvantul de citit fara spatii
            JocNou();
            CuvCitit=CuvCitit.ToUpper();
            if (CuvCitit.Length > 2 && CuvCitit.All(Char.IsLetter) && PosibilGhicit(CuvCitit))
            {
                litereGhicite.Add(CuvCitit.Substring(0, 1));//prima litera se cunoaste
                litereGhicite.Add(CuvCitit.Substring(CuvCitit.Length-1, 1));//ultima litera se cunoaste
                Afisare(CuvCitit);
                foreach (Control control in this.Controls)
                {
                    if (control.GetType() == typeof(Button) && control.Text != "Ok"
                    && control.Text != CuvCitit.Substring(0, 1) && control.Text != CuvCitit.Substring(CuvCitit.Length - 1, 1))
                    {
                        control.Enabled = true;
                    }
                }
            }
            else
            {
                string message = "Cuvantul trebuie sa aiba cel putin 3 litere!\nCuvantul nu poate contine spatii, simblouri sau cifre!\nCuvantul trebuie sa contina si litere diferite de prima si ultima!";
                string title = "Cuvant invalid";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Information);
                
                txtCuvCitit.Clear();
            }
            
        }

        private void JocNou() //initializam runda noua
        {
            litereGhicite.Clear();//golim literele ghicite anterior
            Nrrunda++;//creste nr rundei
            lblRunda.Text="Runda: "+Nrrunda.ToString();
            foreach (Control control in this.Controls)
            {
                if (control.GetType() == typeof(Button) && control.Text != "Ok")
                {
                    control.BackColor = Color.FromArgb(64,64, 64);
                    control.Enabled = false;
                }
            }

            nrGreseli = 0;//resetam nr de greseli
            lblGreseli.Text = "Numar greseli: " + nrGreseli.ToString();
            this.BackgroundImage = atestat_spanzuratoarea.Properties.Resources.g0;//resetam imaginea de fundal
            lblMesaj.Text = string.Empty;
        }

        private void CuvEgale()
        {
            bool rezultat = true;//preseupunem ca a descoperit cuvantul
            for (int i = 0; i <= CuvCitit.Length - 1; i++)
            {
                var litera = CuvCitit.Substring(i, 1);//parcurg literele din cuvant
                if (!(litereGhicite.Contains(litera))) // daca o litera nu a fost ghicita
                {
                    rezultat = false;//nu a fost ghicit cuvantul
                }
            }
            if (rezultat == true) AiCastigat();
        }

        private bool Apare (string literaAleasa) //verficam daca litera aleasa apare in cuvant
        {
            bool rezultat = false;//presupunem ca litera nu apare in cuvant
            for (int i = 0; i <= CuvCitit.Length-1; i++)
            {
                var litera = CuvCitit.Substring(i, 1);//parcurg literele din cuvant
                if (litera == literaAleasa) // daca apare
                {
                    litereGhicite.Add(literaAleasa);//marcam ca e ghicita
                    rezultat = true;//marcam ca apare
                    CuvEgale();
                }
            }
            return rezultat;
        }

        private void Afisare (string cuvant) //afisam cuvantul in lblCuvAscuns
        {
            lblCuvAscuns.Text = string.Empty;
            var litera0 = cuvant.Substring(0, 1);//retinem prima litera ca sa putem afisa toate aparitiile
            var literan = cuvant.Substring(cuvant.Length-1, 1);//retinem ultima litera ca sa putem afisa toate aparitiile
            for (int i=0; i<=cuvant.Length-1; i++)
            {
                var litera=cuvant.Substring(i, 1);
                if (litera==literan || litera==litera0 || litereGhicite.Contains(litera))//daca litera e prima, ultima sau ghicita se afiseaza
                {
                    lblCuvAscuns.Text=lblCuvAscuns.Text + litera + " ";//adaugam litera la valoarea initiala
                }
                else
                {
                    lblCuvAscuns.Text = lblCuvAscuns.Text + "_ ";//altfel litera ramane ascunsa
                }

            }
        }

        private void AiPierdut()
        {
            for (int i = 0; i <= CuvCitit.Length - 1; i++)
            {
                var litera = CuvCitit.Substring(i, 1);
                litereGhicite.Add(litera);
            }
            Afisare(CuvCitit);
            lblMesaj.Text = "Ai pierdut runda! :(";
            if ((Nrrunda + 1) % 2 == 0) lblCineScrie.Text = "Jucator 2, introdu un cuvant\nde la tastatura!";
            else lblCineScrie.Text = "Jucator 1, introdu un cuvant\nde la tastatura!";
            foreach (Control control in this.Controls)
            {
                if (control.GetType() == typeof(Button) && control.Text != "Ok")
                {
                    control.Enabled = false;
                }
            }
            txtCuvCitit.Text = string.Empty;
            if (Nrrunda+1 > 10) GataJoc(); // cand se termina cele 10 runde
        }

        private void AiCastigat()
        {
            Afisare(CuvCitit);
            lblMesaj.Text = "Ai castigat runda! :)";
            if ((Nrrunda + 1) % 2 == 0)
            {
                lblCineScrie.Text = "Jucator 2, introdu un cuvant\nde la tastatura!";
                scorJ1++;//creste scorul pt juc 1
                lblJ1.Text = "Scor Jucator 1: " + scorJ1.ToString();
            }
            else
            {
                lblCineScrie.Text = "Jucator 1, introdu un cuvant\nde la tastatura!";
                scorJ2++;//creste scorul pt juc 2
                lblJ2.Text = "Scor Jucator 2: " + scorJ2.ToString();
            }
            foreach (Control control in this.Controls)
            {
                if (control.GetType() == typeof(Button) && control.Text != "Ok")
                {
                    control.Enabled = false;
                }
            }
            txtCuvCitit.Text = string.Empty;
            if (Nrrunda + 1 > 10) GataJoc(); // cand se termina cele 10 runde
        }

        private void GataJoc()
        {
            string message = "Felicitari, ati terminat jocul!";
            string title = "Final joc";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void SchimbFundal(int nrGreseli)
        {
            lblGreseli.Text = "Numar greseli: " + nrGreseli.ToString();
            switch (nrGreseli)
            {
                case 1:
                    this.BackgroundImage = atestat_spanzuratoarea.Properties.Resources.g1;//schimbam imaginea de fundal
                    break;
                case 2:
                    this.BackgroundImage = atestat_spanzuratoarea.Properties.Resources.g2;//schimbam imaginea de fundal
                    break;
                case 3:
                    this.BackgroundImage = atestat_spanzuratoarea.Properties.Resources.g3;//schimbam imaginea de fundal
                    break;
                case 4:
                    this.BackgroundImage = atestat_spanzuratoarea.Properties.Resources.g4;//schimbam imaginea de fundal
                    break;
                case 5:
                    this.BackgroundImage = atestat_spanzuratoarea.Properties.Resources.g5;//schimbam imaginea de fundal
                    break;
                case 6:
                    this.BackgroundImage = atestat_spanzuratoarea.Properties.Resources.g6;//schimbam imaginea de fundal
                    break;
                case 7:
                    this.BackgroundImage = atestat_spanzuratoarea.Properties.Resources.g7;//schimbam imaginea de fundal
                    break;
                case 8:
                    this.BackgroundImage = atestat_spanzuratoarea.Properties.Resources.g8;//schimbam imaginea de fundal
                    AiPierdut();
                    break;
                default: 
                    break;
            }
        }

        #region apasare butoane
        private void button1_Click(object sender, EventArgs e) //A
        {
            if (Apare(button1.Text) == true)
            {
                button1.BackColor = Color.Green;
            }
            else
            {
                button1.BackColor = Color.Red;
                nrGreseli++;
                SchimbFundal(nrGreseli);
            }
            Afisare(CuvCitit);
            button1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e) //B
        {
            if (Apare(button2.Text) == true)
            {
                button2.BackColor = Color.Green;
            }
            else
            {
                button2.BackColor = Color.Red;
                nrGreseli++;
                SchimbFundal(nrGreseli);
            }
            Afisare(CuvCitit);
            button2.Enabled = false;
        }
        private void button3_Click(object sender, EventArgs e) //C
        {
            if (Apare(button3.Text) == true)
            {
                button3.BackColor = Color.Green;
            }
            else
            {
                button3.BackColor = Color.Red;
                nrGreseli++;
                SchimbFundal(nrGreseli);
            }
            Afisare(CuvCitit);
            button3.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e) //D
        {
            if (Apare(button4.Text) == true)
            {
                button4.BackColor = Color.Green;
            }
            else
            {
                button4.BackColor = Color.Red;
                nrGreseli++;
                SchimbFundal(nrGreseli);
            }
            Afisare(CuvCitit);
            button4.Enabled = false;
        }

        private void button5_Click(object sender, EventArgs e) //E
        {
            if (Apare(button5.Text) == true)
            {
                button5.BackColor = Color.Green;
            }
            else
            {
                button5.BackColor = Color.Red;
                nrGreseli++;
                SchimbFundal(nrGreseli);
            }
            Afisare(CuvCitit);
            button5.Enabled = false;
        }

        private void button6_Click(object sender, EventArgs e) //F
        {
            if (Apare(button6.Text) == true)
            {
                button6.BackColor = Color.Green;
            }
            else
            {
                button6.BackColor = Color.Red;
                nrGreseli++;
                SchimbFundal(nrGreseli);
            }
            Afisare(CuvCitit);
            button6.Enabled = false;
        }

        private void button7_Click(object sender, EventArgs e) //G
        {
            if (Apare(button7.Text) == true)
            {
                button7.BackColor = Color.Green;
            }
            else
            {
                button7.BackColor = Color.Red;
                nrGreseli++;
                SchimbFundal(nrGreseli);
            }
            Afisare(CuvCitit);
            button7.Enabled = false;
        }

        private void button8_Click(object sender, EventArgs e) //H
        {
            if (Apare(button8.Text) == true)
            {
                button8.BackColor = Color.Green;
            }
            else
            {
                button8.BackColor = Color.Red;
                nrGreseli++;
                SchimbFundal(nrGreseli);
            }
            Afisare(CuvCitit);
            button8.Enabled = false;
        }

        private void button9_Click(object sender, EventArgs e) //I
        {
            if (Apare(button9.Text) == true)
            {
                button9.BackColor = Color.Green;
            }
            else
            {
                button9.BackColor = Color.Red;
                nrGreseli++;
                SchimbFundal(nrGreseli);
            }
            Afisare(CuvCitit);
            button9.Enabled = false;
        }

        private void button10_Click(object sender, EventArgs e) //J
        {
            if (Apare(button10.Text) == true)
            {
                button10.BackColor = Color.Green;
            }
            else
            {
                button10.BackColor = Color.Red;
                nrGreseli++;
                SchimbFundal(nrGreseli);
            }
            Afisare(CuvCitit);
            button10.Enabled = false;
        }

        private void button11_Click(object sender, EventArgs e) //K
        {
            if (Apare(button11.Text) == true)
            {
                button11.BackColor = Color.Green;
            }
            else
            {
                button11.BackColor = Color.Red;
                nrGreseli++;
                SchimbFundal(nrGreseli);
            }
            Afisare(CuvCitit);
            button11.Enabled = false;
        }

        private void button12_Click(object sender, EventArgs e) //L
        {
            if (Apare(button12.Text) == true)
            {
                button12.BackColor = Color.Green;
            }
            else
            {
                button12.BackColor = Color.Red;
                nrGreseli++;
                SchimbFundal(nrGreseli);
            }
            Afisare(CuvCitit);
            button12.Enabled = false;
        }

        private void button13_Click(object sender, EventArgs e) //M
        {
            if (Apare(button13.Text) == true)
            {
                button13.BackColor = Color.Green;
            }
            else
            {
                button13.BackColor = Color.Red;
                nrGreseli++;
                SchimbFundal(nrGreseli);
            }
            Afisare(CuvCitit);
            button13.Enabled = false;
        }

        private void button14_Click(object sender, EventArgs e) //N
        {
            if (Apare(button14.Text) == true)
            {
                button14.BackColor = Color.Green;
            }
            else
            {
                button14.BackColor = Color.Red;
                nrGreseli++;
                SchimbFundal(nrGreseli);
            }
            Afisare(CuvCitit);
            button14.Enabled = false;
        }

        private void button15_Click(object sender, EventArgs e) //O
        {
            if(Apare(button15.Text) == true)
            {
                button15.BackColor = Color.Green;
            }
            else
            {
                button15.BackColor = Color.Red;
                nrGreseli++;
                SchimbFundal(nrGreseli);
            }
            Afisare(CuvCitit);
            button15.Enabled = false;
        }

        private void button16_Click(object sender, EventArgs e) //P
        {
            if (Apare(button16.Text) == true)
            {
                button16.BackColor = Color.Green;
            }
            else
            {
                button16.BackColor = Color.Red;
                nrGreseli++;
                SchimbFundal(nrGreseli);
            }
            Afisare(CuvCitit);
            button16.Enabled = false;
        }

        private void button17_Click(object sender, EventArgs e) //Q
        {
            if (Apare(button17.Text) == true)
            {
                button17.BackColor = Color.Green;
            }
            else
            {
                button17.BackColor = Color.Red;
                nrGreseli++;
                SchimbFundal(nrGreseli);
            }
            Afisare(CuvCitit);
            button17.Enabled = false;
        }

        private void button18_Click(object sender, EventArgs e) //R
        {
            if (Apare(button18.Text) == true)
            {
                button18.BackColor = Color.Green;
            }
            else
            {
                button18.BackColor = Color.Red;
                nrGreseli++;
                SchimbFundal(nrGreseli);
            }
            Afisare(CuvCitit);
            button18.Enabled = false;
        }

        private void button19_Click(object sender, EventArgs e) //S
        {
            if (Apare(button19.Text) == true)
            {
                button19.BackColor = Color.Green;
            }
            else
            {
                button19.BackColor = Color.Red;
                nrGreseli++;
                SchimbFundal(nrGreseli);
            }
            Afisare(CuvCitit);
            button19.Enabled = false;
        }

        private void button20_Click(object sender, EventArgs e) //T
        {
            if (Apare(button20.Text) == true)
            {
                button20.BackColor = Color.Green;
            }
            else
            {
                button20.BackColor = Color.Red;
                nrGreseli++;
                SchimbFundal(nrGreseli);
            }
            Afisare(CuvCitit);
            button20.Enabled = false;
        }

        private void button21_Click(object sender, EventArgs e) //U
        {
            if (Apare(button21.Text) == true)
            {
                button21.BackColor = Color.Green;
            }
            else
            {
                button21.BackColor = Color.Red;
                nrGreseli++;
                SchimbFundal(nrGreseli);
            }
            Afisare(CuvCitit);
            button21.Enabled = false;
        }

        private void button22_Click(object sender, EventArgs e) //V
        {
            if (Apare(button22.Text) == true)
            {
                button22.BackColor = Color.Green;
            }
            else
            {
                button22.BackColor = Color.Red;
                nrGreseli++;
                SchimbFundal(nrGreseli);
            }
            Afisare(CuvCitit);
            button22.Enabled = false;
        }

        private void button23_Click(object sender, EventArgs e) //W
        {
            if (Apare(button23.Text) == true)
            {
                button23.BackColor = Color.Green;
            }
            else
            {
                button23.BackColor = Color.Red;
                nrGreseli++;
                SchimbFundal(nrGreseli);
            }
            Afisare(CuvCitit);
            button23.Enabled = false;
        }

        private void button24_Click(object sender, EventArgs e) //X
        {
            if (Apare(button24.Text) == true)
            {
                button24.BackColor = Color.Green;
            }
            else
            {
                button24.BackColor = Color.Red;
                nrGreseli++;
                SchimbFundal(nrGreseli);
            }
            Afisare(CuvCitit);
            button24.Enabled = false;
        }

        private void button25_Click(object sender, EventArgs e) //Y
        {
            if (Apare(button25.Text) == true)
            {
                button25.BackColor = Color.Green;
            }
            else
            {
                button25.BackColor = Color.Red;
                nrGreseli++;
                SchimbFundal(nrGreseli);
            }
            Afisare(CuvCitit);
            button25.Enabled = false;
        }

        private void button26_Click(object sender, EventArgs e) //Z
        {
            if (Apare(button26.Text) == true)
            {
                button26.BackColor = Color.Green;
            }
            else
            {
                button26.BackColor = Color.Red;
                nrGreseli++;
                SchimbFundal(nrGreseli);
            }
            Afisare(CuvCitit);
            button26.Enabled = false;
        }
        #endregion
    }
}
