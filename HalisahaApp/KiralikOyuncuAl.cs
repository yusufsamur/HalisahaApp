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
    public partial class KiralikOyuncuAl : Form
    {
        public KiralikOyuncuAl()
        {
            InitializeComponent();
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
            string selectedSehir = comboBox1.SelectedItem?.ToString();
            string selectedIlce = comboBox2.SelectedItem?.ToString();
            string selectedSaatAraligi = comboBox3.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(selectedSehir) || string.IsNullOrEmpty(selectedIlce) || string.IsNullOrEmpty(selectedSaatAraligi))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DatabaseHelper dbHelper = new DatabaseHelper();
            DataTable kiralikOyuncular = dbHelper.GetKiralikOyuncular(selectedSehir, selectedIlce, selectedSaatAraligi);

            if (kiralikOyuncular != null && kiralikOyuncular.Rows.Count > 0)
            {
                dataGridView1.DataSource = kiralikOyuncular;
                dataGridView1.AutoResizeColumns();
            }
            else
            {
                MessageBox.Show("Seçilen kriterlere uygun kiralık oyuncu bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedSehir = comboBox1.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedSehir)) return;

            DatabaseHelper dbHelper = new DatabaseHelper();
            List<string> ilceler = dbHelper.IlceleriGetir(selectedSehir);

            comboBox2.Items.Clear();

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
                comboBox3.Enabled = true;
            }
            string selectedSaatAraligi = comboBox3.SelectedItem?.ToString();
        }
    }
}
