namespace EShoppingLab4.CodeOnly
{
    partial class FrmLogin
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblMode;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblHint;

        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;

        private System.Windows.Forms.Button btnLogin;
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
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblHint = new System.Windows.Forms.Label();

            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();

            this.btnLogin = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();

            this.SuspendLayout();

            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font(
                "Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor =
                System.Drawing.Color.FromArgb(31, 78, 121);
            this.lblTitle.Location =
                new System.Drawing.Point(30, 20);
            this.lblTitle.Text = "FORM ĐĂNG NHẬP";

            this.lblMode.Location =
                new System.Drawing.Point(30, 70);
            this.lblMode.Size =
                new System.Drawing.Size(470, 25);
            this.lblMode.ForeColor =
                System.Drawing.Color.DarkOrange;
            this.lblMode.Visible = false;

            this.lblUsername.Location =
                new System.Drawing.Point(45, 125);
            this.lblUsername.Size =
                new System.Drawing.Size(135, 26);
            this.lblUsername.Text = "Tên đăng nhập";

            this.txtUsername.Location =
                new System.Drawing.Point(190, 120);
            this.txtUsername.Size =
                new System.Drawing.Size(260, 30);

            this.lblPassword.Location =
                new System.Drawing.Point(45, 180);
            this.lblPassword.Size =
                new System.Drawing.Size(135, 26);
            this.lblPassword.Text = "Mật khẩu";

            this.txtPassword.Location =
                new System.Drawing.Point(190, 175);
            this.txtPassword.Size =
                new System.Drawing.Size(260, 30);
            this.txtPassword.UseSystemPasswordChar = true;

            this.btnLogin.Location =
                new System.Drawing.Point(190, 235);
            this.btnLogin.Size =
                new System.Drawing.Size(130, 40);
            this.btnLogin.Text = "Đăng nhập";
            this.btnLogin.Click +=
                new System.EventHandler(this.btnLogin_Click);

            this.btnClose.Location =
                new System.Drawing.Point(330, 235);
            this.btnClose.Size =
                new System.Drawing.Size(120, 40);
            this.btnClose.Text = "Đóng";
            this.btnClose.Click +=
                new System.EventHandler(this.btnClose_Click);

            this.lblHint.Location =
                new System.Drawing.Point(45, 305);
            this.lblHint.Size =
                new System.Drawing.Size(430, 25);
            this.lblHint.Text =
                "Tài khoản mẫu: demo / 123456";

            this.AcceptButton = this.btnLogin;
            this.AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize =
                new System.Drawing.Size(540, 355);

            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblMode);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblHint);

            this.Font =
                new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "FrmLogin";
            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng nhập";

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}