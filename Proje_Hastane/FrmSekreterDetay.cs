using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Proje_Hastane
{
    public partial class FrmSekreterDetay : Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }
        sqlBaglantisi bgl = new sqlBaglantisi();
        public string tcNumara;
        public string adSoyad;
     
        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {

            // T.C. kimlik Çekme
            LblTC.Text = tcNumara;
            


            // Ad-Soyad Çekme
            SqlCommand komut1 = new SqlCommand("select SekreterAdSoyad from Tbl_Sekreter where SekreterTC=@p1", bgl.baglanti());
            komut1.Parameters.AddWithValue("@p1", LblTC.Text);
            SqlDataReader dr = komut1.ExecuteReader();
            while (dr.Read())
            {
                LblAdSoyad.Text = dr[0].ToString();
            }

            bgl.baglanti().Close();


            // Branşları DataGridviewe aktarma
            DataTable dt1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Tbl_Branslar ", bgl.baglanti());
            da.Fill(dt1);
            dataGridView2.DataSource = dt1;

            // Doktorları DataGridviewe aktarma
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 =
                new SqlDataAdapter("Select (DoktorAd +' ' + DoktorSoyad)  as 'Doktorlar' , DoktorBrans from Tbl_Doktorlar", bgl.baglanti());
            da2.Fill(dt2);
            dataGridView3.DataSource = dt2;

            //Branş getirme

            SqlCommand komut3 = new SqlCommand("select BransAd from Tbl_Branslar", bgl.baglanti());
            komut3.Parameters.AddWithValue("@p1", CmbBrans.Text);
            SqlDataReader dr2 = komut3.ExecuteReader();
            while (dr2.Read())
            {
                CmbBrans.Items.Add(dr2[0].ToString());
            }
           bgl.baglanti().Close();


          




        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
                SqlCommand Komut2 = new SqlCommand
                ("insert into Tbl_Randevular (RandevuTarih,RandevuSaat,RandevuBrans,RandevuDoktor) values (@p1,@p2,@p3,@p4)",
                bgl.baglanti());

            Komut2.Parameters.AddWithValue("@p1", MskTarih.Text);
            Komut2.Parameters.AddWithValue("@p2", MskSaat.Text);
            Komut2.Parameters.AddWithValue("@p3", CmbBrans.Text);
            Komut2.Parameters.AddWithValue("@p4", CmbDoktor.Text);

            Komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu başarıyla kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }



        private void CmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Doktor getirme
            CmbDoktor.Items.Clear();
            SqlCommand komut4 = new SqlCommand("select DoktorAd ,DoktorSoyad from Tbl_Doktorlar where DoktorBrans=@p1", bgl.baglanti());
            komut4.Parameters.AddWithValue("@p1", CmbBrans.Text);
            SqlDataReader dr3 = komut4.ExecuteReader();
            while (dr3.Read())
            {
                CmbDoktor.Items.Add(dr3[0] + " " + dr3[1]);
            }
            bgl.baglanti().Close();
        }

        private void BtnDuyuruOlustur_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Duyurular (Duyuru) values (@p1)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", RchDuyuru.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
        }

        private void BtnDoktorPanel_Click(object sender, EventArgs e)
        {
            FrmDoktorPaneli frm = new FrmDoktorPaneli();
            frm.ShowDialog();
        }

        private void BtnBransPanel_Click(object sender, EventArgs e)
        {
            FrmBrans frmBrans = new FrmBrans();
            frmBrans.ShowDialog();
        }

        private void BtnRandevuListesi_Click(object sender, EventArgs e)
        {
            FrmRandevuListesi frmRandevuListesi = new FrmRandevuListesi();
            frmRandevuListesi.Show();
         
        }

        private void BtnDuyurular_Click(object sender, EventArgs e)
        {
            FrmDuyurular frm = new FrmDuyurular();
            frm.ShowDialog();   
        }
    }
    }
 
