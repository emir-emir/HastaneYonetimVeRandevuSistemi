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
    public partial class FrmBrans : Form
    {
        public FrmBrans()
        {
            InitializeComponent();
        }
        sqlBaglantisi bgl = new sqlBaglantisi();
        private void FrmBrans_Load(object sender, EventArgs e)
        {
           
            DataTable data = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from  Tbl_Branslar",bgl.baglanti());
            sqlDataAdapter.Fill(data);
            dataGridView1.DataSource = data;
            bgl.baglanti().Close();


        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand komut1 = new SqlCommand("insert into Tbl_Branslar (BransAd) values (@p1) ",bgl.baglanti());
            komut1.Parameters.AddWithValue("@p1", TxBransAd.Text);
            komut1.ExecuteNonQuery();
            bgl.baglanti().Close();

            SqlCommand komut2 = new SqlCommand("select * from Tbl_Branslar", bgl.baglanti());
            DataTable data = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(komut2);
            dataAdapter.Fill(data);
           dataGridView1.DataSource = data;
            MessageBox.Show("Branş başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
      int data = dataGridView1.SelectedCells[0].RowIndex;
            TxBransAd.Text = dataGridView1.Rows[data].Cells[1].Value.ToString();
            Txtİd.Text = dataGridView1.Rows[data].Cells[0].Value.ToString();

        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut3 = new SqlCommand("delete from Tbl_Branslar where Bransid=@p1", bgl.baglanti());
            komut3.Parameters.AddWithValue("@p1", Txtİd.Text);
            komut3.ExecuteNonQuery();
            bgl.baglanti().Close();

            SqlCommand komut4 = new SqlCommand("select * from Tbl_Branslar", bgl.baglanti());
            DataTable data = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(komut4);
            dataAdapter.Fill(data);
            dataGridView1.DataSource = data;
            MessageBox.Show("Branş başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut5 = new SqlCommand("update Tbl_Branslar set BransAd=@p1 where Bransid=@p2", bgl.baglanti());
            komut5.Parameters.AddWithValue("@p1", TxBransAd.Text);
            komut5.Parameters.AddWithValue("@p2", Txtİd.Text);
            komut5.ExecuteNonQuery();
            bgl.baglanti().Close();

            SqlCommand komut6 = new SqlCommand("select * from Tbl_Branslar", bgl.baglanti());
            DataTable data = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(komut6);
            dataAdapter.Fill(data); 
            dataGridView1.DataSource = data;
            MessageBox.Show("Branş başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
