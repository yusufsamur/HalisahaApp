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
            
            DatabaseHelper dbHelper = new DatabaseHelper();

            if (dbHelper.KullaniciDogrula(kullaniciAdi, sifre))
            {
                string uyelikTuru = dbHelper.GetUyelikTuru(kullaniciAdi, sifre);
                dbHelper.setUserID(kullaniciAdi,sifre);

                if (uyelikTuru == "Oyuncu")
                {
                    MessageBox.Show("Giriş başarılı! Oyuncu sayfasına yönlendiriliyorsunuz.");
                    KullaniciAnasayfa oyuncuAnasayfa = new KullaniciAnasayfa();
                    oyuncuAnasayfa.Show();
                }
                else if (uyelikTuru == "Saha Yoneticisi")
                {
                    MessageBox.Show("Giriş başarılı! Saha yöneticisi sayfasına yönlendiriliyorsunuz.");
                    SahaSahibiAnasayfa sahaAnasayfa = new SahaSahibiAnasayfa();
                    sahaAnasayfa.Show();
                }
                else if(uyelikTuru == "admin") // admin ancak database üzerinden eklenebilecek
                {
                    MessageBox.Show("Giriş başarılı! Admin sayfasına yönlendiriliyorsunuz.");
                    AdminPanel adminPanel = new AdminPanel();
                    adminPanel.Show();
                }
                else
                {
                    MessageBox.Show("Üyelik türü bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                this.Hide(); // Giriş ekranını gizle
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre hatalı.", "Giriş Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loginPanel_Load(object sender, EventArgs e)
        {

        }

        private void kayitolbtn_Click(object sender, EventArgs e)
        {
            KayıtOl kayitsayfa = new KayıtOl(this);
            kayitsayfa.Show();
            this.Hide();
        }
    }
}
