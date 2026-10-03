namespace EShoppingLab4.CodeOnly
{
    partial class FrmRegister
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblMode;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.Label lblBirth;
        private System.Windows.Forms.Label lblIdentity;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPassword;

        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.TextBox txtIdentity;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;

        private System.Windows.Forms.DateTimePicker dtpBirth;

        private System.Windows.Forms.Button btnRegister;
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
            this.lblFullName = new System.Windows.Forms.Label();
            this.lblBirth = new System.Windows.Forms.Label();
            this.lblIdentity = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();

            this.txtFullName = new System.Windows.Forms.TextBox();
            this.txtIdentity = new System.Windows.Forms.TextBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();

            this.dtpBirth =
                new System.Windows.Forms.DateTimePicker();

            this.btnRegister = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();

            this.SuspendLayout();

            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font(
                "Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor =
                System.Drawing.Color.FromArgb(31, 78, 121);
            this.lblTitle.Location =
                new System.Drawing.Point(25, 15);
            this.lblTitle.Text =
                "FORM ĐĂNG KÝ TÀI KHOẢN";

            this.lblMode.Location =
                new System.Drawing.Point(25, 60);
            this.lblMode.Size =
                new System.Drawing.Size(500, 25);
            this.lblMode.ForeColor =
                System.Drawing.Color.DarkOrange;
            this.lblMode.Visible = false;

            this.lblFullName.Location =
                new System.Drawing.Point(30, 105);
            this.lblFullName.Size =
                new System.Drawing.Size(155, 26);
            this.lblFullName.Text = "Họ và tên";

            this.txtFullName.Location =
                new System.Drawing.Point(190, 100);
            this.txtFullName.Size =
                new System.Drawing.Size(300, 30);

            this.lblBirth.Location =
                new System.Drawing.Point(30, 153);
            this.lblBirth.Size =
                new System.Drawing.Size(155, 26);
            this.lblBirth.Text = "Ngày sinh";

            this.dtpBirth.Location =
                new System.Drawing.Point(190, 148);
            this.dtpBirth.Size =
                new System.Drawing.Size(300, 30);
            this.dtpBirth.Format =
                System.Windows.Forms.DateTimePickerFormat.Short;

            this.lblIdentity.Location =
                new System.Drawing.Point(30, 201);
            this.lblIdentity.Size =
                new System.Drawing.Size(155, 26);
            this.lblIdentity.Text = "CMND/Passport";

            this.txtIdentity.Location =
                new System.Drawing.Point(190, 196);
            this.txtIdentity.Size =
                new System.Drawing.Size(300, 30);

            this.lblAddress.Location =
                new System.Drawing.Point(30, 249);
            this.lblAddress.Size =
                new System.Drawing.Size(155, 26);
            this.lblAddress.Text = "Địa chỉ";

            this.txtAddress.Location =
                new System.Drawing.Point(190, 244);
            this.txtAddress.Size =
                new System.Drawing.Size(300, 30);

            this.lblPhone.Location =
                new System.Drawing.Point(30, 297);
            this.lblPhone.Size =
                new System.Drawing.Size(155, 26);
            this.lblPhone.Text = "Điện thoại";

            this.txtPhone.Location =
                new System.Drawing.Point(190, 292);
            this.txtPhone.Size =
                new System.Drawing.Size(300, 30);

            this.lblEmail.Location =
                new System.Drawing.Point(30, 345);
            this.lblEmail.Size =
                new System.Drawing.Size(155, 26);
            this.lblEmail.Text = "Email";

            this.txtEmail.Location =
                new System.Drawing.Point(190, 340);
            this.txtEmail.Size =
                new System.Drawing.Size(300, 30);

            this.lblUsername.Location =
                new System.Drawing.Point(30, 393);
            this.lblUsername.Size =
                new System.Drawing.Size(155, 26);
            this.lblUsername.Text = "Tên đăng nhập";

            this.txtUsername.Location =
                new System.Drawing.Point(190, 388);
            this.txtUsername.Size =
                new System.Drawing.Size(300, 30);

            this.lblPassword.Location =
                new System.Drawing.Point(30, 441);
            this.lblPassword.Size =
                new System.Drawing.Size(155, 26);
            this.lblPassword.Text = "Mật khẩu";

            this.txtPassword.Location =
                new System.Drawing.Point(190, 436);
            this.txtPassword.Size =
                new System.Drawing.Size(300, 30);
            this.txtPassword.UseSystemPasswordChar = true;

            this.btnRegister.Location =
                new System.Drawing.Point(190, 500);
            this.btnRegister.Size =
                new System.Drawing.Size(150, 42);
            this.btnRegister.Text = "Đăng ký";
            this.btnRegister.Click +=
                new System.EventHandler(
                    this.btnRegister_Click);

            this.btnClose.Location =
                new System.Drawing.Point(355, 500);
            this.btnClose.Size =
                new System.Drawing.Size(135, 42);
            this.btnClose.Text = "Đóng";
            this.btnClose.Click +=
                new System.EventHandler(this.btnClose_Click);

            this.AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize =
                new System.Drawing.Size(640, 580);

            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblMode);
            this.Controls.Add(this.lblFullName);
            this.Controls.Add(this.txtFullName);
            this.Controls.Add(this.lblBirth);
            this.Controls.Add(this.dtpBirth);
            this.Controls.Add(this.lblIdentity);
            this.Controls.Add(this.txtIdentity);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.btnClose);

            this.Font =
                new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "FrmRegister";
            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng ký tài khoản";

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}