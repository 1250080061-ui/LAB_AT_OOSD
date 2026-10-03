using System;
using System.Drawing;
using System.Windows.Forms;

namespace EShoppingLab4.CodeOnly
{
    public partial class FrmOrderSuccess : Form
    {
        private AppServices _services;
        private OrderResult _result;
        private OrderDraft _order;

        public FrmOrderSuccess()
        {
            InitializeComponent();
        }

        public FrmOrderSuccess(
            AppServices services,
            OrderResult result,
            OrderDraft order) : this()
        {
            _services = services;
            _result = result;
            _order = order;

            ShowOrder();
        }

        private void ShowOrder()
        {
            if (_result == null || _order == null)
                return;

            lblMode.Text = _services == null
                ? "CHƯA KẾT NỐI SQL"
                : _services.ModeText;

            lblMode.ForeColor =
                _services != null && _services.UseSql
                    ? Color.DarkGreen
                    : Color.DarkOrange;

            lblOrderNumber.Text =
                "Mã đơn hàng: " +
                _result.OrderNumber;

            lblRecipient.Text =
                "Người nhận: " +
                _order.RecipientName;

            lblPhone.Text =
                "Điện thoại: " +
                _order.RecipientPhone;

            lblAddress.Text =
                "Địa chỉ: " +
                _order.RecipientAddress;

            lblPayment.Text =
                "Thanh toán: " +
                _order.CardBrand +
                " - " +
                _order.MaskedPan;

            dgvItems.DataSource =
                _order.Items;

            lblSubtotal.Text =
                "Tạm tính: " +
                _order.Subtotal.ToString("N0") +
                " đ";

            lblShipping.Text =
                "Phí giao hàng: " +
                _order.ShippingFee.ToString("N0") +
                " đ";

            lblTotal.Text =
                "TỔNG CỘNG: " +
                _order.GrandTotal.ToString("N0") +
                " đ";
        }

        private void btnFinish_Click(
            object sender,
            EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}