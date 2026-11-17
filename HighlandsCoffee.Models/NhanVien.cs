using System;

namespace HighlandsCoffee.Models
{
    public class NhanVien
    {
        public int MaNV { get; set; }
        public string HoTen { get; set; } = string.Empty;
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; } = "Nam";
        public string SoDienThoai { get; set; } = string.Empty;
        public string DiaChi { get; set; } = string.Empty;
        public string ChucVu { get; set; } = string.Empty;
        public bool TrangThai { get; set; } = true;
    }
}