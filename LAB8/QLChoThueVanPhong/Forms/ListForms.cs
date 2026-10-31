namespace QLChoThueVanPhong.Forms;

public sealed class FrmTaiKhoan : ListFormBase
{
    protected override string ScreenTitle => "Quản lý tài khoản";
    protected override string QueryText =>
        "SELECT MaTaiKhoan, TenDangNhap, VaiTro, TrangThai, NgayTao FROM TaiKhoan ORDER BY MaTaiKhoan";
}

public sealed class FrmNhanVien : ListFormBase
{
    protected override string ScreenTitle => "Quản lý nhân viên";
    protected override string QueryText => """
        SELECT nv.MaNhanVien, nv.HoTen, nv.SoDienThoai, nv.Email,
               nv.ChucVu, nv.NgayVaoLam, nv.TrangThai, tk.TenDangNhap
        FROM NhanVien nv
        INNER JOIN TaiKhoan tk ON tk.MaTaiKhoan = nv.MaTaiKhoan
        ORDER BY nv.MaNhanVien
        """;
}

public sealed class FrmKhachHang : ListFormBase
{
    protected override string ScreenTitle => "Quản lý khách hàng";
    protected override string QueryText => """
        SELECT MaKhachHang, TenKhachHang, LoaiKhachHang, CCCD,
               MaSoThue, NguoiDaiDien, SoDienThoai, Email, DiaChi
        FROM KhachHang
        ORDER BY MaKhachHang DESC
        """;
}

public sealed class FrmTang : ListFormBase
{
    protected override string ScreenTitle => "Quản lý tầng";
    protected override string QueryText =>
        "SELECT MaTang, SoTang, TenTang, MoTa, TrangThai FROM Tang ORDER BY SoTang";
}

public sealed class FrmLoaiVanPhong : ListFormBase
{
    protected override string ScreenTitle => "Quản lý loại văn phòng";
    protected override string QueryText => """
        SELECT MaLoaiVanPhong, TenLoaiVanPhong, DienTichToiThieu,
               GiaThueMacDinh, SoNguoiToiDa, MoTa
        FROM LoaiVanPhong
        ORDER BY MaLoaiVanPhong
        """;
}

public sealed class FrmVanPhong : ListFormBase
{
    protected override string ScreenTitle => "Quản lý văn phòng";
    protected override string QueryText => """
        SELECT vp.MaVanPhong, vp.MaPhong, t.TenTang,
               lvp.TenLoaiVanPhong, vp.DienTich,
               vp.GiaThueThang, vp.TrangThai, vp.MoTa
        FROM VanPhong vp
        INNER JOIN Tang t ON t.MaTang = vp.MaTang
        INNER JOIN LoaiVanPhong lvp
            ON lvp.MaLoaiVanPhong = vp.MaLoaiVanPhong
        ORDER BY t.SoTang, vp.MaPhong
        """;
}

public sealed class FrmHopDong : ListFormBase
{
    protected override string ScreenTitle => "Quản lý hợp đồng thuê";
    protected override string QueryText => """
        SELECT hd.MaHopDong, hd.SoHopDong, kh.TenKhachHang,
               vp.MaPhong, nv.HoTen AS NhanVienLap,
               hd.NgayKy, hd.NgayBatDau, hd.NgayKetThuc,
               hd.GiaThueThang, hd.TienTraTruoc, hd.TrangThai
        FROM HopDong hd
        INNER JOIN KhachHang kh ON kh.MaKhachHang = hd.MaKhachHang
        INNER JOIN VanPhong vp ON vp.MaVanPhong = hd.MaVanPhong
        INNER JOIN NhanVien nv ON nv.MaNhanVien = hd.MaNhanVien
        ORDER BY hd.MaHopDong DESC
        """;
}

public sealed class FrmDichVu : ListFormBase
{
    protected override string ScreenTitle => "Quản lý dịch vụ";
    protected override string QueryText =>
        "SELECT MaDichVu, TenDichVu, DonViTinh, DonGia, TrangThai, MoTa FROM DichVu ORDER BY MaDichVu";
}

public sealed class FrmSuDungDichVu : ListFormBase
{
    protected override string ScreenTitle => "Ghi nhận sử dụng dịch vụ";
    protected override string QueryText => """
        SELECT sd.MaSuDung, hd.SoHopDong, vp.MaPhong, dv.TenDichVu,
               sd.KySuDung, sd.ChiSoCu, sd.ChiSoMoi,
               sd.SoLuong, sd.DonGia, sd.ThanhTien
        FROM SuDungDichVu sd
        INNER JOIN HopDong hd ON hd.MaHopDong = sd.MaHopDong
        INNER JOIN VanPhong vp ON vp.MaVanPhong = hd.MaVanPhong
        INNER JOIN DichVu dv ON dv.MaDichVu = sd.MaDichVu
        ORDER BY sd.KySuDung DESC, sd.MaSuDung DESC
        """;
}

public sealed class FrmHoaDon : ListFormBase
{
    protected override string ScreenTitle => "Quản lý hóa đơn";
    protected override string QueryText => """
        SELECT h.MaHoaDon, hd.SoHopDong, kh.TenKhachHang,
               h.KyThanhToan, h.LoaiHoaDon, h.NgayLap,
               h.HanThanhToan, h.TongTien, h.TrangThai
        FROM HoaDon h
        INNER JOIN HopDong hd ON hd.MaHopDong = h.MaHopDong
        INNER JOIN KhachHang kh ON kh.MaKhachHang = hd.MaKhachHang
        ORDER BY h.MaHoaDon DESC
        """;
}

public sealed class FrmThanhToan : ListFormBase
{
    protected override string ScreenTitle => "Quản lý thanh toán";
    protected override string QueryText => """
        SELECT tt.MaThanhToan, tt.MaHoaDon, hd.SoHopDong,
               kh.TenKhachHang, tt.NgayThanhToan, tt.SoTien,
               tt.PhuongThuc, tt.MaGiaoDich, nv.HoTen AS NhanVienThu
        FROM ThanhToan tt
        INNER JOIN HoaDon h ON h.MaHoaDon = tt.MaHoaDon
        INNER JOIN HopDong hd ON hd.MaHopDong = h.MaHopDong
        INNER JOIN KhachHang kh ON kh.MaKhachHang = hd.MaKhachHang
        LEFT JOIN NhanVien nv ON nv.MaNhanVien = tt.MaNhanVien
        ORDER BY tt.MaThanhToan DESC
        """;
}
