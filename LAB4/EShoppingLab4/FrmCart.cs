using System;
using System.Linq;
using System.Windows.Forms;

namespace EShoppingLab4.CodeOnly
{
    public partial class FrmCart : Form
    {
        private AppServices _services;

        public FrmCart()
        {
            InitializeComponent();
        }

        public FrmCart(AppServices services) : this()
        {
            _services = services;
            lblMode.Text = services.ModeText;
            RefreshCart();
        }

        private CartLine SelectedLine()
        {
            if (dgvCart.CurrentRow == null)
                return null;

            return dgvCart.CurrentRow.DataBoundItem
                as CartLine;
        }

        private void RefreshCart()
        {
            if (_services == null) return;

            dgvCart.DataSource = null;
            dgvCart.DataSource =
                _services.Cart.Lines.ToList();

            lblTotal.Text =
                "Tổng tạm tính: " +
                _services.Cart.Subtotal.ToString("N0") +
                " đ";
        }

        private void btnPlus_Click(
            object sender,
            EventArgs e)
        {
            CartLine line = SelectedLine();

            if (line == null) return;

            _services.Cart.Change(line.Product, 1);
            RefreshCart();
        }

        private void btnMinus_Click(
            object sender,
            EventArgs e)
        {
            CartLine line = SelectedLine();

            if (line == null) return;

            _services.Cart.Change(line.Product, -1);
            RefreshCart();
        }

        private void btnRemove_Click(
            object sender,
            EventArgs e)
        {
            CartLine line = SelectedLine();

            if (line == null) return;

            _services.Cart.Remove(line.Product);
            RefreshCart();
        }

        private void btnCheckout_Click(
            object sender,
            EventArgs e)
        {
            if (_services.Cart.Lines.Count == 0)
            {
                MessageBox.Show(
                    "Giỏ hàng đang trống.");
                return;
            }

            if (!_services.Session.IsLoggedIn)
            {
                using (FrmLogin login =
                       new FrmLogin(_services))
                {
                    if (login.ShowDialog(this)
                        != DialogResult.OK)
                    {
                        return;
                    }
                }
            }

            using (FrmCheckout form =
                   new FrmCheckout(_services))
            {
                form.ShowDialog(this);
            }

            RefreshCart();
        }

        private void btnClose_Click(
            object sender,
            EventArgs e)
        {
            Close();
        }
    }
}