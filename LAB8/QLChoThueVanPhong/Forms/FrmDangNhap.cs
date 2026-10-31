using System.Data;
using Microsoft.Data.SqlClient;
using QLChoThueVanPhong.Infrastructure;
using QLChoThueVanPhong.Ui;
using QLChoThueVanPhong.Utilities;

namespace QLChoThueVanPhong.Forms;

public sealed class FrmDangNhap : Form
{
    private readonly TextBox _txtTenDangNhap = new();
    private readonly TextBox _txtMatKhau = new();
    private readonly Label _lblStatus = new();
    private readonly Button _btnDangNhap;

    public FrmDangNhap()
    {
        Text = "Đăng nhập - Quản lý cho thuê văn phòng";
        StartPosition = FormStartPosition.CenterScreen;
        ClientSize = new Size(980, 610);
        MinimumSize = new Size(900, 560);
        Font = UiTheme.DefaultFont;
        BackColor = UiTheme.Background;

        _btnDangNhap = UiTheme.CreatePrimaryButton("ĐĂNG NHẬP");
        BuildInterface();
        AcceptButton = _btnDangNhap;
    }

    private void BuildInterface()
    {
        Panel brandPanel = new()
        {
            Dock = DockStyle.Left,
            Width = 420,
            BackColor = UiTheme.Navy
        };

        Label brandTitle = new()
        {
            Text = "OFFICE\nRENTAL",
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 31F, FontStyle.Bold),
            AutoSize = true,
            Location = new Point(62, 178)
        };

        Label brandSubtitle = new()
        {
            Text = "HỆ THỐNG QUẢN LÝ\nCHO THUÊ VĂN PHÒNG",
            ForeColor = Color.FromArgb(194, 220, 237),
            Font = new Font("Segoe UI", 13F),
            AutoSize = true,
            Location = new Point(66, 292)
        };

        brandPanel.Controls.Add(brandTitle);
        brandPanel.Controls.Add(brandSubtitle);

        Panel loginPanel = new()
        {
            Width = 390,
            Height = 430,
            BackColor = Color.White,
            Anchor = AnchorStyles.None,
            Location = new Point(505, 88),
            Padding = new Padding(42, 34, 42, 34)
        };

        Label title = new()
        {
            Text = "Đăng nhập hệ thống",
            ForeColor = UiTheme.Navy,
            Font = new Font("Segoe UI", 22F, FontStyle.Bold),
            AutoSize = true,
            Location = new Point(38, 35)
        };

        Label subtitle = new()
        {
            Text = "Nhập tài khoản được cấp để tiếp tục",
            ForeColor = UiTheme.Muted,
            AutoSize = true,
            Location = new Point(42, 87)
        };

        Label lblUser = CreateFieldLabel("Tên đăng nhập", 126);
        _txtTenDangNhap.SetBounds(42, 153, 306, 36);
        _txtTenDangNhap.BorderStyle = BorderStyle.FixedSingle;
        _txtTenDangNhap.Text = "admin";

        Label lblPassword = CreateFieldLabel("Mật khẩu", 206);
        _txtMatKhau.SetBounds(42, 233, 306, 36);
        _txtMatKhau.BorderStyle = BorderStyle.FixedSingle;
        _txtMatKhau.UseSystemPasswordChar = true;
        _txtMatKhau.Text = "123456";

        CheckBox chkShowPassword = new()
        {
            Text = "Hiển thị mật khẩu",
            AutoSize = true,
            ForeColor = UiTheme.Muted,
            Location = new Point(42, 278)
        };
        chkShowPassword.CheckedChanged += (_, _) =>
            _txtMatKhau.UseSystemPasswordChar = !chkShowPassword.Checked;

        _btnDangNhap.SetBounds(42, 315, 306, 43);
        _btnDangNhap.Click += DangNhap;

        _lblStatus.SetBounds(42, 368, 306, 48);
        _lblStatus.ForeColor = Color.Firebrick;
        _lblStatus.TextAlign = ContentAlignment.TopCenter;

        loginPanel.Controls.AddRange([
            title, subtitle, lblUser, _txtTenDangNhap,
            lblPassword, _txtMatKhau, chkShowPassword,
            _btnDangNhap, _lblStatus
        ]);

        Controls.Add(loginPanel);
        Controls.Add(brandPanel);

        Resize += (_, _) =>
        {
            int rightAreaWidth = ClientSize.Width - brandPanel.Width;
            loginPanel.Left = brandPanel.Width + Math.Max(20, (rightAreaWidth - loginPanel.Width) / 2);
            loginPanel.Top = Math.Max(30, (ClientSize.Height - loginPanel.Height) / 2);
        };
    }

    private static Label CreateFieldLabel(string text, int top)
    {
        return new Label
        {
            Text = text,
            ForeColor = UiTheme.Text,
            Font = new Font("Segoe UI", 10F, FontStyle.Bold),
            AutoSize = true,
            Location = new Point(42, top)
        };
    }

    private void DangNhap(object? sender, EventArgs e)
    {
        string userName = _txtTenDangNhap.Text.Trim();
        string password = _txtMatKhau.Text;

        if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
        {
            _lblStatus.Text = "Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu.";
            return;
        }

        try
        {
            const string sql = """
                SELECT TOP 1
                    tk.MaTaiKhoan,
                    tk.TenDangNhap,
                    tk.VaiTro,
                    nv.MaNhanVien,
                    ISNULL(nv.HoTen, tk.TenDangNhap) AS HoTen
                FROM TaiKhoan tk
                LEFT JOIN NhanVien nv ON nv.MaTaiKhoan = tk.MaTaiKhoan
                WHERE tk.TenDangNhap = @TenDangNhap
                  AND
                  (
                      tk.MatKhauHash = @MatKhau
                      OR
                      (
                          @MatKhau = '123456'
                          AND tk.MatKhauHash = CONCAT('demo_hash_', tk.TenDangNhap)
                      )
                  )
                  AND tk.TrangThai = 1;
                """;

            DataTable table = DatabaseHelper.Query(
                sql,
                new SqlParameter("@TenDangNhap", userName),
                new SqlParameter("@MatKhau", password));

            if (table.Rows.Count == 0)
            {
                _lblStatus.Text = "Tên đăng nhập hoặc mật khẩu không chính xác.";
                return;
            }

            DataRow row = table.Rows[0];
            SessionManager.MaTaiKhoan = Convert.ToInt32(row["MaTaiKhoan"]);
            SessionManager.MaNhanVien = row["MaNhanVien"] == DBNull.Value
                ? null
                : Convert.ToInt32(row["MaNhanVien"]);
            SessionManager.TenDangNhap = row["TenDangNhap"].ToString() ?? string.Empty;
            SessionManager.HoTen = row["HoTen"].ToString() ?? string.Empty;
            SessionManager.VaiTro = row["VaiTro"].ToString() ?? string.Empty;

            Hide();
            using FrmMain mainForm = new();
            mainForm.ShowDialog();

            if (mainForm.DialogResult == DialogResult.Retry)
            {
                SessionManager.Clear();
                _txtMatKhau.Clear();
                _lblStatus.Text = string.Empty;
                Show();
                _txtMatKhau.Focus();
            }
            else
            {
                Close();
            }
        }
        catch (Exception ex)
        {
            _lblStatus.Text = "Không thể kết nối SQL Server. Kiểm tra tên máy chủ và cơ sở dữ liệu.";
            MessageBox.Show(
                ex.Message,
                "Lỗi kết nối",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}
