using CLB_Bida.Dto;
using CLB_Bida.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CLB_Bida.Views
{
    public partial class frmDrink : Form
    {
        private DrinkServices services;
        public frmDrink()
        {
            InitializeComponent();
            services = new DrinkServices();
        }

        public void SetDataGridView()
        {
            dgvData.Columns.Clear();
            dgvData.DataSource = null;
            dgvData.DataSource = services.GetDrinks();
            dgvData.Columns["DrinkName"].HeaderText = "Tên";
            dgvData.Columns["Price"].HeaderText = "Đơn Giá";
            dgvData.Columns["Price"].DefaultCellStyle.Format = "N0";
            dgvData.Columns["Id"].Visible = false;
        }        
        private void frmDrink_Load(object sender, EventArgs e)
        {
            Action actionLoadData = () =>
            {
                var data = services.GetDrinks();
                Invoke(new Action(() =>
                {
                    dgvData.Columns.Clear();
                    dgvData.DataSource = null;
                    dgvData.DataSource = data;
                    dgvData.Columns["DrinkName"].HeaderText = "Tên";
                    dgvData.Columns["Price"].HeaderText = "Đơn Giá";
                    dgvData.Columns["Price"].DefaultCellStyle.Format = "N0";
                    dgvData.Columns["Id"].Visible = false;
                }));
                    
            };

            Thread t2 = new Thread(() =>
            {
                actionLoadData();
            });
            t2.Start();
             

        }

        private void dgvData_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DialogResult rs = MessageBox.Show("Bạn muốn thay đổi giá trị?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(rs == DialogResult.Yes)
                {
                    DataGridViewRow row = dgvData.Rows[e.RowIndex];

                    decimal PriceParse = 0;

                    if (decimal.TryParse(row.Cells["Price"].Value.ToString(), out PriceParse) == false)
                    {
                        MessageBox.Show("Đơn giá sai định dạng!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    
                    DrinkDto entity = new DrinkDto();
                    entity.Id = int.Parse( row.Cells["Id"].Value.ToString());
                    entity.DrinkName = row.Cells["DrinkName"].Value.ToString();
                    entity.Price = PriceParse;
                    services.EditDrink(entity);
                    SetDataGridView();
                }
            }
        }

        private void dgvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                foreach (DataGridViewRow row in dgvData.SelectedRows)
                {
                    DialogResult rs = MessageBox.Show("Bạn muốn xoá nước uống này ?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (rs == DialogResult.Yes)
                    {
                        services.DeleteDrink(int.Parse(row.Cells["Id"].Value.ToString()));
                        SetDataGridView();
                    }
                    
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            decimal PriceParsed = 0;
            if (string.IsNullOrWhiteSpace(txtDrinkName.Text))
            {
                MessageBox.Show("Món ăn không được để trống!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                MessageBox.Show("Đơn giá không được để trống!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (decimal.TryParse(txtPrice.Text, out PriceParsed) == false)
            {
                MessageBox.Show("Đơn giá sai định dạng!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DrinkDto drink = new DrinkDto() { DrinkName = txtDrinkName.Text.Trim(), Price = PriceParsed };
            if (services.CreateDrink(drink))
            {
                MessageBox.Show("Thành công!", "Infomation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SetDataGridView();
                txtDrinkName.Clear();
                txtPrice.Clear();
            }
            else
            {
                MessageBox.Show("Lỗi. Vui Lòng Thử Lại!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
