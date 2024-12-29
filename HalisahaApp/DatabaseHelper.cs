using System;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using NpgsqlTypes;

namespace HalisahaApp
{
    internal class DatabaseHelper
    {
        private string connectionString = "Host=localhost;Username=postgres;Password=qweasdx123;Database=HaliSaha";
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

                    // Prepare the command to call the stored procedure
                    string query = "CALL kullanici_ekle(@p_kullanici_adi, @p_sifre, @p_eposta, @p_tel_no, @p_uyelik_turu)";
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        // Add parameters with appropriate types
                        cmd.Parameters.AddWithValue("@p_kullanici_adi", NpgsqlDbType.Text, kullaniciAdi);
                        cmd.Parameters.AddWithValue("@p_sifre", NpgsqlDbType.Text, sifre);
                        cmd.Parameters.AddWithValue("@p_eposta", NpgsqlDbType.Text, eposta);
                        cmd.Parameters.AddWithValue("@p_tel_no", NpgsqlDbType.Text, telNo);
                        cmd.Parameters.AddWithValue("@p_uyelik_turu", NpgsqlDbType.Text, uyelikTuru);

                        // Execute the command
                        cmd.ExecuteNonQuery();

                        // Inform the user
                        MessageBox.Show("Kullanıcı başarıyla eklendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true; // Successfully added
                    }
                }
                catch (PostgresException ex)
                {
                    // Handle database-specific errors
                    string errorMessage = "";
                    errorMessage = "Veritabanı hatası: " + ex.MessageText;
                    MessageBox.Show(errorMessage, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
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
                    string query = @"SELECT rezervasyonid, uye_adi, sahaadi, saha_sehir, saha_ilce,saha_tel_no, baslangic_saati, bitis_saati, gun
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

                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "CALL add_rezervasyon(@sahaId, @uyeId, @baslangicSaati, @bitisSaati, @tarih)";
                        cmd.CommandType = CommandType.Text;

                        // Parametreleri ekliyoruz
                        cmd.Parameters.Add("@sahaId", NpgsqlDbType.Integer).Value = sahaId;
                        cmd.Parameters.Add("@uyeId", NpgsqlDbType.Integer).Value = loggedUserID;
                        cmd.Parameters.Add("@baslangicSaati", NpgsqlDbType.Time).Value = baslangicSaati;
                        cmd.Parameters.Add("@bitisSaati", NpgsqlDbType.Time).Value = bitisSaati;
                        cmd.Parameters.Add("@tarih", NpgsqlDbType.Date).Value = tarih.Date;

                        cmd.ExecuteNonQuery();

                        return true;
                    }
                }
                catch (PostgresException ex)
                {
                    MessageBox.Show(ex.MessageText); // Trigger'dan gelen hata mesajını göster
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        public bool AddSaha(string sehir, string ilce, string sahaAdi)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand("CALL saha_ekle(@p_sehir, @p_ilce, @p_sahaadi, @p_saha_yonetici_id)", conn))
                    {
                        // Parametreleri ekliyoruz
                        cmd.Parameters.Add("@p_sehir", NpgsqlDbType.Text).Value = sehir;
                        cmd.Parameters.Add("@p_ilce", NpgsqlDbType.Text).Value = ilce;
                        cmd.Parameters.Add("@p_sahaadi", NpgsqlDbType.Text).Value = sahaAdi;
                        cmd.Parameters.Add("@p_saha_yonetici_id", NpgsqlDbType.Integer).Value = loggedUserID; // loggedUserID yöneticinin ID'si

                        // Prosedürü çalıştırıyoruz
                        cmd.ExecuteNonQuery();

                        return true; // Başarıyla eklendi
                    }
                }
                catch (PostgresException ex)
                {           
                    string errorMessage = "Veritabanı hatası: " + ex.MessageText;
                    
                    MessageBox.Show(errorMessage, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }
        public bool AddKiralikOyuncuIlan(string sehir, string ilce, DateTime tarih, TimeSpan baslangicSaati, TimeSpan bitisSaati)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand("CALL kiralik_oyuncu_ilan_ekle(@p_uyeid, @p_baslangic_saati, @p_bitis_saati, @p_gun, @p_sehir, @p_ilce)", conn))
                    {
                        // Parametreleri ekliyoruz
                        cmd.Parameters.Add("@p_uyeid", NpgsqlDbType.Integer).Value = loggedUserID;
                        cmd.Parameters.Add("@p_baslangic_saati", NpgsqlDbType.Time).Value = baslangicSaati;
                        cmd.Parameters.Add("@p_bitis_saati", NpgsqlDbType.Time).Value = bitisSaati;
                        cmd.Parameters.Add("@p_gun", NpgsqlDbType.Date).Value = tarih.Date;
                        cmd.Parameters.Add("@p_sehir", NpgsqlDbType.Text).Value = sehir;
                        cmd.Parameters.Add("@p_ilce", NpgsqlDbType.Text).Value = ilce;

                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
                catch (PostgresException ex)
                {
                    // Veritabanı hatalarını yakalama
                    string errorMessage = "Veritabanı hatası: " + ex.MessageText;
                    

                    MessageBox.Show(errorMessage, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        public DataTable GetKiralikOyuncular(string sehir, string ilce, string saatAraligi)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                SELECT 
                    u.kullanici_adi as ""Kullanıcı Adı"",
                    u.tel_no as ""Telefon"",
                    u.eposta as ""E-posta"",
                    k.gun as ""Tarih""
                FROM kiralikoyuncular k
                INNER JOIN uyeler u ON k.uyeid = u.uyeid
                WHERE k.sehir = @sehir 
                AND k.ilce = @ilce
                AND k.baslangic_saati::text = @baslangicSaati";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@sehir", sehir);
                        cmd.Parameters.AddWithValue("@ilce", ilce);

                        string baslangicSaati = saatAraligi.Split('-')[0].Trim()+":00";
                        cmd.Parameters.AddWithValue("@baslangicSaati", baslangicSaati);

                        using (var adapter = new NpgsqlDataAdapter(cmd))
                        {
                            DataTable table = new DataTable();
                            adapter.Fill(table);
                            return table;
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
        public int GetSahaIdByUyeId()
        {
            int sahaid = -1; // Default return value if no saha is found

            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT sahaid FROM sahalar WHERE saha_yonetici_id = @uyeid";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@uyeid", loggedUserID);

                        var result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            sahaid = Convert.ToInt32(result);
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
        public DataTable GetSahaReservations(int sahaId)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                SELECT 
                    r.rezervasyonid as ""Rezervasyon ID"",
                    u.kullanici_adi as ""Müşteri Adı"",
                    u.tel_no as ""Telefon"",
                    r.gun as ""Tarih"",
                    r.baslangic_saati as ""Başlangıç Saati"",
                    r.bitis_saati as ""Bitiş Saati"",
                    s.sahaadi as ""Saha Adı""
                FROM rezervasyonlar r
                INNER JOIN uyeler u ON r.uyeid = u.uyeid
                INNER JOIN sahalar s ON r.sahaid = s.sahaid
                WHERE r.sahaid = @sahaId
                ORDER BY r.gun, r.baslangic_saati";

                    using (var adapter = new NpgsqlDataAdapter(query, conn))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@sahaId", sahaId);

                        DataTable reservationsTable = new DataTable();
                        adapter.Fill(reservationsTable);
                        return reservationsTable;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Rezervasyonlar getirilirken bir hata oluştu: " + ex.Message,
                                  "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
        }

        public bool DeleteReservation(int rezervasyonId)
{
    using (var conn = new NpgsqlConnection(connectionString))
    {
        try
        {
            conn.Open();
            string query = "DELETE FROM rezervasyonlar WHERE rezervasyonid = @rezervasyonId";

            using (var cmd = new NpgsqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@rezervasyonId", rezervasyonId);
                int affectedRows = cmd.ExecuteNonQuery();
                return affectedRows > 0;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Rezervasyon silinirken bir hata oluştu: " + ex.Message, 
                          "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
    }
}

        // DatabaseHelper.cs içine eklenecek metodlar:

        public class SahaBilgileri
        {
            public string SehirAdi { get; set; }
            public string IlceAdi { get; set; }
            public string SahaAdi { get; set; }
        }

        public SahaBilgileri GetSahaBilgileri()
        {
            SahaBilgileri sahaBilgileri = null;
            int sahaId = GetSahaIdByUyeId();

            if (sahaId != -1)
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string query = @"SELECT saha_sehir, saha_ilce, sahaadi 
                               FROM sahalar 
                               WHERE sahaid = @sahaId";

                        using (var cmd = new NpgsqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@sahaId", sahaId);

                            using (var reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    sahaBilgileri = new SahaBilgileri
                                    {
                                        SehirAdi = reader["saha_sehir"].ToString(),
                                        IlceAdi = reader["saha_ilce"].ToString(),
                                        SahaAdi = reader["sahaadi"].ToString()
                                    };
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Saha bilgileri getirilirken bir hata oluştu: " + ex.Message,
                            "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            return sahaBilgileri;
        }

        public bool UpdateSahaBilgileri(string sehir, string ilce, string sahaAdi)
        {
            int sahaId = GetSahaIdByUyeId();
            if (sahaId == -1) return false;

            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"UPDATE sahalar 
                           SET saha_sehir = @sehir, 
                               saha_ilce = @ilce,
                               sahaadi = @sahaAdi
                           WHERE sahaid = @sahaId";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@sehir", sehir);
                        cmd.Parameters.AddWithValue("@ilce", ilce);
                        cmd.Parameters.AddWithValue("@sahaAdi", sahaAdi);
                        cmd.Parameters.AddWithValue("@sahaId", sahaId);

                        int affectedRows = cmd.ExecuteNonQuery();
                        return affectedRows > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Saha bilgileri güncellenirken bir hata oluştu: " + ex.Message,
                        "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }
        public bool SilRezervasyon(int rezervasyonId)
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open(); // Bağlantıyı aç
                    string query = "DELETE FROM rezervasyonlar WHERE rezervasyonid = @RezervasyonID"; // Silme sorgusu

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        // Parametreyi ekleyin
                        cmd.Parameters.AddWithValue("@RezervasyonID", rezervasyonId);

                        // Sorguyu çalıştır ve etkilenen satır sayısını kontrol et
                        int rowsAffected = cmd.ExecuteNonQuery();

                        return rowsAffected > 0; // Eğer satır silindiyse, true döndür
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
                return false;
            }
        }

        public DataTable GetUyeler()
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                SELECT * FROM uyeler
                WHERE uyelik_turu = 'Oyuncu';";


                    using (var adapter = new NpgsqlDataAdapter(query, conn))
                    {
                        DataTable reservationsTable = new DataTable();
                        adapter.Fill(reservationsTable);
                        return reservationsTable;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Rezervasyonlar getirilirken bir hata oluştu: " + ex.Message,
                                  "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
        }
        public bool DeleteUser(int uyeID)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM uyeler WHERE uyeid = @uyeid";
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@uyeid", uyeID);
                        int affectedRows = cmd.ExecuteNonQuery();
                        return affectedRows > 0;
                    }
                }
                catch (Exception ex)
                {
                    // Hata durumunda işlem başarısız
                    return false;
                }
            }
        }
        public DataTable GetReservationsByUyeID(int uyeID)
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
                             WHERE uye_id = @uyeID";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        // loggedUserID parametresini ekliyoruz
                        cmd.Parameters.AddWithValue("@uyeID", uyeID);

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

        public DataTable GetSahaSahipleri()
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                SELECT 
                    u.uyeid, 
                    u.kullanici_adi, 
                    u.eposta, 
                    u.tel_no, 
                    s.sahaid, 
                    s.sahaadi
                FROM uyeler u
                INNER JOIN sahalar s ON u.uyeid = s.saha_yonetici_id";

                    using (var adapter = new NpgsqlDataAdapter(query, conn))
                    {
                        DataTable sahaSahipleriTable = new DataTable();
                        adapter.Fill(sahaSahipleriTable);
                        return sahaSahipleriTable;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Hata: " + ex.Message);
                    return null;
                }
            }
        }

    }
}
