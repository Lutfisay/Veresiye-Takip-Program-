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
    public partial class KasaHareketleri : DevExpress.XtraEditors.XtraForm
    {
        public KasaHareketleri()
        {
            InitializeComponent();
        }
        static string kaynak = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " + System.Windows.Forms.Application.StartupPath.ToString() + "\\db.mdb;";
        OleDbConnection baglanti = new OleDbConnection(kaynak);
        
        private void KasaHareketleri_Load(object sender, EventArgs e)
        {
            sifirla();
        }

        void sifirla()
        {
            listBoxControl1.Items.Clear();
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("Select * From Veresiyeler", baglanti);
            OleDbDataReader oku = komut.ExecuteReader();
            double gelirTopla = 0;
            double giderTopla = 0;
            while (oku.Read())
            {
                listBoxControl1.Items.Add(oku["VeresiyeTarih"].ToString() + "    -    " + oku["CariAd"].ToString() + "    -    " + oku["VeresiyeAciklama"].ToString() + "    =    " + oku["VeresiyeTutar"].ToString() + " TL");
                if (oku["VeresiyeAciklama"].ToString() == "*** Borç Ödeme***")
                {
                    gelirTopla += Convert.ToDouble(oku["VeresiyeTutar"]);
                }
                else
                {
                    giderTopla += Convert.ToDouble(oku["VeresiyeTutar"]);
                }
            }
            baglanti.Close();

            textEdit1.Text =  giderTopla.ToString("c");
            textEdit2.Text =  gelirTopla.ToString("c");
            textEdit3.Text =  (gelirTopla + giderTopla).ToString("c"); 
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("Select * From Veresiyeler Where VeresiyeTarih='" + dateEdit1.Text + "'", baglanti);
            OleDbDataReader oku = komut.ExecuteReader();
            double gelirTopla = 0;
            double giderTopla = 0;

            listBoxControl1.Items.Clear();

            while (oku.Read())
            {
                listBoxControl1.Items.Add(oku["VeresiyeTarih"].ToString() + "    -    " + oku["CariAd"].ToString() + "    -    " + oku["VeresiyeAciklama"].ToString() + "    =    " + oku["VeresiyeTutar"].ToString() + " TL");
                if (oku["VeresiyeAciklama"].ToString() == "*** Borç Ödeme***")
                {
                    gelirTopla += Convert.ToDouble(oku["VeresiyeTutar"]);
                }
                else
                {
                    giderTopla += Convert.ToDouble(oku["VeresiyeTutar"]);
                }
            }
            baglanti.Close();

            textEdit1.Text = giderTopla.ToString("c");
            textEdit2.Text = gelirTopla.ToString("c");
            textEdit3.Text = (gelirTopla + giderTopla).ToString("c");
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            sifirla();
        }
    }
}