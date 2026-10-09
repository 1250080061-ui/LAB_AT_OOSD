using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyCongTyDuLich.Forms
{
    public static class UiStyle
    {
        // Bảng màu chung: xanh rêu, nền kem và điểm nhấn vàng đất.
        public static readonly Color Sidebar = Color.FromArgb(30, 49, 42);
        public static readonly Color Primary = Color.FromArgb(53, 93, 73);
        public static readonly Color Accent = Color.FromArgb(228, 191, 128);
        public static readonly Color Background = Color.FromArgb(245, 243, 236);
        public static readonly Color Surface = Color.FromArgb(255, 255, 252);
        public static readonly Color Border = Color.FromArgb(223, 226, 216);
        public static readonly Color TextColor = Color.FromArgb(41, 52, 46);
        public static readonly Color MutedText = Color.FromArgb(100, 110, 100);
        private static readonly Color PaleGreen = Color.FromArgb(226, 237, 227);
        private static readonly Color Danger = Color.FromArgb(164, 70, 54);

        public static void Apply(Form form)
        {
            form.SuspendLayout();
            form.BackColor = Background;
            form.Font = new Font("Segoe UI", 9F);
            form.ForeColor = TextColor;
            StyleControls(form.Controls);
            form.ResumeLayout(true);
        }

        public static void SetButtonStyle(Button button, Color background, Color foreground)
        {
            button.UseVisualStyleBackColor = false;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = background;
            button.ForeColor = foreground;
            button.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            button.Cursor = Cursors.Hand;
            button.Padding = new Padding(8, 0, 8, 0);
            button.FlatAppearance.MouseOverBackColor = ControlPaint.Light(background, 0.15F);
            button.FlatAppearance.MouseDownBackColor = ControlPaint.Dark(background, 0.08F);
        }

        private static void StyleControls(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                var grid = control as DataGridView;
                var button = control as Button;
                var flow = control as FlowLayoutPanel;
                if (grid != null)
                {
                    StyleGrid(grid);
                }
                else if (button != null)
                {
                    StyleActionButton(button);
                }
                else if (control is TextBox)
                {
                    var textBox = (TextBox)control;
                    textBox.BorderStyle = BorderStyle.FixedSingle;
                    textBox.BackColor = textBox.ReadOnly ? Background : Surface;
                    textBox.ForeColor = TextColor;
                    textBox.Font = new Font("Segoe UI", 9F);
                    // Không ép Height để giữ được TextBox nhiều dòng và Dock hiện có.
                }
                else if (control is ComboBox)
                {
                    var combo = (ComboBox)control;
                    combo.FlatStyle = FlatStyle.Standard;
                    combo.BackColor = Surface;
                    combo.ForeColor = TextColor;
                    combo.Font = new Font("Segoe UI", 9F);
                    combo.DropDownWidth = Math.Max(combo.DropDownWidth, 220);
                }
                else if (control is DateTimePicker)
                {
                    var date = (DateTimePicker)control;
                    date.Font = new Font("Segoe UI", 9F);
                    date.CalendarMonthBackground = Surface;
                    date.CalendarForeColor = TextColor;
                    date.CalendarTitleBackColor = Primary;
                    date.CalendarTitleForeColor = Color.White;
                }
                else if (control is Label || control is CheckBox || control is RadioButton)
                {
                    control.ForeColor = TextColor;
                    control.Font = new Font("Segoe UI", 9F);
                }
                else if (flow != null)
                {
                    flow.BackColor = Background;
                    // Các form dùng vùng nhập cao cố định; cho phép cuộn khi xuống dòng.
                    flow.AutoScroll = true;
                }
                else if (control is Panel || control is GroupBox || control is TabPage)
                {
                    control.BackColor = Surface;
                    control.ForeColor = TextColor;
                }
                // DataGridView tự quản lý các control soạn thảo bên trong.
                if (grid == null) StyleControls(control.Controls);
            }
        }

        private static void StyleActionButton(Button button)
        {
            string caption = button.Text.Trim();
            Color background = Primary;
            Color foreground = Color.White;
            if (caption.StartsWith("Xóa", StringComparison.OrdinalIgnoreCase))
                background = Danger;
            else if (caption.StartsWith("Sửa", StringComparison.OrdinalIgnoreCase))
            {
                background = Accent;
                foreground = TextColor;
            }
            else if (caption.StartsWith("Tải", StringComparison.OrdinalIgnoreCase) ||
                     caption.StartsWith("Xem", StringComparison.OrdinalIgnoreCase))
            {
                background = PaleGreen;
                foreground = Primary;
            }
            SetButtonStyle(button, background, foreground);
            button.MinimumSize = new Size(button.Width, Math.Max(button.Height, 34));
            button.AutoSize = true;
            button.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        }

        private static void StyleGrid(DataGridView grid)
        {
            grid.BackgroundColor = Surface;
            grid.BorderStyle = BorderStyle.None;
            grid.GridColor = Border;
            grid.EnableHeadersVisualStyles = false;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Primary;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = Primary;
            grid.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            grid.ColumnHeadersHeight = 38;
            grid.RowTemplate.Height = 32;
            grid.RowHeadersVisible = false;
            grid.DefaultCellStyle.BackColor = Surface;
            grid.DefaultCellStyle.ForeColor = TextColor;
            grid.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            grid.DefaultCellStyle.SelectionBackColor = PaleGreen;
            grid.DefaultCellStyle.SelectionForeColor = TextColor;
            grid.AlternatingRowsDefaultCellStyle.BackColor = Background;
            grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
            grid.ScrollBars = ScrollBars.Both;
            SetColumnWidths(grid);
            grid.DataBindingComplete -= GridDataBindingComplete;
            grid.DataBindingComplete += GridDataBindingComplete;
        }

        private static void GridDataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            SetColumnWidths((DataGridView)sender);
        }

        private static void SetColumnWidths(DataGridView grid)
        {
            if (grid == null || grid.IsDisposed)
                return;

            // Tạm bỏ việc chỉnh MinimumWidth
            // để kiểm tra lỗi khi khởi tạo DataGridView.
        }
    }
}
