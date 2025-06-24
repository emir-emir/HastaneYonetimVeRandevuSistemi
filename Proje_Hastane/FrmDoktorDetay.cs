using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Proje_Hastane
{
    public partial class FrmDoktorDetay : Form
    {
        public string TCnumara;
        public string Sifre;
        public FrmDoktorDetay()
        {
            InitializeComponent();
            // Formun sabit kalmasını sağlar
            this.AutoScaleMode = AutoScaleMode.None;
            this.AutoSize = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Form boyutu sabit
            this.Size = new Size(1361, 844); // kendi ölçünü gir
           
        }

        private void FrmDoktorDetay_Load(object sender, EventArgs e)
        {
            // Doktor TC Kimlik Çekme
            LblTC.Text = TCnumara;

            // Doktor Ad Soyad Çekme
            sqlBaglantisi bgl = new sqlBaglantisi();
              
            SqlCommand komut = new SqlCommand("Select DoktorAd,DoktorSoyad from Tbl_Doktorlar where DoktorTC=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", LblTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblAdSoyad.Text = dr[0] + " " + dr[1];
            }

            bgl.baglanti().Close();

            // Randevu Geçmişi
            DataTable data = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Tbl_Randevular where RandevuDoktor=@R1",  bgl.baglanti());
            da.SelectCommand.Parameters.AddWithValue("@R1", LblAdSoyad.Text);
            da.Fill(data);
            dataGridView1.DataSource = data;
             
        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            FrmDoktorBilgiDüzenle frm = new FrmDoktorBilgiDüzenle();
            frm.Ad = LblAdSoyad.Text.Split(' ')[0]; // Ad
            frm.Soyad = LblAdSoyad.Text.Split(' ')[1]; // Soyad
            frm.TCnumara = LblTC.Text; // TC Kimlik Numarası

            frm.Show();
        }

        private void BtnDuyurular_Click(object sender, EventArgs e)
        {
            FrmDuyurular frm = new FrmDuyurular();
            frm.Show();
        }

        private void BtnCıkıs_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Uygulamadan çıkış yapar
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            RchSikayet.Text = dataGridView1.Rows[secilen].Cells[7].Value.ToString(); // Randevu şikayetini alır

        }
    }
}
