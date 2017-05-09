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
    public partial class BankaHesaplari : DevExpress.XtraEditors.XtraForm
    {
        public BankaHesaplari()
        {
            InitializeComponent();
        }
        static string kaynak = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " + System.Windows.Forms.Application.StartupPath.ToString() + "\\db.mdb;";
        OleDbConnection baglanti = new OleDbConnection(kaynak);
        
        private void BankaHesaplari_Load(object sender, EventArgs e)
        {
            bankaHesaplariGetir();
        }

        void bankaHesaplariGetir()
        {
            baglanti.Open();
            OleDbDataAdapter adaptor = new OleDbDataAdapter("Select * From BankaHesaplari", baglanti);
            DataSet ds = new DataSet();
            ds.Clear();
            adaptor.Fill(ds,"Banka");
            dataGridView1.DataSource=ds.Tables["Banka"];
            baglanti.Close();

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dataGridView1.Columns[0].Visible = false;

            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[2].Width = 200;
            dataGridView1.Columns[3].Width = 120;
            dataGridView1.Columns[4].Width = 120;

            dataGridView1.Columns[1].HeaderText = "Banka Adı";
            dataGridView1.Columns[2].HeaderText = "IBAN No";
            dataGridView1.Columns[3].HeaderText = "Hesap No";
            dataGridView1.Columns[4].HeaderText = "Hesap Türü";

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            temizle();
        }

        void temizle()
        {
            textEdit1.Text = "";
            textEdit2.Text = "";
            textEdit3.Text = "";
            textEdit4.Text = "";
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("INSERT INTO BankaHesaplari(BankaAd,BankaIBAN,BankaHesapNo,BankaTuru) VALUES('" + textEdit1.Text + "','" + textEdit2.Text + "','" + textEdit3.Text + "','" + textEdit4.Text + "')", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();

            bankaHesaplariGetir();

            MesajKutusu m = new MesajKutusu();
            m.labelControl1.Text = "Banka hesabı kayıt işlemi başarılı.";
            m.pictureEdit2.Visible = true;
            m.ShowDialog();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult sorgu = MessageBox.Show("Seçili banka hesabını silmek istediğinize emin misiniz?","Dikkat",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (sorgu==DialogResult.Yes)
                {
                    baglanti.Open();
                    OleDbCommand komut = new OleDbCommand("DELETE FROM BankaHesaplari WHERE BankaNo=" + dataGridView1.SelectedRows[0].Cells[0].Value, baglanti);
                    komut.ExecuteNonQuery();
                    baglanti.Close();

                    bankaHesaplariGetir();
                }
            }
            catch (Exception hata)
            {
                baglanti.Close();
                MesajKutusu m = new MesajKutusu();
                m.labelControl1.Text = "Lütfen bir banka hesabını seçiniz. Hata detayı: "+hata.Message;
                m.pictureEdit1.Visible = true;
                m.ShowDialog();
            }
        }
    }
}