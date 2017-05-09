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
    public partial class GidenEposta : DevExpress.XtraEditors.XtraForm
    {
        public GidenEposta()
        {
            InitializeComponent();
        }
        static string kaynak = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " + System.Windows.Forms.Application.StartupPath.ToString() + "\\db.mdb;";
        OleDbConnection baglanti = new OleDbConnection(kaynak);
        
        private void GidenEposta_Load(object sender, EventArgs e)
        {
            dataGridDoldur();
        }
        void dataGridDoldur()
        {
            baglanti.Open();
            OleDbDataAdapter adaptor = new OleDbDataAdapter("Select * From GidenEposta", baglanti);
            DataSet ds = new DataSet();
            ds.Clear();
            adaptor.Fill(ds, "giden");
            dataGridView1.DataSource = ds.Tables["giden"];
            baglanti.Close();

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[3].Visible = false;

            dataGridView1.Columns[1].Width = 250;
            dataGridView1.Columns[2].Width = 250;
            dataGridView1.Columns[4].Width = 250;

            dataGridView1.Columns[1].HeaderText = "Alıcı";
            dataGridView1.Columns[2].HeaderText = "Konu";
            dataGridView1.Columns[4].HeaderText = "Tarih";

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.Silver;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;

        }
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult sorgu = MessageBox.Show("Seçili e-postayı silmek istediğinize emin misiniz?", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (sorgu == DialogResult.Yes)
                {
                    baglanti.Open();
                    OleDbCommand komut = new OleDbCommand("DELETE FROM GidenEposta WHERE GidenEpostaNo=" + dataGridView1.SelectedRows[0].Cells[0].Value, baglanti);
                    komut.ExecuteNonQuery();
                    baglanti.Close();

                    dataGridDoldur();
                }
            }
            catch (Exception hata)
            {
                baglanti.Close();

                MesajKutusu m = new MesajKutusu();
                m.labelControl1.Text = "Lütfen bir e-posta seçiniz. Hata detayı: " + hata.Message;
                m.pictureEdit1.Visible = true;
                m.ShowDialog();
            }
        }
    }
}