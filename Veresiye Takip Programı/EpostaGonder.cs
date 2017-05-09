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
using System.Net.Mail;

namespace Veresiye_Takip_Programı
{
    public partial class EpostaGonder : DevExpress.XtraEditors.XtraForm
    {
        public EpostaGonder()
        {
            InitializeComponent();
        }
        static string kaynak = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " + System.Windows.Forms.Application.StartupPath.ToString() + "\\db.mdb;";
        OleDbConnection baglanti = new OleDbConnection(kaynak);
        
        private void EpostaGonder_Load(object sender, EventArgs e)
        {
            textEdit1.Text = aliciKisi;
        }

        public string aliciKisi = "";

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void Gonder(string konu, string kime, string icerik)
        {
            try
            {
                MailMessage ePosta = new MailMessage();
                ePosta.From = new MailAddress("mailHesabi");
                //
                ePosta.To.Add(kime);
                //
                if (durum==true)
                {
                    ePosta.Attachments.Add(new Attachment(@ekDosyaYolu));
                }
                //
                ePosta.Subject = konu;
                //

                ePosta.IsBodyHtml = true;
                ePosta.Body = icerik;
                //
                SmtpClient smtp = new SmtpClient();
                //
                smtp.Credentials = new System.Net.NetworkCredential("mailHesabi","parola");
                smtp.Port = Convert.ToInt32("587");
                smtp.Host = "smtp.live.com";
                smtp.EnableSsl = true;
                object userState = ePosta;
                try
                {
                    smtp.SendAsync(ePosta, (object)ePosta);
                }
                catch (SmtpException ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message, "Mail Gönderme Hatasi");
                }
                MessageBox.Show("E-postanız " + textEdit1.Text + " mail adresine başarıyla gönderilmiştir.", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception hata)
            {
                MessageBox.Show("Hata oluştu. " + hata.Message);
            }
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            Gonder(textEdit2.Text, textEdit1.Text, richTextBox1.Text);
            gidenKutusunaKaydet();
            this.Close();
        }

        bool durum = false;
        string ekDosyaYolu = "";
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            durum = true;
            ekDosyaYolu = openFileDialog1.FileName;
            textEdit3.Text = openFileDialog1.SafeFileName;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        void gidenKutusunaKaydet()
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("INSERT INTO GidenEposta(GidenEpostaAlici,GidenEpostaKonu,GidenEpostaIcerik,GidenEpostaTarih) VALUES('" + textEdit1.Text + "','" + textEdit2.Text + "','" + richTextBox1.Text + "','" + DateTime.Now.ToShortDateString() + "')", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
        }





/// ///////////////////////////////////////////////////// Araç Çubuğu İşlemleri ///////////////////////

        
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            FontStyle old_style = richTextBox1.SelectionFont.Style;
            richTextBox1.SelectionFont = new Font(this.richTextBox1.Font, ((FontStyle)(old_style | FontStyle.Bold)));
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            FontStyle old_style = richTextBox1.SelectionFont.Style;
            richTextBox1.SelectionFont = new Font(this.richTextBox1.Font, ((FontStyle)(old_style | FontStyle.Italic)));
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            FontStyle old_style = richTextBox1.SelectionFont.Style;
            richTextBox1.SelectionFont = new Font(this.richTextBox1.Font, ((FontStyle)(old_style | FontStyle.Underline)));
        
        }
        int alignment_count=0;
        private void simpleButton8_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
            if (alignment_count > 1)
            {
                richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
                alignment_count = 0;
            }
            alignment_count++;
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
            if (alignment_count > 1)
            {
                richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
                alignment_count = 0;
            }
            alignment_count++;
        }

        private void simpleButton10_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
            if (alignment_count > 1)
            {
                richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
                alignment_count = 0;
            }
            alignment_count++;
        }

        private void simpleButton11_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            richTextBox1.SelectionColor=colorDialog1.Color;
        }

        private void simpleButton12_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            richTextBox1.SelectionBackColor = colorDialog1.Color;
        }

    }
}