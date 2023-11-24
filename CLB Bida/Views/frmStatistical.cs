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
using System.Windows.Forms.DataVisualization.Charting;

namespace CLB_Bida.Views
{
    public partial class frmStatistical : Form
    {
        private StatisticalServices services;
        private CommonFilterDto filter;
        public frmStatistical()
        {
            InitializeComponent();
            services = new StatisticalServices();
            filter = new CommonFilterDto();
        }

        private void GetFilter()
        {
            DateTime fromDate = new DateTime(2000, 01, 01);
            DateTime toDate = new DateTime(4000, 12, 31);
            if (dtFrom.Checked == true)
            {
                fromDate = dtFrom.Value;
            }
            if (dtTo.Checked == true)
            {
                toDate = dtTo.Value;
            }
            filter.fromDate = fromDate;
            filter.toDate = toDate;
        }
        private void btnView_Click(object sender, EventArgs e)
        {
            GetFilter();
           
            dgvData.DataSource = null;
            dgvData.DataSource = services.Get(filter);

            dgvData.Columns["Index"].HeaderText = "STT";
            dgvData.Columns["ProductName"].HeaderText = "Sản Phẩm";
            dgvData.Columns["CatName"].HeaderText = "Phân Loại";
            dgvData.Columns["TotalQty"].HeaderText = "Số Lượng";
            dgvData.Columns["UnitPrice"].HeaderText = "Đơn Giá";
            dgvData.Columns["TotalPrice"].HeaderText = "Tổng";

            dgvData.Columns["UnitPrice"].DefaultCellStyle.Format = "N0";
            dgvData.Columns["TotalPrice"].DefaultCellStyle.Format = "N0";

            dgvData.Columns["ProductCode"].Visible = false;
            dgvData.Columns["CatId"].Visible = false;
        }

        private void btnshow_Click(object sender, EventArgs e)
        {
            GetFilter();

            // Retrieve data from the service
            var data = services.Get(filter);

            // Bind the data to the DataGridView
            dgvData.DataSource = null;
            dgvData.DataSource = data;

            // Set up the chart
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            chart1.ChartAreas.Add("ChartArea1");

            // Create a new series for the chart
            Series series = new Series("TotalPrice");
            series.ChartType = SeriesChartType.Column;

            // Add data points to the series
            foreach (var item in data)
            {
                var productName = item.ProductName;
                var totalPrice = Convert.ToInt32(item.TotalPrice);

                series.Points.AddXY(productName, totalPrice);
            }

            // Add the series to the chart
            chart1.Series.Add(series);

            // Customize the appearance of the chart
            chart1.Titles.Clear();
            chart1.Titles.Add("Total Price by Product");
            chart1.ChartAreas["ChartArea1"].AxisX.Title = "Product";
            chart1.ChartAreas["ChartArea1"].AxisY.Title = "Total Price";
        }

    }
}
