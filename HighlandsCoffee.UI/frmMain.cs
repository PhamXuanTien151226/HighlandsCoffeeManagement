using System;
using System.Drawing;
using System.Windows.Forms;
using HighlandsCoffee.Models;

namespace HighlandsCoffee.UI
{
    public class frmMain : Form
    {
        private Button btnNhanVien = null!, btnBanHang = null!, btnThongKe = null!;

        public frmMain()
        {
            this.Text = "HỆ THỐNG QUẢN LÝ HIGHLANDS COFFEE";
            this.Size = new Size(760, 460);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            Panel pnlTop = new Panel { Dock = DockStyle.Top, Height = 70, BackColor = Color.FromArgb(178, 40, 48) };
            Label lblInfo = new Label
            {
                Text = $"Xin chào: {Session.HoTen} | Vai trò: {Session.QuyenHan}",
                Location = new Point(20, 22),
                AutoSize = true,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold)
            };

            Button btnLogout = new Button
            {
                Text = "Đăng xuất",
                Location = new Point(620, 18),
                Size = new Size(105, 34),
                BackColor = Color.White,
                ForeColor = Color.FromArgb(178, 40, 48),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            btnLogout.Click += (s, e) => { Session.Clear(); this.Close(); };
            pnlTop.Controls.AddRange(new Control[] { lblInfo, btnLogout });

            btnNhanVien = new Button
            {
                Text = "QUẢN LÝ NHÂN VIÊN & PHÂN QUYỀN\n(Chức năng của Phạm Xuân Tiến - Chỉ Admin)",
                Location = new Point(60, 110),
                Size = new Size(620, 75),
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                BackColor = Color.LightSkyBlue,
                FlatStyle = FlatStyle.Flat
            };
            btnNhanVien.Click += (s, e) => new frmNhanVien().ShowDialog();

            btnBanHang = new Button
            {
                Text = "BÁN HÀNG, GỌI MÓN & THANH TOÁN\n(Chức năng của Nguyễn Hoàng Linh - Admin & Staff)",
                Location = new Point(60, 205),
                Size = new Size(620, 75),
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                BackColor = Color.LightGreen,
                FlatStyle = FlatStyle.Flat
            };
            btnBanHang.Click += (s, e) => MessageBox.Show("Chờ Nguyễn Hoàng Linh tích hợp Form Gọi món & Bàn vào đây!", "Thông báo nhóm");

            btnThongKe = new Button
            {
                Text = "DASHBOARD & THỐNG KÊ DOANH THU\n(Chức năng của Kiều Đức Dương - Chỉ Admin)",
                Location = new Point(60, 300),
                Size = new Size(620, 75),
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                BackColor = Color.LightYellow,
                FlatStyle = FlatStyle.Flat
            };
            btnThongKe.Click += (s, e) => MessageBox.Show("Chờ Kiều Đức tích hợp Form Thống kê vào đây!", "Thông báo nhóm");

            this.Controls.AddRange(new Control[] { pnlTop, btnNhanVien, btnBanHang, btnThongKe });
            ApDungPhanQuyen();
        }

        private void ApDungPhanQuyen()
        {
            if (Session.QuyenHan != "Admin")
            {
                btnNhanVien.Enabled = false;
                btnNhanVien.BackColor = Color.LightGray;
                btnNhanVien.Text += " [ĐÃ KHÓA DO KHÔNG PHẢI ADMIN]";

                btnThongKe.Enabled = false;
                btnThongKe.BackColor = Color.LightGray;
            }
        }
    }
}