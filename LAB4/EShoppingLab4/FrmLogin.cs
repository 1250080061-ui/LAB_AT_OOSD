using System;
using System.Windows.Forms;

namespace EShoppingLab4.CodeOnly
{
    public partial class FrmLogin : Form
    {
        private AppServices _services;

        public FrmLogin()
        {
            InitializeComponent();
        }

        public FrmLogin(AppServices services) : this()
        {
            _services = services;
            lblMode.Text = services.ModeText;

            lblHint.Text = services.UseSql
                ? "Tài khoản được kiểm tra từ SQL Server"
                : "Tài khoản mẫu: demo / 123456";
        }

        private void btnLogin_Click(
            object sender,
            EventArgs e)
        {
            if (_services == null) return;

            try
            {
                UserSession found =
                    _services.Accounts.Login(
                        txtUsername.Text.Trim(),
                        txtPassword.Text
                    );

                if (found == null)
                {
                    MessageBox.Show(
                        "Tên đăng nhập hoặc mật khẩu không đúng.");
                    return;
                }

                _services.Session.CustomerId =
                    found.CustomerId;

                _services.Session.Username =
                    found.Username;

                _services.Session.FullName =
                    found.FullName;

                MessageBox.Show(
                    "Đăng nhập thành công. Xin chào " +
                    found.FullName
                );

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Lỗi đăng nhập"
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