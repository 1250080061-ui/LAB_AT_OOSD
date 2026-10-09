using System;
using System.Windows.Forms;
using QuanLyCongTyDuLich.Services;

namespace QuanLyCongTyDuLich.Forms
{
    public class FrmChuyenLe : Form
    {
        private readonly TextBox ma=new TextBox(), don=new TextBox();
        private readonly ComboBox tour=new ComboBox(), trangThai=new ComboBox();
        private readonly DateTimePicker di=new DateTimePicker(), ve=new DateTimePicker();
        private readonly DataGridView grid=new DataGridView();
        private readonly ChuyenLeService service=new ChuyenLeService();

        public FrmChuyenLe()
        {
            Text="Quản lý chuyến khách lẻ"; Width=1000; Height=620; StartPosition=FormStartPosition.CenterParent;
            var top=new FlowLayoutPanel{Dock=DockStyle.Top,Height=145,Padding=new Padding(10)};
            Controls.Add(grid);Controls.Add(top);grid.Dock=DockStyle.Fill;grid.ReadOnly=true;grid.AllowUserToAddRows=false;grid.AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill;
            tour.DropDownStyle=ComboBoxStyle.DropDownList;tour.Width=200;di.Format=DateTimePickerFormat.Short;ve.Format=DateTimePickerFormat.Short;
            trangThai.DropDownStyle=ComboBoxStyle.DropDownList;trangThai.Items.AddRange(new object[]{"Mở đăng ký","Đóng đăng ký"});trangThai.SelectedIndex=0;
            Field(top,"Mã chuyến",ma);Field(top,"Tour",tour);Field(top,"Ngày đi",di);Field(top,"Ngày về",ve);Field(top,"Điểm đón",don);Field(top,"Trạng thái",trangThai);
            Button(top,"Tải",()=>LoadData());Button(top,"Thêm",()=>Run(()=>{service.Add(ma.Text.Trim(),Convert.ToString(tour.SelectedValue),di.Value,ve.Value,don.Text.Trim(),Convert.ToString(trangThai.SelectedItem));LoadData();}));Button(top,"Xóa",()=>Run(()=>{service.Delete(ma.Text.Trim());LoadData();}));
            tour.DataSource=service.GetTours();tour.DisplayMember="TenTour";tour.ValueMember="MaTour";
            LoadData();
            UiStyle.Apply(this);
        }
        private void LoadData(){try{grid.DataSource=service.GetAll();}catch(Exception ex){MessageBox.Show(ex.Message);}}
        private void Run(Action a){try{a();}catch(Exception ex){MessageBox.Show(ex.Message,"Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);}}
        private void Field(FlowLayoutPanel p,string l,Control c){var x=new Panel{Width=150,Height=55};x.Controls.Add(new Label{Text=l,Dock=DockStyle.Top,Height=20});c.Dock=DockStyle.Bottom;x.Controls.Add(c);p.Controls.Add(x);}
        private void Button(FlowLayoutPanel p,string t,Action a){var b=new System.Windows.Forms.Button{Text=t,Width=75,Height=28,Margin=new Padding(5,20,5,0)};b.Click+=(s,e)=>a();p.Controls.Add(b);}
    }
}
