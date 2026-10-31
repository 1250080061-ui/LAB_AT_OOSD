
using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using QLNhanSu.Service;

namespace QLNhanSu.UI
{
    public class FrmDuLieuBase : Form
    {
        private readonly QuanLyService service =
            new QuanLyService();

        private readonly string tenBang;
        private readonly BindingSource binding =
            new BindingSource();

        private DataTable duLieu;
        private DataGridView dgv;
        private TextBox txtTimKiem;

        private Label lblSoBanGhi;
        private Label lblSoCot;
        private Label lblThayDoi;
        private Label lblTrangThai;

        private readonly Color navy =
            Color.FromArgb(23, 37, 63);

        private readonly Color blue =
            Color.FromArgb(53, 111, 211);

        public FrmDuLieuBase(string bang, string tieuDe)
        {
            tenBang = bang;

            Text = tieuDe;
            Font = new Font("Segoe UI", 10);
            BackColor = Color.FromArgb(245, 247, 251);

            TaoGiaoDien(tieuDe);

            Load += (s, e) => TaiDuLieu();
        }

        private void TaoGiaoDien(string tieuDe)
        {
            TableLayoutPanel layout =
                new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 1,
                    RowCount = 5,
                    Padding = new Padding(18),
                    BackColor = Color.FromArgb(245, 247, 251)
                };

            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 85));

            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 115));

            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 70));

            layout.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100));

            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 42));

            Controls.Add(layout);

            // TIEU DE
            Panel header = new Panel
            {
                Dock = DockStyle.Fill
            };

            Label title = new Label
            {
                Text = tieuDe,
                Font = new Font(
                    "Segoe UI", 20, FontStyle.Bold),
                ForeColor = navy,
                AutoSize = true,
                Location = new Point(5, 2)
            };

            Label sub = new Label
            {
                Text = "Quản lý, cập nhật và tra cứu dữ liệu",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(110, 125, 145),
                AutoSize = true,
                Location = new Point(7, 44)
            };

            header.Controls.Add(title);
            header.Controls.Add(sub);

            layout.Controls.Add(header, 0, 0);

            // CAC THE THONG KE
            TableLayoutPanel cards =
                new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 3,
                    RowCount = 1
                };

            for (int i = 0; i < 3; i++)
            {
                cards.ColumnStyles.Add(
                    new ColumnStyle(
                        SizeType.Percent, 100f / 3f));
            }

            cards.Controls.Add(
                TaoThe("TỔNG BẢN GHI", out lblSoBanGhi,
                    Color.FromArgb(53, 111, 211)), 0, 0);

            cards.Controls.Add(
                TaoThe("TRƯỜNG DỮ LIỆU", out lblSoCot,
                    Color.FromArgb(21, 156, 130)), 1, 0);

            cards.Controls.Add(
                TaoThe("THAY ĐỔI CHƯA LƯU", out lblThayDoi,
                    Color.FromArgb(230, 153, 60)), 2, 0);

            layout.Controls.Add(cards, 0, 1);

            // THANH TIM KIEM + CHUC NANG
            TableLayoutPanel toolbar =
                new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 2
                };

            toolbar.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 40));

            toolbar.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 60));

            Panel searchPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(0, 10, 10, 10)
            };

            txtTimKiem = new TextBox
            {
                Width = 275,
                Font = new Font("Segoe UI", 11),
                Location = new Point(3, 16)
            };

            txtTimKiem.TextChanged +=
                (s, e) => LocDuLieu();

            searchPanel.Controls.Add(txtTimKiem);

            Label lblSearch = new Label
            {
                Text = "Tìm kiếm",
                AutoSize = true,
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.Gray,
                Location = new Point(5, 0)
            };

            searchPanel.Controls.Add(lblSearch);

            toolbar.Controls.Add(searchPanel, 0, 0);

            FlowLayoutPanel actions =
                new FlowLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    FlowDirection = FlowDirection.RightToLeft,
                    WrapContents = false,
                    AutoScroll = true,
                    Padding = new Padding(0, 8, 0, 0)
                };

            Button btnRefresh = TaoNut(
                "Làm mới", Color.FromArgb(108, 117, 134));

            Button btnDelete = TaoNut(
                "Xóa", Color.FromArgb(214, 73, 86));

            Button btnSave = TaoNut(
                "Lưu", Color.FromArgb(35, 155, 117));

            Button btnAdd = TaoNut(
                "+ Thêm mới", blue);

            btnRefresh.Click += (s, e) => TaiDuLieu();
            btnDelete.Click += (s, e) => Xoa();
            btnSave.Click += (s, e) => Luu();
            btnAdd.Click += (s, e) => Them();

            actions.Controls.Add(btnRefresh);
            actions.Controls.Add(btnDelete);
            actions.Controls.Add(btnSave);
            actions.Controls.Add(btnAdd);

            toolbar.Controls.Add(actions, 1, 0);

            layout.Controls.Add(toolbar, 0, 2);

            // DATAGRIDVIEW
            Panel tableCard = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(2),
                BorderStyle = BorderStyle.FixedSingle
            };

            dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                AutoGenerateColumns = true,
                AllowUserToAddRows = true,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                MultiSelect = false,
                RowHeadersVisible = false,
                RowTemplate = { Height = 40 },
                SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.DisplayedCells
            };

            dgv.EnableHeadersVisualStyles = false;

            dgv.ColumnHeadersHeight = 48;
            dgv.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            dgv.ColumnHeadersDefaultCellStyle.BackColor =
                navy;

            dgv.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;

            dgv.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 10, FontStyle.Bold);

            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor =
                navy;

            dgv.DefaultCellStyle.Font =
                new Font("Segoe UI", 10);

            dgv.DefaultCellStyle.BackColor = Color.White;

            dgv.DefaultCellStyle.ForeColor =
                Color.FromArgb(50, 65, 85);

            dgv.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(216, 231, 251);

            dgv.DefaultCellStyle.SelectionForeColor =
                navy;

            dgv.DefaultCellStyle.Padding =
                new Padding(7, 0, 7, 0);

            dgv.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(246, 249, 253);

            dgv.GridColor = Color.FromArgb(230, 235, 242);

            dgv.CellBorderStyle =
                DataGridViewCellBorderStyle.SingleHorizontal;

            dgv.CellBeginEdit += KiemTraSuaKhoa;

            dgv.CellEndEdit +=
                (s, e) => CapNhatThongKe();

            dgv.UserAddedRow +=
                (s, e) => CapNhatThongKe();

            dgv.DataError += (s, e) =>
            {
                MessageBox.Show(
                    "Dữ liệu không đúng kiểu hoặc định dạng.",
                    "Dữ liệu không hợp lệ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                e.ThrowException = false;
            };

            tableCard.Controls.Add(dgv);
            layout.Controls.Add(tableCard, 0, 3);

            // FOOTER
            lblTrangThai = new Label
            {
                Dock = DockStyle.Fill,
                Text = "SQL Server | Sẵn sàng",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(105, 120, 140),
                TextAlign = ContentAlignment.MiddleLeft
            };

            layout.Controls.Add(lblTrangThai, 0, 4);
        }

        private Panel TaoThe(
            string tieuDe, out Label so, Color mau)
        {
            Panel card = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Margin = new Padding(0, 0, 13, 12)
            };

            Panel accent = new Panel
            {
                Dock = DockStyle.Left,
                Width = 5,
                BackColor = mau
            };

            Label label = new Label
            {
                Text = tieuDe,
                Font = new Font(
                    "Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.Gray,
                AutoSize = true,
                Location = new Point(22, 16)
            };

            so = new Label
            {
                Text = "0",
                Font = new Font(
                    "Segoe UI", 23, FontStyle.Bold),
                ForeColor = navy,
                AutoSize = true,
                Location = new Point(20, 37)
            };

            card.Controls.Add(label);
            card.Controls.Add(so);
            card.Controls.Add(accent);

            return card;
        }

        private Button TaoNut(string text, Color color)
        {
            Button button = new Button
            {
                Text = text,
                Width = 105,
                Height = 40,
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Font = new Font(
                    "Segoe UI", 9, FontStyle.Bold),
                Margin = new Padding(5, 0, 0, 0)
            };

            button.FlatAppearance.BorderSize = 0;
            return button;
        }

        private void TaiDuLieu()
        {
            try
            {
                if (duLieu != null &&
                    duLieu.GetChanges() != null)
                {
                    DialogResult rs = MessageBox.Show(
                        "Có thay đổi chưa lưu. Bạn muốn tải lại?",
                        "Xác nhận",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (rs == DialogResult.No)
                        return;
                }

                duLieu = service.LayDanhSach(tenBang);

                binding.DataSource = duLieu.DefaultView;
                dgv.DataSource = binding;

                txtTimKiem.Clear();

                CapNhatThongKe();

                lblTrangThai.Text =
                    "SQL Server  |  dbo." + tenBang +
                    "  |  Đã tải dữ liệu";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không tải được dữ liệu:\n" + ex.Message,
                    "Lỗi kết nối",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void LocDuLieu()
        {
            if (duLieu == null)
                return;

            try
            {
                string key = txtTimKiem.Text.Trim()
                    .Replace("'", "''")
                    .Replace("[", "[[]")
                    .Replace("%", "[%]")
                    .Replace("*", "[*]");

                DataView view = duLieu.DefaultView;

                if (string.IsNullOrWhiteSpace(key))
                {
                    view.RowFilter = "";
                }
                else
                {
                    StringBuilder filter =
                        new StringBuilder();

                    foreach (DataColumn col in duLieu.Columns)
                    {
                        if (col.DataType != typeof(string))
                            continue;

                        if (filter.Length > 0)
                            filter.Append(" OR ");

                        filter.Append(
                            "[" + col.ColumnName + "] LIKE '%" +
                            key + "%'");
                    }

                    view.RowFilter = filter.ToString();
                }

                lblTrangThai.Text =
                    "Đang hiển thị " + view.Count +
                    " bản ghi trong bảng " + tenBang;
            }
            catch (Exception)
            {
                // Khong ap dung bo loc neu tu khoa khong hop le.
                duLieu.DefaultView.RowFilter = "";
            }
        }

        private void Them()
        {
            if (duLieu == null)
                return;

            try
            {
                txtTimKiem.Clear();
                binding.AddNew();
                dgv.Focus();
                CapNhatThongKe();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Xoa()
        {
            if (dgv.CurrentRow == null ||
                dgv.CurrentRow.IsNewRow)
                return;

            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa bản ghi này?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            DataRowView row =
                dgv.CurrentRow.DataBoundItem as DataRowView;

            if (row == null)
                return;

            row.Delete();
            CapNhatThongKe();
        }

        private void Luu()
        {
            if (duLieu == null)
                return;

            try
            {
                dgv.EndEdit();
                binding.EndEdit();

                if (duLieu.GetChanges() == null)
                {
                    MessageBox.Show(
                        "Không có thay đổi cần lưu.");
                    return;
                }

                int count =
                    service.LuuThayDoi(tenBang, duLieu);

                MessageBox.Show(
                    "Lưu dữ liệu thành công!\n" +
                    "Số bản ghi thay đổi: " + count,
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                TaiDuLieu();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lưu dữ liệu thất bại:\n" + ex.Message +
                    "\nVui lòng kiểm tra khóa chính, khóa ngoại " +
                    "và các trường bắt buộc.",
                    "Lỗi dữ liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void CapNhatThongKe()
        {
            if (duLieu == null)
                return;

            int tong = 0;
            int thayDoi = 0;

            foreach (DataRow row in duLieu.Rows)
            {
                if (row.RowState != DataRowState.Deleted)
                    tong++;

                if (row.RowState == DataRowState.Added ||
                    row.RowState == DataRowState.Modified ||
                    row.RowState == DataRowState.Deleted)
                    thayDoi++;
            }

            lblSoBanGhi.Text = tong.ToString();
            lblSoCot.Text = duLieu.Columns.Count.ToString();
            lblThayDoi.Text = thayDoi.ToString();
        }

        private void KiemTraSuaKhoa(
            object sender,
            DataGridViewCellCancelEventArgs e)
        {
            if (duLieu == null ||
                e.RowIndex < 0 ||
                e.ColumnIndex < 0)
                return;

            DataRowView row =
                dgv.Rows[e.RowIndex].DataBoundItem
                as DataRowView;

            if (row == null)
                return;

            string tenCot =
                dgv.Columns[e.ColumnIndex].DataPropertyName;

            foreach (DataColumn col in duLieu.PrimaryKey)
            {
                if (col.ColumnName == tenCot &&
                    row.Row.RowState != DataRowState.Added &&
                    row.Row.RowState != DataRowState.Detached)
                {
                    e.Cancel = true;
                    return;
                }
            }
        }
    }
}
