namespace EShoppingLab4.CodeOnly
{
    partial class FrmProducts
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblMode;
        private System.Windows.Forms.Label lblGroup;

        private System.Windows.Forms.ComboBox cboGroup;
        private System.Windows.Forms.DataGridView dgvProducts;

        private System.Windows.Forms.DataGridViewTextBoxColumn colCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colManufacturer;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGroup;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStock;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;

        private System.Windows.Forms.Button btnDetail;
        private System.Windows.Forms.Button btnAddCart;
        private System.Windows.Forms.Button btnClose;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblMode = new System.Windows.Forms.Label();
            this.lblGroup = new System.Windows.Forms.Label();

            this.cboGroup = new System.Windows.Forms.ComboBox();
            this.dgvProducts = new System.Windows.Forms.DataGridView();

            this.colCode =
                new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName =
                new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colManufacturer =
                new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGroup =
                new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrice =
                new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStock =
                new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus =
                new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.btnDetail = new System.Windows.Forms.Button();
            this.btnAddCart = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)
                (this.dgvProducts)).BeginInit();

            this.SuspendLayout();

            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font(
                "Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor =
                System.Drawing.Color.FromArgb(31, 78, 121);
            this.lblTitle.Location =
                new System.Drawing.Point(20, 15);
            this.lblTitle.Text =
                "FORM DANH SÁCH SẢN PHẨM";

            this.lblMode.Location =
                new System.Drawing.Point(720, 25);
            this.lblMode.Size =
                new System.Drawing.Size(340, 28);
            this.lblMode.ForeColor =
                System.Drawing.Color.DarkOrange;
            this.lblMode.Visible = false;

            this.lblGroup.Location =
                new System.Drawing.Point(20, 77);
            this.lblGroup.Size =
                new System.Drawing.Size(135, 26);
            this.lblGroup.Text = "Nhóm sản phẩm:";

            this.cboGroup.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGroup.Location =
                new System.Drawing.Point(155, 72);
            this.cboGroup.Size =
                new System.Drawing.Size(270, 31);
            this.cboGroup.Items.AddRange(new object[]
            {
                "Tất cả",
                "Thiết bị máy tính",
                "Máy ảnh",
                "Thiết bị gia dụng",
                "Đồ chơi"
            });
            this.cboGroup.SelectedIndexChanged +=
                new System.EventHandler(
                    this.cboGroup_SelectedIndexChanged);

            this.dgvProducts.AllowUserToAddRows = false;
            this.dgvProducts.AllowUserToDeleteRows = false;
            this.dgvProducts.AutoGenerateColumns = false;
            this.dgvProducts.BackgroundColor =
                System.Drawing.Color.White;
            this.dgvProducts.Location =
                new System.Drawing.Point(20, 120);
            this.dgvProducts.MultiSelect = false;
            this.dgvProducts.ReadOnly = true;
            this.dgvProducts.RowHeadersVisible = false;
            this.dgvProducts.SelectionMode =
                System.Windows.Forms
                    .DataGridViewSelectionMode.FullRowSelect;
            this.dgvProducts.Size =
                new System.Drawing.Size(1040, 390);

            this.colCode.HeaderText = "Mã";
            this.colCode.DataPropertyName = "ProductCode";
            this.colCode.Width = 80;

            this.colName.HeaderText = "Tên sản phẩm";
            this.colName.DataPropertyName = "ProductName";
            this.colName.Width = 220;

            this.colManufacturer.HeaderText = "Nhà sản xuất";
            this.colManufacturer.DataPropertyName = "Manufacturer";
            this.colManufacturer.Width = 130;

            this.colGroup.HeaderText = "Nhóm";
            this.colGroup.DataPropertyName = "GroupName";
            this.colGroup.Width = 170;

            this.colPrice.HeaderText = "Giá";
            this.colPrice.DataPropertyName = "Price";
            this.colPrice.Width = 120;
            this.colPrice.DefaultCellStyle.Format = "N0";

            this.colStock.HeaderText = "Tồn kho";
            this.colStock.DataPropertyName = "StockQuantity";
            this.colStock.Width = 80;

            this.colStatus.HeaderText = "Tình trạng";
            this.colStatus.DataPropertyName = "StockStatus";
            this.colStatus.Width = 100;

            this.dgvProducts.Columns.AddRange(
                new System.Windows.Forms.DataGridViewColumn[]
                {
                    this.colCode,
                    this.colName,
                    this.colManufacturer,
                    this.colGroup,
                    this.colPrice,
                    this.colStock,
                    this.colStatus
                });

            this.btnDetail.Location =
                new System.Drawing.Point(20, 525);
            this.btnDetail.Size =
                new System.Drawing.Size(150, 40);
            this.btnDetail.Text = "Xem chi tiết";
            this.btnDetail.Click +=
                new System.EventHandler(this.btnDetail_Click);

            this.btnAddCart.Location =
                new System.Drawing.Point(185, 525);
            this.btnAddCart.Size =
                new System.Drawing.Size(160, 40);
            this.btnAddCart.Text = "Thêm vào giỏ";
            this.btnAddCart.Click +=
                new System.EventHandler(this.btnAddCart_Click);

            this.btnClose.Location =
                new System.Drawing.Point(910, 525);
            this.btnClose.Size =
                new System.Drawing.Size(150, 40);
            this.btnClose.Text = "Đóng";
            this.btnClose.Click +=
                new System.EventHandler(this.btnClose_Click);

            this.AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize =
                new System.Drawing.Size(1080, 585);

            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblMode);
            this.Controls.Add(this.lblGroup);
            this.Controls.Add(this.cboGroup);
            this.Controls.Add(this.dgvProducts);
            this.Controls.Add(this.btnDetail);
            this.Controls.Add(this.btnAddCart);
            this.Controls.Add(this.btnClose);

            this.Font =
                new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "FrmProducts";
            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh sách sản phẩm";

            ((System.ComponentModel.ISupportInitialize)
                (this.dgvProducts)).EndInit();

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}