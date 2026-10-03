using System;
using System.Linq;
using System.Windows.Forms;

namespace EShoppingLab4.CodeOnly
{
    public partial class FrmCheckout : Form
    {
        private AppServices _services;

        public FrmCheckout()
        {
            InitializeComponent();
        }

        public FrmCheckout(AppServices services) : this()
        {
            _services = services;
            lblMode.Text = services.ModeText;

            dgvOrder.DataSource =
                services.Cart.Snapshot();

            txtRecipient.Text =
                services.Session.FullName ?? "";

            UpdateMoney();
        }

        private string AreaCode()
        {
            if (cboArea.SelectedIndex == 0)
                return "INNER";

            if (cboArea.SelectedIndex == 1)
                return "OUTER";

            return "REMOTE";
        }

        private byte DeliveryId()
        {
            return (byte)(cboDelivery.SelectedIndex + 1);
        }

        private decimal ShippingFee()
        {
            if (_services == null)
                return 25000m;

            return _services.Shipping.Calculate(
                AreaCode(),
                DeliveryId(),
                _services.Cart.Subtotal
            );
        }

        private void UpdateMoney()
        {
            if (_services == null) return;

            decimal shipping = ShippingFee();

            lblSummary.Text = string.Format(
                "Tạm tính: {0:N0} đ\n" +
                "Phí giao hàng: {1:N0} đ\n" +
                "Tổng cộng: {2:N0} đ",
                _services.Cart.Subtotal,
                shipping,
                _services.Cart.Subtotal + shipping
            );
        }

        private void cboArea_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            UpdateMoney();
        }

        private void cboDelivery_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            UpdateMoney();
        }

        private void btnOrder_Click(
            object sender,
            EventArgs e)
        {
            if (_services == null) return;

            try
            {
                if (string.IsNullOrWhiteSpace(txtRecipient.Text) ||
                    string.IsNullOrWhiteSpace(txtAddress.Text) ||
                    string.IsNullOrWhiteSpace(txtPhone.Text))
                {
                    throw new InvalidOperationException(
                        "Nhập đủ thông tin người nhận.");
                }

                string digits = new string(
                    txtCard.Text
                        .Where(char.IsDigit)
                        .ToArray()
                );

                string brand =
                    cboBrand.SelectedItem.ToString();

                int cardLength =
                    brand == "AMEX" ? 15 : 16;

                int cvvLength =
                    brand == "AMEX" ? 4 : 3;

                if (digits.Length != cardLength)
                {
                    throw new InvalidOperationException(
                        brand + " phải có " +
                        cardLength + " chữ số.");
                }

                if (txtCvv.Text.Length != cvvLength ||
                    !txtCvv.Text.All(char.IsDigit))
                {
                    throw new InvalidOperationException(
                        "CVV/CSV phải có " +
                        cvvLength + " chữ số.");
                }

                bool expired =
                    dtpExpiry.Value.Year <
                        DateTime.Today.Year ||
                    (dtpExpiry.Value.Year ==
                         DateTime.Today.Year &&
                     dtpExpiry.Value.Month <
                         DateTime.Today.Month);

                if (expired)
                {
                    throw new InvalidOperationException(
                        "Thẻ đã hết hạn.");
                }

                string masked =
                    new string('*', digits.Length - 4) +
                    digits.Substring(digits.Length - 4);

                OrderDraft draft = new OrderDraft
                {
                    CustomerId =
                        _services.Session.CustomerId.Value,

                    DeliveryMethodId = DeliveryId(),
                    AreaCode = AreaCode(),

                    RecipientName =
                        txtRecipient.Text.Trim(),

                    RecipientAddress =
                        txtAddress.Text.Trim(),

                    RecipientPhone =
                        txtPhone.Text.Trim(),

                    Subtotal =
                        _services.Cart.Subtotal,

                    ShippingFee =
                        ShippingFee(),

                    PaymentFee = 0,
                    CardBrand = brand,
                    MaskedPan = masked,

                    Items =
                        _services.Cart.Snapshot()
                };

                OrderResult result =
                    _services.Orders.Save(draft);

                if (!result.Success)
                {
                    throw new InvalidOperationException(
                        result.Message);
                }

                using (FrmOrderSuccess success =
                       new FrmOrderSuccess(
                           _services,
                           result,
                           draft))
                {
                    success.ShowDialog(this);
                }

                _services.Cart.Clear();

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Không thể đặt hàng"
                );
            }
        }

        private void btnClose_Click(
            object sender,
            EventArgs e)
        {
            Close();
        }
    }
}