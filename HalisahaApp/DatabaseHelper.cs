using System;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HalisahaApp
{
    internal class DatabaseHelper
    {
        private string connectionString = "Host=localhost;Username=postgres;Password=1234;Database=HaliSaha";

        public bool KullaniciDogrula(string kullaniciAdi, string sifre)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM uyeler WHERE kullanici_adi = @kullaniciAdi AND sifre = @sifre";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@kullaniciAdi", kullaniciAdi);
                        cmd.Parameters.AddWithValue("@sifre", sifre);

                        int userCount = Convert.ToInt32(cmd.ExecuteScalar());
                        return userCount > 0;
                    }
                }
                catch (Exception)
                {
                    // Hata durumunda false döndürülür.
                    return false;
                }
            }


        }


        public bool KullaniciEkle(string kullaniciAdi, string sifre, string eposta, string telNo,string uyelikTuru)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO uyeler (kullanici_adi, sifre, eposta, tel_no, uyelik_turu) VALUES (@kullaniciAdi, @sifre, @eposta, @telNo, @uyelikTuru)";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@kullaniciAdi", kullaniciAdi);
                        cmd.Parameters.AddWithValue("@sifre", sifre);
                        cmd.Parameters.AddWithValue("@eposta", eposta);
                        cmd.Parameters.AddWithValue("@telNo", telNo);
                        cmd.Parameters.AddWithValue("@uyelikTuru", uyelikTuru);

                        cmd.ExecuteNonQuery();
                        return true; // Başarıyla eklendi
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

        }



    }
}
