using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Proje_Hastane
{
    public partial class FrmHastaDetay : Form
    {
        public FrmHastaDetay()
        {
            InitializeComponent();
        }
        public string TCnumara;
        public string Sifre;

        sqlBaglantisi bgl = new sqlBaglantisi();
        private void FrmHastaDetay_Load(object sender, EventArgs e)
        {
            // Tc kimlik çekme
            LblTC.Text = TCnumara;
            // Ad Soyad Çekme

            SqlCommand komut = new SqlCommand("Select HastaAd,HastaSoyad  from Tbl_Hastalar where HastaTC=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", LblTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {

                LblAdSoyad.Text = dr[0] + " " + dr[1];
            }

            bgl.baglanti().Close();

            // Randevu Geçmişi

            DataTable data = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select  * from Tbl_Randevular  where HastaTC=" + TCnumara, bgl.baglanti());
            da.Fill(data);
            dataGridView1.DataSource = data;

            // Branşları çekme
            SqlCommand komut2 = new SqlCommand("Select BransAd from Tbl_Branslar ", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                CmbBrans.Items.Add(dr2[0]);
            }
            bgl.baglanti().Close();

        }


        private void CmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {

            // Doktor Çekme
            CmbDoktor.Items.Clear();
            SqlCommand komut3 = new SqlCommand("select  DoktorAd,DoktorSoyAd from Tbl_Doktorlar where DoktorBrans=@p1", bgl.baglanti());
            komut3.Parameters.AddWithValue("@p1", CmbBrans.Text);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read()) { 
            CmbDoktor.Items.Add(dr3[0] + "  " + dr3[1]);
            }
            bgl.baglanti().Close();
        }

        private void CmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
        
            string doktor = CmbDoktor.Text.Trim().Replace("  ", " ");
            string brans = CmbBrans.Text.Trim();

            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT * FROM Tbl_Randevular WHERE RandevuBrans = @brans AND RandevuDoktor = @doktor AND RandevuDurum = 0",
                bgl.baglanti());

            da.SelectCommand.Parameters.AddWithValue("@brans", brans);
            da.SelectCommand.Parameters.AddWithValue("@doktor", doktor);

            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;




        }

        private void LnkBilgiDüzenle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmHastaBilgiDüzenle fr = new FrmHastaBilgiDüzenle();
            fr.TcNo = LblTC.Text;
            fr.Show();
         
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            Txtid.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();


        }

        private void BtnRandevuAl_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update Tbl_Randevular set RandevuDurum=1, HastaTC=@p1,HastaSikayet=@p2 where Randevuid=@p3", bgl.baglanti());
           komut.Parameters.AddWithValue("@p1", LblTC.Text);
            komut.Parameters.AddWithValue("@p2", RchSikayet.Text);
            komut.Parameters.AddWithValue("@p3", Txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevunuz Alınmıştır.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
