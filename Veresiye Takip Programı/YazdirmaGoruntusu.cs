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
    public partial class YazdirmaGoruntusu : DevExpress.XtraEditors.XtraForm
    {
        public YazdirmaGoruntusu()
        {
            InitializeComponent();
        }
        static string kaynak = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " + System.Windows.Forms.Application.StartupPath.ToString() + "\\db.mdb;";
        OleDbConnection baglanti = new OleDbConnection(kaynak);
        
        AnaForm a = (AnaForm)Application.OpenForms["AnaForm"];
        public int durum = 0;

        private void YazdirmaGoruntusu_Load(object sender, EventArgs e)
        {

        }

        //Yazdırma dökümanı oluşturuluyor.
        int i = 0;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            string firmaAd = "";
            string firmaAd2 = "";
            string yetkiliKisi = "";
            string isTel = "";
            string vergiNo = "";

            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("Select * From Ayarlar", baglanti);
            OleDbDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                 firmaAd = oku["FirmaAd"].ToString();
                 firmaAd2 = oku["FirmaAd2"].ToString();
                 yetkiliKisi = oku["YetkiliKisi"].ToString();
                 isTel = oku["IsTel"].ToString();
                 vergiNo = oku["VergiNo"].ToString();
            }
            baglanti.Close();

            if (durum==0)
            {
                //ÇİZİM BAŞLANGICI
                Font myFont = new Font("Calibri", 9);
                SolidBrush sbrush = new SolidBrush(Color.Black);
                Pen myPen = new Pen(Color.Black);

                e.Graphics.DrawString("Düzenlenme Tarihi: " + DateTime.Now.ToLongDateString() + "   " + DateTime.Now.ToLongTimeString(), myFont, sbrush, 50, 25);
                e.Graphics.DrawString("Mali geçerliliği yoktur.", myFont, sbrush, 650, 25);

                e.Graphics.DrawLine(myPen, 50, 45, 770, 45); // 1. Kalem, 2. X, 3. Y Koordinatı, 4. Uzunluk, 5. BitişX 
                myFont = new Font("Calibri", 13);
                e.Graphics.DrawString(firmaAd, myFont, sbrush, 50, 50); // 1.text, 2.font, 3.fırça,4.X,5.Y
                myFont = new Font("Calibri", 10);
                e.Graphics.DrawString(firmaAd2, myFont, sbrush, 50, 70);
                e.Graphics.DrawString(yetkiliKisi, myFont, sbrush, 50, 84);
                e.Graphics.DrawString("İş Tel: "+isTel, myFont, sbrush, 50, 98);
                e.Graphics.DrawString("Vergi No: "+vergiNo, myFont, sbrush, 50, 112);

                // İSTENİRSE AŞAĞIDAKİ KAPALI BLOK İLE DE SAĞ TARAFA VERESİYE BORCU OLAN KİŞİNİN ADI VE SOYADI GETİRİLİR.

                //e.Graphics.DrawString("Müşteri bilgileri; ", myFont, sbrush, 550, 52);
                //e.Graphics.DrawString("Cari Kod: " + MusteriNo, myFont, sbrush, 550, 67);
                //e.Graphics.DrawString("Ad-Soyad: " + MusteriAd + " " + MusteriSoyad, myFont, sbrush, 550, 82);
                //e.Graphics.DrawString("Telefon: " + MusteriTel, myFont, sbrush, 550, 97);
                //e.Graphics.DrawString("Adres: " + MusteriAdres, myFont, sbrush, 550, 112);

                e.Graphics.DrawLine(myPen, 50, 140, 770, 140);// 1. Kalem, 2. X, 3. Y Koordinatı, 4. Uzunluk, 5. BitişX
                myFont = new Font("Calibri", 15, FontStyle.Bold);
                e.Graphics.DrawString("HESAP DÖKÜMÜ", myFont, sbrush, 350, 145);
                e.Graphics.DrawLine(myPen, 50, 175, 770, 175);

                myFont = new Font("Calibri", 10, FontStyle.Bold);
                e.Graphics.DrawString("Tarih", myFont, sbrush, 50, 200);
                e.Graphics.DrawString("Açıklama", myFont, sbrush, 250, 200);
                e.Graphics.DrawString("Tutar", myFont, sbrush, 650, 200);


                e.Graphics.DrawLine(myPen, 50, 220, 770, 220);

                int y = 230;



                myFont = new Font("Calibri", 10);

                while (i < a.dataGridView2.Rows.Count)
                {
                    e.Graphics.DrawString(a.dataGridView2[3, i].Value.ToString(), myFont, sbrush, 50, y);
                    e.Graphics.DrawString(a.dataGridView2[4, i].Value.ToString(), myFont, sbrush, 250, y);
                    e.Graphics.DrawString(a.dataGridView2[5, i].Value.ToString(), myFont, sbrush, 650, y);

                    y += 20;

                    i += 1;


                    //yeni sayfaya geçme kontrolü
                    if (y > 1000)
                    {
                        e.Graphics.DrawString("(Devamı -->)", myFont, sbrush, 700, y + 50);
                        y = 50;
                        break; //burada yazdırma sınırına ulaştığımız için while döngüsünden çıkıyoruz
                        //çıktığımızda whil baştan başlıyor i değişkeni değer almaya devam ediyor
                        //yazdırma yeni sayfada başlamış oluyor
                    }
                }

                myFont = new Font("Calibri", 10, FontStyle.Bold);
                e.Graphics.DrawLine(myPen, 50, y, 770, y);
                //e.Graphics.DrawString("Genel Toplam:", myFont, sbrush, 600, y + 20);
                e.Graphics.DrawString(a.labelControl3.Text, myFont, sbrush, 600, y + 20);


                //çoklu sayfa kontrolü
                if (i < a.dataGridView2.Rows.Count)
                {
                    e.HasMorePages = true;
                }
                else
                {
                    e.HasMorePages = false;
                    i = 0;
                }


                StringFormat myStringFormat = new StringFormat();
                myStringFormat.Alignment = StringAlignment.Far;
            }
            else if(durum==1)
            {
                //ÇİZİM BAŞLANGICI
                Font myFont = new Font("Calibri", 9);
                SolidBrush sbrush = new SolidBrush(Color.Black);
                Pen myPen = new Pen(Color.Black);

                e.Graphics.DrawString("Düzenlenme Tarihi: " + DateTime.Now.ToLongDateString() + "   " + DateTime.Now.ToLongTimeString(), myFont, sbrush, 50, 25);

                e.Graphics.DrawLine(myPen, 50, 45, 770, 45); // 1. Kalem, 2. X, 3. Y Koordinatı, 4. Uzunluk, 5. BitişX 
                myFont = new Font("Calibri", 13);
                e.Graphics.DrawString(firmaAd, myFont, sbrush, 50, 50); // 1.text, 2.font, 3.fırça,4.X,5.Y
                myFont = new Font("Calibri", 10);
                e.Graphics.DrawString(firmaAd2, myFont, sbrush, 50, 70);
                e.Graphics.DrawString(yetkiliKisi, myFont, sbrush, 50, 84);
                e.Graphics.DrawString("İş Tel: " + isTel, myFont, sbrush, 50, 98);
                e.Graphics.DrawString("Vergi No: " + vergiNo, myFont, sbrush, 50, 112);
                
                e.Graphics.DrawLine(myPen, 50, 140, 770, 140);// 1. Kalem, 2. X, 3. Y Koordinatı, 4. Uzunluk, 5. BitişX
                myFont = new Font("Calibri", 15, FontStyle.Bold);
                e.Graphics.DrawString("CARİ HESAPLAR", myFont, sbrush, 350, 145);
                e.Graphics.DrawLine(myPen, 50, 175, 770, 175);

                myFont = new Font("Calibri", 10, FontStyle.Bold);
                e.Graphics.DrawString("Cari No", myFont, sbrush, 50, 200);
                e.Graphics.DrawString("Cari Ad", myFont, sbrush, 120, 200);
                e.Graphics.DrawString("İş Tel", myFont, sbrush, 250, 200);
                e.Graphics.DrawString("Cep Tel", myFont, sbrush, 330, 200);
                e.Graphics.DrawString("Adres", myFont, sbrush, 410, 200);
                e.Graphics.DrawString("Kayıt Tarihi", myFont, sbrush, 700, 200);


                e.Graphics.DrawLine(myPen, 50, 220, 770, 220);

                int y = 230;

                myFont = new Font("Calibri", 10);

                while (i < a.dataGridView1.Rows.Count)
                {
                    e.Graphics.DrawString(a.dataGridView1[0, i].Value.ToString(), myFont, sbrush, 50, y);
                    e.Graphics.DrawString(a.dataGridView1[1, i].Value.ToString(), myFont, sbrush, 120, y);
                    e.Graphics.DrawString(a.dataGridView1[2, i].Value.ToString(), myFont, sbrush, 250, y);
                    e.Graphics.DrawString(a.dataGridView1[3, i].Value.ToString(), myFont, sbrush, 330, y);
                    e.Graphics.DrawString(a.dataGridView1[4, i].Value.ToString(), myFont, sbrush, 410, y);
                    e.Graphics.DrawString(a.dataGridView1[8, i].Value.ToString(), myFont, sbrush, 700, y);
                    y += 20;

                    i += 1;


                    //yeni sayfaya geçme kontrolü
                    if (y > 1000)
                    {
                        e.Graphics.DrawString("(Devamı -->)", myFont, sbrush, 700, y + 50);
                        y = 50;
                        break; //burada yazdırma sınırına ulaştığımız için while döngüsünden çıkıyoruz
                        //çıktığımızda whil baştan başlıyor i değişkeni değer almaya devam ediyor
                        //yazdırma yeni sayfada başlamış oluyor
                    }
                }

                //çoklu sayfa kontrolü
                if (i < a.dataGridView2.Rows.Count)
                {
                    e.HasMorePages = true;
                }
                else
                {
                    e.HasMorePages = false;
                    i = 0;
                }

                StringFormat myStringFormat = new StringFormat();
                myStringFormat.Alignment = StringAlignment.Far;
            }
        }

        // FATURALANDIRMA VE YAZDIRMA İŞLEMLERİ.........................


    }
}