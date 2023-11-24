using CLB_Bida.Domain;
using CLB_Bida.Dto;
using CLB_Bida.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CLB_Bida.Views
{
    public partial class frmOrderDetail : Form
    {
        private ProductServices productServices;
        private CategoryServices categoryServices;
        private OrderServices OrderServices;
        private int InternalOrderNum = 0;
        private CommonFilterDto filter;
        public frmOrderDetail(int InternalOrderNum)
        {
            InitializeComponent();
            productServices = new ProductServices();
            OrderServices = new OrderServices();
            categoryServices = new CategoryServices();
            filter = new CommonFilterDto();
            this.InternalOrderNum = InternalOrderNum;
           
        }
        private void initComboBox()
        {
           

           
            cbType.DataSource = null;
            cbType.DataSource = categoryServices.GetAll();
            cbType.DisplayMember = "Name";
            cbType.ValueMember = "Id";
            cbType.SelectedIndex = 0;

            cbProduct.DataSource = null;
            cbProduct.DataSource = productServices.Get(new CommonFilterDto { CatId = (int)cbType.SelectedValue });
            cbProduct.DisplayMember = "Name";
            cbProduct.ValueMember = "Id";

        }
        private void GetFilter()
        {
            filter.InternalOrderNum = InternalOrderNum;
        }
        private void SetDataGridView()
        {
            int index = 1;
            dgvData.DataSource = null;
            GetFilter();

          
            dgvData.DataSource = OrderServices.GetOrderDetails(filter);
            dgvData.Columns["Index"].HeaderText = "STT";
            dgvData.Columns["ProductName"].HeaderText = "Sản Phẩm";
            dgvData.Columns["OrderQty"].HeaderText = "Số Lượng Order";
            dgvData.Columns["OrderDate"].HeaderText = "Ngày Order";
            dgvData.Columns["InternalOrderLineNum"].Visible = false;
            dgvData.Columns["InternalOrderNum"].Visible = false;
            dgvData.Columns["ProductCode"].Visible = false;

            dgvData.Columns["OrderDate"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            dgvData.Columns["ProductName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvData.Columns["OrderQty"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvData.Columns["Index"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
           
        }
        private void InitForm()
        {
            initComboBox();
            SetDataGridView();
            txtOrderQty.Text = string.Empty;
        }
        private void frmOrderDetail_Load(object sender, EventArgs e)
        {
            InitForm();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int OrderQtyParsed = -1;
            bool parsed = int.TryParse(txtOrderQty.Text, out OrderQtyParsed);

          
            if (parsed == false)
            {
                MessageBox.Show("Số Lượng Order sai định dạng", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<OrderDetailDto> orderDetails = new List<OrderDetailDto>();
                orderDetails.Add(new OrderDetailDto
                {
                    InternalOrderNum = InternalOrderNum,
                    ProductCode = (int)cbProduct.SelectedValue,
                    ProductName = cbProduct.Text,
                    OrderQty = OrderQtyParsed
                });
                if (OrderServices.CreateOrderDetail(orderDetails))
                {
                    MessageBox.Show("Thành công", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    InitForm();
                }
                else
                {
                    MessageBox.Show("Lỗi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
           
        }

        private void cbType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cbProduct.DataSource = null;
            cbProduct.DataSource = productServices.Get(new CommonFilterDto { CatId = (int)cbType.SelectedValue });
            cbProduct.DisplayMember = "Name";
            cbProduct.ValueMember = "Id";
        }

        private void dgvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                List<int> InternalOrderLineNumRemove = new List<int>();
                foreach (DataGridViewRow row in dgvData.SelectedRows)
                {
                    InternalOrderLineNumRemove.Add((int)row.Cells["InternalOrderLineNum"].Value);   
                }
                DialogResult rs = MessageBox.Show("Bạn muốn xoá dữ liệu này ?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rs == DialogResult.Yes)
                {
                    if (OrderServices.DeleteOrderDetail(InternalOrderLineNumRemove))
                    {
                        MessageBox.Show("Thành công", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        InitForm();
                    }
                    else
                    {
                        MessageBox.Show("Lỗi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }

                }
            }
        }

        private void txtFoodQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAdd_Click(null, null);
            }
        }
    }
}
