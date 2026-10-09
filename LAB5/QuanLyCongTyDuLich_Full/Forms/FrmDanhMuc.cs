using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyCongTyDuLich.Services;

namespace QuanLyCongTyDuLich.Forms
{
    public class FrmDanhMuc : Form
    {
        private readonly ComboBox cbo = new ComboBox();
        private readonly TextBox txtMa = new TextBox();
        private readonly TextBox txtTen = new TextBox();
        private readonly TextBox txtTT = new TextBox();
        private readonly TextBox txtTT2 = new TextBox();
        private readonly DataGridView grid = new DataGridView();
        private readonly DanhMucService service = new DanhMucService();

        public FrmDanhMuc()
        {
            Text = "Quản lý danh mục";
            Width = 1000; Height = 650; StartPosition = FormStartPosition.CenterParent;
            var top = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 150, Padding = new Padding(10), AutoSize = false };
            Controls.Add(grid); Controls.Add(top);
            grid.Dock = DockStyle.Fill; grid.ReadOnly = true; grid.AllowUserToAddRows = false; grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            cbo.DropDownStyle = ComboBoxStyle.DropDownList; cbo.Width = 190;
            cbo.Items.AddRange(new object[] { "Phương tiện", "Điểm bán vé", "Hướng dẫn viên", "Điểm tham quan" }); cbo.SelectedIndex = 0;
            AddField(top, "Loại danh mục", cbo); AddField(top, "Mã", txtMa); AddField(top, "Tên", txtTen); AddField(top, "Thông tin", txtTT); AddField(top, "Thông tin 2", txtTT2);
            AddButton(top, "Tải", (s,e)=>LoadData());
            AddButton(top, "Thêm", (s,e)=>Run(()=>{ service.Add(TableName(),txtMa.Text.Trim(),txtTen.Text.Trim(),txtTT.Text.Trim(),txtTT2.Text.Trim()); LoadData(); }));
            AddButton(top, "Xóa", (s,e)=>Run(()=>{ service.Delete(TableName(),txtMa.Text.Trim()); LoadData(); }));
            cbo.SelectedIndexChanged += (s,e)=>LoadData();
            grid.CellClick += (s,e)=>{ if(e.RowIndex>=0){ txtMa.Text=Convert.ToString(grid.Rows[e.RowIndex].Cells[0].Value); if(grid.Columns.Count>1) txtTen.Text=Convert.ToString(grid.Rows[e.RowIndex].Cells[1].Value); } };
            LoadData();
            UiStyle.Apply(this);
        }

        private string TableName()
        {
            switch (cbo.SelectedIndex) { case 0:return "PhuongTien"; case 1:return "DiemBanVe"; case 2:return "HuongDanVien"; default:return "DiemThamQuan"; }
        }
        private void LoadData() { Run(()=>grid.DataSource=service.Get(TableName())); }
        private void Run(Action action) { try { action(); } catch(Exception ex){ MessageBox.Show(ex.Message,"Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Warning); } }
        private void AddField(FlowLayoutPanel p,string label,Control c){ var box=new Panel{Width=180,Height=55}; box.Controls.Add(new Label{Text=label,Dock=DockStyle.Top,Height=20}); c.Dock=DockStyle.Bottom; box.Controls.Add(c); p.Controls.Add(box); }
        private void AddButton(FlowLayoutPanel p,string text,EventHandler handler){var b=new Button{Text=text,Width=90,Height=30,Margin=new Padding(5,20,5,0)};b.Click+=handler;p.Controls.Add(b);}
    }
}
