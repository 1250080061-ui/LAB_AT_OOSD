namespace EShoppingLab4.CodeOnly
{
    partial class FrmOrderSuccess
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblIcon;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblMode;
        private System.Windows.Forms.Label lblOrderNumber;
        private System.Windows.Forms.Label lblRecipient;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblPayment;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.Label lblShipping;
        private System.Windows.Forms.Label lblTotal;

        private System.Windows.Forms.DataGridView dgvItems;

        private System.Windows.Forms.DataGridViewTextBoxColumn colCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotal;

        private System.Windows.Forms.Button btnFinish;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblIcon = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblMode = new System.Windows.Forms.Label();
            this.lblOrderNumber = new System.Windows.Forms.Label();
            this.lblRecipient = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblPayment = new System.Windows.Forms.Label();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.lblShipping = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();

            this.dgvItems = new System.Windows.Forms.DataGridView();

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

            this.btnFinish = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)
                (this.dgvItems)).BeginInit();

            this.SuspendLayout();

            this.lblIcon.Font = new System.Drawing.Font(
                "Segoe UI", 38F, System.Drawing.FontStyle.Bold);
            this.lblIcon.ForeColor =
                System.Drawing.Color.Green;
            this.lblIcon.Location =
                new System.Drawing.Point(25, 15);
            this.lblIcon.Size =
                new System.Drawing.Size(90, 75);
            this.lblIcon.Text = "✓";
            this.lblIcon.TextAlign =
                System.Drawing.ContentAlignment.MiddleCenter;

            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font(
                "Segoe UI", 19F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor =
                System.Drawing.Color.Green;
            this.lblTitle.Location =
                new System.Drawing.Point(115, 25);
            this.lblTitle.Text =
                "ĐẶT HÀNG THÀNH CÔNG";

            this.lblMode.Location =
                new System.Drawing.Point(120, 70);
            this.lblMode.Size =
                new System.Drawing.Size(500, 25);
            this.lblMode.ForeColor =
                System.Drawing.Color.DarkOrange;
            this.lblMode.Visible = false;

            this.lblOrderNumber.Location =
                new System.Drawing.Point(30, 105);
            this.lblOrderNumber.Size =
                new System.Drawing.Size(680, 28);
            this.lblOrderNumber.Text =
                "Mã đơn hàng: MOCK-DEMO-001";

            this.lblRecipient.Location =
                new System.Drawing.Point(30, 140);
            this.lblRecipient.Size =
                new System.Drawing.Size(680, 28);
            this.lblRecipient.Text =
                "Người nhận: Nguyễn Văn An";

            this.lblPhone.Location =
                new System.Drawing.Point(30, 175);
            this.lblPhone.Size =
                new System.Drawing.Size(680, 28);
            this.lblPhone.Text =
                "Điện thoại: 0901234567";

            this.lblAddress.Location =
                new System.Drawing.Point(30, 210);
            this.lblAddress.Size =
                new System.Drawing.Size(680, 28);
            this.lblAddress.Text =
                "Địa chỉ: 123 Nguyễn Văn Cừ, TP.HCM";

            this.lblPayment.Location =
                new System.Drawing.Point(30, 245);
            this.lblPayment.Size =
                new System.Drawing.Size(680, 28);
            this.lblPayment.Text =
                "Thanh toán: VISA - ************1111";

            this.dgvItems.AllowUserToAddRows = false;
            this.dgvItems.AllowUserToDeleteRows = false;
            this.dgvItems.AutoGenerateColumns = false;
            this.dgvItems.BackgroundColor =
                System.Drawing.Color.White;
            this.dgvItems.Location =
                new System.Drawing.Point(25, 280);
            this.dgvItems.ReadOnly = true;
            this.dgvItems.RowHeadersVisible = false;
            this.dgvItems.SelectionMode =
                System.Windows.Forms
                    .DataGridViewSelectionMode.FullRowSelect;
            this.dgvItems.Size =
                new System.Drawing.Size(700, 180);

            this.colCode.HeaderText = "Mã";
            this.colCode.DataPropertyName = "ProductCode";
            this.colCode.Width = 80;

            this.colName.HeaderText = "Tên sản phẩm";
            this.colName.DataPropertyName = "ProductName";
            this.colName.Width = 250;

            this.colPrice.HeaderText = "Đơn giá";
            this.colPrice.DataPropertyName = "UnitPrice";
            this.colPrice.Width = 120;
            this.colPrice.DefaultCellStyle.Format = "N0";

            this.colQuantity.HeaderText = "SL";
            this.colQuantity.DataPropertyName = "Quantity";
            this.colQuantity.Width = 60;

            this.colTotal.HeaderText = "Thành tiền";
            this.colTotal.DataPropertyName = "LineTotal";
            this.colTotal.Width = 130;
            this.colTotal.DefaultCellStyle.Format = "N0";

            this.dgvItems.Columns.AddRange(
                new System.Windows.Forms.DataGridViewColumn[]
                {
                    this.colCode,
                    this.colName,
                    this.colPrice,
                    this.colQuantity,
                    this.colTotal
                });

            this.lblSubtotal.Location =
                new System.Drawing.Point(400, 475);
            this.lblSubtotal.Size =
                new System.Drawing.Size(325, 30);
            this.lblSubtotal.TextAlign =
                System.Drawing.ContentAlignment.MiddleRight;
            this.lblSubtotal.Text =
                "Tạm tính: 18.500.000 đ";

            this.lblShipping.Location =
                new System.Drawing.Point(400, 505);
            this.lblShipping.Size =
                new System.Drawing.Size(325, 30);
            this.lblShipping.TextAlign =
                System.Drawing.ContentAlignment.MiddleRight;
            this.lblShipping.Text =
                "Phí giao hàng: 25.000 đ";

            this.lblTotal.Location =
                new System.Drawing.Point(400, 535);
            this.lblTotal.Size =
                new System.Drawing.Size(325, 30);
            this.lblTotal.TextAlign =
                System.Drawing.ContentAlignment.MiddleRight;
            this.lblTotal.Font = new System.Drawing.Font(
                "Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTotal.ForeColor =
                System.Drawing.Color.Red;
            this.lblTotal.Text =
                "TỔNG CỘNG: 18.525.000 đ";

            this.btnFinish.Location =
                new System.Drawing.Point(25, 520);
            this.btnFinish.Size =
                new System.Drawing.Size(170, 48);
            this.btnFinish.BackColor =
                System.Drawing.Color.ForestGreen;
            this.btnFinish.ForeColor =
                System.Drawing.Color.White;
            this.btnFinish.Text = "Hoàn tất";
            this.btnFinish.Click +=
                new System.EventHandler(
                    this.btnFinish_Click);

            this.AcceptButton = this.btnFinish;
            this.AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize =
                new System.Drawing.Size(750, 590);

            this.Controls.Add(this.lblIcon);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblMode);
            this.Controls.Add(this.lblOrderNumber);
            this.Controls.Add(this.lblRecipient);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.lblPayment);
            this.Controls.Add(this.dgvItems);
            this.Controls.Add(this.lblSubtotal);
            this.Controls.Add(this.lblShipping);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.btnFinish);

            this.Font =
                new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "FrmOrderSuccess";
            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đặt hàng thành công";

            ((System.ComponentModel.ISupportInitialize)
                (this.dgvItems)).EndInit();

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}