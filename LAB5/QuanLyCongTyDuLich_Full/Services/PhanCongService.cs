using System;
using System.Data;
using QuanLyCongTyDuLich.Data;

namespace QuanLyCongTyDuLich.Services
{
    public class PhanCongService
    {
        public DataTable GetAll()
        {
            return Db.Query(@"SELECT p.MaPC,p.MaHDV,h.HoTen,p.LoaiDoiTuong,p.MaChuyen,p.SoDKDoan,p.NgayBatDau,p.NgayKetThuc,p.ThuLaoTour
                              FROM PhanCongHDV p JOIN HuongDanVien h ON p.MaHDV=h.MaHDV ORDER BY p.NgayBatDau");
        }

        public DataTable GetGuides()
        {
            return Db.Query("SELECT MaHDV,HoTen FROM HuongDanVien WHERE DangLamViec=1 ORDER BY HoTen");
        }

        public void Assign(string maPC, string maHDV, string loai, string maChuyen, string soDKDoan, DateTime start, DateTime end, decimal thuLao)
        {
            if (end.Date < start.Date) throw new ArgumentException("Ngày kết thúc phải bằng hoặc sau ngày bắt đầu.");
            var conflict = Db.Scalar(@"SELECT COUNT(*) FROM PhanCongHDV
                                       WHERE MaHDV=@hdv AND @start <= NgayKetThuc AND @end >= NgayBatDau",
                Db.P("@hdv", maHDV), Db.P("@start", start.Date), Db.P("@end", end.Date));
            if (System.Convert.ToInt32(conflict) > 0) throw new ArgumentException("Hướng dẫn viên bị trùng lịch.");
            if (loai == "LE")
                Db.Execute(@"INSERT INTO PhanCongHDV(MaPC,MaHDV,LoaiDoiTuong,MaChuyen,SoDKDoan,NgayBatDau,NgayKetThuc,ThuLaoTour)
                             VALUES(@pc,@hdv,'LE',@chuyen,NULL,@bd,@kt,@luong)",
                    Db.P("@pc", maPC), Db.P("@hdv", maHDV), Db.P("@chuyen", maChuyen), Db.P("@bd", start.Date), Db.P("@kt", end.Date), Db.P("@luong", thuLao));
            else
                Db.Execute(@"INSERT INTO PhanCongHDV(MaPC,MaHDV,LoaiDoiTuong,MaChuyen,SoDKDoan,NgayBatDau,NgayKetThuc,ThuLaoTour)
                             VALUES(@pc,@hdv,'DOAN',NULL,@doan,@bd,@kt,@luong)",
                    Db.P("@pc", maPC), Db.P("@hdv", maHDV), Db.P("@doan", soDKDoan), Db.P("@bd", start.Date), Db.P("@kt", end.Date), Db.P("@luong", thuLao));
        }
    }
}
