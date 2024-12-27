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

        private void button6_Click(object sender, EventArgs e)
        {
            // Kullanıcının seçtiği rezervasyonu doğrula
            if (dataGridView1.SelectedRows.Count > 0) // DataGridView'den seçili satır kontrolü
            {
                // Seçilen rezervasyonun ID'sini al
                int rezervasyonId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["rezervasyonid"].Value);

                // Kullanıcıdan onay al
                DialogResult result = MessageBox.Show("Seçili rezervasyonu silmek istediğinizden emin misiniz?",
                                                      "Onay",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        // Veritabanı bağlantısı ve silme işlemi
                        DatabaseHelper dbHelper = new DatabaseHelper();
                        bool isDeleted = dbHelper.SilRezervasyon(rezervasyonId);

                        if (isDeleted)
                        {
                            MessageBox.Show("Rezervasyon başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // DataGridView'i güncelle
                            try
                            {
                                var rezervasyonlar = dbHelper.GetSahaBilgileri(); // Rezervasyonları veritabanından çek
                                dataGridView1.DataSource = rezervasyonlar; // DataGridView'e bağla
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Rezervasyonlar yüklenirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Rezervasyon silinemedi. Lütfen tekrar deneyin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen silmek için bir rezervasyon seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
