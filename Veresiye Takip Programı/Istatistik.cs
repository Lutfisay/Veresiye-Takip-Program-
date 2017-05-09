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
    public partial class Istatistik : DevExpress.XtraEditors.XtraForm
    {
        public Istatistik()
        {
            InitializeComponent();
        }
        static string kaynak = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " + System.Windows.Forms.Application.StartupPath.ToString() + "\\db.mdb;";
        OleDbConnection baglanti = new OleDbConnection(kaynak);
        public int cariNo = 0;
        private void Istatistik_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("Select * From Veresiyeler Where CariNo=" + cariNo, baglanti);
            OleDbDataReader oku = komut.ExecuteReader();
            int i = 0;
            while (oku.Read())
            {
                DateTime tarih = new DateTime();
                tarih = Convert.ToDateTime(oku["VeresiyeTarih"]);

                DevExpress.XtraCharts.SeriesPoint p = new DevExpress.XtraCharts.SeriesPoint();
                p.Argument = tarih.ToString();
                chartControl1.Series[0].Points.Add(p);

                double[] say = new double[] { Convert.ToDouble(oku["VeresiyeTutar"]) };
                chartControl1.Series[0].Points[i].Values = say;
                i++;
            }
            baglanti.Close();
        }
    }
}