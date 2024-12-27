﻿using System;
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
    public partial class ÜyeleriGörüntüle : Form
    {
        public ÜyeleriGörüntüle()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SahaSahipleriniGörüntüle sahaSahipleriniGörüntüle = new SahaSahipleriniGörüntüle();
            sahaSahipleriniGörüntüle.Show();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ÜyeleriGörüntüle üyeleriGörüntüle = new ÜyeleriGörüntüle();
            this.Close();
            üyeleriGörüntüle.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            loginPanel loginPanel = new loginPanel();
            this.Close();
            loginPanel.Show();
        }

        private void ÜyeleriGörüntüle_Load(object sender, EventArgs e)
        {
            DatabaseHelper dbhelper = new DatabaseHelper();
            DataTable uyeler = new DataTable();
            uyeler = dbhelper.GetUyeler();

            if (uyeler != null)
            {
                dataGridView1.DataSource = uyeler; // DataGridView'e bağlanıyor
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Seçili satırdaki üye ID'sini alıyoruz
                int selectedUserID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["uyeid"].Value);

                DatabaseHelper dbHelper = new DatabaseHelper();

                // Üyeyi siliyoruz
                bool isDeleted = dbHelper.DeleteUser(selectedUserID);

                if (isDeleted)
                {
                    MessageBox.Show("Üye başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // DataGridView'ı güncelliyoruz
                    dataGridView1.DataSource = dbHelper.GetUyeler();
                }
                else
                {
                    MessageBox.Show("Üye silinirken bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Lütfen silinecek üyeyi seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0) // DataGridView'den seçili satır kontrolü
            {
                ÜyeRezervasyonlarınıGörüntüle üyeRezervasyonlarınıGörüntüle = new ÜyeRezervasyonlarınıGörüntüle(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["uyeid"].Value));
                this.Close();
                üyeRezervasyonlarınıGörüntüle.Show();
            }
            else
            {
                MessageBox.Show("Lütfen üye seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
    }
}
