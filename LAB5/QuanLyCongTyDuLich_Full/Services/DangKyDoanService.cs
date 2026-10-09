using System;
using System.Data;
using QuanLyCongTyDuLich.Data;

namespace QuanLyCongTyDuLich.Services
{
    public class DangKyDoanService
    {
        public DataTable GetAll()
        {
            return Db.Query(@"SELECT d.SoDKDoan,d.MaDoan,d.MaTour,t.TenTour,d.NgayDangKy,d.NgayDi,d.NgayKetThucDuKien,d.SoNguoi,d.TienCoc,d.TongTienDuKien,d.TrangThai
                              FROM DangKyDoan d JOIN Tour t ON d.MaTour=t.MaTour ORDER BY d.NgayDangKy DESC");
        }

        public DataTable GetTours()
        {
            return Db.Query("SELECT MaTour,TenTour,DonGiaKhach FROM Tour WHERE DangMoBan=1 ORDER BY TenTour");
        }

        public int Add(string soDK, string maDoan, string maTour, DateTimeWrapper ngayDi, DateTimeWrapper ngayVe, int soNguoi, string diaDiem, decimal tienCoc, bool baoHiem, bool daCoc, decimal tongTien)
        {
            if (soNguoi <= 12) throw new System.ArgumentException("Khách đoàn phải trên 12 người.");
            return Db.Execute(@"INSERT INTO DangKyDoan(SoDKDoan,MaDoan,MaTour,NgayDangKy,NgayDi,NgayKetThucDuKien,SoNguoi,DiaDiemDon,MuaBaoHiem,TienCoc,DaThanhToanCoc,TongTienDuKien,TrangThai)
                                VALUES(@so,@doan,@tour,SYSDATETIME(),@di,@ve,@nguoi,@dd,@bh,@coc,@dacoc,@tong,N'Đã đăng ký')",
                Db.P("@so", soDK), Db.P("@doan", maDoan), Db.P("@tour", maTour), Db.P("@di", ngayDi.Value.Date), Db.P("@ve", ngayVe.Value.Date),
                Db.P("@nguoi", soNguoi), Db.P("@dd", diaDiem), Db.P("@bh", baoHiem), Db.P("@coc", tienCoc), Db.P("@dacoc", daCoc), Db.P("@tong", tongTien));
        }
    }

    public class DateTimeWrapper
    {
        public System.DateTime Value { get; set; }
        public DateTimeWrapper(System.DateTime value) { Value = value; }
    }
}
