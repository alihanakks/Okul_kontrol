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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
       

        SqlConnection conn = new SqlConnection("Data Source=alihans;Initial Catalog=Okul_Sistem;Integrated Security=True;TrustServerCertificate=True");
        SqlCommand cmd;                                                     
        SqlDataReader dr;
        public static string KullaniciAdi;
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (long.TryParse(guna2TextBox1.Text, out long sayi))
            {
                string sorgu = "SELECT * FROM Ogrenci where sifre=@pass and tc=@tc ";
                cmd = new SqlCommand(sorgu, conn);
                cmd.Parameters.AddWithValue("@tc", guna2TextBox1.Text);
                cmd.Parameters.AddWithValue("@pass", guna2TextBox2.Text);
                conn.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    MessageBox.Show("öğrenci girişi. ");
                    Ogrenci OG = new Ogrenci();
                    OG.gelentc = guna2TextBox1.Text;
                    OG.ShowDialog();
                    OG.Show();
                    this.Hide();


                }
                else
                {

                    conn.Close();
                    string sorgu1 = "SELECT * FROM Personel where  sifre=@pass and tc=@tc ";
                    cmd = new SqlCommand(sorgu1, conn);
                    cmd.Parameters.AddWithValue("@tc", guna2TextBox1.Text);
                    cmd.Parameters.AddWithValue("@pass", guna2TextBox2.Text);
                    conn.Open();
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {

                        MessageBox.Show("personel girişi ");
                        Personel PE = new Personel();
                        PE.gelentc = guna2TextBox1.Text;
                        PE.ShowDialog();
                        PE.Show();
                        this.Hide();



                    }
                    else
                    {
                        MessageBox.Show("Kullanıcı adını ve şifrenizi kontrol ediniz.");
                    }
                }
                conn.Close();

            }
            else MessageBox.Show("lütfen girdilerinizi kontrol ediniz");
        }
    }
}

