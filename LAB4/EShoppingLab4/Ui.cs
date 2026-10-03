using System.Drawing;
using System.Windows.Forms;

namespace EShoppingLab4.CodeOnly
{
    internal static class Ui
    {
        public static void Setup(Form form, string title, int width, int height)
        {
            form.Text = title;
            form.Width = width; form.Height = height;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Font = new Font("Segoe UI", 10F);
            form.BackColor = Color.White;
        }

        public static Label Label(string text, int left, int top, int width = 180, int height = 26)
        {
            return new Label { Text = text, Left = left, Top = top, Width = width, Height = height };
        }

        public static Label Title(string text, int left, int top, int width = 700)
        {
            return new Label { Text = text, Left = left, Top = top, Width = width, Height = 38,
                Font = new Font("Segoe UI", 17F, FontStyle.Bold), ForeColor = Color.FromArgb(31, 78, 121) };
        }

        public static TextBox TextBox(int left, int top, int width = 260, bool password = false)
        {
            return new TextBox { Left = left, Top = top, Width = width, UseSystemPasswordChar = password };
        }

        public static Button Button(string text, int left, int top, int width = 150, int height = 38)
        {
            return new Button { Text = text, Left = left, Top = top, Width = width, Height = height,
                BackColor = Color.FromArgb(221, 235, 247), FlatStyle = FlatStyle.Flat };
        }

        public static DataGridView Grid(int left, int top, int width, int height)
        {
            return new DataGridView { Left = left, Top = top, Width = width, Height = height,
                ReadOnly = true, AllowUserToAddRows = false, AllowUserToDeleteRows = false,
                AutoGenerateColumns = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false, BackgroundColor = Color.White, RowHeadersVisible = false };
        }

        public static void AddTextColumn(DataGridView grid, string header, string property, int width, string format = null)
        {
            var col = new DataGridViewTextBoxColumn { HeaderText = header, DataPropertyName = property, Width = width };
            if (!string.IsNullOrEmpty(format)) col.DefaultCellStyle.Format = format;
            grid.Columns.Add(col);
        }
    }
}
