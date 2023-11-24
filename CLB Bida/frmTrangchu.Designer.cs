namespace CLB_Bida
{
    partial class frmTrangchu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnban = new System.Windows.Forms.Button();
            this.btnsp = new System.Windows.Forms.Button();
            this.btnThongKe = new System.Windows.Forms.Button();
            this.btnthoat = new System.Windows.Forms.Button();
            this.btnTable = new System.Windows.Forms.Button();
            this.btnOutsideOrder = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnban
            // 
            this.btnban.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnban.Location = new System.Drawing.Point(51, 38);
            this.btnban.Margin = new System.Windows.Forms.Padding(2);
            this.btnban.Name = "btnban";
            this.btnban.Size = new System.Drawing.Size(135, 114);
            this.btnban.TabIndex = 0;
            this.btnban.Text = "Bàn Bida";
            this.btnban.UseVisualStyleBackColor = true;
            this.btnban.Click += new System.EventHandler(this.btnban_Click);
            // 
            // btnsp
            // 
            this.btnsp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsp.Location = new System.Drawing.Point(51, 249);
            this.btnsp.Margin = new System.Windows.Forms.Padding(2);
            this.btnsp.Name = "btnsp";
            this.btnsp.Size = new System.Drawing.Size(135, 114);
            this.btnsp.TabIndex = 1;
            this.btnsp.Text = "Sản Phẩm";
            this.btnsp.UseVisualStyleBackColor = true;
            this.btnsp.Click += new System.EventHandler(this.btnsp_Click);
            // 
            // btnThongKe
            // 
            this.btnThongKe.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThongKe.Location = new System.Drawing.Point(217, 251);
            this.btnThongKe.Margin = new System.Windows.Forms.Padding(2);
            this.btnThongKe.Name = "btnThongKe";
            this.btnThongKe.Size = new System.Drawing.Size(135, 114);
            this.btnThongKe.TabIndex = 3;
            this.btnThongKe.Text = "Thống Kê";
            this.btnThongKe.UseVisualStyleBackColor = true;
            this.btnThongKe.Click += new System.EventHandler(this.btnThongKe_Click);
            // 
            // btnthoat
            // 
            this.btnthoat.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnthoat.Location = new System.Drawing.Point(800, 429);
            this.btnthoat.Margin = new System.Windows.Forms.Padding(2);
            this.btnthoat.Name = "btnthoat";
            this.btnthoat.Size = new System.Drawing.Size(90, 32);
            this.btnthoat.TabIndex = 4;
            this.btnthoat.Text = "Thoát";
            this.btnthoat.UseVisualStyleBackColor = true;
            this.btnthoat.Click += new System.EventHandler(this.btnthoat_Click);
            // 
            // btnTable
            // 
            this.btnTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTable.Location = new System.Drawing.Point(217, 38);
            this.btnTable.Margin = new System.Windows.Forms.Padding(2);
            this.btnTable.Name = "btnTable";
            this.btnTable.Size = new System.Drawing.Size(135, 114);
            this.btnTable.TabIndex = 6;
            this.btnTable.Text = "Bàn";
            this.btnTable.UseVisualStyleBackColor = true;
            this.btnTable.Click += new System.EventHandler(this.btnTable_Click);
            // 
            // btnOutsideOrder
            // 
            this.btnOutsideOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOutsideOrder.Location = new System.Drawing.Point(379, 38);
            this.btnOutsideOrder.Margin = new System.Windows.Forms.Padding(2);
            this.btnOutsideOrder.Name = "btnOutsideOrder";
            this.btnOutsideOrder.Size = new System.Drawing.Size(135, 114);
            this.btnOutsideOrder.TabIndex = 7;
            this.btnOutsideOrder.Text = "Khách Vãng Lai";
            this.btnOutsideOrder.UseVisualStyleBackColor = true;
            this.btnOutsideOrder.Click += new System.EventHandler(this.btnOutsideOrder_Click);
            // 
            // frmTrangchu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::CLB_Bida.Properties.Resources._1;
            this.ClientSize = new System.Drawing.Size(898, 471);
            this.Controls.Add(this.btnOutsideOrder);
            this.Controls.Add(this.btnTable);
            this.Controls.Add(this.btnthoat);
            this.Controls.Add(this.btnThongKe);
            this.Controls.Add(this.btnsp);
            this.Controls.Add(this.btnban);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmTrangchu";
            this.Text = "frmTrangchu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnban;
        private System.Windows.Forms.Button btnsp;
        private System.Windows.Forms.Button btnThongKe;
        private System.Windows.Forms.Button btnthoat;
        private System.Windows.Forms.Button btnTable;
        private System.Windows.Forms.Button btnOutsideOrder;
    }
}