namespace EShoppingLab4.CodeOnly
{
    partial class FrmCheckout
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblMode;
        private System.Windows.Forms.Label lblRecipient;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblArea;
        private System.Windows.Forms.Label lblDelivery;
        private System.Windows.Forms.Label lblBrand;
        private System.Windows.Forms.Label lblCard;
        private System.Windows.Forms.Label lblCvv;
        private System.Windows.Forms.Label lblExpiry;
        private System.Windows.Forms.Label lblSummary;

        private System.Windows.Forms.TextBox txtRecipient;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtCard;
        private System.Windows.Forms.TextBox txtCvv;

        private System.Windows.Forms.ComboBox cboArea;
        private System.Windows.Forms.ComboBox cboDelivery;
        private System.Windows.Forms.ComboBox cboBrand;

        private System.Windows.Forms.DateTimePicker dtpExpiry;
        private System.Windows.Forms.DataGridView dgvOrder;

        private System.Windows.Forms.DataGridViewTextBoxColumn colCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotal;

        private System.Windows.Forms.Button btnOrder;
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

            this.dgvOrder = new System.Windows.Forms.DataGridView();

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

            this.lblRecipient = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblArea = new System.Windows.Forms.Label();
            this.lblDelivery = new System.Windows.Forms.Label();
            this.lblBrand = new System.Windows.Forms.Label();
            this.lblCard = new System.Windows.Forms.Label();
            this.lblCvv = new System.Windows.Forms.Label();
            this.lblExpiry = new System.Windows.Forms.Label();
            this.lblSummary = new System.Windows.Forms.Label();

            this.txtRecipient = new System.Windows.Forms.TextBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.txtCard = new System.Windows.Forms.TextBox();
            this.txtCvv = new System.Windows.Forms.TextBox();

            this.cboArea = new System.Windows.Forms.ComboBox();
            this.cboDelivery = new System.Windows.Forms.ComboBox();
            this.cboBrand = new System.Windows.Forms.ComboBox();

            this.dtpExpiry =
                new System.Windows.Forms.DateTimePicker();

            this.btnOrder = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)
                (this.dgvOrder)).BeginInit();

            this.SuspendLayout();

            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font(
                "Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor =
                System.Drawing.Color.FromArgb(31, 78, 121);
            this.lblTitle.Location =
                new System.Drawing.Point(20, 15);
            this.lblTitle.Text =
                "FORM ĐẶT HÀNG VÀ THANH TOÁN";

            this.lblMode.Location =
                new System.Drawing.Point(720, 25);
            this.lblMode.Size =
                new System.Drawing.Size(280, 25);
            this.lblMode.ForeColor =
                System.Drawing.Color.DarkOrange;
            this.lblMode.Visible = false;

            this.dgvOrder.AllowUserToAddRows = false;
            this.dgvOrder.AllowUserToDeleteRows = false;
            this.dgvOrder.AutoGenerateColumns = false;
            this.dgvOrder.BackgroundColor =
                System.Drawing.Color.White;
            this.dgvOrder.Location =
                new System.Drawing.Point(20, 75);
            this.dgvOrder.Size =
                new System.Drawing.Size(970, 170);
            this.dgvOrder.ReadOnly = true;
            this.dgvOrder.RowHeadersVisible = false;
            this.dgvOrder.SelectionMode =
                System.Windows.Forms
                    .DataGridViewSelectionMode.FullRowSelect;

            this.colCode.HeaderText = "Mã";
            this.colCode.DataPropertyName = "ProductCode";
            this.colCode.Width = 90;

            this.colName.HeaderText = "Sản phẩm";
            this.colName.DataPropertyName = "ProductName";
            this.colName.Width = 280;

            this.colPrice.HeaderText = "Đơn giá";
            this.colPrice.DataPropertyName = "UnitPrice";
            this.colPrice.Width = 130;
            this.colPrice.DefaultCellStyle.Format = "N0";

            this.colQuantity.HeaderText = "SL";
            this.colQuantity.DataPropertyName = "Quantity";
            this.colQuantity.Width = 70;

            this.colTotal.HeaderText = "Thành tiền";
            this.colTotal.DataPropertyName = "LineTotal";
            this.colTotal.Width = 150;
            this.colTotal.DefaultCellStyle.Format = "N0";

            this.dgvOrder.Columns.AddRange(
                new System.Windows.Forms.DataGridViewColumn[]
                {
                    this.colCode,
                    this.colName,
                    this.colPrice,
                    this.colQuantity,
                    this.colTotal
                });

            this.lblRecipient.Location =
                new System.Drawing.Point(30, 285);
            this.lblRecipient.Size =
                new System.Drawing.Size(140, 26);
            this.lblRecipient.Text = "Người nhận";

            this.txtRecipient.Location =
                new System.Drawing.Point(180, 280);
            this.txtRecipient.Size =
                new System.Drawing.Size(270, 30);

            this.lblAddress.Location =
                new System.Drawing.Point(30, 330);
            this.lblAddress.Size =
                new System.Drawing.Size(140, 26);
            this.lblAddress.Text = "Địa chỉ";

            this.txtAddress.Location =
                new System.Drawing.Point(180, 325);
            this.txtAddress.Size =
                new System.Drawing.Size(350, 30);

            this.lblPhone.Location =
                new System.Drawing.Point(30, 375);
            this.lblPhone.Size =
                new System.Drawing.Size(140, 26);
            this.lblPhone.Text = "Điện thoại";

            this.txtPhone.Location =
                new System.Drawing.Point(180, 370);
            this.txtPhone.Size =
                new System.Drawing.Size(270, 30);

            this.lblArea.Location =
                new System.Drawing.Point(30, 420);
            this.lblArea.Size =
                new System.Drawing.Size(140, 26);
            this.lblArea.Text = "Khu vực";

            this.cboArea.Location =
                new System.Drawing.Point(180, 415);
            this.cboArea.Size =
                new System.Drawing.Size(270, 31);
            this.cboArea.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboArea.Items.AddRange(new object[]
            {
                "INNER - Nội thành",
                "OUTER - Ngoại thành",
                "REMOTE - Vùng xa"
            });
            this.cboArea.SelectedIndex = 0;
            this.cboArea.SelectedIndexChanged +=
                new System.EventHandler(
                    this.cboArea_SelectedIndexChanged);

            this.lblDelivery.Location =
                new System.Drawing.Point(30, 465);
            this.lblDelivery.Size =
                new System.Drawing.Size(140, 26);
            this.lblDelivery.Text = "Loại giao hàng";

            this.cboDelivery.Location =
                new System.Drawing.Point(180, 460);
            this.cboDelivery.Size =
                new System.Drawing.Size(350, 31);
            this.cboDelivery.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDelivery.Items.AddRange(new object[]
            {
                "1 - Đặt hàng thường",
                "2 - Chuyển phát nhanh",
                "3 - Chuyển phát trong ngày"
            });
            this.cboDelivery.SelectedIndex = 0;
            this.cboDelivery.SelectedIndexChanged +=
                new System.EventHandler(
                    this.cboDelivery_SelectedIndexChanged);

            this.lblBrand.Location =
                new System.Drawing.Point(550, 285);
            this.lblBrand.Size =
                new System.Drawing.Size(140, 26);
            this.lblBrand.Text = "Loại thẻ";

            this.cboBrand.Location =
                new System.Drawing.Point(700, 280);
            this.cboBrand.Size =
                new System.Drawing.Size(250, 31);
            this.cboBrand.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBrand.Items.AddRange(new object[]
            {
                "VISA",
                "MASTER",
                "DISCOVER",
                "AMEX"
            });
            this.cboBrand.SelectedIndex = 0;

            this.lblCard.Location =
                new System.Drawing.Point(550, 330);
            this.lblCard.Size =
                new System.Drawing.Size(140, 26);
            this.lblCard.Text = "Số thẻ";

            this.txtCard.Location =
                new System.Drawing.Point(700, 325);
            this.txtCard.Size =
                new System.Drawing.Size(250, 30);

            this.lblCvv.Location =
                new System.Drawing.Point(550, 375);
            this.lblCvv.Size =
                new System.Drawing.Size(140, 26);
            this.lblCvv.Text = "CVV/CSV";

            this.txtCvv.Location =
                new System.Drawing.Point(700, 370);
            this.txtCvv.Size =
                new System.Drawing.Size(120, 30);
            this.txtCvv.UseSystemPasswordChar = true;

            this.lblExpiry.Location =
                new System.Drawing.Point(550, 420);
            this.lblExpiry.Size =
                new System.Drawing.Size(140, 26);
            this.lblExpiry.Text = "Hết hạn";

            this.dtpExpiry.Location =
                new System.Drawing.Point(700, 415);
            this.dtpExpiry.Size =
                new System.Drawing.Size(250, 30);
            this.dtpExpiry.Format =
                System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpExpiry.CustomFormat = "MM/yyyy";
            this.dtpExpiry.ShowUpDown = true;

            this.lblSummary.Location =
                new System.Drawing.Point(650, 470);
            this.lblSummary.Size =
                new System.Drawing.Size(340, 80);
            this.lblSummary.Font = new System.Drawing.Font(
                "Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblSummary.Text =
                "Tạm tính: 0 đ\n" +
                "Phí giao hàng: 25.000 đ\n" +
                "Tổng cộng: 25.000 đ";

            this.btnOrder.Location =
                new System.Drawing.Point(650, 565);
            this.btnOrder.Size =
                new System.Drawing.Size(210, 45);
            this.btnOrder.Text = "Xác nhận đặt hàng";
            this.btnOrder.Click +=
                new System.EventHandler(this.btnOrder_Click);

            this.btnClose.Location =
                new System.Drawing.Point(875, 565);
            this.btnClose.Size =
                new System.Drawing.Size(115, 45);
            this.btnClose.Text = "Đóng";
            this.btnClose.Click +=
                new System.EventHandler(this.btnClose_Click);

            this.AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize =
                new System.Drawing.Size(1010, 640);

            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblMode);
            this.Controls.Add(this.dgvOrder);
            this.Controls.Add(this.lblRecipient);
            this.Controls.Add(this.txtRecipient);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.lblArea);
            this.Controls.Add(this.cboArea);
            this.Controls.Add(this.lblDelivery);
            this.Controls.Add(this.cboDelivery);
            this.Controls.Add(this.lblBrand);
            this.Controls.Add(this.cboBrand);
            this.Controls.Add(this.lblCard);
            this.Controls.Add(this.txtCard);
            this.Controls.Add(this.lblCvv);
            this.Controls.Add(this.txtCvv);
            this.Controls.Add(this.lblExpiry);
            this.Controls.Add(this.dtpExpiry);
            this.Controls.Add(this.lblSummary);
            this.Controls.Add(this.btnOrder);
            this.Controls.Add(this.btnClose);

            this.Font =
                new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "FrmCheckout";
            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đặt hàng và thanh toán";

            ((System.ComponentModel.ISupportInitialize)
                (this.dgvOrder)).EndInit();

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}