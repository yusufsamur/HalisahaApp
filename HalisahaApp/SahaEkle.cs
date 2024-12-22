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
    public partial class SahaEkle : Form
    {
        public SahaEkle()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            string sehir = comboBox1.SelectedItem?.ToString(); // Şehir ComboBox'tan alınır
            string ilce = textBox2.Text.Trim();
            string sahaAdi = textBox3.Text.Trim();
            int yoneticiId = DatabaseHelper.getuserID(); // Giriş yapan kullanıcı ID'si

            // Input validation
            if (string.IsNullOrWhiteSpace(sehir))
            {
                MessageBox.Show("Lütfen bir şehir seçin!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(ilce))
            {
                MessageBox.Show("Lütfen ilçe adını girin!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(sahaAdi))
            {
                MessageBox.Show("Lütfen saha adını girin!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Saha ekleme
            DatabaseHelper dbHelper = new DatabaseHelper();
            if (dbHelper.AddSaha(sehir, ilce, sahaAdi))
            {
                MessageBox.Show("Saha başarıyla eklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Formu temizle
                comboBox1.SelectedIndex = -1;
                textBox2.Clear();
                textBox3.Clear();
                loginPanel login = new loginPanel();
                login.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Saha eklenirken bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
