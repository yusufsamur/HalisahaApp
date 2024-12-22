using System;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace HalisahaApp
{
    internal class DatabaseHelper
    {
        private string connectionString = "Host=localhost;Username=postgres;Password=1234;Database=HaliSaha";
        private static int loggedUserID;//neseye özgü değil sınıfa özgü olduğu için sabit kalıyor program içinde sayfalar değiştikçe değişmiyor

        public void resetuserID() {
            loggedUserID = 0;
        }
        public static int getuserID()
        {

        return loggedUserID; }

        public void setUserID(string kullaniciAdi, string sifre)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT uyeid FROM uyeler WHERE kullanici_adi = @kullaniciAdi AND sifre = @sifre";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@kullaniciAdi", kullaniciAdi);
                        cmd.Parameters.AddWithValue("@sifre", sifre);

                        loggedUserID = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

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

        public string GetUyelikTuru(string kullaniciAdi, string sifre)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT uyelik_turu FROM uyeler WHERE kullanici_adi = @kullaniciAdi AND sifre = @sifre";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@kullaniciAdi", kullaniciAdi);
                        cmd.Parameters.AddWithValue("@sifre", sifre);

                        var result = cmd.ExecuteScalar();
                        return result?.ToString(); // Üyelik türünü döndür
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
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

        public DataTable GetReservationsByLoggedUser()
        {
            // loggedUserID'nin sıfır olmaması gerektiğini kontrol ediyoruz
            if (loggedUserID == 0)
            {
                MessageBox.Show("Kullanıcı giriş yapmadı veya geçersiz kullanıcı ID'si!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Sorgu
                    string query = @"SELECT rezervasyonid, uye_adi, sahaadi, saha_sehir, saha_ilce, baslangic_saati, bitis_saati, gun
                             FROM reservation_view
                             WHERE uye_id = @loggedUserID";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        // loggedUserID parametresini ekliyoruz
                        cmd.Parameters.AddWithValue("@loggedUserID", loggedUserID);

                        using (var adapter = new NpgsqlDataAdapter(cmd))
                        {
                            // Sonuçları doldurmak için bir DataTable oluşturuyoruz
                            DataTable table = new DataTable();
                            adapter.Fill(table);
                            return table; // DataTable döndürülüyor
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
        }

        public List<string> IlceleriGetir(string sehir)
        {
            List<string> ilceler = new List<string>();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT DISTINCT saha_ilce FROM sahalar WHERE saha_sehir = @sehir";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@sehir", sehir);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ilceler.Add(reader.GetString(0));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return ilceler;
        }

        public int GetSahaID(string sehir, string ilce, string sahaadi)
        {
            int sahaid = -1; // Varsayılan olarak -1 döndürülür, böylece hata durumunda ayırt edilebilir.

            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT sahaid FROM sahalar WHERE saha_ilce = @ilce AND saha_sehir = @sehir AND sahaadi = @sahaadi";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@sehir", sehir);
                        cmd.Parameters.AddWithValue("@ilce", ilce);
                        cmd.Parameters.AddWithValue("@sahaadi", sahaadi);

                        var result = cmd.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int id))
                        {
                            sahaid = id;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return sahaid;
        }

        public List<string> SahalariGetir(string ilce)
        {
            List<string> sahalar = new List<string>();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT sahaadi FROM sahalar WHERE saha_ilce = @ilce";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ilce", ilce);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                sahalar.Add(reader.GetString(0));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return sahalar;
        }

        public DataTable GetRezervasyonlar(int sahaId, DateTime tarih)
        {
            DataTable rezervasyonlar = new DataTable();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                SELECT baslangic_saati, bitis_saati 
                FROM rezervasyonlar 
                WHERE sahaid = @sahaId AND gun = @tarih";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@sahaId", sahaId);
                        cmd.Parameters.AddWithValue("@tarih", tarih);
                        using (var reader = cmd.ExecuteReader())
                        {
                            rezervasyonlar.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return rezervasyonlar;
        }

        public bool AddRezervasyon(int sahaId, DateTime tarih, TimeSpan baslangicSaati, TimeSpan bitisSaati)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                INSERT INTO rezervasyonlar (sahaid, uyeid, baslangic_saati, bitis_saati, gun)
                VALUES (@sahaId, @uyeId, @baslangicSaati, @bitisSaati, @tarih)";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@sahaId", sahaId);
                        cmd.Parameters.AddWithValue("@uyeId", loggedUserID);
                        cmd.Parameters.AddWithValue("@baslangicSaati", baslangicSaati);
                        cmd.Parameters.AddWithValue("@bitisSaati", bitisSaati);
                        cmd.Parameters.AddWithValue("@tarih", tarih);

                        cmd.ExecuteNonQuery();
                        return true;
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
