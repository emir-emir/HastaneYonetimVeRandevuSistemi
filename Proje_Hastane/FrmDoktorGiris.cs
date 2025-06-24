using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using  System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Proje_Hastane
{

    public partial class FrmDoktorGiris : Form
    {

        public FrmDoktorGiris()
        {

            InitializeComponent();
     

       


        }
        sqlBaglantisi bgl = new sqlBaglantisi();

        private void BtnGirisYap_Click(object sender, EventArgs e)
        {

            SqlCommand komut = new SqlCommand("SELECT * FROM Tbl_Doktorlar WHERE DoktorTC=@p1 AND DoktorSifre=@p2", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", MskTC.Text);
            komut.Parameters.AddWithValue("@p2", TxtSifre.Text);
            SqlDataReader dr = komut.ExecuteReader();

            if (dr.Read())
            {
                MessageBox.Show("Giriş başarılı!");

                FrmDoktorDetay frm = new FrmDoktorDetay();
                frm.TCnumara = MskTC.Text;
                frm.Sifre = TxtSifre.Text;
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre hatalı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmDoktorGiris_Load(object sender, EventArgs e)
        {

        }
    }
}
