using System;
using System.Data;
using Microsoft.Data.SqlClient;
using HighlandsCoffee.Models;

namespace HighlandsCoffee.DAL
{
    public class NhanVienDAL
    {
        public DataTable GetAllNhanVien()
        {
            using SqlConnection conn = DatabaseHelper.GetConnection();
            string sql = "SELECT MaNV, HoTen, NgaySinh, GioiTinh, SoDienThoai, DiaChi, ChucVu FROM NhanVien WHERE TrangThai = 1";
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable TimKiemNhanVien(string keyword)
        {
            using SqlConnection conn = DatabaseHelper.GetConnection();
            string sql = "SELECT MaNV, HoTen, NgaySinh, GioiTinh, SoDienThoai, DiaChi, ChucVu FROM NhanVien WHERE TrangThai = 1 AND (HoTen LIKE @KW OR SoDienThoai LIKE @KW)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@KW", "%" + keyword + "%");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public bool InsertNhanVien(NhanVien nv)
        {
            using SqlConnection conn = DatabaseHelper.GetConnection();
            conn.Open();
            string sql = "INSERT INTO NhanVien (HoTen, NgaySinh, GioiTinh, SoDienThoai, DiaChi, ChucVu, TrangThai) VALUES (@HoTen, @NgaySinh, @GioiTinh, @SDT, @DiaChi, @ChucVu, 1)";
            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@HoTen", nv.HoTen);
            cmd.Parameters.AddWithValue("@NgaySinh", nv.NgaySinh);
            cmd.Parameters.AddWithValue("@GioiTinh", nv.GioiTinh);
            cmd.Parameters.AddWithValue("@SDT", nv.SoDienThoai);
            cmd.Parameters.AddWithValue("@DiaChi", nv.DiaChi);
            cmd.Parameters.AddWithValue("@ChucVu", nv.ChucVu);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool UpdateNhanVien(NhanVien nv)
        {
            using SqlConnection conn = DatabaseHelper.GetConnection();
            conn.Open();
            string sql = "UPDATE NhanVien SET HoTen=@HoTen, NgaySinh=@NgaySinh, GioiTinh=@GioiTinh, SoDienThoai=@SDT, DiaChi=@DiaChi, ChucVu=@ChucVu WHERE MaNV=@MaNV";
            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@MaNV", nv.MaNV);
            cmd.Parameters.AddWithValue("@HoTen", nv.HoTen);
            cmd.Parameters.AddWithValue("@NgaySinh", nv.NgaySinh);
            cmd.Parameters.AddWithValue("@GioiTinh", nv.GioiTinh);
            cmd.Parameters.AddWithValue("@SDT", nv.SoDienThoai);
            cmd.Parameters.AddWithValue("@DiaChi", nv.DiaChi);
            cmd.Parameters.AddWithValue("@ChucVu", nv.ChucVu);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool DeleteNhanVien(int maNV)
        {
            using SqlConnection conn = DatabaseHelper.GetConnection();
            conn.Open();
            string sql = "UPDATE NhanVien SET TrangThai = 0 WHERE MaNV = @MaNV";
            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@MaNV", maNV);
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}