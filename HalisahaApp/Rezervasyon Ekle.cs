using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HalisahaApp
{
    public partial class Rezervasyon_Ekle : Form
    {
        public Rezervasyon_Ekle()
        {
            InitializeComponent();
        }

        private void Rezervasyon_Ekle_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            KullaniciRezervasyonlarım rezervasyonlarim = new KullaniciRezervasyonlarım();
            this.Close();
            rezervasyonlarim.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            KiralıkOyuncuIlan kiralıkOyuncuIlan = new KiralıkOyuncuIlan();
            this.Close();
            kiralıkOyuncuIlan.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            KiralikOyuncuAl kiralikOyuncuAl = new KiralikOyuncuAl();
            this.Close();
            kiralikOyuncuAl.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            loginPanel loginPanel = new loginPanel();
            this.Close();
            loginPanel.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SahaSaatleri sahaSaatleri = new SahaSaatleri();
            this.Close();
            sahaSaatleri.Show();
        }
    }
}
