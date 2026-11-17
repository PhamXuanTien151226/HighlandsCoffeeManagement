namespace HighlandsCoffee.Models
{
    public class TaiKhoan
    {
        public string TenDangNhap { get; set; } = string.Empty;
        public string MatKhau { get; set; } = string.Empty;
        public int MaNV { get; set; }
        public string QuyenHan { get; set; } = "Staff";
        public bool TrangThai { get; set; } = true;
    }
}