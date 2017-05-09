using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.OleDb;

namespace Veresiye_Takip_Programı
{
    public partial class YeniCariEkle : DevExpress.XtraEditors.XtraForm
    {
        public YeniCariEkle()
        {
            InitializeComponent();
        }
        static string kaynak = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " + System.Windows.Forms.Application.StartupPath.ToString() + "\\db.mdb;";
        OleDbConnection baglanti = new OleDbConnection(kaynak);
        
        private void YeniCariEkle_Load(object sender, EventArgs e)
        {
            textEdit9.Text = DateTime.Now.ToShortDateString();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textEdit2.Text!="" && textEdit3.Text!="" && textEdit4.Text!="")
                {
                    baglanti.Open();
                    OleDbCommand komut = new OleDbCommand("INSERT INTO CariHesap(CariAd,CariIsTel,CariCepTel,CariAdres,CariIl,CariIlce,CariEposta,CariKayitTarih,CariNot,CariBakiye) VALUES(@CariAd,@CariIsTel,@CariCepTel,@CariAdres,@CariIl,@CariIlce,@CariEposta,@CariKayitTarih,@CariNot,@CariBakiye)", baglanti);
                    komut.Parameters.AddWithValue("@CariAd", textEdit2.Text);
                    komut.Parameters.AddWithValue("@CariIsTel", textEdit3.Text);
                    komut.Parameters.AddWithValue("@CariCepTel", textEdit4.Text);
                    komut.Parameters.AddWithValue("@CariAdres", textEdit5.Text);
                    komut.Parameters.AddWithValue("@CariIl", textEdit6.Text);
                    komut.Parameters.AddWithValue("@CariIlce", textEdit7.Text);
                    komut.Parameters.AddWithValue("@CariEposta", textEdit8.Text);
                    komut.Parameters.AddWithValue("@CariKayitTarih", textEdit9.Text);
                    komut.Parameters.AddWithValue("@CariNot", richTextBox1.Text);
                    komut.Parameters.AddWithValue("@CariBakiye", 0);
                    komut.ExecuteNonQuery();
                    baglanti.Close();


                    temizle();

                    MesajKutusu m = new MesajKutusu();
                    m.labelControl1.Text = "Kayıt işlemi başarılı.";
                    m.pictureEdit2.Visible = true;
                    m.ShowDialog();
                }
                else
                {
                    MesajKutusu m = new MesajKutusu();
                    m.labelControl1.Text = "Ad-soyad ve telefon bilgileri gereklidir. Boş geçilemez.";
                    m.pictureEdit1.Visible = true;
                    m.ShowDialog();
                }
            }
            catch (Exception)
            {
                baglanti.Close();
                MesajKutusu m = new MesajKutusu();
                m.labelControl1.Text = "Hata oluştu. Lütfen tekrar deneyiniz.";
                m.pictureEdit1.Visible = true;
                m.ShowDialog();
            }
        }



        int karakterSay = 500,durum=0;
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (richTextBox1.TextLength>durum)
            {
                //karakterSay = richTextBox1.TextLength;
                karakterSay=karakterSay-1;
                labelControl10.Text = "Kalan karakter: " + karakterSay;
                durum = richTextBox1.TextLength;
            }
            else if(durum>richTextBox1.TextLength)
            {
                karakterSay += 1;
                labelControl10.Text = "Kalan karakter: " + karakterSay;
                durum = richTextBox1.TextLength;
            }
        }

        void temizle()
        {
            textEdit2.Text = "";
            textEdit3.Text = "";
            richTextBox1.Text = "";
            textEdit4.Text = "";
            textEdit5.Text = ""; 
            textEdit6.Text = "";
            textEdit7.Text = ""; 
            textEdit8.Text = "";
            textEdit9.Text = "";
            labelControl10.Text = "";
        }

    }
}