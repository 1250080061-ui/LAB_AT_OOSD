using QLChoThueVanPhong.Infrastructure;
using QLChoThueVanPhong.Ui;

namespace QLChoThueVanPhong.Forms;

public sealed class FrmBaoCaoThongKe : Form
{
    private readonly FlowLayoutPanel _cards = new();
    private readonly DataGridView _grid = new();

    public FrmBaoCaoThongKe()
    {
        Dock = DockStyle.Fill;
        FormBorderStyle = FormBorderStyle.None;
        TopLevel = false;
        BackColor = UiTheme.Background;
        Font = UiTheme.DefaultFont;
        BuildInterface();
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        LoadStatistics();
    }

    private void BuildInterface()
    {
        Label title = new()
        {
            Text = "Báo cáo - Thống kê",
            Dock = DockStyle.Top,
            Height = 72,
            Padding = new Padding(28, 18, 0, 0),
            BackColor = Color.White,
            ForeColor = UiTheme.Navy,
            Font = new Font("Segoe UI", 20F, FontStyle.Bold)
        };

        _cards.Dock = DockStyle.Top;
        _cards.Height = 150;
        _cards.Padding = new Padding(22, 22, 10, 10);
        _cards.BackColor = UiTheme.Background;
        _cards.WrapContents = false;

        Panel gridHost = new()
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(28),
            BackColor = UiTheme.Background
        };
        _grid.Dock = DockStyle.Fill;
        UiTheme.StyleGrid(_grid);
        gridHost.Controls.Add(_grid);

        Controls.Add(gridHost);
        Controls.Add(_cards);
        Controls.Add(title);
    }

    private void LoadStatistics()
    {
        try
        {
            _cards.Controls.Clear();
            _cards.Controls.Add(CreateCard(
                "Văn phòng trống",
                Convert.ToString(DatabaseHelper.Scalar("SELECT COUNT(*) FROM VanPhong WHERE TrangThai='TRONG'")) ?? "0",
                Color.FromArgb(43, 128, 185)));

            _cards.Controls.Add(CreateCard(
                "Đang cho thuê",
                Convert.ToString(DatabaseHelper.Scalar("SELECT COUNT(*) FROM VanPhong WHERE TrangThai='DANG_THUE'")) ?? "0",
                Color.FromArgb(40, 145, 92)));

            _cards.Controls.Add(CreateCard(
                "Hợp đồng hiệu lực",
                Convert.ToString(DatabaseHelper.Scalar("SELECT COUNT(*) FROM HopDong WHERE TrangThai='HIEU_LUC'")) ?? "0",
                Color.FromArgb(139, 91, 181)));

            decimal revenue = Convert.ToDecimal(DatabaseHelper.Scalar(
                "SELECT ISNULL(SUM(SoTien),0) FROM ThanhToan"));
            _cards.Controls.Add(CreateCard(
                "Tổng doanh thu",
                revenue.ToString("N0") + " đ",
                Color.FromArgb(224, 135, 45)));

            _grid.DataSource = DatabaseHelper.Query("""
                SELECT vp.TrangThai, COUNT(*) AS SoLuong
                FROM VanPhong vp
                GROUP BY vp.TrangThai
                ORDER BY vp.TrangThai
                """);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi thống kê", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private static Panel CreateCard(string title, string value, Color accent)
    {
        Panel card = new()
        {
            Width = 220,
            Height = 102,
            Margin = new Padding(8),
            BackColor = Color.White,
            Padding = new Padding(18)
        };

        Panel stripe = new()
        {
            Dock = DockStyle.Left,
            Width = 6,
            BackColor = accent
        };

        Label lblTitle = new()
        {
            Text = title,
            ForeColor = UiTheme.Muted,
            AutoSize = true,
            Location = new Point(22, 17)
        };

        Label lblValue = new()
        {
            Text = value,
            ForeColor = accent,
            Font = new Font("Segoe UI", 20F, FontStyle.Bold),
            AutoSize = true,
            Location = new Point(20, 43)
        };

        card.Controls.Add(lblValue);
        card.Controls.Add(lblTitle);
        card.Controls.Add(stripe);
        return card;
    }
}
