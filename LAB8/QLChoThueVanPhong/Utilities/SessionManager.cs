namespace QLChoThueVanPhong.Utilities;

public static class SessionManager
{
    public static int MaTaiKhoan { get; set; }
    public static int? MaNhanVien { get; set; }
    public static string TenDangNhap { get; set; } = string.Empty;
    public static string HoTen { get; set; } = string.Empty;
    public static string VaiTro { get; set; } = string.Empty;

    public static void Clear()
    {
        MaTaiKhoan = 0;
        MaNhanVien = null;
        TenDangNhap = string.Empty;
        HoTen = string.Empty;
        VaiTro = string.Empty;
    }
}
