using System.Data;
using QuanLyCongTyDuLich.Data;

namespace QuanLyCongTyDuLich.Services
{
    public class TourService
    {
        public DataTable GetAll()
        {
            return Db.Query("SELECT MaTour, TenTour, SoNgay, SoDem, DonGiaKhach, MoTa, DangMoBan FROM Tour ORDER BY MaTour");
        }

        public int Insert(string ma, string ten, int soNgay, int soDem, decimal gia, string moTa, bool moBan)
        {
            return Db.Execute("INSERT INTO Tour(MaTour,TenTour,SoNgay,SoDem,DonGiaKhach,MoTa,DangMoBan) VALUES(@ma,@ten,@ngay,@dem,@gia,@mota,@moban)",
                Db.P("@ma", ma), Db.P("@ten", ten), Db.P("@ngay", soNgay), Db.P("@dem", soDem),
                Db.P("@gia", gia), Db.P("@mota", moTa), Db.P("@moban", moBan));
        }

        public int Update(string ma, string ten, int soNgay, int soDem, decimal gia, string moTa, bool moBan)
        {
            return Db.Execute("UPDATE Tour SET TenTour=@ten,SoNgay=@ngay,SoDem=@dem,DonGiaKhach=@gia,MoTa=@mota,DangMoBan=@moban WHERE MaTour=@ma",
                Db.P("@ma", ma), Db.P("@ten", ten), Db.P("@ngay", soNgay), Db.P("@dem", soDem),
                Db.P("@gia", gia), Db.P("@mota", moTa), Db.P("@moban", moBan));
        }

        public int Delete(string ma)
        {
            return Db.Execute("DELETE FROM Tour WHERE MaTour=@ma", Db.P("@ma", ma));
        }
    }
}
