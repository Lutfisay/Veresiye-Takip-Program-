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
using System.IO;

namespace Veresiye_Takip_Programı
{
    public partial class Ayarlar : DevExpress.XtraEditors.XtraForm
    {
        public Ayarlar()
        {
            InitializeComponent();
        }
        static string kaynak = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " + System.Windows.Forms.Application.StartupPath.ToString() + "\\db.mdb;";
        OleDbConnection baglanti = new OleDbConnection(kaynak);
        
        private void Ayarlar_Load(object sender, EventArgs e)
        {
            ayarBilgileriniGetir();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (textEditSifre.Text==textEditSifreTekrar.Text)
            {
                baglanti.Open();
                OleDbCommand komut = new OleDbCommand("UPDATE Ayarlar SET FirmaAd=@FirmaAd,FirmaAd2=@FirmaAd2,YetkiliKisi=@YetkiliKisi,IsTel=@IsTel,CepTel=@CepTel,Fax=@Fax,Adres=@Adres,Eposta=@Eposta,VergiNo=@VergiNo,Aciklama=@Aciklama,KullaniciAd=@KullaniciAd,KullaniciSifre=@KullaniciSifre,KullaniciEposta=@KullaniciEposta,GuvenlikSoru=@GuvenlikSoru,GuvenlikCevap=@GuvenlikCevap,EpostaAdres=@EpostaAdres,EpostaSifre=@EpostaSifre,SunucuAd=@SunucuAd,SunucuHost=@SunucuHost,SunucuPort=@SunucuPort,SunucuSSL=@SunucuSSL", baglanti);
                komut.Parameters.AddWithValue("@FirmaAd", textEditFirmaAd.Text);
                komut.Parameters.AddWithValue("@FirmaAd2", textEditFirmaAd2.Text);
                komut.Parameters.AddWithValue("@YetkiliKisi", textEditYetkiliKisi.Text);
                komut.Parameters.AddWithValue("@IsTel", textEditIsYeriTel.Text);
                komut.Parameters.AddWithValue("@CepTel", textEditCepTel.Text);
                komut.Parameters.AddWithValue("@Fax", textEditFax.Text);
                komut.Parameters.AddWithValue("@Adres", textEditAdres.Text);
                komut.Parameters.AddWithValue("@Eposta", textEditFirmaEposta.Text);
                komut.Parameters.AddWithValue("@VergiNo", textEditVergiNo.Text);
                komut.Parameters.AddWithValue("@Aciklama", textEditFirmaAciklama.Text);
                komut.Parameters.AddWithValue("@KullaniciAd", textEditKullaniciAd.Text);
                komut.Parameters.AddWithValue("@KullaniciSifre", textEditSifre.Text);
                komut.Parameters.AddWithValue("@KullaniciEposta", textEditEposta.Text);
                komut.Parameters.AddWithValue("@GuvenlikSoru", textEditGuvenlikSorusu.Text);
                komut.Parameters.AddWithValue("@GuvenlikCevap", textEditCevap.Text);
                komut.Parameters.AddWithValue("@EpostaAdres", textEdit1.Text);
                komut.Parameters.AddWithValue("@EpostaSifre", textEdit2.Text);
                komut.Parameters.AddWithValue("@SunucuAd", comboBoxSunucu.Text);
                komut.Parameters.AddWithValue("@SunucuHost", textEditHost.Text);
                komut.Parameters.AddWithValue("@SunucuPort", textEditPort.Text);
                komut.Parameters.AddWithValue("@SunucuSSL", textEditSSL.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();

                MesajKutusu m = new MesajKutusu();
                m.labelControl1.Text = "Ayar güncelleme işlemi başarılı.";
                m.pictureEdit2.Visible = true;
                m.ShowDialog();
                
            }
            else
            {
                baglanti.Close();

                MesajKutusu m = new MesajKutusu();
                m.labelControl1.Text = "Şifreler aynı değil veya boş!";
                m.pictureEdit1.Visible = true;
                m.ShowDialog();
            }
        }


        //Eposta combobox a göre sunucu getirme..
        private void textEdit4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSunucu.Text == comboBoxSunucu.Properties.Items[0].ToString())
            {
                textEditHost.Text = "smtp.gmail.com";
                textEditPort.Text = "465";
                textEditSSL.Text = "true";
                textEditHost.Enabled = false;
                textEditPort.Enabled = false;
                textEditSSL.Enabled = false;
            }
            else if (comboBoxSunucu.Text == comboBoxSunucu.Properties.Items[1].ToString())
            {
                textEditHost.Text = "smtp.live.com";
                textEditPort.Text = "587";
                textEditSSL.Text = "true";
                textEditHost.Enabled = false;
                textEditPort.Enabled = false;
                textEditSSL.Enabled = false;
            }
            else if (comboBoxSunucu.Text == comboBoxSunucu.Properties.Items[2].ToString())
            {
                textEditHost.Text = "smtp.mail.yahoo.com";
                textEditPort.Text = "587";
                textEditSSL.Text = "true";
                textEditHost.Enabled = false;
                textEditPort.Enabled = false;
                textEditSSL.Enabled = false;
            }
            else if (comboBoxSunucu.Text == comboBoxSunucu.Properties.Items[3].ToString())
            {
                textEditHost.Text = "pop.mynet.com";
                textEditPort.Text = "587";
                textEditSSL.Text = "true";
                textEditHost.Enabled = false;
                textEditPort.Enabled = false;
                textEditSSL.Enabled = false;
            }
            else if (comboBoxSunucu.Text == comboBoxSunucu.Properties.Items[4].ToString())
            {
                textEditHost.Text = "";
                textEditPort.Text = "";
                textEditSSL.Text = "";
                textEditHost.Enabled = true;
                textEditPort.Enabled = true;
                textEditSSL.Enabled = true;
            }
        }
        //Eposta combobox a göre sunucu getirme..



        //Form açılısında tüm bilgilerin listelenmesi...
        void ayarBilgileriniGetir()
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("Select * From Ayarlar", baglanti);
            OleDbDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                textEditFirmaAd.Text = oku["FirmaAd"].ToString();
                textEditFirmaAd2.Text = oku["FirmaAd2"].ToString();
                textEditYetkiliKisi.Text = oku["YetkiliKisi"].ToString();
                textEditIsYeriTel.Text = oku["IsTel"].ToString();
                textEditCepTel.Text = oku["CepTel"].ToString();
                textEditFax.Text = oku["Fax"].ToString();
                textEditAdres.Text = oku["Adres"].ToString();
                textEditFirmaEposta.Text = oku["Eposta"].ToString();
                textEditVergiNo.Text = oku["VergiNo"].ToString();
                textEditFirmaAciklama.Text = oku["Aciklama"].ToString();
                textEditKullaniciAd.Text = oku["KullaniciAd"].ToString();
                textEditSifre.Text = oku["KullaniciSifre"].ToString();
                textEditEposta.Text = oku["KullaniciEposta"].ToString();
                textEditGuvenlikSorusu.Text = oku["GuvenlikSoru"].ToString();
                textEditCevap.Text = oku["GuvenlikCevap"].ToString();
                textEdit1.Text = oku["EpostaAdres"].ToString();
                textEdit2.Text = oku["EpostaSifre"].ToString();
                comboBoxSunucu.Text = oku["SunucuAd"].ToString();
                textEditHost.Text = oku["SunucuHost"].ToString();
                textEditPort.Text = oku["SunucuPort"].ToString();
                textEditSSL.Text = oku["SunucuSSL"].ToString();
            }
            baglanti.Close();

        }
        //Form açılısında tüm bilgilerin listelenmesi...

        /////////////////// YEDEKLEME İŞLEMİ //////////////////////////


        string filenameMusteriler = Directory.GetParent(Application.ExecutablePath).ToString() + "\\veresiyeTakip.mdf", kayityeri1;
        string filenameMusteriler2 = Directory.GetParent(Application.ExecutablePath).ToString() + "\\veresiyeTakip.mdf", kayityeri2;
        

        string adres = DateTime.Now.ToShortDateString();

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                bool x = true;

                Directory.CreateDirectory(@"D:\Program\Yedek\" + adres);
                kayityeri1 = "D:\\Program\\Yedek\\" + adres + "\\Veritabani_yedek_" + adres + ".emre";
                try
                {
                    System.IO.File.Copy(filenameMusteriler, kayityeri1);
                }
                catch (Exception hata)
                {
                    MessageBox.Show("Bugünkü yedekleme işlemini yapmışsınız. Bir günde birden fazla yedekleme işlemi yapmak sabit diskinizde birikmeye yol açar.       " + hata.Message, "Hata ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    x = false;
                }
                if (x)
                {
                    timer1.Enabled = true;
                    progressPanel1.Visible = true;
                    simpleButton3.Enabled = false;
                }
            }

            else if (radioButton2.Checked == true)
            {
                bool x = true;
                try
                {
                    System.IO.File.Copy(filenameMusteriler2, kayityeri2);
                    labelControlKonum.Text = "";
                }
                catch (Exception hata)
                {
                    MessageBox.Show("Hata Oluştu : " + hata.Message, "Hata ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    x = false;
                }
                if (x)
                {
                    timer1.Enabled = true;
                    progressPanel1.Visible = true;
                    simpleButton3.Enabled = false;
                }
            }

        }

        int tick = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            tick++;

            if (tick == 5)
            {
                timer1.Stop();
                progressPanel1.Visible = false;
                simpleButton3.Enabled = true;
                tick = 0;
                MessageBox.Show("Yedekleme başarılı.", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                simpleButton4.Enabled = false;
            }
            else if (radioButton2.Checked == true)
            {
                simpleButton4.Enabled = true;
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            labelControlKonum.Visible = true;
            labelControlKonum.Text = "Konum: " + saveFileDialog1.FileName;
            kayityeri2 = saveFileDialog1.FileName + ".emre";
        }



        //// GERİ YÜKLEME KODLARI BAŞLIYOR...

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                simpleButton5.Enabled = true;
                simpleButton6.Enabled = true;
            }
            else if (checkBox1.Checked == false)
            {
                simpleButton5.Enabled = false;
                simpleButton6.Enabled = false;
            }
        }

        string filenameMusteriler3 = "";
        string kayityeri3 = "";

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Dosya adını giriniz veya dosya seçiniz.";
            openFileDialog1.FileName = "Dosyayı belirtiniz!";
            openFileDialog1.ShowDialog();
            labelControlKonum2.Visible = true;
            labelControlKonum2.Text = openFileDialog1.FileName;
            filenameMusteriler3 = openFileDialog1.FileName;

        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            bool x = true;
            try
            {
                System.IO.File.Delete(Application.StartupPath.ToString() + "\\veresiyeTakip.mdf");

                kayityeri3 = Application.StartupPath.ToString() + "\\veresiyeTakip.mdf";

                System.IO.File.Copy(filenameMusteriler3, kayityeri3);
                labelControlKonum2.Text = "";
            }
            catch (Exception hata)
            {
                MessageBox.Show("Hata Oluştu : " + hata.Message, "Hata ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                x = false;
            }
            if (x)
            {
                timer2.Enabled = true;
                progressPanel2.Visible = true;
                simpleButton6.Enabled = false;
                simpleButton5.Enabled = false;
                checkBox1.Checked = false;
            }
        }

        int tick2 = 0;
        private void timer2_Tick(object sender, EventArgs e)
        {
            tick2++;
            if (tick2 == 5)
            {
                timer2.Stop();
                progressPanel2.Visible = false;
                simpleButton2.Enabled = true;
                tick2 = 0;
                MessageBox.Show("Geri yükleme işlemi başarılı.", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }



    }
}