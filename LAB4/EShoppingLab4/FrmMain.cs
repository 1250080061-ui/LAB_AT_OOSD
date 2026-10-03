using System;
using System.Drawing;
using System.Windows.Forms;

namespace EShoppingLab4.CodeOnly
{
    public partial class FrmMain : Form
    {
        private AppServices _services;

        public FrmMain()
        {
            InitializeComponent();
        }

        public FrmMain(AppServices services) : this()
        {
            _services = services;
            CapNhatTrangThai();
        }

        private void CapNhatTrangThai()
        {
            if (_services == null) return;

            lblMode.Text = _services.ModeText;

            lblMode.ForeColor = _services.UseSql
                ? Color.DarkGreen
                : Color.DarkOrange;

            lblSession.Text = _services.Session.IsLoggedIn
                ? "Đăng nhập: " + _services.Session.FullName
                : "Chưa đăng nhập";

            lblCart.Text = string.Format(
                "Giỏ hàng: {0} sản phẩm | {1:N0} đ",
                _services.Cart.TotalQuantity,
                _services.Cart.Subtotal
            );
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            if (_services == null) return;

            using (FrmProducts form = new FrmProducts(_services))
                form.ShowDialog(this);

            CapNhatTrangThai();
        }

        private void btnCart_Click(object sender, EventArgs e)
        {
            if (_services == null) return;

            using (FrmCart form = new FrmCart(_services))
                form.ShowDialog(this);

            CapNhatTrangThai();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (_services == null) return;

            using (FrmLogin form = new FrmLogin(_services))
                form.ShowDialog(this);

            CapNhatTrangThai();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (_services == null) return;

            using (FrmRegister form = new FrmRegister(_services))
                form.ShowDialog(this);
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            if (_services == null) return;

            if (!_services.Session.IsLoggedIn)
            {
                using (FrmLogin login = new FrmLogin(_services))
                {
                    if (login.ShowDialog(this) != DialogResult.OK)
                        return;
                }
            }

            if (_services.Cart.Lines.Count == 0)
            {
                MessageBox.Show("Giỏ hàng đang trống.");
                return;
            }

            using (FrmCheckout form = new FrmCheckout(_services))
                form.ShowDialog(this);

            CapNhatTrangThai();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}