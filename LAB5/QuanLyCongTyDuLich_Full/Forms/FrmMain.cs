using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCongTyDuLich.Forms
{
    public class FrmMain : Form
    {
        // Mỗi nghiệp vụ khai báo một lần để dùng chung cho tìm kiếm và mở form.
        private sealed class Feature
        {
            public readonly string Code, Title, Group, Description;
            public readonly Func<Form> CreateForm;

            public Feature(string code, string title, string group,
                string description, Func<Form> createForm)
            {
                Code = code;
                Title = title;
                Group = group;
                Description = description;
                CreateForm = createForm;
            }
        }

        private readonly Feature[] features =
        {
            new Feature("DM", "Quản lý danh mục", "Thiết lập",
                "Chuẩn bị phương tiện, điểm bán vé, hướng dẫn viên và điểm tham quan.",
                () => new FrmDanhMuc()),
            new Feature("TO", "Quản lý tour", "Thiết lập",
                "Thiết lập hành trình, số ngày, đơn giá và trạng thái mở bán.",
                () => new FrmTour()),
            new Feature("CL", "Chuyến khách lẻ", "Điều hành",
                "Lên lịch khởi hành, ngày về và điểm đón cho từng chuyến.",
                () => new FrmChuyenLe()),
            new Feature("KL", "Đăng ký khách lẻ", "Đăng ký",
                "Tiếp nhận khách, chọn chuyến và ghi nhận thanh toán.",
                () => new FrmDangKyLe()),
            new Feature("ĐK", "Đăng ký đoàn khách", "Đăng ký",
                "Ghi nhận đoàn, lịch đi, bảo hiểm và tiền đặt cọc.",
                () => new FrmDangKyDoan()),
            new Feature("HD", "Phân công hướng dẫn viên", "Điều hành",
                "Sắp xếp người phụ trách và kiểm tra lịch làm việc bị trùng.",
                () => new FrmPhanCongHDV()),
            new Feature("KS", "Kết thúc tour / khảo sát", "Điều hành",
                "Mở màn hình khảo sát để ghi điểm đánh giá và góp ý của khách.",
                () => new FrmKetThucKhaoSat()),
            new Feature("BC", "Lương và thống kê", "Báo cáo",
                "Tra cứu lương hướng dẫn viên, doanh thu và danh sách tour.",
                () => new FrmLuongThongKe())
        };

        private readonly List<Button> navigationButtons = new List<Button>();
        private readonly TextBox searchBox = new TextBox();
        private readonly Label pageTitle = new Label();
        private readonly Label resultLabel = new Label();
        private readonly Label statusLabel = new Label();
        private readonly Label connectionLabel = new Label();
        private readonly Button connectionButton = new Button();
        private readonly Panel featureHost = new Panel();
        private readonly ToolTip hints = new ToolTip();
        private string selectedGroup = "Tổng quan";
        private int columnCount;

        public FrmMain()
        {
            Text = "Quản lý công ty du lịch | Điều phối hành trình";
            StartPosition = FormStartPosition.CenterScreen;
            Rectangle screen = Screen.PrimaryScreen.WorkingArea;
            Size = new Size(Math.Min(1180, screen.Width), Math.Min(720, screen.Height));
            MinimumSize = new Size(980, 640);
            Font = new Font("Segoe UI", 9F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = UiStyle.Background;
            ForeColor = UiStyle.TextColor;
            KeyPreview = true;

            var shell = new TableLayoutPanel
            {
                Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1,
                Margin = Padding.Empty, Padding = Padding.Empty
            };
            shell.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 216F));
            shell.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            shell.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            shell.Controls.Add(BuildSidebar(), 0, 0);
            shell.Controls.Add(BuildWorkspace(), 1, 0);
            Controls.Add(shell);

            searchBox.TextChanged += (sender, e) => RenderFeatures();
            featureHost.ClientSizeChanged += (sender, e) =>
            {
                // Cửa sổ hẹp chuyển về một cột để các thẻ đủ chỗ hiển thị.
                int next = GetColumnCount();
                if (next != columnCount) RenderFeatures();
            };
            SelectGroup("Tổng quan");
        }

        private Control BuildSidebar()
        {
            var sidebar = new TableLayoutPanel
            {
                Dock = DockStyle.Fill, BackColor = UiStyle.Sidebar,
                ColumnCount = 1, RowCount = 3, Margin = Padding.Empty,
                Padding = new Padding(16, 20, 16, 16)
            };
            sidebar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            sidebar.RowStyles.Add(new RowStyle(SizeType.Absolute, 112F));
            sidebar.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            sidebar.RowStyles.Add(new RowStyle(SizeType.Absolute, 114F));
            sidebar.Controls.Add(new Label
            {
                Text = "QUẢN LÝ\nDU LỊCH", Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 19F, FontStyle.Bold),
                ForeColor = Color.White, Margin = new Padding(10, 0, 0, 0)
            }, 0, 0);

            var navigation = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill, FlowDirection = FlowDirection.TopDown,
                WrapContents = false, AutoScroll = true, Margin = Padding.Empty
            };
            foreach (string group in new[] { "Tổng quan", "Thiết lập", "Đăng ký", "Điều hành", "Báo cáo" })
            {
                var button = new Button
                {
                    Text = group, Tag = group, Width = 180, Height = 46,
                    FlatStyle = FlatStyle.Flat, ForeColor = Color.White,
                    BackColor = UiStyle.Sidebar, Cursor = Cursors.Hand,
                    Font = new Font("Segoe UI", 10F), TextAlign = ContentAlignment.MiddleLeft,
                    Padding = new Padding(14, 0, 0, 0), Margin = new Padding(0, 0, 0, 7)
                };
                button.FlatAppearance.BorderSize = 0;
                button.FlatAppearance.MouseOverBackColor = UiStyle.Primary;
                button.FlatAppearance.MouseDownBackColor = UiStyle.Primary;
                button.Click += (sender, e) => SelectGroup(Convert.ToString(button.Tag));
                navigationButtons.Add(button);
                navigation.Controls.Add(button);
            }
            sidebar.Controls.Add(navigation, 0, 1);

            var databasePanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2,
                Margin = Padding.Empty, Padding = new Padding(0, 12, 0, 0)
            };
            databasePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            databasePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            databasePanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            connectionLabel.Text = "SQL Server\nChưa kiểm tra kết nối";
            connectionLabel.Dock = DockStyle.Fill;
            connectionLabel.ForeColor = Color.FromArgb(204, 222, 210);
            connectionLabel.AutoEllipsis = true;
            connectionButton.Text = "Kiểm tra kết nối";
            connectionButton.Dock = DockStyle.Fill;
            UiStyle.SetButtonStyle(connectionButton, UiStyle.Accent, UiStyle.TextColor);
            connectionButton.Click += CheckConnection;
            databasePanel.Controls.Add(connectionLabel, 0, 0);
            databasePanel.Controls.Add(connectionButton, 0, 1);
            sidebar.Controls.Add(databasePanel, 0, 2);
            return sidebar;
        }

        private Control BuildWorkspace()
        {
            var workspace = new TableLayoutPanel
            {
                Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 4,
                BackColor = UiStyle.Background, Margin = Padding.Empty,
                Padding = new Padding(24, 18, 24, 12)
            };
            workspace.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            workspace.RowStyles.Add(new RowStyle(SizeType.Absolute, 102F));
            workspace.RowStyles.Add(new RowStyle(SizeType.Absolute, 76F));
            workspace.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            workspace.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));

            var header = new TableLayoutPanel
            {
                Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 3, Margin = Padding.Empty
            };
            header.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            header.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
            header.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            header.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            header.Controls.Add(new Label
            {
                Text = "BÀN ĐIỀU PHỐI  /  " + DateTime.Today.ToString("dd.MM.yyyy"),
                Dock = DockStyle.Fill, ForeColor = UiStyle.MutedText,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold)
            }, 0, 0);
            pageTitle.Dock = DockStyle.Fill;
            pageTitle.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            pageTitle.TextAlign = ContentAlignment.MiddleLeft;
            header.Controls.Add(pageTitle, 0, 1);
            header.Controls.Add(new Label
            {
                Text = "Chọn nghiệp vụ để bắt đầu xử lý công việc.",
                Dock = DockStyle.Fill, ForeColor = UiStyle.MutedText
            }, 0, 2);
            workspace.Controls.Add(header, 0, 0);

            var filter = new TableLayoutPanel
            {
                Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 2,
                Margin = new Padding(0, 6, 0, 8)
            };
            filter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            filter.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 210F));
            filter.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            filter.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            filter.Controls.Add(new Label
            {
                Text = "Tìm chức năng trong nhóm đang chọn", Dock = DockStyle.Fill,
                ForeColor = UiStyle.MutedText
            }, 0, 0);
            searchBox.Dock = DockStyle.Fill;
            searchBox.BorderStyle = BorderStyle.FixedSingle;
            searchBox.Font = new Font("Segoe UI", 10F);
            searchBox.AccessibleName = "Tìm chức năng";
            hints.SetToolTip(searchBox, "Ví dụ: tour, khách lẻ, hướng dẫn viên. Ctrl+F để tìm; Esc để xóa.");
            filter.Controls.Add(searchBox, 0, 1);
            resultLabel.Dock = DockStyle.Fill;
            resultLabel.TextAlign = ContentAlignment.MiddleRight;
            resultLabel.ForeColor = UiStyle.Primary;
            filter.Controls.Add(resultLabel, 1, 1);
            workspace.Controls.Add(filter, 0, 1);

            featureHost.Dock = DockStyle.Fill;
            featureHost.AutoScroll = true;
            featureHost.Margin = Padding.Empty;
            workspace.Controls.Add(featureHost, 0, 2);
            statusLabel.Text = "Ctrl+F: tìm chức năng   |   Ctrl+1…8: mở nghiệp vụ";
            statusLabel.Dock = DockStyle.Fill;
            statusLabel.TextAlign = ContentAlignment.MiddleLeft;
            statusLabel.ForeColor = UiStyle.MutedText;
            statusLabel.AutoEllipsis = true;
            workspace.Controls.Add(statusLabel, 0, 3);
            return workspace;
        }

        private void SelectGroup(string group)
        {
            selectedGroup = group;
            pageTitle.Text = group == "Tổng quan" ? "Điều phối hành trình" : group;
            foreach (Button button in navigationButtons)
            {
                bool selected = Convert.ToString(button.Tag) == group;
                button.BackColor = selected ? UiStyle.Primary : UiStyle.Sidebar;
                button.ForeColor = selected ? UiStyle.Accent : Color.White;
            }
            RenderFeatures();
        }

        private void RenderFeatures()
        {
            var visible = new List<Feature>();
            string query = searchBox.Text.Trim().Replace('đ', 'd').Replace('Đ', 'D');
            var comparison = CultureInfo.GetCultureInfo("vi-VN").CompareInfo;
            foreach (Feature feature in features)
            {
                bool sameGroup = selectedGroup == "Tổng quan" || feature.Group == selectedGroup;
                string searchable = feature.Title + " " + feature.Description + " " + feature.Group;
                bool matches = comparison.IndexOf(searchable.Replace('đ', 'd').Replace('Đ', 'D'), query,
                    CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace) >= 0;
                if (sameGroup && matches) visible.Add(feature);
            }

            featureHost.SuspendLayout();
            // Hủy thẻ cũ để không tích lũy control khi tìm nhiều lần.
            while (featureHost.Controls.Count > 0) featureHost.Controls[0].Dispose();
            columnCount = GetColumnCount();
            resultLabel.Text = visible.Count + " chức năng";
            if (visible.Count == 0)
            {
                featureHost.Controls.Add(new Label
                {
                    Text = "Không tìm thấy chức năng.\nThử từ khóa khác hoặc chọn Tổng quan.",
                    Dock = DockStyle.Top, Height = 80, Padding = new Padding(12),
                    ForeColor = UiStyle.MutedText
                });
            }
            else
            {
                int rows = (visible.Count + columnCount - 1) / columnCount;
                int rowHeight = Math.Max(154, Font.Height * 10);
                var cards = new TableLayoutPanel
                {
                    Dock = DockStyle.Top, ColumnCount = columnCount, RowCount = rows,
                    Height = rows * rowHeight, Margin = Padding.Empty,
                    GrowStyle = TableLayoutPanelGrowStyle.FixedSize
                };
                for (int i = 0; i < columnCount; i++)
                    cards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / columnCount));
                for (int i = 0; i < rows; i++)
                    cards.RowStyles.Add(new RowStyle(SizeType.Absolute, rowHeight));
                for (int i = 0; i < visible.Count; i++)
                    cards.Controls.Add(BuildFeatureCard(visible[i]), i % columnCount, i / columnCount);
                featureHost.Controls.Add(cards);
            }
            featureHost.AutoScrollPosition = Point.Empty;
            featureHost.ResumeLayout();
        }

        private int GetColumnCount()
        {
            // Trừ sẵn chỗ cho thanh cuộn để số cột không đổi qua lại khi cuộn xuất hiện.
            return featureHost.Width - SystemInformation.VerticalScrollBarWidth >= 740 ? 2 : 1;
        }

        private Control BuildFeatureCard(Feature feature)
        {
            int shortcut = Array.IndexOf(features, feature) + 1;
            var card = new TableLayoutPanel
            {
                Dock = DockStyle.Fill, BackColor = UiStyle.Surface,
                ColumnCount = 1, RowCount = 3, Padding = new Padding(14, 8, 14, 10),
                Margin = new Padding(0, 0, 10, 10), CellBorderStyle = TableLayoutPanelCellBorderStyle.None
            };
            card.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            card.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            card.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            card.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            var title = new Label
            {
                Text = feature.Code + "  /  " + feature.Title, Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = UiStyle.Primary, TextAlign = ContentAlignment.MiddleLeft,
                AutoEllipsis = true
            };
            hints.SetToolTip(title, feature.Title);
            card.Controls.Add(title, 0, 0);
            card.Controls.Add(new Label
            {
                Text = feature.Description, Dock = DockStyle.Fill,
                ForeColor = UiStyle.MutedText, AutoEllipsis = true
            }, 0, 1);
            var actions = new TableLayoutPanel
            {
                Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1, Margin = Padding.Empty
            };
            actions.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 136F));
            actions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            actions.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            var open = new Button
            {
                Text = "Mở màn hình", Dock = DockStyle.Fill, Margin = Padding.Empty,
                AccessibleName = "Mở " + feature.Title
            };
            UiStyle.SetButtonStyle(open, UiStyle.Primary, Color.White);
            open.Click += (sender, e) => OpenFeature(feature);
            actions.Controls.Add(open, 0, 0);
            actions.Controls.Add(new Label
            {
                Text = "Ctrl+" + shortcut, Dock = DockStyle.Fill,
                ForeColor = UiStyle.MutedText, TextAlign = ContentAlignment.MiddleRight
            }, 1, 0);
            card.Controls.Add(actions, 0, 2);
            return card;
        }

        private void OpenFeature(Feature feature)
        {
            try
            {
                using (Form form = feature.CreateForm())
                {
                    statusLabel.Text = "Đang làm việc: " + feature.Title;
                    form.ShowDialog(this);
                }
                statusLabel.Text = "Vừa đóng: " + feature.Title + "   |   Ctrl+F để tìm chức năng";
            }
            catch (Exception ex)
            {
                statusLabel.Text = "Không mở được: " + feature.Title;
                MessageBox.Show(this,
                    "Không mở được màn hình " + feature.Title +
                    ".\nHãy kiểm tra kết nối SQL Server và dữ liệu.\n\nChi tiết: " + ex.Message,
                    "Mở nghiệp vụ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void CheckConnection(object sender, EventArgs e)
        {
            connectionButton.Enabled = false;
            connectionLabel.Text = "SQL Server\nĐang kiểm tra...";
            try
            {
                // Truy vấn ở luồng nền để giao diện vẫn phản hồi khi SQL chậm.
                object database = await Task.Run(() => Data.Db.Scalar("SELECT DB_NAME()"));
                if (IsDisposed) return;
                connectionLabel.Text = "SQL: kết nối thành công";
                hints.SetToolTip(connectionLabel, "Cơ sở dữ liệu: " + Convert.ToString(database));
                statusLabel.Text = "Đã kết nối cơ sở dữ liệu: " + Convert.ToString(database);
            }
            catch (Exception ex)
            {
                if (IsDisposed) return;
                connectionLabel.Text = "SQL Server\nKết nối chưa thành công";
                statusLabel.Text = "Kiểm tra Data Source trong App.config và dịch vụ SQL Server.";
                MessageBox.Show(this, ex.Message, "Kiểm tra kết nối",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                if (!IsDisposed) connectionButton.Enabled = true;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.F))
            {
                searchBox.Focus();
                searchBox.SelectAll();
                return true;
            }
            if (keyData == Keys.Escape && searchBox.Focused)
            {
                searchBox.Clear();
                return true;
            }
            for (int i = 0; i < features.Length; i++)
            {
                if (keyData == (Keys.Control | (Keys)((int)Keys.D1 + i)))
                {
                    OpenFeature(features[i]);
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) hints.Dispose();
            base.Dispose(disposing);
        }
    }
}
