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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CLB_Bida
{
    public partial class frmCreateBilliardRecord : Form
    {
        private OrderServices services;
        private TableServices tableServices;
        public event LoadOperation LoadFormOperation;
        public delegate void LoadOperation();
        public frmCreateBilliardRecord()
        {
            InitializeComponent();
            services = new OrderServices();
            tableServices = new TableServices();
            LoadTable();
        }

        public void LoadTable()
        {
            var data = tableServices.Get(false);
            cbTable.DataSource = null;
            cbTable.DataSource = data;
            cbTable.ValueMember = "TableId";
            cbTable.DisplayMember = "TableName";
        }
        private void btnCreate_Click(object sender, EventArgs e)
        {
            int TableId = (int) cbTable.SelectedValue;
            string validateTable = tableServices.ValidateTableAction(TableId);
            if (validateTable == Constants.OK)
            {
                if (services.CreateOrderHeader(TableId))
                {
                    if (tableServices.UpdateTableStatus(TableId, true))
                    {
                        MessageBox.Show("Tạo dữ liệu thành công", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadFormOperation();
                        this.Close();
                    }
                }
            }
            else if (validateTable == Constants.IN_USE)
            {
                MessageBox.Show("Bàn đang được sử dụng", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
