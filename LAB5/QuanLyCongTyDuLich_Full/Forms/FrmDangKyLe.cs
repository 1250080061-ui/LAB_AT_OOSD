using System;
using System.Windows.Forms;
using QuanLyCongTyDuLich.Services;

namespace QuanLyCongTyDuLich.Forms
{
    public class FrmDangKyLe : Form
    {
        private readonly TextBox so=new TextBox(),ten=new TextBox(),dt=new TextBox(),nguoi=new TextBox();
        private readonly ComboBox chuyen=new ComboBox(),diemBan=new ComboBox();
        private readonly DataGridView grid=new DataGridView();
        private readonly Label lblGia=new Label{AutoSize=true,Text="Đơn giá: 0"};
        private readonly DangKyLeService service=new DangKyLeService();
        private System.Data.DataTable chuyens;

        public FrmDangKyLe()
        {
            Text="Đăng ký khách lẻ";Width=1000;Height=620;StartPosition=FormStartPosition.CenterParent;
            var top=new FlowLayoutPanel{Dock=DockStyle.Top,Height=145,Padding=new Padding(10)};
            Controls.Add(grid);Controls.Add(top);grid.Dock=DockStyle.Fill;grid.ReadOnly=true;grid.AllowUserToAddRows=false;grid.AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill;
            chuyen.DropDownStyle=ComboBoxStyle.DropDownList;chuyen.Width=220;diemBan.DropDownStyle=ComboBoxStyle.DropDownList;diemBan.Width=170;
            Field(top,"Số đăng ký",so);Field(top,"Chuyến",chuyen);Field(top,"Điểm bán",diemBan);Field(top,"Người đăng ký",ten);Field(top,"Điện thoại",dt);Field(top,"Số người (1-11)",nguoi);top.Controls.Add(lblGia);
            Button(top,"Tải",LoadData);Button(top,"Đăng ký và thanh toán",Register);
            chuyens=service.GetChuyen();chuyen.DataSource=chuyens;chuyen.DisplayMember="TenTour";chuyen.ValueMember="MaChuyen";
            diemBan.DataSource=service.GetDiemBan();diemBan.DisplayMember="TenDiemBan";diemBan.ValueMember="MaDiemBan";
            chuyen.SelectedIndexChanged+=(s,e)=>UpdateGia();UpdateGia();LoadData();
            UiStyle.Apply(this);
        }
        private void UpdateGia(){if(chuyen.SelectedIndex>=0){var row=chuyens.Rows[chuyen.SelectedIndex];lblGia.Text="Đơn giá: "+Convert.ToDecimal(row["DonGiaKhach"]).ToString("N0")+" VNĐ";}}
        private void Register(){try{var row=chuyens.Rows[chuyen.SelectedIndex];var n=int.Parse(nguoi.Text);var gia=Convert.ToDecimal(row["DonGiaKhach"]);service.Register(so.Text.Trim(),Convert.ToString(chuyen.SelectedValue),Convert.ToString(diemBan.SelectedValue),ten.Text.Trim(),dt.Text.Trim(),n,gia);MessageBox.Show("Đăng ký thành công. Tổng tiền: "+(gia*n).ToString("N0")+" VNĐ");LoadData();}catch(Exception ex){MessageBox.Show(ex.Message,"Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);}}
        private void LoadData(){try{grid.DataSource=service.GetAll();}catch(Exception ex){MessageBox.Show(ex.Message);}}
        private void Field(FlowLayoutPanel p,string l,Control c){var x=new Panel{Width=150,Height=55};x.Controls.Add(new Label{Text=l,Dock=DockStyle.Top,Height=20});c.Dock=DockStyle.Bottom;x.Controls.Add(c);p.Controls.Add(x);}
        private void Button(FlowLayoutPanel p,string t,Action a){var b=new System.Windows.Forms.Button{Text=t,Width=145,Height=28,Margin=new Padding(5,20,5,0)};b.Click+=(s,e)=>a();p.Controls.Add(b);}
    }
}
