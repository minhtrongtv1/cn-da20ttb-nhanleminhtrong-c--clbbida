using CLB_Bida.Dto;
using CLB_Bida.Services;
using CLB_Bida.Ultils;
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
    public partial class frmTable : Form
    {
        private TableServices services;
        private string UnitPriceBefore = "";
        private string TableNameBefore = "";        
        public frmTable()
        {
            InitializeComponent();
            services = new TableServices();
        }

        public void SetDataGridView()
        {
            dgvData.Columns.Clear();
            dgvData.DataSource = null;
            dgvData.DataSource = services.GetAll();
            dgvData.Columns["TableName"].HeaderText = "Tên";
            dgvData.Columns["UnitPrice"].HeaderText = "Đơn Giá";
            dgvData.Columns["TableStatus"].HeaderText = "Trạng Thái";
            dgvData.Columns["UnitPrice"].DefaultCellStyle.Format = "N0";
            dgvData.Columns["TableId"].Visible = false;

            foreach (DataGridViewRow row in dgvData.Rows)
            {
                if ((bool)row.Cells["TableStatus"].Value == true)
                {
                    row.ReadOnly = true;
                }
            }
        }        
        private void frmTable_Load(object sender, EventArgs e)
        {
            Action actionLoadData = () =>
            {
                var data = services.GetAll();
                Invoke(new Action(() =>
                {
                    dgvData.Columns.Clear();
                    dgvData.DataSource = null;
                    dgvData.DataSource = data;
                    dgvData.Columns["TableName"].HeaderText = "Tên";
                    dgvData.Columns["UnitPrice"].HeaderText = "Đơn Giá";
                    dgvData.Columns["TableStatus"].HeaderText = "Trạng Thái";
                    dgvData.Columns["UnitPrice"].DefaultCellStyle.Format = "N0";
                    dgvData.Columns["TableId"].Visible = false;
                }));
                foreach (DataGridViewRow row in dgvData.Rows)
                {
                    if ((bool)row.Cells["TableStatus"].Value == true)
                    {
                        Invoke(new Action(() =>
                        {
                            row.ReadOnly = true;
                        }));
                        
                    }
                }
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
                DataGridViewRow rowafter = dgvData.Rows[e.RowIndex];
                string UnitPriceAfter = rowafter.Cells["UnitPrice"].Value.ToString();
                string TableNameAfter = rowafter.Cells["TableName"].Value.ToString();
                if (UnitPriceAfter != UnitPriceBefore || TableNameAfter != TableNameBefore)
                {
                    DialogResult rs = MessageBox.Show("Bạn muốn thay đổi giá trị?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (rs == DialogResult.Yes)
                    {                        
                        decimal PriceParse = 0;

                        if (decimal.TryParse(rowafter.Cells["UnitPrice"].Value.ToString(), out PriceParse) == false)
                        {
                            MessageBox.Show("Đơn giá sai định dạng!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        string validateTable = services.ValidateTableAction((int)rowafter.Cells["TableId"].Value);
                        if (validateTable == Constants.OK)
                        {
                            TableDto entity = new TableDto();
                            entity.TableId = int.Parse(rowafter.Cells["TableId"].Value.ToString());
                            entity.TableName = rowafter.Cells["TableName"].Value.ToString();
                            entity.UnitPrice = PriceParse;
                            services.EditTable(entity);
                            MessageBox.Show("Thành công!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            SetDataGridView();
                        }

                    }
                    else
                    {
                        rowafter.Cells["UnitPrice"].Value = UnitPriceBefore;
                        rowafter.Cells["TableName"].Value = TableNameBefore;
                    }
                }
                
            }
        }

        private void dgvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                foreach (DataGridViewRow row in dgvData.SelectedRows)
                {
                    DialogResult rs = MessageBox.Show("Bạn muốn xoá bàn này ?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (rs == DialogResult.Yes)
                    {
                        int tableId = int.Parse(row.Cells["TableId"].Value.ToString());
                        string validateTable = services.ValidateTableAction(tableId);
                        if (validateTable == Constants.OK)
                        {
                            if (services.DeleteTable(tableId))
                            {
                                MessageBox.Show("Thành công!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Có lỗi \n Xin Vui Lòng Thử Lại!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }
                        }
                        else if (validateTable == Constants.IN_USE)
                        {
                            MessageBox.Show($"{row.Cells["TableName"].Value.ToString()} đang được sử dụng!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }                        
                        
                        SetDataGridView();
                    }
                    
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            decimal PriceParsed = 0;
            if (string.IsNullOrWhiteSpace(txtTableName.Text))
            {
                MessageBox.Show("Tên bàn không được để trống!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtUnitPrice.Text))
            {
                MessageBox.Show("Đơn giá không được để trống!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (decimal.TryParse(txtUnitPrice.Text, out PriceParsed) == false)
            {
                MessageBox.Show("Đơn giá sai định dạng!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            TableDto table = new TableDto() { TableName = txtTableName.Text.Trim(), UnitPrice = PriceParsed };
            if (services.CreateTable(table))
            {
                MessageBox.Show("Thành công!", "Infomation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SetDataGridView();
                txtTableName.Clear();
                txtUnitPrice.Clear();
            }
            else
            {
                MessageBox.Show("Lỗi. Vui Lòng Thử Lại!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtUnitPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAdd_Click(null, null);
            }
        }

        private void dgvData_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            UnitPriceBefore = "";
            TableNameBefore = "";
            if (e.RowIndex >= 0)
            {                
                DataGridViewRow row = dgvData.Rows[e.RowIndex];
                UnitPriceBefore = row.Cells["UnitPrice"].Value.ToString();
                TableNameBefore = row.Cells["TableName"].Value.ToString();
            }
        }
    }
}
