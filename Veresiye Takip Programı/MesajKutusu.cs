using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Veresiye_Takip_Programı
{
    public partial class MesajKutusu : DevExpress.XtraEditors.XtraForm
    {
        public MesajKutusu()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MesajKutusu_Load(object sender, EventArgs e)
        {

        }
    }
}