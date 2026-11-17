namespace HighlandsCoffee.Models
{
    public static class Session
    {
        public static int MaNV { get; set; }
        public static string HoTen { get; set; } = string.Empty;
        public static string TenDangNhap { get; set; } = string.Empty;
        public static string QuyenHan { get; set; } = string.Empty;

        public static void Clear()
        {
            MaNV = 0;
            HoTen = string.Empty;
            TenDangNhap = string.Empty;
            QuyenHan = string.Empty;
        }
    }
}