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
            comboBox2.Enabled = false;
            comboBox3.Enabled = false;
            button5.Enabled = false;
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
            DatabaseHelper dbHelper = new DatabaseHelper();
            int sahaid = dbHelper.GetSahaID(comboBox1.SelectedItem?.ToString(), comboBox2.SelectedItem?.ToString(), comboBox3.SelectedItem?.ToString());
            SahaSaatleri sahaSaatleri = new SahaSaatleri(sahaid);
            this.Close();
            sahaSaatleri.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedSehir = comboBox1.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedSehir)) return;

            DatabaseHelper dbHelper = new DatabaseHelper();
            List<string> ilceler = dbHelper.IlceleriGetir(selectedSehir);

            comboBox2.Items.Clear();
            comboBox3.Items.Clear();

            if (ilceler.Count == 0)
            {
                MessageBox.Show("Bu şehirde ilçe bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comboBox2.Enabled = false;
                comboBox3.Enabled = false;
            }
            else
            {
                comboBox2.Items.AddRange(ilceler.ToArray());
                comboBox2.Enabled = true;
                comboBox3.Enabled = false;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedIlce = comboBox2.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedIlce)) return;

            DatabaseHelper dbHelper = new DatabaseHelper();
            List<string> sahalar = dbHelper.SahalariGetir(selectedIlce);

            comboBox3.Items.Clear();

            if (sahalar.Count == 0)
            {
                MessageBox.Show("Bu ilçede saha bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comboBox3.Enabled = false;
            }
            else
            {
                comboBox3.Items.AddRange(sahalar.ToArray());
                comboBox3.Enabled = true;
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            button5.Enabled = comboBox3.SelectedItem != null;
        }
    }
}
