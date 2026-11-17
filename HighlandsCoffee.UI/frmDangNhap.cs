using System;
using System.Drawing;
using System.Windows.Forms;
using HighlandsCoffee.BLL;

namespace HighlandsCoffee.UI
{
    public class frmDangNhap : Form
    {
        private readonly TaiKhoanBLL bll = new TaiKhoanBLL();
        private TextBox txtUser = null!, txtPass = null!;

        public frmDangNhap()
        {
            this.Text = "ĐĂNG NHẬP - HIGHLANDS COFFEE MANAGEMENT";
            this.Size = new Size(440, 310);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.White;

            Label lblTitle = new Label
            {
                Text = "HIGHLANDS COFFEE\nĐĂNG NHẬP HỆ THỐNG",
                Dock = DockStyle.Top,
                Height = 75,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                ForeColor = Color.FromArgb(178, 40, 48)
            };

            Label l1 = new Label { Text = "Tên đăng nhập:", Location = new Point(45, 95), AutoSize = true, Font = new Font("Segoe UI", 10F) };
            txtUser = new TextBox { Location = new Point(165, 92), Width = 210, Font = new Font("Segoe UI", 10F), Text = "admin" };

            Label l2 = new Label { Text = "Mật khẩu:", Location = new Point(45, 140), AutoSize = true, Font = new Font("Segoe UI", 10F) };
            txtPass = new TextBox { Location = new Point(165, 137), Width = 210, Font = new Font("Segoe UI", 10F), PasswordChar = '●', Text = "123456" };

            Button btnLogin = new Button
            {
                Text = "ĐĂNG NHẬP",
                Location = new Point(165, 190),
                Size = new Size(115, 38),
                BackColor = Color.FromArgb(178, 40, 48),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };

            Button btnExit = new Button
            {
                Text = "Thoát",
                Location = new Point(290, 190),
                Size = new Size(85, 38),
                Font = new Font("Segoe UI", 9.5F)
            };

            btnLogin.Click += BtnLogin_Click;
            btnExit.Click += (s, e) => Application.Exit();
            this.AcceptButton = btnLogin;

            this.Controls.AddRange(new Control[] { lblTitle, l1, txtUser, l2, txtPass, btnLogin, btnExit });
        }

        private void BtnLogin_Click(object? sender, EventArgs e)
        {
            var res = bll.DangNhap(txtUser.Text, txtPass.Text);
            if (res.Success)
            {
                MessageBox.Show(res.Message, "Đăng nhập thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                frmMain mainForm = new frmMain();
                mainForm.FormClosed += (s, args) => { txtPass.Clear(); this.Show(); };
                mainForm.Show();
            }
            else
            {
                MessageBox.Show(res.Message, "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}