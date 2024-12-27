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
    public partial class ÜyeRezervasyonlarınıGörüntüle : Form
    {
        private int uyeid;
        public ÜyeRezervasyonlarınıGörüntüle(int uyeid)
        {
            InitializeComponent();
            this.uyeid = uyeid;
        }

        private void ÜyeRezervasyonlarınıGörüntüle_Load(object sender, EventArgs e)
        {
            DatabaseHelper dbHelper = new DatabaseHelper();
            DataTable reservations = dbHelper.GetReservationsByUyeID(uyeid);
            if (reservations != null)
            {
                dataGridView1.DataSource = reservations; // DataGridView'e bağlanıyor
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ÜyeleriGörüntüle üyeleriGörüntüle = new ÜyeleriGörüntüle();
            this.Close();
            üyeleriGörüntüle.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SahaSahipleriniGörüntüle sahaSahipleriniGörüntüle = new SahaSahipleriniGörüntüle();
            sahaSahipleriniGörüntüle.Show();
            this.Close();
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
