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
    public partial class SahaAboneliği : Form
    {
        public SahaAboneliği()
        {
            InitializeComponent();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            loginPanel loginPanel = new loginPanel();
            loginPanel.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SahaRezervasyonlarım rezervasyonlarım = new SahaRezervasyonlarım();
            this.Hide();
            rezervasyonlarım.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Saha_İstatistikleri saha_İstatistikleri = new Saha_İstatistikleri();
            this.Hide();
            saha_İstatistikleri.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SahaAboneliği saha_Aboneligi = new SahaAboneliği();
            this.Hide();
            saha_Aboneligi.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SahaBilgileriniGüncelle sahaBilgileriniGüncelle = new SahaBilgileriniGüncelle();
            this.Hide();
            sahaBilgileriniGüncelle.Show();
        }
    }
}
