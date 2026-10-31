using System.Data;
using QLChoThueVanPhong.Infrastructure;
using QLChoThueVanPhong.Ui;

namespace QLChoThueVanPhong.Forms;

public abstract class ListFormBase : Form
{
    private readonly Label _lblTitle = new();
    private readonly TextBox _txtSearch = new();
    private readonly Label _lblStatus = new();
    protected readonly DataGridView Grid = new();
    private DataTable? _data;

    protected abstract string ScreenTitle { get; }
    protected abstract string QueryText { get; }

    protected ListFormBase()
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
        _lblTitle.Text = ScreenTitle;
        ReloadData();
    }

    private void BuildInterface()
    {
        Panel header = new()
        {
            Dock = DockStyle.Top,
            Height = 92,
            BackColor = Color.White,
            Padding = new Padding(28, 20, 28, 12)
        };

        _lblTitle.AutoSize = true;
        _lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
        _lblTitle.ForeColor = UiTheme.Navy;
        _lblTitle.Location = new Point(26, 17);

        Label lblSearch = new()
        {
            Text = "Tìm kiếm:",
            AutoSize = true,
            ForeColor = UiTheme.Muted,
            Location = new Point(28, 60)
        };

        _txtSearch.SetBounds(105, 55, 270, 30);
        _txtSearch.TextChanged += (_, _) => ApplyFilter();

        Button btnRefresh = UiTheme.CreatePrimaryButton("Làm mới");
        btnRefresh.SetBounds(390, 52, 105, 34);
        btnRefresh.Height = 34;
        btnRefresh.Click += (_, _) => ReloadData();

        _lblStatus.AutoSize = true;
        _lblStatus.ForeColor = UiTheme.Muted;
        _lblStatus.Location = new Point(520, 62);

        header.Controls.AddRange([_lblTitle, lblSearch, _txtSearch, btnRefresh, _lblStatus]);

        Panel gridHost = new()
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(28),
            BackColor = UiTheme.Background
        };

        Grid.Dock = DockStyle.Fill;
        UiTheme.StyleGrid(Grid);
        gridHost.Controls.Add(Grid);

        Controls.Add(gridHost);
        Controls.Add(header);
    }

    private void ReloadData()
    {
        try
        {
            _data = DatabaseHelper.Query(QueryText);
            Grid.DataSource = _data;
            _lblStatus.ForeColor = UiTheme.Success;
            _lblStatus.Text = $"{_data.Rows.Count} bản ghi";
            ApplyFilter();
        }
        catch (Exception ex)
        {
            _lblStatus.ForeColor = Color.Firebrick;
            _lblStatus.Text = "Không thể tải dữ liệu";
            Grid.DataSource = null;
            MessageBox.Show(ex.Message, "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ApplyFilter()
    {
        if (_data == null)
        {
            return;
        }

        string keyword = _txtSearch.Text.Trim().Replace("'", "''");
        if (keyword.Length == 0)
        {
            _data.DefaultView.RowFilter = string.Empty;
            return;
        }

        IEnumerable<string> filters = _data.Columns
            .Cast<DataColumn>()
            .Select(column =>
                $"CONVERT([{column.ColumnName}], 'System.String') LIKE '%{keyword}%'");

        _data.DefaultView.RowFilter = string.Join(" OR ", filters);
    }
}
