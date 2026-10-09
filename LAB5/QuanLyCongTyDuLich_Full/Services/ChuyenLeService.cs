using System.Data;
using QuanLyCongTyDuLich.Data;

namespace QuanLyCongTyDuLich.Services
{
    public class ChuyenLeService
    {
        public DataTable GetAll()
        {
            return Db.Query(@"SELECT c.MaChuyen,c.MaTour,t.TenTour,c.NgayDi,c.NgayVe,c.DiaDiemDon,c.TrangThai
                              FROM ChuyenLe c INNER JOIN Tour t ON c.MaTour=t.MaTour ORDER BY c.NgayDi");
        }

        public DataTable GetTours()
        {
            return Db.Query("SELECT MaTour,TenTour FROM Tour WHERE DangMoBan=1 ORDER BY TenTour");
        }

        public int Add(string ma, string maTour, System.DateTime di, System.DateTime ve, string don, string trangThai)
        {
            return Db.Execute("INSERT INTO ChuyenLe(MaChuyen,MaTour,NgayDi,NgayVe,DiaDiemDon,TrangThai) VALUES(@ma,@tour,@di,@ve,@don,@tt)",
                Db.P("@ma", ma), Db.P("@tour", maTour), Db.P("@di", di.Date), Db.P("@ve", ve.Date), Db.P("@don", don), Db.P("@tt", trangThai));
        }

        public int Delete(string ma)
        {
            return Db.Execute("DELETE FROM ChuyenLe WHERE MaChuyen=@ma", Db.P("@ma", ma));
        }
    }
}
