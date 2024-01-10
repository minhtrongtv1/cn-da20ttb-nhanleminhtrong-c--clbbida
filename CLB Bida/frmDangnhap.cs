using CLB_Bida.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CLB_Bida
{
    public partial class frmDangnhap : Form
    {
        private UserServices services;
        public frmDangnhap()
        {
            InitializeComponent();
            services = new UserServices();
        }

        private void frmDangnhap_Load(object sender, EventArgs e)
        {

        }

        private void btndangnhap_Click(object sender, EventArgs e)
        {

            try
            {
                bool authen = services.UserAuthenticate(txtdangnhap.Text, txtmatkhau.Text);
                if (authen ==true)
                {
                    MessageBox.Show("Đăng nhập thành công","Thông Báo",MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    this.Hide();
                    frmTrangchu frm = new frmTrangchu();
                    frm.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Đăng nhập thất bại","Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                } 
            }
            catch (SqlException ex)
            {
                // Log and handle the SQL exception
                Console.WriteLine($"SqlException: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");

                MessageBox.Show("Lỗi đăng nhập !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Chắn chắn đóng chương trình ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dg == DialogResult.OK)
            Application.Exit();
        }

        private void txtmatkhau_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btndangnhap_Click(null, null);
            }
        }
    }
}
