using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proje_Hastane
{
    public partial class FrmDoktorBilgiDüzenle : Form
    {
        public FrmDoktorBilgiDüzenle()
        {
            InitializeComponent();
        }
        public string TCnumara;
        public string Sifre;
        public string Ad;
        public string Soyad;
        sqlBaglantisi bgl = new sqlBaglantisi();
        private void FrmDoktorBilgiDüzenle_Load(object sender, EventArgs e)
        {
            // Form elemanlarına değer ata
        
            MskTC.Text = TCnumara;

            // Bağlantıyı bir değişkene al (2 kez çağırmamak için)
         

            // Parametreli sorgu yaz
            SqlCommand komut = new SqlCommand(
                "SELECT * FROM Tbl_Doktorlar WHERE DoktorTC = @tc", bgl.baglanti());

            // TC parametresini ekle
            komut.Parameters.AddWithValue("@tc", MskTC.Text.Trim());

            // Sorguyu çalıştır
            SqlDataReader dr = komut.ExecuteReader();

            if (dr.Read())
            {
                // Doktor branşını al ve combobox'a ata
                CmbBrans.Items.Clear(); // Önceki değerleri temizle
                TxtAd.Text = dr["DoktorAd"].ToString();
                TxtSoyad.Text = dr["DoktorSoyad"].ToString();
                MskTC.Text = dr["DoktorTC"].ToString();
                TxtSifre.Text = dr["DoktorSifre"].ToString();
                // CmbBrans combobox'ına branşı ekle
                CmbBrans.Text = dr["DoktorBrans"].ToString();
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre hatalı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            bgl.baglanti().Close();



        }

        private void BtnBilgiGuncelle_Click(object sender, EventArgs e)
        {

            // Bilgileri güncelleme işlemi
            SqlCommand komut = new SqlCommand("UPDATE Tbl_Doktorlar SET DoktorAd=@p1, DoktorSoyad=@p2, DoktorBrans=@p3, DoktorSifre=@p4 WHERE DoktorTC=@p5", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", CmbBrans.Text);
            komut.Parameters.AddWithValue("@p4", TxtSifre.Text);
            komut.Parameters.AddWithValue("@p5", MskTC.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Bilgiler güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
