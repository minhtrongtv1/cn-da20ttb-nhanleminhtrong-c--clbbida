using CLB_Bida.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CLB_Bida
{
    public partial class frmTrangchu : Form
    {
        public frmTrangchu()
        {
            InitializeComponent();
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
           Application.Exit();
        }

        private void btnban_Click(object sender, EventArgs e)
        {
            frmbanbida frm = new frmbanbida();
            frm.Show();
            
        }

      

        private void btnTable_Click(object sender, EventArgs e)
        {
            frmTable frm = new frmTable();
            frm.ShowDialog();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            frmStatistical frm = new frmStatistical();
            frm.ShowDialog();
        }

        private void btnsp_Click(object sender, EventArgs e)
        {
            frmProduct frm = new frmProduct();
            frm.ShowDialog();
        }

        private void btnOutsideOrder_Click(object sender, EventArgs e)
        {
            frmOutsideOrder frm = new frmOutsideOrder();
            frm.ShowDialog();
        }

        private void lbban_Click(object sender, EventArgs e)
        {
            frmbanbida frm = new frmbanbida();
            frm.Show();
        }

        private void lbsp_Click(object sender, EventArgs e)
        {
            frmProduct frm = new frmProduct();
            frm.ShowDialog();
        }

        private void lbtt_Click(object sender, EventArgs e)
        {
            frmTable frm = new frmTable();
            frm.ShowDialog();
        }

        private void lbkh_Click(object sender, EventArgs e)
        {
            frmOutsideOrder frm = new frmOutsideOrder();
            frm.ShowDialog();
        }

        private void lbtk_Click(object sender, EventArgs e)
        {
            frmStatistical frm = new frmStatistical();
            frm.ShowDialog();
        }

        private void lbthoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
