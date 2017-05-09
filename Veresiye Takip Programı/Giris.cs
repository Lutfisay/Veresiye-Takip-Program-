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
    public partial class Giris : DevExpress.XtraEditors.XtraForm
    {
        public Giris()
        {
            InitializeComponent();
        }
        static string kaynak = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " + System.Windows.Forms.Application.StartupPath.ToString() + "\\db.mdb;";
        OleDbConnection baglanti = new OleDbConnection(kaynak);
        
        string kullaniciAd = "", kullaniciSifre = "";

        private void Giris_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("Select * From Ayarlar", baglanti);
            OleDbDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                kullaniciAd = oku["KullaniciAd"].ToString();
                kullaniciSifre = oku["KullaniciSifre"].ToString();
            }
            baglanti.Close();
        }

        private void simpleButton15_Click(object sender, EventArgs e)
        {
            if (kullaniciAd==textEdit1.Text && kullaniciSifre==textEdit2.Text)
            {
                AnaForm a = (AnaForm)Application.OpenForms["AnaForm"];
                a.acilisDurum = true;
                a.acilisKontrolEt();
                this.Close();
            }
            else
            {
                MesajKutusu m = new MesajKutusu();
                m.labelControl1.Text = "Kullanıcı adı veya parola hatalı!";
                m.pictureEdit1.Visible = true;
                m.ShowDialog();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            AnaForm a = (AnaForm)Application.OpenForms["AnaForm"];
            a.acilisDurum = false;
            a.acilisKontrolEt();
            this.Close();
        }
        
    }
}