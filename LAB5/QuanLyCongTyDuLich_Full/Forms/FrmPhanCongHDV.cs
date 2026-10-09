using System;
using System.Data;
using System.Windows.Forms;
using QuanLyCongTyDuLich.Data;
using QuanLyCongTyDuLich.Services;

namespace QuanLyCongTyDuLich.Forms
{
    public class FrmPhanCongHDV : Form
    {
        private readonly TextBox maPC=new TextBox(),maDoiTuong=new TextBox(),luong=new TextBox();
        private readonly ComboBox hdv=new ComboBox(),loai=new ComboBox();
        private readonly DateTimePicker bd=new DateTimePicker(),kt=new DateTimePicker();
        private readonly DataGridView grid=new DataGridView();
        private readonly PhanCongService service=new PhanCongService();

        public FrmPhanCongHDV()
        {
            Text="Phân công hướng dẫn viên";Width=1100;Height=620;StartPosition=FormStartPosition.CenterParent;
            var top=new FlowLayoutPanel{Dock=DockStyle.Top,Height=140,Padding=new Padding(10)};
            Controls.Add(grid);Controls.Add(top);grid.Dock=DockStyle.Fill;grid.ReadOnly=true;grid.AllowUserToAddRows=false;grid.AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill;
            hdv.DropDownStyle=ComboBoxStyle.DropDownList;loai.DropDownStyle=ComboBoxStyle.DropDownList;loai.Items.AddRange(new object[]{"LE","DOAN"});loai.SelectedIndex=0;
            Field(top,"Mã phân công",maPC);Field(top,"Hướng dẫn viên",hdv);Field(top,"Loại (LE/DOAN)",loai);Field(top,"Mã chuyến hoặc đăng ký đoàn",maDoiTuong);Field(top,"Ngày bắt đầu",bd);Field(top,"Ngày kết thúc",kt);Field(top,"Thù lao",luong);
            Button(top,"Tải",LoadData);Button(top,"Phân công",Assign);
            hdv.DataSource=service.GetGuides();hdv.DisplayMember="HoTen";hdv.ValueMember="MaHDV";LoadData();
            UiStyle.Apply(this);
        }
        private void Assign()
        {
            try
            {
                string kind=Convert.ToString(loai.SelectedItem);string code=maDoiTuong.Text.Trim();
                var conflict=Db.Scalar("SELECT COUNT(*) FROM PhanCongHDV WHERE MaHDV=@h AND @bd<=NgayKetThuc AND @kt>=NgayBatDau",Db.P("@h",hdv.SelectedValue),Db.P("@bd",bd.Value.Date),Db.P("@kt",kt.Value.Date));
                if(Convert.ToInt32(conflict)>0)throw new Exception("Hướng dẫn viên bị trùng lịch.");
                if(kind=="LE")
                    Db.Execute("INSERT INTO PhanCongHDV(MaPC,MaHDV,LoaiDoiTuong,MaChuyen,SoDKDoan,NgayBatDau,NgayKetThuc,ThuLaoTour) VALUES(@pc,@hdv,'LE',@code,NULL,@bd,@kt,@luong)",Db.P("@pc",maPC.Text.Trim()),Db.P("@hdv",hdv.SelectedValue),Db.P("@code",code),Db.P("@bd",bd.Value.Date),Db.P("@kt",kt.Value.Date),Db.P("@luong",decimal.Parse(luong.Text)));
                else
                    Db.Execute("INSERT INTO PhanCongHDV(MaPC,MaHDV,LoaiDoiTuong,MaChuyen,SoDKDoan,NgayBatDau,NgayKetThuc,ThuLaoTour) VALUES(@pc,@hdv,'DOAN',NULL,@code,@bd,@kt,@luong)",Db.P("@pc",maPC.Text.Trim()),Db.P("@hdv",hdv.SelectedValue),Db.P("@code",code),Db.P("@bd",bd.Value.Date),Db.P("@kt",kt.Value.Date),Db.P("@luong",decimal.Parse(luong.Text)));
                MessageBox.Show("Phân công thành công.");LoadData();
            }
            catch(Exception ex){MessageBox.Show(ex.Message,"Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);}
        }
        private void LoadData(){try{grid.DataSource=service.GetAll();}catch(Exception ex){MessageBox.Show(ex.Message);}}
        private void Field(FlowLayoutPanel p,string l,Control c){var x=new Panel{Width=170,Height=55};x.Controls.Add(new Label{Text=l,Dock=DockStyle.Top,Height=20});c.Dock=DockStyle.Bottom;x.Controls.Add(c);p.Controls.Add(x);}
        private void Button(FlowLayoutPanel p,string t,Action a){var b=new System.Windows.Forms.Button{Text=t,Width=100,Height=28,Margin=new Padding(5,20,5,0)};b.Click+=(s,e)=>a();p.Controls.Add(b);}
    }
}
