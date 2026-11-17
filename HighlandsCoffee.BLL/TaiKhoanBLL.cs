using HighlandsCoffee.DAL;
using HighlandsCoffee.Models;
using System.Data;
using static System.Collections.Specialized.BitVector32;

namespace HighlandsCoffee.BLL
{
    public class TaiKhoanBLL
    {
        private readonly TaiKhoanDAL dal = new TaiKhoanDAL();

        public (bool Success, string Message) DangNhap(string user, string pass)
        {
            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(pass))
                return (false, "Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu!");

            bool ok = dal.KiemTraDangNhap(user.Trim(), pass.Trim());
            return ok
                ? (true, $"Chào mừng {Session.HoTen} ({Session.QuyenHan})!")
                : (false, "Sai tài khoản, mật khẩu hoặc tài khoản đã bị khóa!");
        }

        public DataTable LayDanhSachTaiKhoan() => dal.GetDanhSachTaiKhoan();

        public (bool Success, string Message) TaoTaiKhoan(TaiKhoan tk)
        {
            if (string.IsNullOrWhiteSpace(tk.TenDangNhap) || string.IsNullOrWhiteSpace(tk.MatKhau))
                return (false, "Tên đăng nhập và mật khẩu không được để trống!");
            try
            {
                return dal.ThemTaiKhoan(tk)
                    ? (true, "Cấp tài khoản thành công!")
                    : (false, "Không thể tạo tài khoản.");
            }
            catch
            {
                return (false, "Tên đăng nhập này đã tồn tại trên hệ thống!");
            }
        }

        public bool PhanQuyen(string user, string quyen, bool trangThai) => dal.CapNhatQuyenVaTrangThai(user, quyen, trangThai);
    }
}