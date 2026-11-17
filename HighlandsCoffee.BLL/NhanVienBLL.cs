using System.Data;
using HighlandsCoffee.DAL;
using HighlandsCoffee.Models;

namespace HighlandsCoffee.BLL
{
    public class NhanVienBLL
    {
        private readonly NhanVienDAL dal = new NhanVienDAL();

        public DataTable LayTatCaNhanVien() => dal.GetAllNhanVien();
        public DataTable TimKiem(string kw) => dal.TimKiemNhanVien(kw.Trim());

        public (bool Success, string Message) ThemNhanVien(NhanVien nv)
        {
            if (string.IsNullOrWhiteSpace(nv.HoTen)) return (false, "Họ tên nhân viên không được để trống!");
            if (nv.SoDienThoai.Length < 10) return (false, "Số điện thoại phải có ít nhất 10 chữ số!");
            try
            {
                return dal.InsertNhanVien(nv)
                    ? (true, "Thêm nhân viên mới thành công!")
                    : (false, "Thêm thất bại.");
            }
            catch
            {
                return (false, "Số điện thoại này đã thuộc về nhân viên khác!");
            }
        }

        public (bool Success, string Message) SuaNhanVien(NhanVien nv)
        {
            if (nv.MaNV <= 0) return (false, "Vui lòng chọn nhân viên cần sửa trên bảng!");
            if (string.IsNullOrWhiteSpace(nv.HoTen)) return (false, "Họ tên không được để trống!");
            return dal.UpdateNhanVien(nv)
                ? (true, "Cập nhật thông tin nhân viên thành công!")
                : (false, "Cập nhật thất bại.");
        }

        public (bool Success, string Message) XoaNhanVien(int maNV)
        {
            if (maNV <= 0) return (false, "Vui lòng chọn nhân viên cần xóa!");
            return dal.DeleteNhanVien(maNV)
                ? (true, "Đã cho nhân viên nghỉ việc (Xóa thành công)!")
                : (false, "Xóa thất bại.");
        }
    }
}