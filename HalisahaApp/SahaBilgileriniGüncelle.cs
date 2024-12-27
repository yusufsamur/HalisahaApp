using Npgsql;
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
    public partial class SahaBilgileriniGüncelle : Form
    {
        public SahaBilgileriniGüncelle()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void SahaBilgileriniGüncelle_Load(object sender, EventArgs e)
        {
            DatabaseHelper dbHelper = new DatabaseHelper();
            var sahaBilgileri = dbHelper.GetSahaBilgileri();

            if (sahaBilgileri != null)
            {
                // Null checking before accessing properties
                comboBox1.Text = sahaBilgileri.SehirAdi ?? string.Empty;
                textBox2.Text = sahaBilgileri.IlceAdi ?? string.Empty;
                textBox3.Text = sahaBilgileri.SahaAdi ?? string.Empty;
            }
            else
            {
                MessageBox.Show("Saha bilgileri bulunamadı!", "Uyarı",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SahaRezervasyonlarım rezervasyonlarım = new SahaRezervasyonlarım();
            this.Hide();
            rezervasyonlarım.Show();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            SahaBilgileriniGüncelle sahaBilgileriniGüncelle = new SahaBilgileriniGüncelle();
            this.Hide();
            sahaBilgileriniGüncelle.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            loginPanel loginPanel = new loginPanel();
            loginPanel.Show();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DatabaseHelper dbHelper = new DatabaseHelper();
            // Validasyon kontrolleri
            if (string.IsNullOrEmpty(comboBox1.Text) ||
                string.IsNullOrEmpty(textBox2.Text) ||
                string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz!", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Güncelleme işlemi
            bool success = dbHelper.UpdateSahaBilgileri(
                comboBox1.Text,
                textBox2.Text,
                textBox3.Text
            );

            if (success)
            {
                MessageBox.Show("Saha bilgileri başarıyla güncellendi!", "Başarılı",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Güncelleme işlemi başarısız oldu!", "Hata");
            }

        }

    }
}
