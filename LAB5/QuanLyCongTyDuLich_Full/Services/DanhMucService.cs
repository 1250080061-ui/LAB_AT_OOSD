using System.Data;
using QuanLyCongTyDuLich.Data;

namespace QuanLyCongTyDuLich.Services
{
    public class DanhMucService
    {
        public DataTable Get(string table)
        {
            switch (table)
            {
                case "PhuongTien": return Db.Query("SELECT MaPT,TenPT,GhiChu FROM PhuongTien ORDER BY MaPT");
                case "DiemBanVe": return Db.Query("SELECT MaDiemBan,TenDiemBan,DiaChi,DienThoai FROM DiemBanVe ORDER BY MaDiemBan");
                case "HuongDanVien": return Db.Query("SELECT MaHDV,HoTen,DienThoai,LuongCoBan,DangLamViec FROM HuongDanVien ORDER BY MaHDV");
                case "DiemThamQuan": return Db.Query("SELECT MaDiemTQ,TenDiemTQ,DiaDiem,NoiDung,YNghia FROM DiemThamQuan ORDER BY MaDiemTQ");
                default: return new DataTable();
            }
        }

        public int Add(string table, string ma, string ten, string thongTin, string thongTin2)
        {
            switch (table)
            {
                case "PhuongTien":
                    return Db.Execute("INSERT INTO PhuongTien(MaPT,TenPT,GhiChu) VALUES(@ma,@ten,@tt)", Db.P("@ma", ma), Db.P("@ten", ten), Db.P("@tt", thongTin));
                case "DiemBanVe":
                    return Db.Execute("INSERT INTO DiemBanVe(MaDiemBan,TenDiemBan,DiaChi,DienThoai) VALUES(@ma,@ten,@tt,@tt2)", Db.P("@ma", ma), Db.P("@ten", ten), Db.P("@tt", thongTin), Db.P("@tt2", thongTin2));
                case "HuongDanVien":
                    return Db.Execute("INSERT INTO HuongDanVien(MaHDV,HoTen,DienThoai,LuongCoBan,DangLamViec) VALUES(@ma,@ten,@tt,@luong,1)", Db.P("@ma", ma), Db.P("@ten", ten), Db.P("@tt", thongTin), Db.P("@luong", decimal.Parse(thongTin2)));
                case "DiemThamQuan":
                    return Db.Execute("INSERT INTO DiemThamQuan(MaDiemTQ,TenDiemTQ,DiaDiem,NoiDung) VALUES(@ma,@ten,@tt,@tt2)", Db.P("@ma", ma), Db.P("@ten", ten), Db.P("@tt", thongTin), Db.P("@tt2", thongTin2));
                default: return 0;
            }
        }

        public int Delete(string table, string ma)
        {
            switch (table)
            {
                case "PhuongTien": return Db.Execute("DELETE FROM PhuongTien WHERE MaPT=@ma", Db.P("@ma", ma));
                case "DiemBanVe": return Db.Execute("DELETE FROM DiemBanVe WHERE MaDiemBan=@ma", Db.P("@ma", ma));
                case "HuongDanVien": return Db.Execute("DELETE FROM HuongDanVien WHERE MaHDV=@ma", Db.P("@ma", ma));
                case "DiemThamQuan": return Db.Execute("DELETE FROM DiemThamQuan WHERE MaDiemTQ=@ma", Db.P("@ma", ma));
                default: return 0;
            }
        }
    }
}
