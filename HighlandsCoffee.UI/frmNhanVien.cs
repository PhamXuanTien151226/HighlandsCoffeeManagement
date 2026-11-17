using System;
using System.Drawing;
using System.Windows.Forms;
using HighlandsCoffee.BLL;
using HighlandsCoffee.Models;

namespace HighlandsCoffee.UI
{
    public class frmNhanVien : Form
    {
        private readonly NhanVienBLL nvBll = new NhanVienBLL();
        private readonly TaiKhoanBLL tkBll = new TaiKhoanBLL();

        private DataGridView dgvNV = null!, dgvTK = null!;
        private TextBox txtMaNV = null!, txtHoTen = null!, txtSDT = null!, txtDiaChi = null!, txtSearch = null!;
        private ComboBox cboGioiTinh = null!, cboChucVu = null!;
        private DateTimePicker dtpNgaySinh = null!;

        private TextBox txtUser = null!, txtPass = null!;
        private ComboBox cboQuyen = null!;
        private CheckBox chkTrangThaiTK = null!;

        public frmNhanVien()
        {
            this.Text = "QUẢN LÝ NHÂN VIÊN & PHÂN QUYỀN TÀI KHOẢN";
            this.Size = new Size(1180, 680);
            this.StartPosition = FormStartPosition.CenterScreen;

            TabControl tab = new TabControl { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 9.5F) };
            TabPage tabNV = new TabPage("1. Hồ sơ Nhân viên (CRUD)");
            TabPage tabTK = new TabPage("2. Cấp & Phân quyền Tài khoản");

            BuildTabNhanVien(tabNV);
            BuildTabTaiKhoan(tabTK);

            tab.TabPages.AddRange(new TabPage[] { tabNV, tabTK });
            this.Controls.Add(tab);

            LoadDataNV();
            LoadDataTK();
        }

        private void BuildTabNhanVien(TabPage page)
        {
            Panel pnlLeft = new Panel { Dock = DockStyle.Left, Width = 360, Padding = new Padding(15) };
            int y = 15;
            pnlLeft.Controls.Add(new Label { Text = "Mã NV:", Location = new Point(15, y), AutoSize = true });
            txtMaNV = new TextBox { Location = new Point(115, y - 3), Width = 220, ReadOnly = true, BackColor = Color.WhiteSmoke };

            y += 38;
            pnlLeft.Controls.Add(new Label { Text = "Họ và tên:", Location = new Point(15, y), AutoSize = true });
            txtHoTen = new TextBox { Location = new Point(115, y - 3), Width = 220 };

            y += 38;
            pnlLeft.Controls.Add(new Label { Text = "Ngày sinh:", Location = new Point(15, y), AutoSize = true });
            dtpNgaySinh = new DateTimePicker { Location = new Point(115, y - 3), Width = 220, Format = DateTimePickerFormat.Short };

            y += 38;
            pnlLeft.Controls.Add(new Label { Text = "Giới tính:", Location = new Point(15, y), AutoSize = true });
            cboGioiTinh = new ComboBox { Location = new Point(115, y - 3), Width = 220, DropDownStyle = ComboBoxStyle.DropDownList };
            cboGioiTinh.Items.AddRange(new object[] { "Nam", "Nữ" });
            cboGioiTinh.SelectedIndex = 0;

            y += 38;
            pnlLeft.Controls.Add(new Label { Text = "Số ĐT:", Location = new Point(15, y), AutoSize = true });
            txtSDT = new TextBox { Location = new Point(115, y - 3), Width = 220 };

            y += 38;
            pnlLeft.Controls.Add(new Label { Text = "Địa chỉ:", Location = new Point(15, y), AutoSize = true });
            txtDiaChi = new TextBox { Location = new Point(115, y - 3), Width = 220 };

            y += 38;
            pnlLeft.Controls.Add(new Label { Text = "Chức vụ:", Location = new Point(15, y), AutoSize = true });
            cboChucVu = new ComboBox { Location = new Point(115, y - 3), Width = 220, DropDownStyle = ComboBoxStyle.DropDownList };
            cboChucVu.Items.AddRange(new object[] { "Quản lý", "Thu ngân", "Pha chế", "Phục vụ" });
            cboChucVu.SelectedIndex = 1;

            y += 50;
            Button btnAdd = new Button { Text = "Thêm", Location = new Point(15, y), Size = new Size(75, 34), BackColor = Color.LightGreen, FlatStyle = FlatStyle.Flat };
            Button btnEdit = new Button { Text = "Sửa", Location = new Point(98, y), Size = new Size(75, 34), BackColor = Color.LightSkyBlue, FlatStyle = FlatStyle.Flat };
            Button btnDel = new Button { Text = "Xóa", Location = new Point(181, y), Size = new Size(75, 34), BackColor = Color.LightCoral, FlatStyle = FlatStyle.Flat };
            Button btnClear = new Button { Text = "Mới", Location = new Point(264, y), Size = new Size(71, 34) };

            btnAdd.Click += (s, e) => { var r = nvBll.ThemNhanVien(GetFormNV()); MessageBox.Show(r.Message); if (r.Success) LoadDataNV(); };
            btnEdit.Click += (s, e) => { var r = nvBll.SuaNhanVien(GetFormNV()); MessageBox.Show(r.Message); if (r.Success) LoadDataNV(); };
            btnDel.Click += (s, e) =>
            {
                if (int.TryParse(txtMaNV.Text, out int id))
                {
                    var r = nvBll.XoaNhanVien(id);
                    MessageBox.Show(r.Message);
                    if (r.Success) { LoadDataNV(); ClearForm(); }
                }
            };
            btnClear.Click += (s, e) => ClearForm();

            pnlLeft.Controls.AddRange(new Control[] { btnAdd, btnEdit, btnDel, btnClear });

            Panel pnlRight = new Panel { Dock = DockStyle.Fill, Padding = new Padding(10) };
            Panel pnlSearch = new Panel { Dock = DockStyle.Top, Height = 45 };
            txtSearch = new TextBox { Location = new Point(5, 8), Width = 260, PlaceholderText = "Tìm theo tên hoặc SĐT..." };
            Button btnSearch = new Button { Text = "Tìm kiếm", Location = new Point(275, 6), Size = new Size(90, 30) };
            Button btnAll = new Button { Text = "Tất cả", Location = new Point(375, 6), Size = new Size(80, 30) };

            btnSearch.Click += (s, e) => dgvNV.DataSource = nvBll.TimKiem(txtSearch.Text);
            btnAll.Click += (s, e) => LoadDataNV();
            pnlSearch.Controls.AddRange(new Control[] { txtSearch, btnSearch, btnAll });

            dgvNV = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, BackgroundColor = Color.White };
            dgvNV.CellClick += DgvNV_CellClick;

            pnlRight.Controls.Add(dgvNV);
            pnlRight.Controls.Add(pnlSearch);
            page.Controls.Add(pnlRight);
            page.Controls.Add(pnlLeft);
        }

        private void BuildTabTaiKhoan(TabPage page)
        {
            Panel pnlTop = new Panel { Dock = DockStyle.Top, Height = 120, Padding = new Padding(15) };
            pnlTop.Controls.Add(new Label { Text = "Tên đăng nhập:", Location = new Point(15, 20), AutoSize = true });
            txtUser = new TextBox { Location = new Point(125, 17), Width = 170 };

            pnlTop.Controls.Add(new Label { Text = "Mật khẩu:", Location = new Point(315, 20), AutoSize = true });
            txtPass = new TextBox { Location = new Point(390, 17), Width = 160, Text = "123456" };

            pnlTop.Controls.Add(new Label { Text = "Quyền hạn:", Location = new Point(570, 20), AutoSize = true });
            cboQuyen = new ComboBox { Location = new Point(655, 17), Width = 130, DropDownStyle = ComboBoxStyle.DropDownList };
            cboQuyen.Items.AddRange(new object[] { "Admin", "Staff" });
            cboQuyen.SelectedIndex = 1;

            chkTrangThaiTK = new CheckBox { Text = "Hoạt động (Không khóa)", Location = new Point(805, 19), AutoSize = true, Checked = true };

            Button btnCapTK = new Button { Text = "Cấp Tài khoản cho NV đang chọn", Location = new Point(15, 65), Size = new Size(250, 36), BackColor = Color.LightGreen, FlatStyle = FlatStyle.Flat };
            Button btnUpdateQuyen = new Button { Text = "Cập nhật Quyền / Khóa Tài khoản", Location = new Point(280, 65), Size = new Size(250, 36), BackColor = Color.LightSkyBlue, FlatStyle = FlatStyle.Flat };

            btnCapTK.Click += (s, e) =>
            {
                if (!int.TryParse(txtMaNV.Text, out int maNV))
                {
                    MessageBox.Show("Hãy chọn 1 nhân viên ở Tab 1 trước khi cấp tài khoản!");
                    return;
                }
                var res = tkBll.TaoTaiKhoan(new TaiKhoan { TenDangNhap = txtUser.Text, MatKhau = txtPass.Text, MaNV = maNV, QuyenHan = cboQuyen.Text });
                MessageBox.Show(res.Message);
                if (res.Success) LoadDataTK();
            };

            btnUpdateQuyen.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtUser.Text)) return;
                if (tkBll.PhanQuyen(txtUser.Text, cboQuyen.Text, chkTrangThaiTK.Checked))
                {
                    MessageBox.Show("Cập nhật phân quyền tài khoản thành công!");
                    LoadDataTK();
                }
            };

            pnlTop.Controls.AddRange(new Control[] { txtUser, txtPass, cboQuyen, chkTrangThaiTK, btnCapTK, btnUpdateQuyen });

            dgvTK = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, BackgroundColor = Color.White };
            dgvTK.CellClick += (s, e) =>
            {
                if (dgvTK.CurrentRow != null)
                {
                    txtUser.Text = dgvTK.CurrentRow.Cells["TenDangNhap"].Value?.ToString();
                    cboQuyen.Text = dgvTK.CurrentRow.Cells["QuyenHan"].Value?.ToString();
                    chkTrangThaiTK.Checked = Convert.ToBoolean(dgvTK.CurrentRow.Cells["TrangThai"].Value);
                }
            };

            page.Controls.Add(dgvTK);
            page.Controls.Add(pnlTop);
        }

        private NhanVien GetFormNV()
        {
            int.TryParse(txtMaNV.Text, out int maNV);
            return new NhanVien
            {
                MaNV = maNV,
                HoTen = txtHoTen.Text,
                NgaySinh = dtpNgaySinh.Value,
                GioiTinh = cboGioiTinh.Text,
                SoDienThoai = txtSDT.Text,
                DiaChi = txtDiaChi.Text,
                ChucVu = cboChucVu.Text
            };
        }

        private void DgvNV_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (dgvNV.CurrentRow == null) return;
            var r = dgvNV.CurrentRow;
            txtMaNV.Text = r.Cells["MaNV"].Value?.ToString();
            txtHoTen.Text = r.Cells["HoTen"].Value?.ToString();
            if (r.Cells["NgaySinh"].Value != DBNull.Value) dtpNgaySinh.Value = Convert.ToDateTime(r.Cells["NgaySinh"].Value);
            cboGioiTinh.Text = r.Cells["GioiTinh"].Value?.ToString();
            txtSDT.Text = r.Cells["SoDienThoai"].Value?.ToString();
            txtDiaChi.Text = r.Cells["DiaChi"].Value?.ToString();
            cboChucVu.Text = r.Cells["ChucVu"].Value?.ToString();
        }

        private void ClearForm()
        {
            txtMaNV.Clear(); txtHoTen.Clear(); txtSDT.Clear(); txtDiaChi.Clear();
        }

        private void LoadDataNV() => dgvNV.DataSource = nvBll.LayTatCaNhanVien();
        private void LoadDataTK() => dgvTK.DataSource = tkBll.LayDanhSachTaiKhoan();
    }
}