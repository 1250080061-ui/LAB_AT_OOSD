using System;
using System.Data;
using QuanLyCongTyDuLich.Data;

namespace QuanLyCongTyDuLich.Services
{
    public class DangKyLeService
    {
        public DataTable GetAll()
        {
            return Db.Query(@"SELECT d.SoDKLe,d.MaChuyen,d.MaDiemBan,d.NgayDangKy,d.TenNguoiDangKy,d.DienThoai,d.SoNguoi,d.ThanhTien,d.DaThanhToan,d.TrangThai
                              FROM DangKyLe d ORDER BY d.NgayDangKy DESC");
        }

        public DataTable GetChuyen()
        {
            return Db.Query(@"SELECT c.MaChuyen, t.TenTour, c.NgayDi, t.DonGiaKhach
                              FROM ChuyenLe c JOIN Tour t ON c.MaTour=t.MaTour
                              WHERE c.TrangThai=N'Mở đăng ký' ORDER BY c.NgayDi");
        }

        public DataTable GetDiemBan()
        {
            return Db.Query("SELECT MaDiemBan,TenDiemBan FROM DiemBanVe ORDER BY TenDiemBan");
        }

        public int Register(string soDK, string maChuyen, string maDiemBan, string ten, string dienThoai, int soNguoi, decimal gia)
        {
            if (soNguoi < 1 || soNguoi > 11) throw new ArgumentException("Khách lẻ phải từ 1 đến 11 người.");
            var thanhTien = soNguoi * gia;
            return Db.Execute(@"INSERT INTO DangKyLe(SoDKLe,MaChuyen,MaDiemBan,NgayDangKy,TenNguoiDangKy,DienThoai,SoNguoi,ThanhTien,DaThanhToan,TrangThai)
                                VALUES(@so,@chuyen,@diem,SYSDATETIME(),@ten,@dt,@nguoi,@tien,1,N'Đã đăng ký')",
                Db.P("@so", soDK), Db.P("@chuyen", maChuyen), Db.P("@diem", maDiemBan), Db.P("@ten", ten),
                Db.P("@dt", dienThoai), Db.P("@nguoi", soNguoi), Db.P("@tien", thanhTien));
        }
    }
}
