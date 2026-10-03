namespace EShoppingLab4.CodeOnly
{
    partial class FrmMain
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblMode;
        private System.Windows.Forms.Label lblSession;
        private System.Windows.Forms.Label lblCart;

        private System.Windows.Forms.Button btnProducts;
        private System.Windows.Forms.Button btnCart;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Button btnCheckout;
        private System.Windows.Forms.Button btnExit;

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
            this.lblSession = new System.Windows.Forms.Label();
            this.lblCart = new System.Windows.Forms.Label();

            this.btnProducts = new System.Windows.Forms.Button();
            this.btnCart = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnRegister = new System.Windows.Forms.Button();
            this.btnCheckout = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();

            this.SuspendLayout();

            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font(
                "Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor =
                System.Drawing.Color.FromArgb(31, 78, 121);
            this.lblTitle.Location =
                new System.Drawing.Point(30, 25);
            this.lblTitle.Text = "HỆ THỐNG e-SHOPPING";

            this.lblMode.AutoSize = true;
            this.lblMode.Font = new System.Drawing.Font(
                "Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblMode.ForeColor =
                System.Drawing.Color.DarkOrange;
            this.lblMode.Location =
                new System.Drawing.Point(30, 75);
            this.lblMode.Visible = false;

            this.lblSession.Location =
                new System.Drawing.Point(520, 30);
            this.lblSession.Size =
                new System.Drawing.Size(370, 25);
            this.lblSession.Text = "Chưa đăng nhập";

            this.lblCart.Location =
                new System.Drawing.Point(520, 65);
            this.lblCart.Size =
                new System.Drawing.Size(370, 25);
            this.lblCart.Text =
                "Giỏ hàng: 0 sản phẩm | 0 đ";

            this.btnProducts.Location =
                new System.Drawing.Point(45, 140);
            this.btnProducts.Size =
                new System.Drawing.Size(250, 55);
            this.btnProducts.Text =
                "1. Danh sách sản phẩm";
            this.btnProducts.Click +=
                new System.EventHandler(this.btnProducts_Click);

            this.btnCart.Location =
                new System.Drawing.Point(335, 140);
            this.btnCart.Size =
                new System.Drawing.Size(250, 55);
            this.btnCart.Text = "2. Giỏ hàng";
            this.btnCart.Click +=
                new System.EventHandler(this.btnCart_Click);

            this.btnLogin.Location =
                new System.Drawing.Point(45, 230);
            this.btnLogin.Size =
                new System.Drawing.Size(250, 55);
            this.btnLogin.Text = "3. Đăng nhập";
            this.btnLogin.Click +=
                new System.EventHandler(this.btnLogin_Click);

            this.btnRegister.Location =
                new System.Drawing.Point(335, 230);
            this.btnRegister.Size =
                new System.Drawing.Size(250, 55);
            this.btnRegister.Text =
                "4. Đăng ký tài khoản";
            this.btnRegister.Click +=
                new System.EventHandler(this.btnRegister_Click);

            this.btnCheckout.Location =
                new System.Drawing.Point(45, 320);
            this.btnCheckout.Size =
                new System.Drawing.Size(250, 55);
            this.btnCheckout.Text = "5. Thanh toán";
            this.btnCheckout.Click +=
                new System.EventHandler(this.btnCheckout_Click);

            this.btnExit.Location =
                new System.Drawing.Point(335, 320);
            this.btnExit.Size =
                new System.Drawing.Size(250, 55);
            this.btnExit.Text = "Thoát";
            this.btnExit.Click +=
                new System.EventHandler(this.btnExit_Click);

            this.AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize =
                new System.Drawing.Size(930, 470);

            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblMode);
            this.Controls.Add(this.lblSession);
            this.Controls.Add(this.lblCart);
            this.Controls.Add(this.btnProducts);
            this.Controls.Add(this.btnCart);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.btnCheckout);
            this.Controls.Add(this.btnExit);

            this.Font =
                new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "FrmMain";
            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "e-SHOPPING - Trang chính";

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}