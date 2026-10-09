using System;
using System.Windows.Forms;
using QuanLyCongTyDuLich.Services;

namespace QuanLyCongTyDuLich.Forms
{
    public class FrmLuongThongKe : Form
    {
        private readonly DataGridView grid=new DataGridView();
        private readonly ComboBox loai=new ComboBox();
        private readonly ThongKeService service=new ThongKeService();

        public FrmLuongThongKe()
        {
            Text="Lương và thống kê";Width=1000;Height=600;StartPosition=FormStartPosition.CenterParent;
            var top=new FlowLayoutPanel{Dock=DockStyle.Top,Height=60,Padding=new Padding(10)};
            Controls.Add(grid);Controls.Add(top);grid.Dock=DockStyle.Fill;grid.ReadOnly=true;grid.AllowUserToAddRows=false;grid.AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill;
            loai.DropDownStyle=ComboBoxStyle.DropDownList;loai.Items.AddRange(new object[]{"Lương hướng dẫn viên","Doanh thu","Danh sách tour"});loai.SelectedIndex=0;
            top.Controls.Add(loai);var btn=new Button{Text="Xem thống kê",Width=130,Height=28};btn.Click+=(s,e)=>LoadData();top.Controls.Add(btn);loai.SelectedIndexChanged+=(s,e)=>LoadData();LoadData();
            UiStyle.Apply(this);
        }
        private void LoadData()
        {
            try { if(loai.SelectedIndex==0)grid.DataSource=service.LuongHDV();else if(loai.SelectedIndex==1)grid.DataSource=service.DoanhThu();else grid.DataSource=service.Tours(); }
            catch(Exception ex){MessageBox.Show(ex.Message);}
        }
    }
}
