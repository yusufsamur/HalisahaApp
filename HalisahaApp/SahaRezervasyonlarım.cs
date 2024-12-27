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
    public partial class SahaRezervasyonlarım : Form
    {
        public SahaRezervasyonlarım()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
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
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen silinecek rezervasyonu seçin.",
                               "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get the rezervasyonId from the selected row
            int rezervasyonId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Rezervasyon ID"].Value);

            // Show confirmation dialog
            DialogResult result = MessageBox.Show(
                "Seçili rezervasyonu silmek istediğinize emin misiniz?",
                "Rezervasyon Silme",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                DatabaseHelper dbHelper = new DatabaseHelper();
                if (dbHelper.DeleteReservation(rezervasyonId))
                {
                    MessageBox.Show("Rezervasyon başarıyla silindi.",
                                  "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Refresh the DataGridView - assuming you're using GetSahaReservations
                    int sahaId = dbHelper.GetSahaIdByUyeId(); // or pass the sahaId directly
                    dataGridView1.DataSource = dbHelper.GetSahaReservations(sahaId);
                }
                else
                {
                    MessageBox.Show("Rezervasyon silinirken bir hata oluştu.",
                                  "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SahaRezervasyonlarım_Load(object sender, EventArgs e)
        {
            DatabaseHelper dbhelper = new DatabaseHelper();
            DataTable reservations = dbhelper.GetSahaReservations(dbhelper.GetSahaIdByUyeId());
            if (reservations != null)
            {
                dataGridView1.DataSource = reservations; // DataGridView'e bağlanıyor
            }
        }
    }
}
