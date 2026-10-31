namespace QLChoThueVanPhong.Infrastructure;

public static class DbSettings
{
    // Instance SQL Server đang dùng trên máy: SQLEXPRESS
    public const string ConnectionString =
        "Server=.\\SQLEXPRESS;Database=QLChoThueVanPhong;Integrated Security=True;TrustServerCertificate=True;";
}
