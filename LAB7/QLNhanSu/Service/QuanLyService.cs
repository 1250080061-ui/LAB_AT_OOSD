
using System;
using System.Collections.Generic;
using System.Data;
using QLNhanSu.Data;

namespace QLNhanSu.Service
{
    public class QuanLyService
    {
        private readonly SqlData data = new SqlData();

        // Danh sach 21 bang duoc phep truy cap
        private readonly HashSet<string> danhSachBang =
            new HashSet<string>(
                StringComparer.OrdinalIgnoreCase)
        {
            "NhanVien",
            "PhongBan",
            "ChucVu",
            "QuyetDinhTuyenDung",
            "HopDongLaoDong",
            "QuyetDinhChucVu",
            "QuyetDinhLuong",
            "BaoHiem",
            "CaLamViec",
            "KeHoachPhanCong",
            "ChiTietPhanCong",
            "BangChamCong",
            "ChiTietChamCong",
            "PhieuPhuTroi",
            "ChiTietPhuTroi",
            "BangLuong",
            "ChiTietLuong",
            "QuyetDinhKhenThuong",
            "QuyetDinhKyLuat",
            "PhieuChiKhenThuong",
            "PhieuThuNopPhat"
        };

        private void KiemTraBang(string tenBang)
        {
            if (!danhSachBang.Contains(tenBang))
            {
                throw new ArgumentException(
                    "Bang du lieu khong hop le: " + tenBang);
            }
        }

        // Lay danh sach
        public DataTable LayDanhSach(string tenBang)
        {
            KiemTraBang(tenBang);

            return data.LayDuLieu(tenBang);
        }

        // Luu them, sua, xoa
        public int LuuThayDoi(
            string tenBang, DataTable dt)
        {
            KiemTraBang(tenBang);

            if (dt == null)
            {
                throw new ArgumentNullException("dt");
            }

            return data.LuuDuLieu(tenBang, dt);
        }
    }
}
