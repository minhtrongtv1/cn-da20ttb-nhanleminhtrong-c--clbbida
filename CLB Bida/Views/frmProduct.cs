using CLB_Bida.Dto;
using CLB_Bida.Services;
using CLB_Bida.Ultils;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CLB_Bida.Views
{
    public partial class frmProduct : Form
    {
        private ProductServices productServices;
        private CategoryServices categoryServices;
        private CommonFilterDto filter;
        private string UnitPriceBefore = "";
        private string ProductNameBefore = "";
        public frmProduct()
        {
            InitializeComponent();
            productServices = new ProductServices();
            categoryServices = new CategoryServices();
            filter = new CommonFilterDto();
        }
        private void GetFilter()
        {
            
        }
        private void initComboBox()
        {
            var data = categoryServices.GetAll();
            Invoke(new Action(() => 
            {
                cbProductCategory.DataSource = null;
                cbProductCategory.DataSource = data;
                cbProductCategory.DisplayMember = "Name";
                cbProductCategory.ValueMember = "Id";
            }));

        }
        private void SetDataGridView()
        {
            GetFilter();
            var data = productServices.GetAll();

            BeginInvoke(new Action(() =>
            {
                dgvData.DataSource = null;
                dgvData.DataSource = data;
                dgvData.Columns["Index"].HeaderText = "STT";
                dgvData.Columns["Name"].HeaderText = "Sản Phẩm";
                dgvData.Columns["UnitPrice"].HeaderText = "Đơn Giá";               
                dgvData.Columns["Id"].Visible = false;

                dgvData.Columns["CategoryId"].Visible = false;
                dgvData.Columns["UnitPrice"].DefaultCellStyle.Format = "N0";


                dgvData.Columns["CategoryName"].Visible = false;

                //Create Combobox Columns
                DataGridViewComboBoxColumn cmbCol = new DataGridViewComboBoxColumn();
                cmbCol.HeaderText = "Phân Loại";
                cmbCol.Name = "CBCategoryName";
                cmbCol.DisplayMember = "Name";
                cmbCol.ValueMember = "Id";
                cmbCol.Items.Add("True");
                cmbCol.DataSource = categoryServices.GetAll();
                // Add to datagridview
                if (dgvData.Columns.Contains("CBCategoryName") == false)
                {
                    dgvData.Columns.Insert(3, cmbCol);
                }
                else
                {
                    dgvData.Columns["CBCategoryName"].DisplayIndex = 3;
                }
                // Assign value data
                foreach (DataGridViewRow row in dgvData.Rows)
                {
                    row.Cells["CBCategoryName"].Value = row.Cells["CategoryId"].Value;
                }
            }));
           

        }
        private void frmProduct_Load(object sender, EventArgs e)
        {
            Thread t = new Thread(initComboBox);
            Thread t1 = new Thread(SetDataGridView);
            t.Start();
            t1.Start();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            decimal PriceParsed = 0;
            int CatIdParsed = 0;
            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show("Tên sản phẩm không được để trống!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            if (int.TryParse(cbProductCategory.SelectedValue.ToString(), out CatIdParsed) == false)
            {
                MessageBox.Show("Vui lòng chọn lại loại SP!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ProductDto product = new ProductDto() 
            { 
                Name = txtProductName.Text.Trim(), 
                UnitPrice = PriceParsed, 
                CategoryName = cbProductCategory.SelectedText,
                CategoryId = CatIdParsed
            };
            if (productServices.CreateProduct(product))
            {
                MessageBox.Show("Thành công!", "Infomation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SetDataGridView();
                txtUnitPrice.Clear();
                txtProductName.Clear();
            }
            else
            {
                MessageBox.Show("Lỗi. Vui Lòng Thử Lại!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dgvData_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvData.Rows[e.RowIndex];

                DialogResult rs = MessageBox.Show("Bạn muốn thay đổi giá trị?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rs == DialogResult.Yes)
                {
                    decimal PriceParse = 0;

                    if (decimal.TryParse(row.Cells["UnitPrice"].Value.ToString(), out PriceParse) == false)
                    {
                        MessageBox.Show("Đơn giá sai định dạng!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    
                    ProductDto entity = new ProductDto();
                    entity.Id = int.Parse(row.Cells["Id"].Value.ToString());
                    entity.Name = row.Cells["Name"].Value.ToString();
                    entity.UnitPrice = PriceParse;
                    entity.CategoryId =  (int) row.Cells["CBCategoryName"].Value;
                    if (productServices.EditProduct(entity))
                    {
                        MessageBox.Show("Thành công!", "Infomation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SetDataGridView();
                    }
                   
                }
            }
        }

        private void dgvData_KeyDown(object sender, KeyEventArgs e)
        {
            List<int> listId = new List<int>();
            if (e.KeyCode == Keys.Delete)
            {

                foreach (DataGridViewRow row in dgvData.SelectedRows)
                {
                    int productId = (int)row.Cells["Id"].Value;
                    listId.Add(productId);
                }
                if (listId.Count > 0)
                {
                    string validate = productServices.IsInUse(listId);
                    if (validate == Constants.OK)
                    {
                        DialogResult rs = MessageBox.Show("Bạn muốn xoá dữ liệu này?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (rs == DialogResult.Yes)
                        {
                            if (productServices.DeleteProduct(listId))
                            {
                                MessageBox.Show("Thành công!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                SetDataGridView();
                            }
                            else
                            {
                                MessageBox.Show("Lỗi. Vui lòng thử lại!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(validate, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
    }
}
