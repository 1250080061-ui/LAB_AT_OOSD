using System;
using System.Data;
using System.Windows.Forms;
using QuanLyCongTyDuLich.Data;
using QuanLyCongTyDuLich.Services;

namespace QuanLyCongTyDuLich.Forms
{
    public class FrmDangKyDoan : Form
    {
        private readonly TextBox so=new TextBox(),maDoan=new TextBox(),soNguoi=new TextBox(),diaDiem=new TextBox(),coc=new TextBox(),tong=new TextBox();
        private readonly ComboBox tour=new ComboBox();
        private readonly DateTimePicker di=new DateTimePicker(),ve=new DateTimePicker();
        private readonly CheckBox baoHiem=new CheckBox{Text="Mua bảo hiểm",AutoSize=true};
        private readonly DataGridView grid=new DataGridView();
        private readonly DangKyDoanService service=new DangKyDoanService();
        private DataTable tours;

        public FrmDangKyDoan()
        {
            Text="Đăng ký đoàn khách";Width=1100;Height=650;StartPosition=FormStartPosition.CenterParent;
            var top=new FlowLayoutPanel{Dock=DockStyle.Top,Height=160,Padding=new Padding(10)};
            Controls.Add(grid);Controls.Add(top);grid.Dock=DockStyle.Fill;grid.ReadOnly=true;grid.AllowUserToAddRows=false;grid.AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill;
            tour.DropDownStyle=ComboBoxStyle.DropDownList;tour.Width=180;
            Field(top,"Số đăng ký",so);Field(top,"Mã đoàn (tạo trước)",maDoan);Field(top,"Tour",tour);Field(top,"Ngày đi",di);Field(top,"Ngày kết thúc",ve);Field(top,"Số người (>12)",soNguoi);Field(top,"Điểm đón",diaDiem);Field(top,"Tiền cọc",coc);Field(top,"Tổng tiền",tong);top.Controls.Add(baoHiem);
            Button(top,"Tải",LoadData);Button(top,"Đăng ký đoàn",Add);
            tours=service.GetTours();tour.DataSource=tours;tour.DisplayMember="TenTour";tour.ValueMember="MaTour";LoadData();
            UiStyle.Apply(this);
        }
        private void Add()
        {
            try
            {
                var n=int.Parse(soNguoi.Text);var deposit=decimal.Parse(coc.Text);var total=decimal.Parse(tong.Text);
                Db.Execute("IF NOT EXISTS(SELECT 1 FROM DoanKhach WHERE MaDoan=@ma) INSERT INTO DoanKhach(MaDoan,TenCoQuanDaiDien,DiaChi,DienThoai,NguoiDaiDien) VALUES(@ma,@ten,@dia,@dt,@nd)",
                    Db.P("@ma",maDoan.Text.Trim()),Db.P("@ten",maDoan.Text.Trim()),Db.P("@dia",diaDiem.Text.Trim()),Db.P("@dt","0000000000"),Db.P("@nd",maDoan.Text.Trim()));
                var tourId=Convert.ToString(tour.SelectedValue);
                Db.Execute(@"INSERT INTO DangKyDoan(SoDKDoan,MaDoan,MaTour,NgayDangKy,NgayDi,NgayKetThucDuKien,SoNguoi,DiaDiemDon,MuaBaoHiem,TienCoc,DaThanhToanCoc,TongTienDuKien,TrangThai)
                             VALUES(@so,@doan,@tour,SYSDATETIME(),@di,@ve,@n,@dd,@bh,@coc,1,@tong,N'Đã đăng ký')",
                    Db.P("@so",so.Text.Trim()),Db.P("@doan",maDoan.Text.Trim()),Db.P("@tour",tourId),Db.P("@di",di.Value.Date),Db.P("@ve",ve.Value.Date),Db.P("@n",n),Db.P("@dd",diaDiem.Text.Trim()),Db.P("@bh",baoHiem.Checked),Db.P("@coc",deposit),Db.P("@tong",total));
                MessageBox.Show("Đăng ký đoàn thành công.");LoadData();
            }
            catch(Exception ex){MessageBox.Show(ex.Message,"Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);}
        }
        private void LoadData(){try{grid.DataSource=service.GetAll();}catch(Exception ex){MessageBox.Show(ex.Message);}}
        private void Field(FlowLayoutPanel p,string l,Control c){var x=new Panel{Width=150,Height=55};x.Controls.Add(new Label{Text=l,Dock=DockStyle.Top,Height=20});c.Dock=DockStyle.Bottom;x.Controls.Add(c);p.Controls.Add(x);}
        private void Button(FlowLayoutPanel p,string t,Action a){var b=new System.Windows.Forms.Button{Text=t,Width=110,Height=28,Margin=new Padding(5,20,5,0)};b.Click+=(s,e)=>a();p.Controls.Add(b);}
    }
}
