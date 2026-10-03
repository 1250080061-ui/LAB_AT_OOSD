using System;
using System.Windows.Forms;

namespace EShoppingLab4.CodeOnly
{
    public partial class FrmRegister : Form
    {
        private AppServices _services;

        public FrmRegister()
        {
            InitializeComponent();
        }

        public FrmRegister(AppServices services) : this()
        {
            _services = services;
            lblMode.Text = services.ModeText;
        }

        private void btnRegister_Click(
            object sender,
            EventArgs e)
        {
            if (_services == null) return;

            try
            {
                if (string.IsNullOrWhiteSpace(txtFullName.Text) ||
                    string.IsNullOrWhiteSpace(txtIdentity.Text) ||
                    string.IsNullOrWhiteSpace(txtAddress.Text) ||
                    string.IsNullOrWhiteSpace(txtPhone.Text) ||
                    string.IsNullOrWhiteSpace(txtUsername.Text) ||
                    string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    throw new InvalidOperationException(
                        "Vui lòng nhập đủ các trường bắt buộc.");
                }

                RegistrationData data =
                    new RegistrationData
                    {
                        FullName = txtFullName.Text.Trim(),
                        BirthDate = dtpBirth.Value.Date,
                        IdentityDocument =
                            txtIdentity.Text.Trim(),
                        Address = txtAddress.Text.Trim(),
                        Phone = txtPhone.Text.Trim(),
                        Email = txtEmail.Text.Trim(),
                        Username = txtUsername.Text.Trim(),
                        Password = txtPassword.Text
                    };

                _services.Accounts.Register(data);

                MessageBox.Show(
                    "Đăng ký thành công.\nChế độ: " +
                    _services.ModeText
                );

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Đăng ký thất bại"
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