namespace EShoppingLab4.CodeOnly
{
    partial class FrmCart
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblMode;
        private System.Windows.Forms.Label lblTotal;

        private System.Windows.Forms.DataGridView dgvCart;

        private System.Windows.Forms.DataGridViewTextBoxColumn colCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotal;

        private System.Windows.Forms.Button btnPlus;
        private System.Windows.Forms.Button btnMinus;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnCheckout;
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
            this.lblTotal = new System.Windows.Forms.Label();

            this.dgvCart = new System.Windows.Forms.DataGridView();

            this.colCode =
                new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName =
                new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrice =
                new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuantity =
                new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotal =
                new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.btnPlus = new System.Windows.Forms.Button();
            this.btnMinus = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnCheckout = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)
                (this.dgvCart)).BeginInit();

            this.SuspendLayout();

            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font(
                "Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor =
                System.Drawing.Color.FromArgb(31, 78, 121);
            this.lblTitle.Location =
                new System.Drawing.Point(20, 15);
            this.lblTitle.Text =
                "FORM QUẢN LÝ GIỎ HÀNG";

            this.lblMode.Location =
                new System.Drawing.Point(560, 28);
            this.lblMode.Size =
                new System.Drawing.Size(310, 25);
            this.lblMode.ForeColor =
                System.Drawing.Color.DarkOrange;
            this.lblMode.Visible = false;

            this.dgvCart.AllowUserToAddRows = false;
            this.dgvCart.AllowUserToDeleteRows = false;
            this.dgvCart.AutoGenerateColumns = false;
            this.dgvCart.BackgroundColor =
                System.Drawing.Color.White;
            this.dgvCart.Location =
                new System.Drawing.Point(20, 80);
            this.dgvCart.MultiSelect = false;
            this.dgvCart.ReadOnly = true;
            this.dgvCart.RowHeadersVisible = false;
            this.dgvCart.SelectionMode =
                System.Windows.Forms
                    .DataGridViewSelectionMode.FullRowSelect;
            this.dgvCart.Size =
                new System.Drawing.Size(840, 350);

            this.colCode.HeaderText = "Mã";
            this.colCode.DataPropertyName = "ProductCode";
            this.colCode.Width = 90;

            this.colName.HeaderText = "Tên sản phẩm";
            this.colName.DataPropertyName = "ProductName";
            this.colName.Width = 250;

            this.colPrice.HeaderText = "Đơn giá";
            this.colPrice.DataPropertyName = "UnitPrice";
            this.colPrice.Width = 130;
            this.colPrice.DefaultCellStyle.Format = "N0";

            this.colQuantity.HeaderText = "Số lượng";
            this.colQuantity.DataPropertyName = "Quantity";
            this.colQuantity.Width = 90;

            this.colTotal.HeaderText = "Thành tiền";
            this.colTotal.DataPropertyName = "LineTotal";
            this.colTotal.Width = 150;
            this.colTotal.DefaultCellStyle.Format = "N0";

            this.dgvCart.Columns.AddRange(
                new System.Windows.Forms.DataGridViewColumn[]
                {
                    this.colCode,
                    this.colName,
                    this.colPrice,
                    this.colQuantity,
                    this.colTotal
                });

            this.btnPlus.Location =
                new System.Drawing.Point(20, 450);
            this.btnPlus.Size =
                new System.Drawing.Size(130, 40);
            this.btnPlus.Text = "+ Số lượng";
            this.btnPlus.Click +=
                new System.EventHandler(this.btnPlus_Click);

            this.btnMinus.Location =
                new System.Drawing.Point(160, 450);
            this.btnMinus.Size =
                new System.Drawing.Size(130, 40);
            this.btnMinus.Text = "- Số lượng";
            this.btnMinus.Click +=
                new System.EventHandler(this.btnMinus_Click);

            this.btnRemove.Location =
                new System.Drawing.Point(300, 450);
            this.btnRemove.Size =
                new System.Drawing.Size(110, 40);
            this.btnRemove.Text = "Xóa";
            this.btnRemove.Click +=
                new System.EventHandler(this.btnRemove_Click);

            this.btnCheckout.Location =
                new System.Drawing.Point(430, 450);
            this.btnCheckout.Size =
                new System.Drawing.Size(140, 40);
            this.btnCheckout.Text = "Tính tiền";
            this.btnCheckout.Click +=
                new System.EventHandler(this.btnCheckout_Click);

            this.lblTotal.Location =
                new System.Drawing.Point(585, 450);
            this.lblTotal.Size =
                new System.Drawing.Size(275, 35);
            this.lblTotal.Font = new System.Drawing.Font(
                "Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTotal.TextAlign =
                System.Drawing.ContentAlignment.MiddleRight;
            this.lblTotal.Text =
                "Tổng tạm tính: 0 đ";

            this.btnClose.Location =
                new System.Drawing.Point(710, 500);
            this.btnClose.Size =
                new System.Drawing.Size(150, 40);
            this.btnClose.Text = "Đóng";
            this.btnClose.Click +=
                new System.EventHandler(this.btnClose_Click);

            this.AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize =
                new System.Drawing.Size(880, 555);

            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblMode);
            this.Controls.Add(this.dgvCart);
            this.Controls.Add(this.btnPlus);
            this.Controls.Add(this.btnMinus);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnCheckout);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.btnClose);

            this.Font =
                new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "FrmCart";
            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Giỏ hàng";

            ((System.ComponentModel.ISupportInitialize)
                (this.dgvCart)).EndInit();

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}