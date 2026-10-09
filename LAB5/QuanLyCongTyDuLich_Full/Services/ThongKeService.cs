using System.Data;
using QuanLyCongTyDuLich.Data;

namespace QuanLyCongTyDuLich.Services
{
    public class ThongKeService
    {
        public DataTable DoanhThu()
        {
            return Db.Query(@"SELECT N'Khách lẻ' AS Loai, COUNT(*) AS SoPhieu, SUM(ThanhTien) AS TongTien
                              FROM DangKyLe WHERE TrangThai=N'Đã đăng ký'
                              UNION ALL
                              SELECT N'Khách đoàn', COUNT(*), SUM(TongTienDuKien)
                              FROM DangKyDoan WHERE TrangThai<>N'Hủy - mất cọc'");
        }

        public DataTable LuongHDV()
        {
            return Db.Query(@"SELECT h.MaHDV,h.HoTen,h.LuongCoBan,ISNULL(SUM(p.ThuLaoTour),0) AS ThuLaoTour,
                              h.LuongCoBan+ISNULL(SUM(p.ThuLaoTour),0) AS TongLuong
                              FROM HuongDanVien h LEFT JOIN PhanCongHDV p ON h.MaHDV=p.MaHDV
                              GROUP BY h.MaHDV,h.HoTen,h.LuongCoBan ORDER BY h.MaHDV");
        }

        public DataTable Tours()
        {
            return Db.Query("SELECT MaTour,TenTour,SoNgay,SoDem,DonGiaKhach,DangMoBan FROM Tour ORDER BY MaTour");
        }
    }
}
