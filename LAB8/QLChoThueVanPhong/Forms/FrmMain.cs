using QLChoThueVanPhong.Ui;
using QLChoThueVanPhong.Utilities;

namespace QLChoThueVanPhong.Forms;

public sealed class FrmMain : Form
{
    private readonly Panel _contentPanel = new();
    private readonly Label _lblCurrentScreen = new();
    private Form? _currentForm;

    public FrmMain()
    {
        Text = "Quản lý cho thuê văn phòng";
        WindowState = FormWindowState.Maximized;
        MinimumSize = new Size(1120, 720);
        Font = UiTheme.DefaultFont;
        BackColor = UiTheme.Background;
        BuildInterface();
        OpenChild(new FrmBaoCaoThongKe(), "Tổng quan");
    }

    private void BuildInterface()
    {
        Panel sideBar = new()
        {
            Dock = DockStyle.Left,
            Width = 238,
            BackColor = UiTheme.Navy
        };

        Label logo = new()
        {
            Text = "OFFICE RENTAL",
            Dock = DockStyle.Top,
            Height = 78,
            TextAlign = ContentAlignment.MiddleCenter,
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 16F, FontStyle.Bold)
        };

        FlowLayoutPanel menu = new()
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            AutoScroll = true,
            Padding = new Padding(10, 10, 10, 10),
            BackColor = UiTheme.Navy
        };

        AddMenuButton(menu, "Tổng quan", () => OpenChild(new FrmBaoCaoThongKe(), "Tổng quan"));
        AddMenuButton(menu, "Khách hàng", () => OpenChild(new FrmKhachHang(), "Quản lý khách hàng"));
        AddMenuButton(menu, "Văn phòng", () => OpenChild(new FrmVanPhong(), "Quản lý văn phòng"));
        AddMenuButton(menu, "Hợp đồng", () => OpenChild(new FrmHopDong(), "Quản lý hợp đồng"));
        AddMenuButton(menu, "Dịch vụ", () => OpenChild(new FrmDichVu(), "Quản lý dịch vụ"));
        AddMenuButton(menu, "Sử dụng dịch vụ", () => OpenChild(new FrmSuDungDichVu(), "Sử dụng dịch vụ"));
        AddMenuButton(menu, "Hóa đơn", () => OpenChild(new FrmHoaDon(), "Quản lý hóa đơn"));
        AddMenuButton(menu, "Thanh toán", () => OpenChild(new FrmThanhToan(), "Quản lý thanh toán"));
        AddMenuButton(menu, "Báo cáo - thống kê", () =>
            OpenChild(new FrmBaoCaoThongKe(), "Báo cáo - thống kê"));
        AddMenuButton(menu, "Tầng", () => OpenChild(new FrmTang(), "Quản lý tầng"));
        AddMenuButton(menu, "Loại văn phòng", () => OpenChild(new FrmLoaiVanPhong(), "Loại văn phòng"));
        AddMenuButton(menu, "Nhân viên", () => OpenChild(new FrmNhanVien(), "Quản lý nhân viên"));
        AddMenuButton(menu, "Tài khoản", () => OpenChild(new FrmTaiKhoan(), "Quản lý tài khoản"));

        sideBar.Controls.Add(menu);
        sideBar.Controls.Add(logo);

        Panel topBar = new()
        {
            Dock = DockStyle.Top,
            Height = 64,
            BackColor = Color.White,
            Padding = new Padding(22, 0, 22, 0)
        };

        _lblCurrentScreen.Text = "Tổng quan";
        _lblCurrentScreen.Dock = DockStyle.Left;
        _lblCurrentScreen.Width = 340;
        _lblCurrentScreen.TextAlign = ContentAlignment.MiddleLeft;
        _lblCurrentScreen.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        _lblCurrentScreen.ForeColor = UiTheme.Navy;

        Button btnLogout = new()
        {
            Text = "Đăng xuất",
            Dock = DockStyle.Right,
            Width = 105,
            FlatStyle = FlatStyle.Flat,
            BackColor = Color.White,
            ForeColor = Color.Firebrick,
            Cursor = Cursors.Hand
        };
        btnLogout.Click += (_, _) =>
        {
            DialogResult = DialogResult.Retry;
            Close();
        };

        Label userInfo = new()
        {
            Text = $"{SessionManager.HoTen}  •  {SessionManager.VaiTro}",
            Dock = DockStyle.Right,
            Width = 320,
            TextAlign = ContentAlignment.MiddleRight,
            ForeColor = UiTheme.Muted
        };

        topBar.Controls.Add(userInfo);
        topBar.Controls.Add(btnLogout);
        topBar.Controls.Add(_lblCurrentScreen);

        _contentPanel.Dock = DockStyle.Fill;
        _contentPanel.BackColor = UiTheme.Background;

        Controls.Add(_contentPanel);
        Controls.Add(topBar);
        Controls.Add(sideBar);
    }

    private static void AddMenuButton(
        FlowLayoutPanel menu,
        string text,
        Action onClick)
    {
        Button button = new()
        {
            Text = text,
            Width = 200,
            Height = 43,
            Margin = new Padding(4, 2, 4, 2),
            FlatStyle = FlatStyle.Flat,
            BackColor = UiTheme.Navy,
            ForeColor = Color.White,
            TextAlign = ContentAlignment.MiddleLeft,
            Padding = new Padding(14, 0, 0, 0),
            Cursor = Cursors.Hand
        };
        button.FlatAppearance.BorderSize = 0;
        button.FlatAppearance.MouseOverBackColor = UiTheme.Blue;
        button.Click += (_, _) => onClick();
        menu.Controls.Add(button);
    }

    private void OpenChild(Form form, string screenTitle)
    {
        _currentForm?.Close();
        _currentForm?.Dispose();
        _contentPanel.Controls.Clear();

        _currentForm = form;
        form.TopLevel = false;
        form.FormBorderStyle = FormBorderStyle.None;
        form.Dock = DockStyle.Fill;
        _contentPanel.Controls.Add(form);
        _contentPanel.Tag = form;
        _lblCurrentScreen.Text = screenTitle;
        form.Show();
    }
}
