using System;
using System.Data;
using System.Windows.Forms;
using QuanLyCongTyDuLich.Data;

namespace QuanLyCongTyDuLich.Forms
{
    public class FrmKetThucKhaoSat : Form
    {
        private readonly ComboBox loai=new ComboBox();
        private readonly TextBox ma=new TextBox(),diem=new TextBox(),gopy=new TextBox();
        private readonly DataGridView grid=new DataGridView();

        public FrmKetThucKhaoSat()
        {
            Text="Kết thúc tour và khảo sát";Width=1000;Height=600;StartPosition=FormStartPosition.CenterParent;
            var top=new FlowLayoutPanel{Dock=DockStyle.Top,Height=120,Padding=new Padding(10)};
            Controls.Add(grid);Controls.Add(top);grid.Dock=DockStyle.Fill;grid.ReadOnly=true;grid.AllowUserToAddRows=false;grid.AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill;
            loai.DropDownStyle=ComboBoxStyle.DropDownList;loai.Items.AddRange(new object[]{"LE","DOAN"});loai.SelectedIndex=0;
            Field(top,"Loại khách",loai);Field(top,"Mã đăng ký",ma);Field(top,"Điểm 1-5",diem);Field(top,"Góp ý",gopy);
            Button(top,"Tải khảo sát",LoadData);Button(top,"Lưu khảo sát",Save);LoadData();
            UiStyle.Apply(this);
        }
        private void Save()
        {
            try
            {
                string type=Convert.ToString(loai.SelectedItem);int score=int.Parse(diem.Text);
                if(type=="LE")
                    Db.Execute("INSERT INTO KhaoSat(MaKhaoSat,LoaiKhach,SoDKLe,SoDKDoan,NgayGui,NgayPhanHoi,DiemDanhGia,GopY) VALUES(@id,'LE',@ma,NULL,CAST(GETDATE() AS date),CAST(GETDATE() AS date),@diem,@gopy)",Db.P("@id","KS"+DateTime.Now.ToString("yyyyMMddHHmmss")),Db.P("@ma",ma.Text.Trim()),Db.P("@diem",score),Db.P("@gopy",gopy.Text.Trim()));
                else
                    Db.Execute("INSERT INTO KhaoSat(MaKhaoSat,LoaiKhach,SoDKLe,SoDKDoan,NgayGui,NgayPhanHoi,DiemDanhGia,GopY) VALUES(@id,'DOAN',NULL,@ma,CAST(GETDATE() AS date),CAST(GETDATE() AS date),@diem,@gopy)",Db.P("@id","KS"+DateTime.Now.ToString("yyyyMMddHHmmss")),Db.P("@ma",ma.Text.Trim()),Db.P("@diem",score),Db.P("@gopy",gopy.Text.Trim()));
                MessageBox.Show("Lưu khảo sát thành công.");LoadData();
            }
            catch(Exception ex){MessageBox.Show(ex.Message,"Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);}
        }
        private void LoadData(){try{grid.DataSource=Db.Query("SELECT * FROM KhaoSat ORDER BY NgayGui DESC");}catch(Exception ex){MessageBox.Show(ex.Message);}}
        private void Field(FlowLayoutPanel p,string l,Control c){var x=new Panel{Width=180,Height=55};x.Controls.Add(new Label{Text=l,Dock=DockStyle.Top,Height=20});c.Dock=DockStyle.Bottom;x.Controls.Add(c);p.Controls.Add(x);}
        private void Button(FlowLayoutPanel p,string t,Action a){var b=new System.Windows.Forms.Button{Text=t,Width=120,Height=28,Margin=new Padding(5,20,5,0)};b.Click+=(s,e)=>a();p.Controls.Add(b);}
    }
}
