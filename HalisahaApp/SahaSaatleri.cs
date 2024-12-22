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
    public partial class SahaSaatleri : Form
    {
        int sahaid = -1;
        public SahaSaatleri(int sahaid)
        {
            InitializeComponent();
            this.sahaid = sahaid;
        }
        
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (sahaid == -1)
                MessageBox.Show("Saha Bulma Hatası");


            int selectedSahaId = sahaid;
            DateTime selectedDate = dateTimePicker1.Value;

            DatabaseHelper dbHelper = new DatabaseHelper();
            DataTable rezervasyonlar = dbHelper.GetRezervasyonlar(selectedSahaId, selectedDate);

            dataGridView1.DataSource = rezervasyonlar;
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
            int selectedSahaId = sahaid;
            DateTime selectedDate = dateTimePicker1.Value;
            string selectedSaatAraligi = comboBox1.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(selectedSaatAraligi))
            {
                MessageBox.Show("Lütfen bir saat aralığı seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Saat aralığını ayır ve dönüştür
            var saatler = selectedSaatAraligi.Split('-');
            TimeSpan baslangicSaati = TimeSpan.Parse(saatler[0]);
            TimeSpan bitisSaati = TimeSpan.Parse(saatler[1]);

            DatabaseHelper dbHelper = new DatabaseHelper();

            // Seçilen saat aralığında rezervasyon olup olmadığını kontrol et
            DataTable mevcutRezervasyonlar = dbHelper.GetRezervasyonlar(selectedSahaId, selectedDate);
            foreach (DataRow row in mevcutRezervasyonlar.Rows)
            {
                TimeSpan mevcutBaslangic = TimeSpan.Parse(row["baslangic_saati"].ToString());
                TimeSpan mevcutBitis = TimeSpan.Parse(row["bitis_saati"].ToString());

                if ((baslangicSaati >= mevcutBaslangic && baslangicSaati < mevcutBitis) ||
                    (bitisSaati > mevcutBaslangic && bitisSaati <= mevcutBitis))
                {
                    MessageBox.Show("Seçilen saat aralığında zaten bir rezervasyon mevcut!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Yeni rezervasyon ekle
            if (dbHelper.AddRezervasyon(selectedSahaId, selectedDate, baslangicSaati, bitisSaati))
            {
                MessageBox.Show("Rezervasyon başarıyla oluşturuldu.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dateTimePicker1_ValueChanged(null, null); // Listeyi güncelle
            }
        }
    }
}
