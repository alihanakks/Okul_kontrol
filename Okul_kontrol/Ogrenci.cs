using Guna.UI2.WinForms;
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

namespace Okul_kontrol
{
    public partial class Ogrenci : Form
    {
        public Ogrenci()
        {
            InitializeComponent();
        }
        public SqlConnection conn = new SqlConnection("Data Source=alihans;Initial Catalog=Okul_Sistem;Integrated Security=True;TrustServerCertificate=True");
        public SqlCommand cmd;
        public SqlDataReader dr;
        public SqlDataAdapter adapter;
        public int s_geldi;
        public string gelentc = " ";
        private void Ogrenci_Load(object sender, EventArgs e)
        {
           

            SqlCommand cmd = new SqlCommand("select * from Ogrenci Where tc=@tc", conn);
            cmd.Parameters.AddWithValue("@tc", gelentc);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                s_geldi = Convert.ToInt32(dr["s_id"]);
            }
            conn.Close();
            
            d_prog();
            ders();

            Sifre_yenile.Visible = false;
            tabloPanel.Visible = true;
        }
        public void d_prog()
        {

            string baglantı = "select s_id as Sınıf, Ders.d_adı as Ders,gün as Gün, saat as Saat from D_programı\r\n"+
                              "inner join Ders on Ders.id=D_programı.d_id\r\nwhere s_id='" + s_geldi.ToString()+"'" ;
            conn.Open(); 
            SqlDataAdapter adapter = new SqlDataAdapter(baglantı, conn);
            DataTable table1 = new DataTable();
            adapter.Fill(table1);
            guna2DataGridView1.DataSource = table1;
            conn.Close();

        }
        public void ders()
        {
            string baglantı = "select Ders.d_adı as Ders,Personel.isim+' '+Personel.soyisim as Öğretmen_Adı from D_programı as dp\r\n"+
                              "inner join Ders on Ders.id=dp.d_id\r\n"+
                              "inner join Personel on Personel.id=Ders.p_id \r\n"+
                              "where s_id='" + s_geldi.ToString() + "'";
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(baglantı, conn);
            DataTable table1 = new DataTable();
            adapter.Fill(table1);
            guna2DataGridView2.DataSource = table1;
            conn.Close();
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            Form1 form1 = (Form1)Application.OpenForms["Form1"]; // form 1'i bulun
            form1.Close(); // form 1'i kapatın
        }//çıkış
        //şifre yenile
        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {
            Sifre_yenile.Visible = true;
            tabloPanel.Visible = false;
        }
        private void guna2Button5_Click(object sender, EventArgs e)
        {
            if (guna2TextBox21.Text == guna2TextBox22.Text)
            {
                string sorgu1 = "Update Ogrenci set sifre=@sif where tc=@tc  ";
                cmd = new SqlCommand(sorgu1, conn);
                cmd.Parameters.AddWithValue("@tc1", gelentc);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    cmd.Parameters.AddWithValue("@sif", guna2TextBox22.Text);
                    MessageBox.Show("şifre değişimi olmuştur.Yeni Şifre" + guna2TextBox22.Text + " dir");
                }
                conn.Close();
            }
            else { MessageBox.Show("Lütfen aynı şifreyi giriniz"); }
        }
    }
}
