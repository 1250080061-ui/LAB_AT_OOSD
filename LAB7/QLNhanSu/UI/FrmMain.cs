
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QLNhanSu.UI
{
    public partial class FrmMain : Form
    {
        private readonly Color navy = Color.FromArgb(23, 37, 63);
        private readonly Color navyLight = Color.FromArgb(43, 65, 99);
        private readonly Color blue = Color.FromArgb(53, 111, 211);

        private Panel contentHost;
        private Label lblPage;
        private Label lblPageSub;

        private Form activeForm;
        private Button activeButton;

        public FrmMain()
        {
            Text = "HR Management - Công ty Sơn Đông Á";
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Maximized;
            MinimumSize = new Size(1100, 700);
            BackColor = Color.FromArgb(245, 247, 251);
            Font = new Font("Segoe UI", 10);

            BuildInterface();
            OpenForm(new FrmNhanVien(), "Nhân viên");
        }

        private void BuildInterface()
        {
            TableLayoutPanel shell = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Margin = Padding.Empty
            };

            shell.ColumnStyles.Add(
                new ColumnStyle(SizeType.Absolute, 270));

            shell.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 100));

            shell.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100));

            Controls.Add(shell);

            // SIDEBAR
            TableLayoutPanel sidebar = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = navy,
                ColumnCount = 1,
                RowCount = 3
            };

            sidebar.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 110));

            sidebar.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100));

            sidebar.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 65));

            shell.Controls.Add(sidebar, 0, 0);

            // LOGO
            Panel logo = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(22, 18, 0, 0)
            };

            Label logoTitle = new Label
            {
                Text = "HR MANAGEMENT",
                Font = new Font(
                    "Segoe UI", 17, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 20)
            };

            Label logoSub = new Label
            {
                Text = "CÔNG TY SƠN ĐÔNG Á",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(170, 190, 215),
                AutoSize = true,
                Location = new Point(22, 62)
            };

            logo.Controls.Add(logoTitle);
            logo.Controls.Add(logoSub);
            sidebar.Controls.Add(logo, 0, 0);

            // MENU
            FlowLayoutPanel menu = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                Padding = new Padding(12, 4, 5, 10),
                BackColor = navy
            };

            sidebar.Controls.Add(menu, 0, 1);

            AddGroup(menu, "QUẢN LÝ NHÂN SỰ");
            AddButton(menu, "Nhân viên",
                () => new FrmNhanVien());
            AddButton(menu, "Phòng ban",
                () => new FrmPhongBan());
            AddButton(menu, "Chức vụ",
                () => new FrmChucVu());

            AddGroup(menu, "TUYỂN DỤNG");
            AddButton(menu, "Tuyển dụng - thử việc",
                () => new FrmTuyenDung());
            AddButton(menu, "Hợp đồng lao động",
                () => new FrmHopDong());

            AddGroup(menu, "LƯƠNG VÀ BẢO HIỂM");
            AddButton(menu, "Quyết định lương",
                () => new FrmQuyetDinhLuong());
            AddButton(menu, "Quyết định chức vụ",
                () => new FrmQuyetDinhChucVu());
            AddButton(menu, "Bảo hiểm",
                () => new FrmBaoHiem());

            AddGroup(menu, "PHÂN CÔNG - CHẤM CÔNG");
            AddButton(menu, "Ca làm việc",
                () => new FrmCaLamViec());
            AddButton(menu, "Kế hoạch phân công",
                () => new FrmKeHoachPhanCong());
            AddButton(menu, "Chi tiết phân công",
                () => new FrmChiTietPhanCong());
            AddButton(menu, "Bảng chấm công",
                () => new FrmChamCong());
            AddButton(menu, "Chi tiết chấm công",
                () => new FrmChiTietChamCong());

            AddGroup(menu, "PHỤ TRỘI - TIỀN LƯƠNG");
            AddButton(menu, "Phiếu phụ trội",
                () => new FrmPhuTroi());
            AddButton(menu, "Chi tiết phụ trội",
                () => new FrmChiTietPhuTroi());
            AddButton(menu, "Bảng lương",
                () => new FrmBangLuong());
            AddButton(menu, "Chi tiết lương",
                () => new FrmChiTietLuong());

            AddGroup(menu, "KHEN THƯỞNG - KỶ LUẬT");
            AddButton(menu, "Khen thưởng",
                () => new FrmKhenThuong());
            AddButton(menu, "Phiếu chi khen thưởng",
                () => new FrmPhieuChiKhenThuong());
            AddButton(menu, "Kỷ luật",
                () => new FrmKyLuat());
            AddButton(menu, "Phiếu thu nộp phạt",
                () => new FrmPhieuThuNopPhat());

            Label footer = new Label
            {
                Dock = DockStyle.Fill,
                Text = "LAB 7  |  .NET Framework 4.7.2",
                ForeColor = Color.FromArgb(165, 185, 210),
                Font = new Font("Segoe UI", 9),
                TextAlign = ContentAlignment.MiddleCenter
            };

            sidebar.Controls.Add(footer, 0, 2);

            // NOI DUNG BEN PHAI
            TableLayoutPanel right = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                BackColor = Color.FromArgb(245, 247, 251)
            };

            right.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 82));

            right.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100));

            shell.Controls.Add(right, 1, 0);

            Panel topbar = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };

            lblPage = new Label
            {
                Text = "Quản lý nhân viên",
                AutoSize = true,
                Font = new Font(
                    "Segoe UI", 15, FontStyle.Bold),
                ForeColor = navy,
                Location = new Point(27, 14)
            };

            lblPageSub = new Label
            {
                Text = "Hệ thống / Nhân viên",
                AutoSize = true,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray,
                Location = new Point(30, 50)
            };

            Label date = new Label
            {
                Text = DateTime.Now.ToString("dd/MM/yyyy"),
                Dock = DockStyle.Right,
                Width = 135,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.FromArgb(90, 105, 130),
                Font = new Font("Segoe UI", 10)
            };

            topbar.Controls.Add(lblPage);
            topbar.Controls.Add(lblPageSub);
            topbar.Controls.Add(date);

            right.Controls.Add(topbar, 0, 0);

            contentHost = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(245, 247, 251),
                Padding = new Padding(12)
            };

            right.Controls.Add(contentHost, 0, 1);
        }

        private void AddGroup(
            FlowLayoutPanel menu, string title)
        {
            Label label = new Label
            {
                Text = title,
                Width = 225,
                Height = 35,
                ForeColor = Color.FromArgb(142, 165, 198),
                Font = new Font(
                    "Segoe UI", 9, FontStyle.Bold),
                TextAlign = ContentAlignment.BottomLeft,
                Margin = new Padding(12, 13, 0, 8)
            };

            menu.Controls.Add(label);
        }

        private void AddButton(
            FlowLayoutPanel menu,
            string text,
            Func<Form> factory)
        {
            Button button = new Button
            {
                Text = "   " + text,
                Width = 225,
                Height = 42,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 10),
                FlatStyle = FlatStyle.Flat,
                BackColor = navy,
                ForeColor = Color.FromArgb(225, 232, 242),
                Cursor = Cursors.Hand,
                Margin = new Padding(0, 2, 0, 2),
                Tag = text
            };

            button.FlatAppearance.BorderSize = 0;

            button.MouseEnter += (s, e) =>
            {
                if (button != activeButton)
                    button.BackColor = navyLight;
            };

            button.MouseLeave += (s, e) =>
            {
                if (button != activeButton)
                    button.BackColor = navy;
            };

            button.Click += (s, e) =>
            {
                if (activeButton != null)
                {
                    activeButton.BackColor = navy;
                    activeButton.ForeColor =
                        Color.FromArgb(225, 232, 242);
                }

                activeButton = button;
                activeButton.BackColor = blue;
                activeButton.ForeColor = Color.White;

                OpenForm(factory(), text);
            };

            menu.Controls.Add(button);
        }

        private void OpenForm(Form form, string title)
        {
            if (activeForm != null)
            {
                contentHost.Controls.Remove(activeForm);
                activeForm.Close();
                activeForm.Dispose();
            }

            activeForm = form;

            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            contentHost.Controls.Add(form);

            lblPage.Text = title;
            lblPageSub.Text = "Hệ thống / " + title;

            form.Show();
            form.BringToFront();
        }
    }
}
