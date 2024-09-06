using Google.Protobuf.WellKnownTypes;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;
using System.Windows.Forms;
using TheArtOfDevHtmlRenderer.Adapters;

namespace Okul_kontrol
{
    public partial class Personel : Form
    {
        public Personel()
        {
            InitializeComponent();
        }
        //güncelleme if çalışmadı kontrol et
        public SqlConnection conn = new SqlConnection("Data Source=alihans;Initial Catalog=Okul_Sistem;Integrated Security=True;TrustServerCertificate=True");
        public SqlCommand cmd;
        public SqlDataReader dr;
        public SqlDataAdapter adapter;
        public int D_prog_id;//D_prog id sini kullanmak için veri secmse
        public int Sınıf_id;// sınıf id sini kullanmak için veri secmse
        public int Rütbe_id;// rütbe id sini kullanmak için veri 
        Form1 form = (Form1)Application.OpenForms["Form1"]; // form 1'i bulun


        private void Personel_Load(object sender, EventArgs e)
        {

            Personel_veri.Visible = false;
            Ogrenci_veri.Visible = false;
            DersP_veri.Visible = false;
            Ders_veri.Visible = false;
            Sınıf_veri.Visible = false;
            Rütbe_veri.Visible = false;
            Sifre_yenile.Visible = false;
            
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e) // kapatma
        {
            form.Close(); // form 1'i kapatın
        }
        private void guna2Button6_Click(object sender, EventArgs e)//Çıkış
        {
            form.Show();
            this.Hide();            
        }

        /// fonskiyonlar/// 
            //Tablo doldurma
        public void personel()
        {
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(" select isim as İsim ,soyisim as Soyisim , Rutbe.rütbe as Rütbe, tc as TC,alan as Alan,iletisim as İletşim ,sifre as Şifre from Personel" + " " +
                                                        "inner join Rutbe on Rutbe.id=Personel.rütbe_id ", conn);
            DataTable table1 = new DataTable();
            adapter.Fill(table1);
            guna2DataGridView1.DataSource = table1;
            conn.Close();  
        }
        public void ogrenci()
        {
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("select isim as İSim ,soyisim as Soyisim ,veli_ismi as Veli,veli_iletisim as Veli_Tel,tc AS TC , Sınıf.sınıf as Sınıf, adress as Adress ,sifre as Şifre from Ogrenci" + " " +
                                                        "inner join Sınıf on Sınıf.id=Ogrenci.s_id ", conn);
            DataTable table1 = new DataTable();
            adapter.Fill(table1);
            guna2DataGridView1.DataSource = table1;
            conn.Close();
        }
        public void ders()
        {
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("select d_adı as Ders_Kodu,Personel.isim +' '+Personel.soyisim as personel from Ders" + " " +
                                                        "inner join Personel on Personel.id=Ders.p_id", conn);
            DataTable table1 = new DataTable();
            adapter.Fill(table1);
            guna2DataGridView1.DataSource = table1;
            conn.Close();
        }
        public void dprog()
        {
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("select D_programı.id as İD, Ders.d_adı as Ders_Kodu ,gün as Gün,saat as Saatler,Sınıf.sınıf as Sınıf  from  D_programı " +
                                                        " inner join Sınıf on Sınıf.id=D_programı.s_id" +
                                                        " inner join Ders on Ders.id=D_programı.d_id ", conn);
            DataTable table1 = new DataTable();
            adapter.Fill(table1);
            guna2DataGridView1.DataSource = table1;
            conn.Close();
        }
        public void rütbe()
        {

            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from Rutbe  ", conn);
            DataTable table1 = new DataTable();
            adapter.Fill(table1);
            guna2DataGridView1.DataSource = table1;
            conn.Close();
        }
        public void sınıf()
        {
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from Sınıf  ", conn);
            DataTable table1 = new DataTable();
            adapter.Fill(table1);
            guna2DataGridView1.DataSource = table1;
            conn.Close();
        }
        //tablo son

        //combobox doldurma
        public void personelAdd()
        {
            //Ders combo box icin
            if (guna2ComboBox4.Items.Count != personelS())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select isim+' '+soyisim+' '+ alan as isimsoy from Personel where rütbe_id<=3", conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    guna2ComboBox4.Items.Add(dr["isimsoy"]);
                }
                conn.Close();
            }
        }
        public void dersAdd()
        {
            //dersP
            if (guna2ComboBox6.Items.Count != dersS())
            {
               
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from Ders", conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    guna2ComboBox6.Items.Add(dr["d_adı"]);
                }
                conn.Close();
            }
        }
        public void rütbeAdd()
        {
            if (guna2ComboBox1.Items.Count !=Convert.ToInt32( rütbeS()))
            { 
                //personel
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from Rutbe", conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    guna2ComboBox1.Items.Add(dr["rütbe"]);
                }
                conn.Close();
                
            }
        }
        public void sınıfAdd(string num)
        {
            if (num == "1")
            {//öğrenci combo box
                if (guna2ComboBox2.Items.Count != sınıfS())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select * from Sınıf", conn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        guna2ComboBox2.Items.Add(dr["sınıf"]);


                    }
                    conn.Close();
                }
            }//ögrenci
            else if (num == "2")
            {//dersP combo box icin
                if (guna2ComboBox5.Items.Count != sınıfS())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select * from Sınıf", conn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        guna2ComboBox5.Items.Add(dr["sınıf"]);

                    }
                    conn.Close();
                }
            }//dersP

        }
        //combox son

        //veri sayısı örenme
        public int personelS()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select Count(id) as sa from Personel where rütbe_id<=3", conn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string top = dr["sa"].ToString();
                conn.Close();
                
                return Convert.ToInt32(top);
                
            }
            else
            {
                conn.Close();
                return 0;
            }
        }
        public int dersS()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select Count(id) as sa from Ders", conn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string top = dr["sa"].ToString();
                conn.Close();
                return Convert.ToInt32(top);
            }
            else
            {
                conn.Close();
                return 0;
            }
        }
        public int rütbeS()//rütbe tablosununda veri sayı öğrenme
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select Count(id) as sa from Rutbe", conn);
            SqlDataReader dr = cmd.ExecuteReader();
            if(dr.Read())
            {
                string top = dr["sa"].ToString();
                conn.Close();
                return Convert.ToInt32(top);
            }else 
            {
                conn.Close();
                return 0;
            }
            

        }
        public int sınıfS()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select Count(id) as sa from Sınıf", conn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string top = dr["sa"].ToString();
                conn.Close();
                return Convert.ToInt32(top);
            }
            else
            {
                conn.Close();
                return 0;
            }

        }
            //veri öğrenme son

        ///Fonksiyonalr Son///


        ///panel kontrolu
        private void guna2Button1_Click(object sender, EventArgs e)//personel
        {
            personel();
            
            Personel_veri.Visible = true;

            Ogrenci_veri.Visible = false;
            DersP_veri.Visible = false;
            Ders_veri.Visible = false; 
            Sınıf_veri.Visible = false;
            Rütbe_veri.Visible = false;
            Sifre_yenile.Visible = false;
            rütbeAdd();
        }
        private void guna2Button2_Click(object sender, EventArgs e)//Ogrenci
        {
            ogrenci();
            Ogrenci_veri.Visible = true;

            Personel_veri.Visible = false;
            
            DersP_veri.Visible = false;
            Ders_veri.Visible = false;
            Sınıf_veri.Visible = false;
            Rütbe_veri.Visible = false;
            Sifre_yenile.Visible = false;
            sınıfAdd("1");
        }
        private void guna2Button3_Click(object sender, EventArgs e)//Ders
        {
            ders();
            Ders_veri.Visible = true;

            Personel_veri.Visible = false;
            Ogrenci_veri.Visible = false;

            DersP_veri.Visible = false;
            Sınıf_veri.Visible = false;
            Rütbe_veri.Visible=false;
            Sifre_yenile.Visible = false;
            personelAdd();
        }
        private void guna2Button4_Click(object sender, EventArgs e)//DersP
        {
            dprog();
            DersP_veri.Visible = true;

            Personel_veri.Visible = false;
            Ogrenci_veri.Visible = false;
            Ders_veri.Visible = false;

            Sınıf_veri.Visible = false;
            Rütbe_veri.Visible= false;
            Sifre_yenile.Visible = false;

            sınıfAdd("2");
                dersAdd();
            

        }
        private void guna2ComboBox7_SelectedIndexChanged(object sender, EventArgs e)// sınıf Rutbe
        {
            if (guna2ComboBox7.SelectedIndex == 0)
            {
                sınıf();
                Sınıf_veri.Visible = true;

                Personel_veri.Visible = false;
                Ogrenci_veri.Visible = false;
                DersP_veri.Visible = false;
                Ders_veri.Visible = false;

                Rütbe_veri.Visible=false;
                Sifre_yenile.Visible = false;
            }
        else if (guna2ComboBox7.SelectedIndex == 1)
            {

                rütbe();
                Rütbe_veri.Visible = true;
                
                Personel_veri.Visible = false;
                Ogrenci_veri.Visible = false;
                DersP_veri.Visible = false;
                Ders_veri.Visible = false;
                Sınıf_veri.Visible = false;
                Sifre_yenile.Visible = false;
            }
        }
        /// panel kontrolu son

        ///veri seçme ///
        public void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Personel_veri.Visible == true)
            {
                guna2TextBox1.Text = guna2DataGridView1.CurrentRow.Cells[0].Value.ToString();
                guna2TextBox2.Text = guna2DataGridView1.CurrentRow.Cells[1].Value.ToString();
                guna2TextBox3.Text = guna2DataGridView1.CurrentRow.Cells[3].Value.ToString();
                guna2TextBox4.Text = guna2DataGridView1.CurrentRow.Cells[4].Value.ToString();
                guna2TextBox5.Text = guna2DataGridView1.CurrentRow.Cells[5].Value.ToString();
                guna2TextBox6.Text = guna2DataGridView1.CurrentRow.Cells[6].Value.ToString();


            }
            else if (Ogrenci_veri.Visible == true)
            {
                guna2TextBox7.Text = guna2DataGridView1.CurrentRow.Cells[0].Value.ToString();
                guna2TextBox8.Text = guna2DataGridView1.CurrentRow.Cells[1].Value.ToString();
                guna2TextBox9.Text = guna2DataGridView1.CurrentRow.Cells[2].Value.ToString();
                guna2TextBox10.Text = guna2DataGridView1.CurrentRow.Cells[3].Value.ToString();
                guna2TextBox11.Text = guna2DataGridView1.CurrentRow.Cells[4].Value.ToString();
                guna2TextBox12.Text = guna2DataGridView1.CurrentRow.Cells[6].Value.ToString();
                guna2TextBox13.Text = guna2DataGridView1.CurrentRow.Cells[7].Value.ToString();



            }
            else if (DersP_veri.Visible == true)
            {
                D_prog_id =Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells[0].Value.ToString());
                guna2TextBox18.Text = guna2DataGridView1.CurrentRow.Cells[2].Value.ToString();
                guna2TextBox19.Text = guna2DataGridView1.CurrentRow.Cells[3].Value.ToString();


            }
            else if (Ders_veri.Visible == true)
            {
                guna2TextBox16.Text = guna2DataGridView1.CurrentRow.Cells[0].Value.ToString();

            }
            else if (Sınıf_veri.Visible == true)
            {
                Sınıf_id =Convert.ToInt32( guna2DataGridView1.CurrentRow.Cells[0].Value.ToString());
                guna2TextBox17.Text = guna2DataGridView1.CurrentRow.Cells[1].Value.ToString();
            }
            else if (Rütbe_veri.Visible == true)
            {
                Rütbe_id =Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells[0].Value.ToString());
                guna2TextBox20.Text = guna2DataGridView1.CurrentRow.Cells[1].Value.ToString();
            }
        }
        ///veri Seçme///
        
        /// Kayıt Butonu      
        private void guna2Button7_Click(object sender, EventArgs e)
        { 
            if (Personel_veri.Visible==true)
            {
                string sorgu1 = "SELECT * FROM Personel where  tc=@tc1 ";
                cmd = new SqlCommand(sorgu1, conn);
                cmd.Parameters.AddWithValue("@tc1", guna2TextBox3.Text);
                conn.Open();//-------
                SqlDataReader dr1 = cmd.ExecuteReader();
                if (dr1.Read()) //değer kontrolu varsa kayıt işlemi olmuyor
                {
                    conn.Close();//------
                    MessageBox.Show( "Tc vardır lütfen kontrol ediniz");
                   
                }
                else //yoksa yeni kayıt olarak ekleniryor
                {
                    conn.Close();//------

                    string sorgu = "INSERT INTO Personel (isim,soyisim,rütbe_id,tc,alan,sifre,iletisim)" +
                                   " VALUES (@is, @soy, @rü, @Tc,@al,@sif,@ilet)";

                    cmd = new SqlCommand(sorgu, conn);
                    cmd.Parameters.AddWithValue("@is", guna2TextBox1.Text);
                    cmd.Parameters.AddWithValue("@soy", guna2TextBox2.Text);
                    cmd.Parameters.AddWithValue("@Tc", guna2TextBox3.Text);
                    cmd.Parameters.AddWithValue("@al", guna2TextBox4.Text);
                    cmd.Parameters.AddWithValue("@ilet", guna2TextBox5.Text);
                    cmd.Parameters.AddWithValue("@sif", guna2TextBox6.Text);

                    conn.Open();
                    SqlCommand cmd1 = new SqlCommand("select * from Rutbe", conn);
                    SqlDataReader dr = cmd1.ExecuteReader();
                    
                        while (dr.Read())
                        {
                            if (dr["rütbe"].ToString() == guna2ComboBox1.SelectedItem.ToString()) 
                            {
                            
                            cmd.Parameters.AddWithValue("@rü", dr["id"]);
                            }
                        }
                    
                    conn.Close();

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    personel();
                    MessageBox.Show("Kayıt başarılı");
                }//personel kayıt ekle

            }//personel Kayıt
            else if(Ogrenci_veri.Visible == true)
            {
                string sorgu1 = "SELECT * FROM Ogrenci where tc=@otc1 ";
                cmd = new SqlCommand(sorgu1, conn);
                cmd.Parameters.AddWithValue("@otc1", guna2TextBox10.Text);
                conn.Open();//-------
                SqlDataReader dr1 = cmd.ExecuteReader();
                if (dr1.Read()) //değer kontolu varsa kayıt işlemi olmuyor
                {
                    conn.Close();//------
                    MessageBox.Show("Tc vardır lütfen kontrol ediniz");

                }
                else
                {
                    conn.Close();
                    string sorgu = "insert into Ogrenci (isim,soyisim,veli_ismi,tc,s_id,veli_iletisim,sifre,adress ) "
                                   + "values (@ois,@osoy,@vel,@tc,@sid,@veltel,@osif,@adr)";
                    cmd = new SqlCommand(sorgu, conn);
                    cmd.Parameters.AddWithValue("@ois", guna2TextBox7.Text);
                    cmd.Parameters.AddWithValue("@osoy", guna2TextBox8.Text);
                    cmd.Parameters.AddWithValue("@vel", guna2TextBox9.Text);
                    cmd.Parameters.AddWithValue("@tc", guna2TextBox11.Text);
                    cmd.Parameters.AddWithValue("@veltel", guna2TextBox10.Text);
                    cmd.Parameters.AddWithValue("@osif", guna2TextBox13.Text);
                    cmd.Parameters.AddWithValue("@adr", guna2TextBox12.Text);
                   
                    conn.Open();
                    SqlCommand cmd1 = new SqlCommand("select * from Sınıf", conn);
                    SqlDataReader dr = cmd1.ExecuteReader();
                    while (dr.Read())
                    {
                        if (dr["sınıf"].ToString() == guna2ComboBox2.SelectedItem.ToString())
                        {
                            cmd.Parameters.AddWithValue("@sid", dr["id"]);
                        }
                    }
                    conn.Close();

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    ogrenci();
                }

            }//ögrenci
            else if (DersP_veri.Visible==true)
            {
                string sorgu = "insert into D_programı (s_id,d_id,gün,saat)\r\n values (@sid1,@did,@gun,@saat)";
                cmd = new SqlCommand(sorgu, conn);
                cmd.Parameters.AddWithValue("@gun", guna2TextBox18.Text);
                cmd.Parameters.AddWithValue("@saat", guna2TextBox19.Text);

                
                conn.Open();
                SqlCommand cmd2 = new SqlCommand("select * from Sınıf", conn);
                SqlDataReader dr1 = cmd2.ExecuteReader();
                
                    while (dr1.Read())
                    {
                        if (dr1["sınıf"].ToString() == guna2ComboBox5.SelectedItem.ToString())
                        {
                            cmd.Parameters.AddWithValue("@sid1", dr1["id"]);
                        }
                    }
                
                conn.Close();

                int top = dersS();
                conn.Open();
                SqlCommand cmd1 = new SqlCommand("select * from Ders", conn);
                SqlDataReader dr = cmd1.ExecuteReader();
               
                    while (dr.Read())
                    {
                        if (dr["d_adı"].ToString() == guna2ComboBox6.SelectedItem.ToString())
                        {
                            cmd.Parameters.AddWithValue("@did", dr["id"]);
                        }
                    }
                
                conn.Close();
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                dprog();


            }//dersP
            else if (Ders_veri.Visible==true)///ders
            {

                 string sorgu1 = "SELECT * FROM Ders where  d_adı=@dadı ";
                cmd = new SqlCommand(sorgu1, conn);
                cmd.Parameters.AddWithValue("@dadı", guna2TextBox16.Text);
                conn.Open();//-------
                SqlDataReader dr1 = cmd.ExecuteReader();
                if (dr1.Read()) //değer kontrolu varsa kayıt işlemi olmuyor
                {
                    conn.Close();//------
                    MessageBox.Show("ders vardır vardır lütfen kontrol ediniz");

                }
                else //yoksa yeni kayıt olarak ekleniryor
                {
                    conn.Close();//------

                    string sorgu = "insert into Ders (d_adı,p_id) values (@d_ad,@pid)";
                    cmd = new SqlCommand(sorgu, conn);

                    cmd.Parameters.AddWithValue("@d_ad", guna2TextBox16.Text);

                    conn.Open();
                    SqlCommand cmd1 = new SqlCommand("select  isim+' '+soyisim+' '+ alan as isimsoy ,id from Personel", conn);
                    SqlDataReader dr = cmd1.ExecuteReader();
                    while (dr.Read())
                    {

                        if (dr["isimsoy"].ToString() == guna2ComboBox4.SelectedItem.ToString())
                        {

                            cmd.Parameters.AddWithValue("@pid", dr["id"]);
                        }

                    }
                    conn.Close();

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    ders();
                }
            }//ders
            else if (Sınıf_veri.Visible==true)
            {
                string sorgu = "insert into Sınıf (sınıf) values (@sın)";
                cmd = new SqlCommand(sorgu, conn);

                cmd.Parameters.AddWithValue("@sın", guna2TextBox17.Text);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                sınıf();
            }//sınıf
            else if (Rütbe_veri.Visible==true)
            {
                string sorgu = "insert into Rutbe (rütbe) values (@rüt)";
                cmd = new SqlCommand(sorgu, conn);

                cmd.Parameters.AddWithValue("@rüt", guna2TextBox20.Text);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                rütbe();
            }//rütbe
        }
        ///kayıt butanu son

        //guncelle
        private void guna2Button8_Click(object sender, EventArgs e)
        {
            if (Personel_veri.Visible == true)
            {
                if (guna2ComboBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("lütfen rütbe seçin");
                }
                else
                {
                    string sorgu = "Update Personel set isim=@is ,soyisim=@soy,rütbe_id=@rü_id,alan=@al,sifre=@sif" +
                                            ",iletisim=@ilet where tc=@tc";
                    cmd = new SqlCommand(sorgu, conn);
                    cmd.Parameters.AddWithValue("@is", guna2TextBox1.Text);
                    cmd.Parameters.AddWithValue("@soy", guna2TextBox2.Text);
                    cmd.Parameters.AddWithValue("@tc", guna2TextBox3.Text);
                    cmd.Parameters.AddWithValue("@al", guna2TextBox4.Text);
                    cmd.Parameters.AddWithValue("@ilet", guna2TextBox5.Text);
                    cmd.Parameters.AddWithValue("@sif", guna2TextBox6.Text);
                    conn.Open();
                    SqlCommand cmd1 = new SqlCommand("select * from Rutbe", conn);
                    SqlDataReader dr = cmd1.ExecuteReader();
                    while (dr.Read())
                    {
                        if (dr["rütbe"].ToString() == guna2ComboBox1.SelectedItem.ToString())
                        {

                            cmd.Parameters.AddWithValue("@rü_id", dr["id"]);
                        }
                    }
                    conn.Close();
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    personel();
                    MessageBox.Show("güncelleme başarılı");
                }
            }
            else if (Ogrenci_veri.Visible == true)
            {
                if (guna2ComboBox2.SelectedIndex == -1)
                {
                    MessageBox.Show("lütfen sınıf seçiniz");
                }
                else
                {
                    string sorgu = "Update Ogrenci set isim=@is,soyisim=@soy,veli_ismi=@vel,tc=@tc,s_id=@sid,adress=@adr,sifre=@sif,veli_iletisim=@veltel"+
                                    " where tc = @tc";
                    cmd = new SqlCommand(sorgu, conn);
                    cmd.Parameters.AddWithValue("@is", guna2TextBox7.Text);
                    cmd.Parameters.AddWithValue("@soy", guna2TextBox8.Text);
                    cmd.Parameters.AddWithValue("@vel", guna2TextBox9.Text);
                    cmd.Parameters.AddWithValue("@tc", guna2TextBox11.Text);
                    cmd.Parameters.AddWithValue("@veltel", guna2TextBox10.Text);
                    cmd.Parameters.AddWithValue("@sif", guna2TextBox13.Text);
                    cmd.Parameters.AddWithValue("@adr", guna2TextBox12.Text);

                    conn.Open();
                    SqlCommand cmd1 = new SqlCommand("select * from Sınıf", conn);
                    SqlDataReader dr = cmd1.ExecuteReader();
                    while (dr.Read())
                    {
                        if (dr["sınıf"].ToString() == guna2ComboBox2.SelectedItem.ToString())
                        {
                            cmd.Parameters.AddWithValue("@sid", dr["id"]);
                        }
                    }
                    conn.Close();

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    ogrenci();
                }
            }
            else if (DersP_veri.Visible == true)
            {
                string sorgu = "update D_programı Set s_id=@sid,d_id=@did,gün=@gun,saat=@saat where id=@id";
                cmd = new SqlCommand(sorgu, conn);
                cmd.Parameters.AddWithValue("@id", D_prog_id);
                cmd.Parameters.AddWithValue("@gun", guna2TextBox18.Text);
                cmd.Parameters.AddWithValue("@saat", guna2TextBox19.Text);


                conn.Open();
                SqlCommand cmd2 = new SqlCommand("select * from Sınıf", conn);
                SqlDataReader dr1 = cmd2.ExecuteReader();

                while (dr1.Read())
                {
                    if (dr1["sınıf"].ToString() == guna2ComboBox5.SelectedItem.ToString())
                    {
                        cmd.Parameters.AddWithValue("@sid", dr1["id"]);
                    }
                }

                conn.Close();

                int top = dersS();
                conn.Open();
                SqlCommand cmd1 = new SqlCommand("select * from Ders", conn);
                SqlDataReader dr = cmd1.ExecuteReader();

                while (dr.Read())
                {
                    if (dr["d_adı"].ToString() == guna2ComboBox6.SelectedItem.ToString())
                    {
                        cmd.Parameters.AddWithValue("@did", dr["id"]);
                    }
                }


                conn.Close();
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                dprog();
            }
            else if (Ders_veri.Visible == true)
            {
                string sorgu = "Update Ders set d_adı=@d_ad,p_id=@pid where d_adı=@d_ad";
                cmd = new SqlCommand(sorgu, conn);

                cmd.Parameters.AddWithValue("@d_ad", guna2TextBox16.Text);       
                
                SqlCommand cmd1 = new SqlCommand("select  isim+' '+soyisim+' '+ alan as isimsoy ,id from Personel", conn);
                conn.Open();
                SqlDataReader dr = cmd1.ExecuteReader();
                while (dr.Read())
                {
                    if (dr["isimsoy"].ToString()  == guna2ComboBox4.SelectedItem.ToString())
                    {
                        cmd.Parameters.AddWithValue("@pid", dr["id"]);
                    }
                }
                conn.Close();

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                ders();
            }
            else if (Sınıf_veri.Visible == true)
            {
                string sorgu = "update Sınıf set sınıf=@sın where id=@id ";
                cmd = new SqlCommand(sorgu, conn);


                
                cmd.Parameters.AddWithValue("@sın", guna2TextBox17.Text);
                cmd.Parameters.AddWithValue("@id", Sınıf_id);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                sınıf();
            }
            else if (Rütbe_veri.Visible == true)
            {
                string sorgu = "Update Rutbe set rütbe=@rüt where id=@id";
                cmd = new SqlCommand(sorgu, conn);

                cmd.Parameters.AddWithValue("@rüt", guna2TextBox20.Text);
                cmd.Parameters.AddWithValue("@id", Rütbe_id);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                rütbe();
               
            }
        }
        //guncelle Son

        //sil
        private void guna2Button9_Click(object sender, EventArgs e)
        {
            if (Personel_veri.Visible == true) 
            {
                DialogResult result1 = MessageBox.Show("Silmek İstediğinize eminmisiniz ?" +
                    "\r\nBu personelin bağlı olduğu diğer tablolardan da tüm veriler de silinecektir",
                                                "Sil", MessageBoxButtons.YesNo);
                if (result1 == DialogResult.Yes)
                {
                    string sorgu = "delete from Personel where tc=@tc";
                    cmd = new SqlCommand(sorgu, conn);

                    cmd.Parameters.AddWithValue("@tc", guna2TextBox3.Text);
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    conn.Close();
                    personel();
                }
                else
                {
                    MessageBox.Show("silme iptal edildi");
                }
               
            }
            else if (Ogrenci_veri.Visible == true)
            {
                DialogResult result1 = MessageBox.Show("Silmek İstediğinize eminmisiniz ?" +
                    "\r\nBu Öğrencinin bağlı olduğu diğer tablolardan da tüm veriler de silinecektir",
                                                "Sil", MessageBoxButtons.YesNo);
                if (result1 == DialogResult.Yes)
                {
                    string sorgu = "DELETE from Ogrenci where tc=@tc";
                    cmd = new SqlCommand(sorgu, conn);

                    cmd.Parameters.AddWithValue("@tc", guna2TextBox11.Text);
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    conn.Close();
                    ogrenci();
                }
                else
                {
                    MessageBox.Show("silme iptal edildi");
                }
            }
            else if (DersP_veri.Visible == true)
            {
                DialogResult result1 = MessageBox.Show("Silmek İstediğinize eminmisiniz ?" ,
                                                "Sil", MessageBoxButtons.YesNo);
                if (result1 == DialogResult.Yes)
                {
                    string sorgu = "delete from D_programı where id=@id";
                    cmd = new SqlCommand(sorgu, conn);

                    cmd.Parameters.AddWithValue("@id", D_prog_id);
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    conn.Close();
                    dprog();
                }
                else
                {
                    MessageBox.Show("silme iptal edildi");
                }
            }
            else if (Ders_veri.Visible == true)
            {
                DialogResult result1 = MessageBox.Show("Silmek İstediğinize eminmisiniz ?" +
                    "\r\nBu dersin bağlı olduğu diğer tablolardan da tüm veriler de silinecektir",
                                                "Sil", MessageBoxButtons.YesNo);
                if (result1 == DialogResult.Yes)
                {
                    string sorgu = "delete from Ders where d_adı=@d_ad";
                    cmd = new SqlCommand(sorgu, conn);

                    cmd.Parameters.AddWithValue("@d_ad", guna2TextBox16.Text);
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    conn.Close();
                    ders();
                }
                else
                {
                    MessageBox.Show("silme iptal edildi");
                }
            }
            else if (Sınıf_veri.Visible == true)
            {
                DialogResult result1 = MessageBox.Show("Silmek İstediğinize eminmisiniz ?" +
                    "\r\nBu Sınıfın bağlı olduğu diğer tablolardan da tüm veriler de silinecektir",
                                                "Sil", MessageBoxButtons.YesNo);
                if (result1 == DialogResult.Yes)
                {
                    string sorgu = "delete from Sınıf where id=@id";
                    cmd = new SqlCommand(sorgu, conn);

                    cmd.Parameters.AddWithValue("@id", Sınıf_id);
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    conn.Close();
                    sınıf();
                }
                else
                {
                    MessageBox.Show("silme iptal edildi");
                }
            }
            else if (Rütbe_veri.Visible == true) 
            {
                DialogResult result1 = MessageBox.Show("Silmek İstediğinize eminmisiniz ?" +
                    "\r\nBu Rütbe'nin bağlı olduğu diğer tablolardan da tüm veriler de silinecektir",
                                                "Sil", MessageBoxButtons.YesNo);
                if (result1 == DialogResult.Yes)
                {
                    string sorgu = "delete from Rutbe where id=@id";
                    cmd = new SqlCommand(sorgu, conn);

                    cmd.Parameters.AddWithValue("@id", Rütbe_id);
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    conn.Close();
                    rütbe();
                   
                }
                else
                {
                    MessageBox.Show("silme iptal edildi");
                }
            }
        }
        //sil son
        //sifre yenile
        public string gelentc = " ";
        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {
          Sifre_yenile.Visible = true;
        }
        private void guna2Button5_Click(object sender, EventArgs e)
        {
            if (guna2TextBox21.Text == guna2TextBox22.Text)
            {
                string sorgu1 = "Update Personel set sifre=@sif where tc=@tc  ";
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
        //sifre yenile son
    }
}
