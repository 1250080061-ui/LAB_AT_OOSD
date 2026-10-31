namespace QLChoThueVanPhong.Ui;

public static class UiTheme
{
    public static readonly Color Navy = Color.FromArgb(18, 63, 92);
    public static readonly Color Blue = Color.FromArgb(30, 112, 168);
    public static readonly Color LightBlue = Color.FromArgb(232, 243, 250);
    public static readonly Color Background = Color.FromArgb(245, 247, 250);
    public static readonly Color Text = Color.FromArgb(35, 45, 55);
    public static readonly Color Muted = Color.FromArgb(100, 112, 124);
    public static readonly Color Success = Color.FromArgb(40, 145, 92);
    public static readonly Font DefaultFont = new("Segoe UI", 10F);

    public static Button CreatePrimaryButton(string text)
    {
        return new Button
        {
            Text = text,
            BackColor = Blue,
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI", 10F, FontStyle.Bold),
            Height = 42,
            Cursor = Cursors.Hand,
            UseVisualStyleBackColor = false
        };
    }

    public static void StyleGrid(DataGridView grid)
    {
        grid.BackgroundColor = Color.White;
        grid.BorderStyle = BorderStyle.None;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.ReadOnly = true;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.MultiSelect = false;
        grid.RowHeadersVisible = false;
        grid.EnableHeadersVisualStyles = false;
        grid.ColumnHeadersDefaultCellStyle.BackColor = Navy;
        grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        grid.ColumnHeadersHeight = 38;
        grid.DefaultCellStyle.Font = DefaultFont;
        grid.DefaultCellStyle.SelectionBackColor = LightBlue;
        grid.DefaultCellStyle.SelectionForeColor = Text;
        grid.RowTemplate.Height = 34;
    }
}
