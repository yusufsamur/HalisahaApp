using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace HalisahaApp
{
    public partial class KayıtOl : Form
    {
        private loginPanel parentForm; // Form1 referansı

        public KayıtOl(loginPanel form1)
        {
            InitializeComponent();
            parentForm = form1; // Gelen Form1 referansını sakla
        }
        private bool EpostaGecerliMi(string eposta)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$"; // Basit e-posta regex deseni
            return Regex.IsMatch(eposta, pattern);
        }
        private bool TelefonGecerliMi(string telNo)
        {
            return telNo.Length == 10 && telNo.All(char.IsDigit); // 10 haneli ve sadece rakam olmalı
        }

        public KayıtOl()
        {
            InitializeComponent();
        }

        private void kayitolbtn_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = textBox1.Text;
            string sifre = textBox2.Text;
            string eposta = textBox4.Text;
            string telNo = textBox3.Text;
            string uyelikTuru = comboBox1.SelectedItem?.ToString(); // Seçilen üyelik türü

            if (string.IsNullOrWhiteSpace(kullaniciAdi) || string.IsNullOrWhiteSpace(sifre) || string.IsNullOrWhiteSpace(eposta) || string.IsNullOrWhiteSpace(telNo))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!EpostaGecerliMi(eposta))
            {
                MessageBox.Show("Geçerli bir e-posta adresi girin!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!TelefonGecerliMi(telNo))
            {
                MessageBox.Show("Geçerli bir telefon numarası girin (10 rakam)!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Lütfen üyelik türünü seçin!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(uyelikTuru))
            {
                MessageBox.Show("Lütfen üyelik türünü seçin!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DatabaseHelper dbHelper = new DatabaseHelper();

            if (dbHelper.KullaniciEkle(kullaniciAdi, sifre, eposta, telNo, uyelikTuru))
            {
                if(uyelikTuru=="Saha Yoneticisi")
                {
                    SahaEkle sahaekle = new SahaEkle();
                    dbHelper.setUserID(kullaniciAdi,sifre);
                    sahaekle.Show();
                    this.Close();

                }
                else
                {
                    MessageBox.Show("Kayıt başarılı! Giriş ekranına yönlendiriliyorsunuz.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    parentForm.Show(); // Form1'i geri göster
                    this.Close(); // KayıtOl formunu kapat
                }

            }
            else
            {
                MessageBox.Show("Kayıt sırasında bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void geributon_Click(object sender, EventArgs e)
        {
            parentForm.Show(); 
            this.Close();
        }
    }
}
