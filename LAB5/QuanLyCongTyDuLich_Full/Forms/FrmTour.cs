using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyCongTyDuLich.Services;

namespace QuanLyCongTyDuLich.Forms
{
    public class FrmTour : Form
    {
        private readonly TextBox ma=new TextBox(), ten=new TextBox(), ngay=new TextBox(), dem=new TextBox(), gia=new TextBox(), mota=new TextBox();
        private readonly CheckBox moban=new CheckBox{Text="Đang mở bán",Checked=true,AutoSize=true};
        private readonly DataGridView grid=new DataGridView();
        private readonly TourService service=new TourService();

        public FrmTour()
        {
            Text="Quản lý tour"; Width=1000; Height=650; StartPosition=FormStartPosition.CenterParent;
            var top=new FlowLayoutPanel{Dock=DockStyle.Top,Height=150,Padding=new Padding(10)};
            Controls.Add(grid); Controls.Add(top); grid.Dock=DockStyle.Fill; grid.ReadOnly=true; grid.AllowUserToAddRows=false; grid.AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill;
            AddField(top,"Mã tour",ma); AddField(top,"Tên tour",ten); AddField(top,"Số ngày",ngay); AddField(top,"Số đêm",dem); AddField(top,"Đơn giá",gia); AddField(top,"Mô tả",mota); top.Controls.Add(moban);
            AddButton(top,"Tải",()=>LoadData()); AddButton(top,"Thêm",()=>Run(()=>{service.Insert(ma.Text.Trim(),ten.Text.Trim(),int.Parse(ngay.Text),int.Parse(dem.Text),decimal.Parse(gia.Text),mota.Text,moban.Checked);LoadData();}));
            AddButton(top,"Sửa",()=>Run(()=>{service.Update(ma.Text.Trim(),ten.Text.Trim(),int.Parse(ngay.Text),int.Parse(dem.Text),decimal.Parse(gia.Text),mota.Text,moban.Checked);LoadData();}));
            AddButton(top,"Xóa",()=>Run(()=>{service.Delete(ma.Text.Trim());LoadData();}));
            grid.CellClick+=(s,e)=>{if(e.RowIndex>=0){var r=grid.Rows[e.RowIndex];ma.Text=Convert.ToString(r.Cells["MaTour"].Value);ten.Text=Convert.ToString(r.Cells["TenTour"].Value);ngay.Text=Convert.ToString(r.Cells["SoNgay"].Value);dem.Text=Convert.ToString(r.Cells["SoDem"].Value);gia.Text=Convert.ToString(r.Cells["DonGiaKhach"].Value);mota.Text=Convert.ToString(r.Cells["MoTa"].Value);moban.Checked=Convert.ToBoolean(r.Cells["DangMoBan"].Value);}};
            LoadData();
            UiStyle.Apply(this);
        }
        private void LoadData(){Run(()=>grid.DataSource=service.GetAll());}
        private void Run(Action a){try{a();}catch(Exception ex){MessageBox.Show(ex.Message,"Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);}}
        private void AddField(FlowLayoutPanel p,string label,Control c){var box=new Panel{Width=130,Height=55};box.Controls.Add(new Label{Text=label,Dock=DockStyle.Top,Height=20});c.Dock=DockStyle.Bottom;box.Controls.Add(c);p.Controls.Add(box);}
        private void AddButton(FlowLayoutPanel p,string text,Action a){var b=new Button{Text=text,Width=75,Height=30,Margin=new Padding(4,20,4,0)};b.Click+=(s,e)=>a();p.Controls.Add(b);}
    }
}
