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
    public partial class VeresiyeEkle : DevExpress.XtraEditors.XtraForm
    {
        public VeresiyeEkle()
        {
            InitializeComponent();
        }
        static string kaynak = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " + System.Windows.Forms.Application.StartupPath.ToString() + "\\db.mdb;";
        OleDbConnection baglanti = new OleDbConnection(kaynak);
        
        public string cariAd = "";
        public int cariNo = 0;
        public int durum = 0;
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            try
            {
                if (textEdit1.Text!="" && textEdit2.Text!="" && textEdit3.Text!="")
                {
                    baglanti.Open();
                    if (durum==0)
                    {
                        OleDbCommand komu = new OleDbCommand("INSERT INTO Veresiyeler(CariNo,CariAd,VeresiyeTarih,VeresiyeAciklama,VeresiyeTutar) VALUES(" + cariNo + ",'" + cariAd + "','" + textEdit2.Text + "','" + textEdit1.Text + "','" + textEdit3.Text + "')", baglanti);
                        komu.ExecuteNonQuery();
                    }
                    else
                    {
                        OleDbCommand komu = new OleDbCommand("INSERT INTO Veresiyeler(CariNo,CariAd,VeresiyeTarih,VeresiyeAciklama,VeresiyeTutar) VALUES(" + cariNo + ",'" + cariAd + "','" + textEdit2.Text + "','" + textEdit1.Text + "','-" + textEdit3.Text + "')", baglanti);
                        komu.ExecuteNonQuery();
                    }


                    OleDbCommand komut2 = new OleDbCommand("Select * From Veresiyeler Where CariNo=" + cariNo, baglanti);
                    OleDbDataReader oku = komut2.ExecuteReader();
                    double genelToplam = 0;
                    while (oku.Read())
                    {
                        genelToplam += Convert.ToDouble(oku["VeresiyeTutar"]);
                    }
                    oku.Dispose();
                    OleDbCommand komut3 = new OleDbCommand("UPDATE CariHesap SET CariBakiye=" + genelToplam + " Where CariNo=" + cariNo, baglanti);
                    komut3.ExecuteNonQuery();

                    baglanti.Close();

                    AnaForm ana=(AnaForm)Application.OpenForms["AnaForm"];
                    ana.musteriHesabiGetir();
                    this.Close();
                }
                else
                {
                    MesajKutusu m = new MesajKutusu();
                    m.labelControl1.Text="Lütfen tüm bilgileri doldurunuz.";
                    m.pictureEdit1.Visible = true;
                    m.ShowDialog();
                }
            }
            catch (Exception hata)
            {
                baglanti.Close();
                MessageBox.Show(hata.Message);
            }
        }

        private void VeresiyeEkle_Load(object sender, EventArgs e)
        {
            textEdit2.Text = DateTime.Now.ToShortDateString();
        }
    }
}