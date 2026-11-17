using HighlandsCoffee.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using static System.Collections.Specialized.BitVector32;

namespace HighlandsCoffee.DAL
{
    public class TaiKhoanDAL
    {
        public bool KiemTraDangNhap(string user, string pass)
        {
            using SqlConnection conn = DatabaseHelper.GetConnection();
            conn.Open();
            string sql = @"SELECT tk.TenDangNhap, tk.MaNV, tk.QuyenHan, nv.HoTen 
                           FROM TaiKhoan tk 
                           JOIN NhanVien nv ON tk.MaNV = nv.MaNV 
                           WHERE tk.TenDangNhap = @User AND tk.MatKhau = @Pass AND tk.TrangThai = 1 AND nv.TrangThai = 1";
            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@User", user);
            cmd.Parameters.AddWithValue("@Pass", pass);

            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                Session.TenDangNhap = reader["TenDangNhap"].ToString()!;
                Session.MaNV = Convert.ToInt32(reader["MaNV"]);
                Session.QuyenHan = reader["QuyenHan"].ToString()!;
                Session.HoTen = reader["HoTen"].ToString()!;
                return true;
            }
            return false;
        }

        public DataTable GetDanhSachTaiKhoan()
        {
            using SqlConnection conn = DatabaseHelper.GetConnection();
            string sql = @"SELECT tk.TenDangNhap, nv.HoTen, tk.QuyenHan, tk.TrangThai 
                           FROM TaiKhoan tk JOIN NhanVien nv ON tk.MaNV = nv.MaNV";
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public bool ThemTaiKhoan(TaiKhoan tk)
        {
            using SqlConnection conn = DatabaseHelper.GetConnection();
            conn.Open();
            string sql = "INSERT INTO TaiKhoan (TenDangNhap, MatKhau, MaNV, QuyenHan, TrangThai) VALUES (@User, @Pass, @MaNV, @Quyen, 1)";
            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@User", tk.TenDangNhap);
            cmd.Parameters.AddWithValue("@Pass", tk.MatKhau);
            cmd.Parameters.AddWithValue("@MaNV", tk.MaNV);
            cmd.Parameters.AddWithValue("@Quyen", tk.QuyenHan);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool CapNhatQuyenVaTrangThai(string username, string quyenMoi, bool trangThai)
        {
            using SqlConnection conn = DatabaseHelper.GetConnection();
            conn.Open();
            string sql = "UPDATE TaiKhoan SET QuyenHan = @Quyen, TrangThai = @TT WHERE TenDangNhap = @User";
            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Quyen", quyenMoi);
            cmd.Parameters.AddWithValue("@TT", trangThai);
            cmd.Parameters.AddWithValue("@User", username);
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}