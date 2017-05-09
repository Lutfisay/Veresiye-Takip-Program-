using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;


namespace Veresiye_Takip_Programı
{
    public partial class AnaForm : DevExpress.XtraEditors.XtraForm
    {
        //lütficansay.com
        public AnaForm()
        {
            InitializeComponent();
        }
        static string kaynak = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " + System.Windows.Forms.Application.StartupPath.ToString() + "\\db.mdb;";
        OleDbConnection baglanti = new OleDbConnection(kaynak);

        public bool acilisDurum = false;
        public void acilisKontrolEt()
        {
            if (acilisDurum==true)
            {
                dataGridDoldur("Select * From CariHesap");
            }
            else
            {
                System.Windows.Forms.Application.Exit();
            }
        }
        private void AnaForm_Load(object sender, EventArgs e)
        {
            Giris g = new Giris();
            g.ShowDialog();
            dataGridDoldur("Select * From CariHesap");
        }

        //Ana sayfadaki dataGrid dolduruluyor.(PANEL1)
        void dataGridDoldur(string sorgu)
        {
            baglanti.Open();
            OleDbDataAdapter adaptor = new OleDbDataAdapter(sorgu, baglanti);
            DataSet ds=new DataSet();
            ds.Clear();
            adaptor.Fill(ds,"CariHesap");
            dataGridView1.DataSource = ds.Tables["CariHesap"];
            baglanti.Close();

            //dataGridView1.Columns[2].Visible = false;
            //dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[9].Visible = false;

            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].Width = 170;
            dataGridView1.Columns[2].Width = 120;
            dataGridView1.Columns[3].Width = 120; 
            dataGridView1.Columns[5].Width = 120;
            dataGridView1.Columns[10].Width = 120;

            dataGridView1.Columns[0].HeaderText = "Cari No";
            dataGridView1.Columns[1].HeaderText = "Ad";
            dataGridView1.Columns[2].HeaderText = "İş Tel";
            dataGridView1.Columns[3].HeaderText = "Cep Tel";
            dataGridView1.Columns[5].HeaderText = "İl";
            dataGridView1.Columns[10].HeaderText = "Toplam Bakiye (TL)";

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.Silver;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;

            int i = 0;
            while (i<dataGridView1.Rows.Count)
            {
                double bakiye=Convert.ToDouble(dataGridView1[10,i].Value);
                dataGridView1[10,i].Value=bakiye.ToString("c");
                i++;
            }
        }
        //Ana sayfadaki dataGrid dolduruluyor.(PANEL1)

        //Müşteri hesabı buton aracılığıyla açılıyor. (PANEL1)
        bool secimKontrol = false;
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (secimKontrol==false)
            {
                MessageBox.Show("Lütfen görüntülemek için bir cari hesabı seçiniz.");
            }
            else
            {
                musteriHesabiGetir();
                panelControlAna.Visible = false;
                panelControl1.Visible = true;
            }
        }
        //Müşteri hesabı buton aracılığıyla açılıyor. (PANEL1)

        //Her dataGrid hareketinde PANEL2 deki müşteri bilgileri güncelleniyor.
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                secimKontrol = true;
                labelControl1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                labelControl2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            }
            catch (Exception)
            {
                
            }
        }
        //Her dataGrid hareketinde PANEL2 deki müşteri bilgileri güncelleniyor.

        //Müşteri hesabı çift tıklama ile açılıyor.
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            musteriHesabiGetir();
            panelControlAna.Visible = false;
            panelControl1.Visible = true;
        }
        //Müşteri hesabı çift tıklama ile açılıyor.

        //Müşteri Hesabını getirme fonksiyonu
        public void musteriHesabiGetir()
        {
            try
            {
                baglanti.Open();
                OleDbDataAdapter adaptor = new OleDbDataAdapter("Select * From Veresiyeler Where CariNo=" + dataGridView1.SelectedRows[0].Cells[0].Value, baglanti);
                DataSet ds = new DataSet();
                ds.Clear();
                adaptor.Fill(ds,"Veresiyeler");
                dataGridView2.DataSource=ds.Tables["Veresiyeler"];
                baglanti.Close();

                dataGridView2.Columns[1].Visible = false;
                dataGridView2.Columns[2].Visible = false;

                dataGridView2.Columns[0].Width = 90;
                dataGridView2.Columns[3].Width = 120;
                dataGridView2.Columns[4].Width = 420;
                dataGridView2.Columns[5].Width = 120;


                dataGridView2.Columns[0].HeaderText = "Veresiye ID";
                dataGridView2.Columns[3].HeaderText = "Tarih";
                dataGridView2.Columns[4].HeaderText = "Veresiye Açıklama";
                dataGridView2.Columns[5].HeaderText = "Tutar";

                dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView2.DefaultCellStyle.SelectionBackColor = Color.Silver;
                dataGridView2.DefaultCellStyle.SelectionForeColor = Color.White;

                //ParaHesapla
                int i = 0;
                double toplam = 0;
                while (i < dataGridView2.Rows.Count)
                {
                    toplam += Convert.ToDouble(dataGridView2[5, i].Value);
                    i++;
                }
                labelControl3.Text = "Toplam Borç: " + toplam.ToString("c");

                //Para düzenleme
                i = 0;
                while (i < dataGridView2.Rows.Count)
                {
                    double bakiye = Convert.ToDouble(dataGridView2[5, i].Value);
                    dataGridView2[5, i].Value = bakiye.ToString("c");
                    i++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                baglanti.Close();
            }

        }
        //Müşteri Hesabını getirme fonksiyonu


        //Arama işlemi yapılıyor.
        private void buttonEdit1_EditValueChanged(object sender, EventArgs e)
        {
            dataGridDoldur("Select * from CariHesap where CariAd Like'" + buttonEdit1.Text + "%'");
        }
        //Arama işlemi yapılıyor.

        //Yenileme işlemi yapılıyor.
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            dataGridDoldur("Select * From CariHesap");
        }
        //Yenileme işlemi yapılıyor.

        //E-posta gönderme işlemi yapılıyor.
        private void simpleButton6_Click(object sender, EventArgs e)
        {
            try
            {
                EpostaGonder ee = new EpostaGonder();
                ee.aliciKisi=dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
                ee.ShowDialog();
            }
            catch (Exception)
            {
                MesajKutusu m = new MesajKutusu();
                m.labelControl1.Text = "Direk e-posta gönderim yolunu tercih edecekseniz bir cari hesabı seçmek zorundasınız.";
                m.pictureEdit1.Visible = true;
                m.ShowDialog();
            }
        }
        //E-posta gönderme işlemi yapılıyor.

        //Excel'e gönderme işlemi yapılıyor.
        private void simpleButton7_Click(object sender, EventArgs e)
        {
            
        }
        //Excel'e gönderme işlemi yapılıyor.

        //Yazdırma işlemi yapılıyor.
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            YazdirmaGoruntusu aa = new YazdirmaGoruntusu();
            aa.durum = 1;
            aa.ShowDialog();
        }
        //Yazdırma işlemi yapılıyor.

        //Yeni cari ekleme sayfası açılıyor......
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            YeniCariEkle y = new YeniCariEkle();
            y.ShowDialog();
        }
        //Yeni cari ekleme sayfası açılıyor......

        //Cari kartını silmek için butona tıklanınca çalışacak metod.
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult sorgu = MessageBox.Show("Seçili hesabı ("+dataGridView1.SelectedRows[0].Cells[1].Value.ToString()+") silmek istediğinize emin misiniz?","Dikkat",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (DialogResult.Yes==sorgu)
                {
                    baglanti.Open();

                    OleDbCommand komut2 = new OleDbCommand("DELETE FROM Veresiyeler WHERE CariNo=" + dataGridView1.SelectedRows[0].Cells[0].Value, baglanti);
                    komut2.ExecuteNonQuery();

                    OleDbCommand komut = new OleDbCommand("DELETE FROM CariHesap WHERE CariNo=" + dataGridView1.SelectedRows[0].Cells[0].Value, baglanti);
                    komut.ExecuteNonQuery();
                    baglanti.Close();

                    dataGridDoldur("Select * From CariHesap");

                    MesajKutusu m = new MesajKutusu();
                    m.labelControl1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString() + " isimli cari hesabını silme işlemi başarılı.";
                    m.pictureEdit2.Visible = true;
                    m.ShowDialog();
                }
            }
            catch (Exception)
            {
                baglanti.Close();

                MesajKutusu m = new MesajKutusu();
                m.labelControl1.Text = "Hata oluştu. Lütfen tekrar deneyiniz. Seçili bir cari hesap yoksa lütfen bir cari hesap seçiniz.";
                m.pictureEdit1.Visible = true;
                m.ShowDialog();
            }
        }
        //Cari kartını silmek için butona tıklanınca çalışacak metod.







///////////////////////////// PANEL 2 İŞLEMLERİ //////////////////////////////////////////////





        //İstatistik butonu aktif ediliyor.(PANEL2)
        private void simpleButton13_Click(object sender, EventArgs e)
        {
            try
            {
                Istatistik ı = new Istatistik();
                ı.cariNo = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells[1].Value);
                ı.ShowDialog();
            }
            catch (Exception)
            {
                MesajKutusu m = new MesajKutusu();
                m.labelControl1.Text = "Hesaba ait herhangi bir ve/veya birden fazla bilgi olmadan istatistik hesaplaması yapılamaz.";
                m.pictureEdit1.Visible = true;
                m.ShowDialog();
                //MessageBox.Show("Hesaba ait herhangi bir ve/veya birden fazla bilgi olmadan istatistik hesaplaması yapılamaz.","Hata",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
        }
        //İstatistik butonu aktif ediliyor.(PANEL2)

        //Müşteri hesabı kapatılıyor, ana hesaba geçiliyor.(PANEL2)
        private void simpleButton8_Click(object sender, EventArgs e)
        {
            dataGridDoldur("Select * From CariHesap");
            panelControl1.Visible = false;
            panelControlAna.Visible = true;
        }

        //Veresiye Ekleme işlemi
        private void simpleButton9_Click(object sender, EventArgs e)
        {
            VeresiyeEkle v = new VeresiyeEkle();
            v.cariAd=dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            v.cariNo = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            v.ShowDialog();
        }
        //Veresiye Ekleme işlemi

        //Ödeme Ekleme İşlemi
        private void simpleButton16_Click(object sender, EventArgs e)
        {
            VeresiyeEkle v = new VeresiyeEkle();
            v.Text = "Ödeme Ekle";
            v.textEdit1.Text = "*** Borç Ödeme***";
            v.durum = 1;
            v.textEdit1.Enabled = false;
            v.cariAd = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            v.cariNo = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            v.ShowDialog();
        }
        //Ödeme Ekleme İşlemi

        //Seçili bilgiyi silme işlemi.
        private void simpleButton10_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult sorgu = MessageBox.Show("Seçili bilgiyi silmek istediğinize emin misiniz?", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (DialogResult.Yes == sorgu)
                {
                    baglanti.Open();
                    OleDbCommand komut = new OleDbCommand("DELETE FROM Veresiyeler WHERE VeresiyeNo=" + dataGridView2.SelectedRows[0].Cells[0].Value, baglanti);
                    komut.ExecuteNonQuery();

                    OleDbCommand komut2 = new OleDbCommand("Select * From Veresiyeler Where CariNo=" + dataGridView1.SelectedRows[0].Cells[0].Value, baglanti);
                    OleDbDataReader oku = komut2.ExecuteReader();
                    double genelToplam = 0;
                    while (oku.Read())
                    {
                        genelToplam += Convert.ToDouble(oku["VeresiyeTutar"]);
                    }
                    oku.Dispose();
                    OleDbCommand komut3 = new OleDbCommand("UPDATE CariHesap SET CariBakiye=" + genelToplam + " Where CariNo=" + dataGridView1.SelectedRows[0].Cells[0].Value, baglanti);
                    komut3.ExecuteNonQuery(); 
                    
                    baglanti.Close();

                    musteriHesabiGetir();

                    MesajKutusu m = new MesajKutusu();
                    m.labelControl1.Text = "Seçili bilgiyi silme işlemi başarılı.";
                    m.pictureEdit2.Visible = true;
                    m.ShowDialog();
                }
            }
            catch (Exception)
            {
                baglanti.Close();
                MesajKutusu m = new MesajKutusu();
                m.labelControl1.Text = "Hata oluştu. Lütfen tekrar deneyiniz. Seçili bir bilgi yoksa lütfen bir bilgi seçiniz.";
                m.pictureEdit1.Visible = true;
                m.ShowDialog();
            }
        }
        //Seçili bilgiyi silme işlemi.

        //Sayfayı yenileme işlemi
        private void simpleButton15_Click(object sender, EventArgs e)
        {

        }
        //Sayfayı yenileme işlemi

        //E-posta gönderme işlemi
        private void simpleButton12_Click(object sender, EventArgs e)
        {
            EpostaGonder ee = new EpostaGonder();
            ee.aliciKisi=dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
            ee.ShowDialog();
        }
        //E-posta gönderme işlemi

        //Excel'e aktarma işlemi
        private void simpleButton11_Click(object sender, EventArgs e)
        {
           
        }
        //Excel'e aktarma işlemi

        //Yazdırma işlemi
        private void simpleButton14_Click(object sender, EventArgs e)
        {
            YazdirmaGoruntusu y = new YazdirmaGoruntusu();
            y.ShowDialog();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            
        }

        //Yazdırma işlemi








/// <summary>
/// ////////////////////////////////////////////// ÜST MENÜ İŞLEMLERİ ////////////////////////////////////////
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>


        private void epostaGönderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EpostaGonder ee = new EpostaGonder();
            ee.ShowDialog();
        }

        private void gidenKutusuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GidenEposta g = new GidenEposta();
            g.ShowDialog();
        }

        private void yedekleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ayarlar a = new Ayarlar();
            a.xtraTabControl1.SelectedTabPage = a.xtraTabControl1.TabPages[4];
            a.ShowDialog();
        }

        private void geriYükleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ayarlar a = new Ayarlar();
            a.xtraTabControl1.SelectedTabPage = a.xtraTabControl1.TabPages[4];
            a.ShowDialog();
        }

        //  AYARLAR
        private void firmaBilgileriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ayarlar a = new Ayarlar();
            a.xtraTabControl1.SelectedTabPage = a.xtraTabControl1.TabPages[0];
            a.ShowDialog();
        }

        private void kullanıcıAyarlarıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ayarlar a = new Ayarlar();
            a.xtraTabControl1.SelectedTabPage = a.xtraTabControl1.TabPages[1];
            a.ShowDialog();
        }

        private void epostaSunucuAyarlarıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ayarlar a = new Ayarlar();
            a.xtraTabControl1.SelectedTabPage = a.xtraTabControl1.TabPages[2];
            a.ShowDialog();
        }

        private void smsAyarlarıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ayarlar a = new Ayarlar();
            a.xtraTabControl1.SelectedTabPage = a.xtraTabControl1.TabPages[3];
            a.ShowDialog();
        }
        //  AYARLAR


        private void yardımToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MesajKutusu m = new MesajKutusu();
            m.labelControl1.Text = "Bu özellik şu anda aktif değil.";
            m.pictureEdit1.Visible = true;
            m.ShowDialog();
        }

        private void programHakkındaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProgramHakkinda p = new ProgramHakkinda();
            p.ShowDialog();
        }

        
        private void çıkışToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void çıkışToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void kasaHareketleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KasaHareketleri k = new KasaHareketleri();
            k.ShowDialog();
        }

        private void bankaHesaplarıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BankaHesaplari b = new BankaHesaplari();
            b.ShowDialog();
        }

        private void istatistiklerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MesajKutusu m = new MesajKutusu();
            m.labelControl1.Text = "Bu özellik şu anda aktif değil.";
            m.pictureEdit1.Visible = true;
            m.ShowDialog();
        }

        private void yenidenBaşlatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Restart();
        }

        private void smsGönderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MesajKutusu m = new MesajKutusu();
            m.labelControl1.Text = "Bu özellik şu anda aktif değil.";
            m.pictureEdit1.Visible = true;
            m.ShowDialog();
        }

        private void gidenKutusuToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MesajKutusu m = new MesajKutusu();
            m.labelControl1.Text = "Bu özellik şu anda aktif değil.";
            m.pictureEdit1.Visible = true;
            m.ShowDialog();
        }


    }
}
