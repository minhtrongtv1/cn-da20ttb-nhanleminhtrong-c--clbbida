using CLB_Bida.Domain;
using CLB_Bida.Dto;
using CLB_Bida.Services;
using CLB_Bida.Views;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CLB_Bida
{
    public partial class frmbanbida : Form
    {
        private OrderServices services;
        private TableServices tableServices;
        public frmbanbida()
        {
            InitializeComponent();
            this.services = new OrderServices();
            tableServices = new TableServices();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            SetDataGridView();
        }

        private void frmbanbida_Load(object sender, EventArgs e)
        {
            SetDataGridView();
        }
        private void SetDataGridView()
        {
            dgvData.Columns.Clear();
            dgvData.DataSource = null;
            dgvData.DataSource = services.Get(new CommonFilterDto() { });
            
            dgvData.Columns["TableId"].HeaderText = "STT Bàn";
            dgvData.Columns["StartDateTime"].HeaderText = "Giờ Bắt Đầu";
            dgvData.Columns["EndDateTime"].HeaderText = "Giờ Kết Thúc";
            dgvData.Columns["TableStatus"].HeaderText = "Trạng Thái";
            dgvData.Columns["TableName"].HeaderText = "Tên Bàn";

            dgvData.Columns["InternalOrderNum"].Visible = false;
            dgvData.Columns["TableId"].Visible = false;
            dgvData.Columns["StartDateTime"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            dgvData.Columns["EndDateTime"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
        }

        private void btnBatDau_Click(object sender, EventArgs e)
        {
            frmCreateBilliardRecord frm = new frmCreateBilliardRecord();
            frm.Show();
            frm.LoadFormOperation += SetDataGridView;
        }

        private void btnKetThuc_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                if (row.Selected)
                {
                    int integerParsed;
                    bool ParseOk =  int.TryParse (row.Cells["InternalOrderNum"].Value.ToString(), out integerParsed);
                    if (ParseOk)
                    {
                        OrderHeaderDto data = new OrderHeaderDto() { InternalOrderNum = integerParsed };
                        if (services.EditOrderHeader(data))
                        {
                            tableServices.UpdateTableStatus((int)row.Cells["TableId"].Value, false);
                            string totalHours = services.PriceCalculate(data.InternalOrderNum);
                            MessageBox.Show( $@"Tổng thời gian chơi là : {totalHours} giờ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
                            // Export 
                            FolderBrowserDialog dg = new FolderBrowserDialog();
                            if (dg.ShowDialog() == DialogResult.OK)
                            {                                
                                string fileName = dg.SelectedPath + @"\" + "Bill_" + row.Cells["TableName"].Value.ToString() + "_" + string.Format("{0:ddMMyyyyhhmmss}", DateTime.Now) + ".xlsx";
                                try
                                {
                                    Export(integerParsed, fileName);                                    
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                
                            }
                            SetDataGridView();
                        }
                        else
                        {
                            MessageBox.Show("Có lỗi trong quá trình kết thúc", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng chọn lại bàn cần thanh toán ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
        }

        private void dgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvData.Rows[e.RowIndex];
                if ((bool)row.Cells["TableStatus"].Value == true)
                {
                    int internalOrderNum = (int)row.Cells["InternalOrderNum"].Value;

                    frmOrderDetail frm = new frmOrderDetail(internalOrderNum);
                    frm.Show();
                }
                else
                {
                    MessageBox.Show("Bàn không sử dụng, không thể thêm order", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
               
            }
        }
        private void Export(int InternalOrderNum, string fileName)
        {
            StatisticalServices statisServices = new StatisticalServices();

            List<OrderHeaderDto> header = services.Get(new CommonFilterDto { InternalOrderNum = InternalOrderNum });

            List<StatisticalDto> data = statisServices.Get(InternalOrderNum);

            FileInfo pathTemplate = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"Resources\Template\Bill_Bida_Template.xlsx");
            FileInfo pathExport = new FileInfo(fileName);
            try
            {   
                
                using (ExcelPackage package = new ExcelPackage(pathTemplate))
                {
                    ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                    //template GoodsReceipt
                    ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet1"];
                    ExcelRange range = worksheet.Cells;
                    int index = 0;
                    int startRow = 7;
                    decimal hour = decimal.Parse(services.PriceCalculate(InternalOrderNum));
                    decimal UnitPrice = tableServices.GetById(header.FirstOrDefault().TableId).UnitPrice;
                    range["A3"].Value = header.FirstOrDefault().TableName;
                    range["B3"].Value = header.FirstOrDefault().StartDateTime;
                    range["C3"].Value = header.FirstOrDefault().EndDateTime;
                    range["D3"].Value = hour;
                    range["E3"].Value = UnitPrice;
                    range["F3"].Value = hour * UnitPrice;
                    decimal TongTien = hour * UnitPrice;
                    if (data.Count > 0)
                    {
                        worksheet.InsertRow((startRow + 1), (data.Count() - 1), startRow);

                        foreach (var i in data)
                        {
                            range[$"B{index + startRow}"].Value = i.Index;
                            range[$"C{index + startRow}"].Value = i.ProductName;
                            range[$"D{index + startRow}"].Value = i.UnitPrice;
                            range[$"E{index + startRow}"].Value = i.TotalQty;
                            range[$"F{index + startRow}"].Value = i.TotalQty * i.UnitPrice;
                            TongTien = TongTien + (decimal)range[$"F{index + startRow}"].Value;
                            index++;
                        }
                    }
                    int lastRow = data.Count + startRow + 1;
                    using (ExcelRange totalRowRange = worksheet.Cells[$"A{lastRow}:F{lastRow}"])
                    {
                        // Merge cells
                        totalRowRange.Merge = true;

                        // Fill background color (orange)
                        totalRowRange.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        totalRowRange.Style.Fill.BackgroundColor.SetColor(Color.Orange);
                        totalRowRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                        // Bold text
                        totalRowRange.Style.Font.Bold = true;
                        totalRowRange.Style.Font.UnderLine = true;
                        totalRowRange.Style.Font.Size = 14;
                        // Set text to "Total"
                        worksheet.Cells[$"A{lastRow}"].Value = "Tổng tiền";

                        // Sum the TotalPrice column (assuming TotalPrice is in column F)
                        worksheet.Cells[$"F{lastRow+1}"].Value = TongTien;
                    }
                    package.SaveAs(pathExport);
                    MessageBox.Show("Export Successful!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Process.Start(fileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
