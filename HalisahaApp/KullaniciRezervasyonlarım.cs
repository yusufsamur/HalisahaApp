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
    public partial class KullaniciRezervasyonlarım : Form
    {
        public KullaniciRezervasyonlarım()
        {
            InitializeComponent();
        }

        private void KullaniciRezervasyonlarım_Load(object sender, EventArgs e)
        {
            DatabaseHelper dbHelper = new DatabaseHelper();
            DataTable reservations = dbHelper.GetReservationsByLoggedUser();
            if (reservations != null)
            {
                dataGridView1.DataSource = reservations; // DataGridView'e bağlanıyor
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Rezervasyon_Ekle rezervasyon_Ekle = new Rezervasyon_Ekle();
            this.Close();
            rezervasyon_Ekle.Show();
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
