using System;
using Npgsql;
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
    public partial class loginPanel : Form
    {
        public loginPanel()
        {
            InitializeComponent();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_MouseEnter(object sender, EventArgs e)
        {
            
        }

        private void girisyapbtn_MouseLeave(object sender, EventArgs e)
        {
            girisyapbtn.BackColor = Color.ForestGreen;
        }

        private void kayitolbtn_MouseEnter(object sender, EventArgs e)
        {
            kayitolbtn.BackColor = Color.DarkGreen;

        }

        private void girisyapbtn_MouseEnter(object sender, EventArgs e)
        {
            girisyapbtn.BackColor = Color.DarkGreen;
        }

        private void kayitolbtn_MouseLeave(object sender, EventArgs e)
        {
            girisyapbtn.BackColor = Color.ForestGreen;
        }

        private void girisyapbtn_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void girisyapbtn_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = textBox1.Text;
            string sifre = textBox2.Text;

            string connString = "Host=localhost;Username=postgres;Password=qweasdx123;Database=HaliSaha";

            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                try
                {
                    conn.Open();

                    // Kullanıcıyı kontrol eden sorgu
                    string query = "SELECT COUNT(*) FROM uyeler WHERE kullanici_adi = @kullaniciAdi AND sifre = @sifre";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@kullaniciAdi", kullaniciAdi);
                        cmd.Parameters.AddWithValue("@sifre", sifre);

                        int userCount = Convert.ToInt32(cmd.ExecuteScalar());

                        if (userCount > 0)
                        {
                            MessageBox.Show("Giriş başarılı!");
                            // Ana sayfayı göster
                            KullaniciAnayasayfa anasayfa = new KullaniciAnayasayfa();
                            anasayfa.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Kullanıcı adı veya şifre hatalı.", "Giriş Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message);
                }
            }
        }

        private void loginPanel_Load(object sender, EventArgs e)
        {
            NpgsqlConnection conn = new NpgsqlConnection("server=localHost; port=5432 ; Database=HaliSaha; " +
                "user ID=postgres; password=1234");

        }
    }
}
