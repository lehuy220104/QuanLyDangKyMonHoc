    using BUS_QuanLy;
    using DTO_QuanLy;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    namespace GUI_QuanLy
    {
        public partial class GUI_Admin : Form
        {
            private BUS_TaiKhoan bus = new BUS_TaiKhoan();
            BUS_MonHoc busMH = new BUS_MonHoc();
            BUS_SinhVien busSV = new BUS_SinhVien();
            BUS_GiangVien busGV = new BUS_GiangVien();
            BUS_Khoa busKhoa = new BUS_Khoa();
            BUS_LopHocPhan busLHP = new BUS_LopHocPhan();
            BUS_HocKy busHK = new BUS_HocKy();

            DataTable dtMon;
            DataTable dtKhoaGV; 
            DataTable dtMonLHP, dtGiangVienLHP, dtHocKyLHP;

          

            private string maADDangNhap;
            public GUI_Admin(string maAD)
            {
                InitializeComponent();
                maADDangNhap = maAD;
            }
            public GUI_Admin()
            {
                InitializeComponent();
            }
            private void GUI_Admin_Load(object sender, EventArgs e)
            {
                maAD.Text = maADDangNhap;
                cbVaiTro.Items.AddRange(new string[] { "SV", "GV", "AD" });

                cbGioiTinh.Items.Clear();
                cbGioiTinh.Items.Add("M");
                cbGioiTinh.Items.Add("F");
                cbGioiTinh.SelectedIndex = 0; // Mặc định chọn "Nam"

                cbGioiTinhGV.Items.Clear();
                cbGioiTinhGV.Items.Add("M");
                cbGioiTinhGV.Items.Add("F");
                cbGioiTinhGV.SelectedIndex = 0;

                cbMaLopSV.DataSource = busSV.LayDanhSachLop();
                cbMaLopSV.DisplayMember = "MaLop";
                cbMaLopSV.ValueMember = "MaLop";

                cbTrangThaiHocVu.Items.Clear();
                cbTrangThaiHocVu.Items.Add("Binh thuong");
                cbTrangThaiHocVu.Items.Add("Canh bao");

                cbBatDau.Items.Clear();
                cbBatDau.Items.AddRange(new object[] { "1", "4", "7" });

                cbKetThuc.Items.Clear();
                cbKetThuc.Items.AddRange(new object[] { "3", "6", "9" });

                cbThu.Items.Clear();
                cbThu.Items.AddRange(new object[] { "2", "3", "4", "5", "6", "7" });

                cbPhongHoc.Items.Clear();
            cbPhongHoc.Items.AddRange(new object[] { "A101", "B202", "C303", "D404" });

                LoadSinhVien();
                LoadTaiKhoan();
                LoadDataMonHoc();
                LoadMonTienQuyet();
                LoadKhoaGV();
                LoadGiangVien();

                LoadLopHocPhan();
                LoadGiangVienLHP();
                LoadHocKyLHP();
                LoadMonHocLHP();
                LoadHocKyLHP();
                
            }


            //Quan Ly Tai Khoan
            private void btnThem_Click(object sender, EventArgs e)
            {
                if (string.IsNullOrWhiteSpace(txtTenDN.Text) ||
                        string.IsNullOrWhiteSpace(txtMatKhau.Text) ||
                        cbVaiTro.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin tài khoản!",
                                    "Thiếu dữ liệu",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    return;
                }
                if (bus.KiemTraTonTai(txtTenDN.Text.Trim()))
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DTO_TaiKhoan tk = new DTO_TaiKhoan(
                    txtTenDN.Text.Trim(),
                    txtMatKhau.Text.Trim(),
                    cbVaiTro.Text
                );

                if (bus.ThemTaiKhoan(tk))
                    MessageBox.Show("Thêm thành công!");
                else
                    MessageBox.Show("Thêm thất bại!");

                LoadTaiKhoan();
                Reset();
            }
            private void LoadTaiKhoan()
            {
                dgvTaiKhoan.DataSource = bus.LayTatCaTaiKhoan();
            }
            private void btnSua_Click(object sender, EventArgs e)
            {
                // Kiểm tra dữ liệu trống
                if (string.IsNullOrWhiteSpace(txtTenDN.Text) ||
                    string.IsNullOrWhiteSpace(txtMatKhau.Text) ||
                    cbVaiTro.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin tài khoản!",
                                    "Thiếu dữ liệu",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    return;
                }

                // Tạo DTO
                DTO_TaiKhoan tk = new DTO_TaiKhoan(
                    txtTenDN.Text.Trim(),
                    txtMatKhau.Text.Trim(),
                    cbVaiTro.Text
                );

                // Gọi BUS để sửa
                if (bus.SuaTaiKhoan(tk))
                    MessageBox.Show("Sửa thành công!");
                else
                    MessageBox.Show("Sửa thất bại!");

                LoadTaiKhoan();
                Reset();
            }
            private void btnXoa_Click(object sender, EventArgs e)
            {
                if (string.IsNullOrWhiteSpace(txtTenDN.Text))
                {
                    MessageBox.Show("Vui lòng chọn tài khoản muốn xóa!",
                                    "Chưa chọn dữ liệu",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa tài khoản '{txtTenDN.Text}' không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    if (bus.XoaTaiKhoan(txtTenDN.Text.Trim()))
                        MessageBox.Show("Xóa thành công!");
                    else
                        MessageBox.Show("Xóa thất bại!");

                    LoadTaiKhoan();
                    Reset();
                }
            }
            private void Reset()
            {
                txtTenDN.Clear();
                txtMatKhau.Clear();
                cbVaiTro.SelectedIndex = -1;
                txtTenDN.Focus();
            }
            private void txtTimKiem_TextChanged(object sender, EventArgs e)
            {
                dgvTaiKhoan.DataSource = bus.TimKiemTaiKhoan(txtTimKiem.Text.Trim());
            }
            private void dgvTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dgvTaiKhoan.Rows[e.RowIndex];
                    txtTenDN.Text = row.Cells["TenDangNhap"].Value.ToString();
                    txtMatKhau.Text = row.Cells["MatKhau"].Value.ToString();
                    cbVaiTro.Text = row.Cells["VaiTro"].Value.ToString();
                }
            }

            /*---------------------------------------------------------------------------------------------------*/

            //Quan Ly Mon Hoc 
        
        
            private void LoadMonTienQuyet()
            {
                dtMon = busMH.LayDanhSachMonHoc(); // Lấy tất cả môn học từ DB
                cbMonTienQuyet.DataSource = dtMon;
                cbMonTienQuyet.DisplayMember = "TenMH";  // Hiển thị tên môn
                cbMonTienQuyet.ValueMember = "MaMH";     // Giá trị là mã môn
                cbMonTienQuyet.SelectedIndex = -1;       // Không chọn gì ban đầu
                cbMonTienQuyet.DropDownStyle = ComboBoxStyle.DropDown;
            }

            private void cbMonTienQuyet_TextUpdate(object sender, EventArgs e)
            {
                string filter = cbMonTienQuyet.Text.ToLower();

                DataTable filtered;

                if (string.IsNullOrEmpty(filter))
                {
                    // Nếu không nhập gì thì hiển thị toàn bộ
                    filtered = dtMon;
                }
                else
                {
                    var query = dtMon.AsEnumerable()
                        .Where(row =>
                            row.Field<string>("TenMH").ToLower().Contains(filter) ||
                            row.Field<string>("MaMH").ToLower().Contains(filter));

                    // Nếu có dữ liệu thì copy, không thì tạo bảng rỗng cùng cấu trúc
                    if (query.Any())
                        filtered = query.CopyToDataTable();
                    else
                        filtered = dtMon.Clone();
                }

                // Gán dữ liệu cho combobox
                cbMonTienQuyet.DataSource = filtered;
                cbMonTienQuyet.DisplayMember = "TenMH";
                cbMonTienQuyet.ValueMember = "MaMH";

                // Mở dropdown để hiển thị kết quả lọc
                cbMonTienQuyet.DroppedDown = true;

                // Giữ nguyên text mà người dùng đã nhập
                cbMonTienQuyet.Text = filter;

                // Giữ con trỏ chuột ở cuối text
                cbMonTienQuyet.SelectionStart = cbMonTienQuyet.Text.Length;
                cbMonTienQuyet.SelectionLength = 0;
            }

            private void btnThemMH_Click(object sender, EventArgs e)
            {
                if (string.IsNullOrWhiteSpace(txtMaMH.Text) ||
                    string.IsNullOrWhiteSpace(txtTenMH.Text) ||
                    string.IsNullOrWhiteSpace(txtSoTinChi.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                if (busMH.KiemTraTonTai(txtMaMH.Text.Trim()))
                {
                    MessageBox.Show("Mã môn học đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int soTinChi;
                if (!int.TryParse(txtSoTinChi.Text, out soTinChi))
                {
                    MessageBox.Show("Số tín chỉ phải là số!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DTO_MonHoc mh = new DTO_MonHoc(
                    txtMaMH.Text.Trim(),
                    txtTenMH.Text.Trim(),
                    int.Parse(txtSoTinChi.Text.Trim()),
                    cbMonTienQuyet.SelectedValue == null ? null : cbMonTienQuyet.SelectedValue.ToString()
                );

                if (busMH.ThemMonHoc(mh))
                {
                    MessageBox.Show("Thêm môn học thành công!");
                    LoadDataMonHoc(); // load lại danh sách
                    ResetMonHoc();
                }
                else
                {
                    MessageBox.Show("Thêm môn học thất bại!");
                }
            }

            private void LoadDataMonHoc()
            {
                dgvMH.DataSource = busMH.LayDanhSachMonHoc();
            }

            private void btnSuaMH_Click(object sender, EventArgs e)
            {
                if (string.IsNullOrWhiteSpace(txtMaMH.Text))
                {
                    MessageBox.Show("Vui lòng chọn môn học để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtTenMH.Text) ||
                    string.IsNullOrWhiteSpace(txtSoTinChi.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int soTinChi;
                if (!int.TryParse(txtSoTinChi.Text, out soTinChi))
                {
                    MessageBox.Show("Số tín chỉ phải là số!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DTO_MonHoc mh = new DTO_MonHoc(
                    txtMaMH.Text.Trim(),
                    txtTenMH.Text.Trim(),
                    int.Parse(txtSoTinChi.Text.Trim()),
                    cbMonTienQuyet.SelectedValue == null ? null : cbMonTienQuyet.SelectedValue.ToString()
                );

                if (busMH.SuaMonHoc(mh))
                {
                    MessageBox.Show("Sửa môn học thành công!");
                    LoadDataMonHoc();
                    ResetMonHoc();
                }
                else
                {
                    MessageBox.Show("Sửa môn học thất bại!");
                }
            }

            private void btnXoaMH_Click(object sender, EventArgs e)
            {
                if (string.IsNullOrWhiteSpace(txtMaMH.Text))
                {
                    MessageBox.Show("Vui lòng chọn môn học để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show(
                    $"Bạn có chắc muốn xóa môn học '{txtTenMH.Text}' không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    if (busMH.XoaMonHoc(txtMaMH.Text.Trim()))
                    {
                        MessageBox.Show("Xóa môn học thành công!");
                        LoadDataMonHoc();
                        ResetMonHoc();
                    }
                    else
                    {
                        MessageBox.Show("Xóa môn học thất bại!");
                    }
                }
            }

            private void txtTimMH_TextChanged(object sender, EventArgs e)
            {
                dgvMH.DataSource = busMH.TimKiemMonHoc(txtTimMH.Text.Trim());
            }

            private void dgvMonHoc_CellClick(object sender, DataGridViewCellEventArgs e)
            {
                if (e.RowIndex >= 0) // Đảm bảo click vào hàng hợp lệ
                {
                    DataGridViewRow row = dgvMH.Rows[e.RowIndex];

                    txtMaMH.Text = row.Cells["MaMH"].Value?.ToString();
                    txtTenMH.Text = row.Cells["TenMH"].Value?.ToString();
                    txtSoTinChi.Text = row.Cells["SoTinChi"].Value?.ToString();

                    // Lấy mã môn tiên quyết từ cột
                    string maTienQuyet = row.Cells["MonTienQuyet"].Value?.ToString();

                    if (!string.IsNullOrEmpty(maTienQuyet))
                    {
                        // Gán SelectedValue để combobox chọn đúng môn
                        cbMonTienQuyet.SelectedValue = maTienQuyet;
                    }
                    else
                    {
                        // Nếu không có môn tiên quyết thì bỏ chọn
                        cbMonTienQuyet.SelectedIndex = -1;
                        cbMonTienQuyet.Text = "";
                    }
                }
            }

            private void ResetMonHoc()
            {
                txtMaMH.Clear();
                txtTenMH.Clear();
                txtSoTinChi.Clear();
                cbMonTienQuyet.SelectedIndex = -1;
                cbMonTienQuyet.Text = "";

                txtMaMH.Focus();
            }

            /*---------------------------------------------------------------------------------------------------*/

            //QUẢN LÝ SINH VIÊN
        
            private void LoadSinhVien()
            {
                dgvSinhVien.DataSource = busSV.LayDanhSachSinhVien();
            }

            private void btnThemSV_Click(object sender, EventArgs e)
            {
                if (string.IsNullOrWhiteSpace(txtMaSV.Text) ||
                string.IsNullOrWhiteSpace(txtHoTenSV.Text) ||
                cbMaLopSV.SelectedIndex == -1 ||
                cbGioiTinh.SelectedIndex == -1 ||
                cbTrangThaiHocVu.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(txtEmailSV.Text)) 
            
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (busSV.KiemTraTonTai(txtMaSV.Text.Trim()))
                {
                    MessageBox.Show("Mã sinh viên đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DTO_SinhVien sv = new DTO_SinhVien(
                txtMaSV.Text.Trim(),
                txtHoTenSV.Text.Trim(),
                dtNgaySinhSV.Value,
                cbGioiTinh.Text,
                cbMaLopSV.SelectedValue.ToString(),
                cbTrangThaiHocVu.Text.Trim(),
                txtEmailSV.Text.Trim()
                );

                if (busSV.ThemSinhVien(sv))
                {
                    MessageBox.Show("Thêm thành công!");
                    LoadSinhVien();
                    ResetSinhVien();
                }
                else
                {
                    MessageBox.Show("Thêm thất bại!");
                }
            }

            private void btnSuaSV_Click(object sender, EventArgs e)
            {
                if (string.IsNullOrWhiteSpace(txtMaSV.Text) ||
                    string.IsNullOrWhiteSpace(txtHoTenSV.Text) ||
                    cbMaLopSV.SelectedIndex == -1 ||
                    cbGioiTinh.SelectedIndex == -1 ||
                    cbTrangThaiHocVu.SelectedIndex == -1 ||
                    string.IsNullOrWhiteSpace(txtEmailSV.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DTO_SinhVien sv = new DTO_SinhVien(
                 txtMaSV.Text.Trim(),
                txtHoTenSV.Text.Trim(),
                dtNgaySinhSV.Value,
                cbGioiTinh.Text,
                cbMaLopSV.SelectedValue.ToString(),
                cbTrangThaiHocVu.Text.Trim(),
                txtEmailSV.Text.Trim()
         );

                if (busSV.SuaSinhVien(sv))
                {
                    MessageBox.Show("Sửa thành công!");
                    LoadSinhVien();
                    ResetSinhVien();
                }
                else
                {
                    MessageBox.Show("Sửa thất bại!");
                }
            }

            private void btnXoaSV_Click(object sender, EventArgs e)
            {

                if (string.IsNullOrWhiteSpace(txtMaSV.Text))
                {
                    MessageBox.Show("Vui lòng chọn sinh viên muốn xóa!",
                                    "Chưa chọn dữ liệu",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa sinh viên {txtHoTenSV.Text} không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );
                if (result == DialogResult.Yes)
                {
                    if (busSV.XoaSinhVien(txtMaSV.Text.Trim()))
                    {
                        MessageBox.Show("Xóa sinh viên thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                        LoadSinhVien();
                        LoadSinhVien();
                        ResetSinhVien();
                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại!",
                              "Lỗi",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
                    }
                }
            }

            private void txtTimSV_TextChanged(object sender, EventArgs e)
            {
                dgvSinhVien.DataSource = busSV.TimKiemSinhVien(txtTimSV.Text);
            }

            private void dgvSinhVien_CellClick(object sender, DataGridViewCellEventArgs e)
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dgvSinhVien.Rows[e.RowIndex];
                    txtMaSV.Text = row.Cells["MaSV"].Value.ToString();
                    txtHoTenSV.Text = row.Cells["HoTen"].Value.ToString();
                    dtNgaySinhSV.Value = Convert.ToDateTime(row.Cells["NgaySinh"].Value);
                    cbGioiTinh.Text = row.Cells["GioiTinh"].Value.ToString();
                    cbMaLopSV.SelectedValue = row.Cells["MaLop"].Value.ToString();
                    cbTrangThaiHocVu.Text = row.Cells["TrangThai"].Value.ToString();
                    txtEmailSV.Text = row.Cells["Email"].Value.ToString();
                }
            }

            private void ResetSinhVien()
            {
                txtMaSV.Clear();
                txtHoTenSV.Clear();
                txtEmailSV.Clear();
                cbGioiTinh.SelectedIndex = -1;
                cbMaLopSV.SelectedIndex = -1;
                cbTrangThaiHocVu.SelectedIndex = -1;
                dtNgaySinhSV.Value = DateTime.Now;
                txtMaSV.Focus();
            }

            /*---------------------------------------------------------------------------------------------------*/

            //QUẢN LÝ GIẢNG VIÊN
       
        
            private void LoadKhoaGV()
            {
                dtKhoaGV = busKhoa.LayDanhSachKhoa(); // Lấy tất cả khoa từ DB
            

                cbMaKhoaGV.DataSource = dtKhoaGV;
                cbMaKhoaGV.DisplayMember = "TenKhoa";
                cbMaKhoaGV.ValueMember = "MaKhoa";
                cbMaKhoaGV.SelectedIndex = -1;
                cbMaKhoaGV.DropDownStyle = ComboBoxStyle.DropDown;
                cbMaKhoaGV.AutoCompleteMode = AutoCompleteMode.None;
                cbMaKhoaGV.AutoCompleteSource = AutoCompleteSource.None;

            }
            private void LoadGiangVien()
            {
                dgvGiangVien.DataSource = busGV.LayDanhSachGiangVien();
            }

            private void btnThemGV_Click(object sender, EventArgs e)
            {
                if (string.IsNullOrWhiteSpace(txtMaGV.Text) || string.IsNullOrWhiteSpace(txtHoTenGV.Text) || string.IsNullOrWhiteSpace(txtEmailGV.Text) || cbMaKhoaGV.SelectedIndex == -1 || cbGioiTinhGV.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (busGV.KiemTraTonTai(txtMaGV.Text.Trim()))
                {
                    MessageBox.Show("Mã giảng viên đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DTO_GiangVien gv = new DTO_GiangVien(
                    txtMaGV.Text.Trim(),
                    txtHoTenGV.Text.Trim(),
                    txtEmailGV.Text.Trim(),
                    cbMaKhoaGV.SelectedValue.ToString(),
                    cbGioiTinhGV.Text
                );

                if (busGV.ThemGiangVien(gv))
                {
                    MessageBox.Show("Thêm giảng viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadGiangVien();
                    ResetGiangVien();
                }
                else
                {
                    MessageBox.Show("Thêm giảng viên thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            private void btnSuaGV_Click(object sender, EventArgs e)
            {
                if (string.IsNullOrWhiteSpace(txtMaGV.Text))
                {
                    MessageBox.Show("Vui lòng chọn giảng viên cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DTO_GiangVien gv = new DTO_GiangVien(
                    txtMaGV.Text.Trim(),
                    txtHoTenGV.Text.Trim(),
                    txtEmailGV.Text.Trim(),
                    cbMaKhoaGV.SelectedValue.ToString(),
                    cbGioiTinhGV.Text
                );

                if (busGV.CapNhatGiangVien(gv))
                {
                    MessageBox.Show("Sửa giảng viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadGiangVien();
                    ResetGiangVien();
                }
                else
                {
                    MessageBox.Show("Sửa giảng viên thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            private void btnXoaGV_Click(object sender, EventArgs e)
            {
                if (string.IsNullOrWhiteSpace(txtMaGV.Text))
                {
                    MessageBox.Show("Vui lòng chọn giảng viên cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Bạn có chắc chắn muốn xóa giảng viên này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (busGV.XoaGiangVien(txtMaGV.Text.Trim()))
                    {
                        MessageBox.Show("Xóa giảng viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadGiangVien();
                        ResetGiangVien();
                    }
                    else
                    {
                        MessageBox.Show("Xóa giảng viên thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                }

            private void txtTimGV_TextChanged(object sender, EventArgs e)
            {
                dgvGiangVien.DataSource = busGV.TimKiemGiangVien(txtTimKiemGV.Text.Trim());
            }

            private void dgvGiangVien_CellClick(object sender, DataGridViewCellEventArgs e)
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dgvGiangVien.Rows[e.RowIndex];
                    txtMaGV.Text = row.Cells["MaGV"].Value.ToString();
                    txtHoTenGV.Text = row.Cells["HoTen"].Value.ToString();
                    txtEmailGV.Text = row.Cells["Email"].Value.ToString();
                    cbMaKhoaGV.SelectedValue = row.Cells["MaKhoa"].Value.ToString();
                    cbGioiTinhGV.Text = row.Cells["GioiTinh"].Value.ToString();
                }
            }
            private void ResetGiangVien()
            {
                txtMaGV.Clear();
                txtHoTenGV.Clear();
                txtEmailGV.Clear();
                cbMaKhoaGV.SelectedIndex = -1;
                cbGioiTinhGV.SelectedIndex = -1;
            }

            private void cbMaKhoaGV_TextUpdate(object sender, EventArgs e)
            {
                string filter = cbMaKhoaGV.Text; // KHÔNG Trim() để giữ dấu cách
                int cursorPos = cbMaKhoaGV.SelectionStart; // lưu vị trí con trỏ

                DataTable filtered;

                if (string.IsNullOrEmpty(filter))
                {
                    filtered = dtKhoaGV; // hiển thị toàn bộ nếu không nhập gì
                }
                else
                {
                    var query = dtKhoaGV.AsEnumerable()
                        .Where(row =>
                            row.Field<string>("TenKhoa").IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0 ||
                            row.Field<string>("MaKhoa").IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0);

                    filtered = query.Any() ? query.CopyToDataTable() : dtKhoaGV.Clone();
                }

                // gán dữ liệu mới
                cbMaKhoaGV.DataSource = filtered;
                cbMaKhoaGV.DisplayMember = "TenKhoa";
                cbMaKhoaGV.ValueMember = "MaKhoa";

                cbMaKhoaGV.DroppedDown = true; // mở dropdown

                cbMaKhoaGV.Text = filter; // giữ lại text
                cbMaKhoaGV.SelectionStart = cursorPos; // giữ nguyên vị trí con trỏ
                cbMaKhoaGV.SelectionLength = 0; // không chọn lại text
            }


            /*---------------------------------------------------------------------------------------------------*/
            //QUẢN LÝ LỚP HỌC PHẦN
            private void LoadLopHocPhan()
            {
                dgvLHP.DataSource = busLHP.LayTatCaLopHocPhan();
            }

            private void LoadMonHocLHP()
            {
                dtMonLHP = busMH.LayDanhSachMonHoc();
                cbMonLHP.DataSource = dtMonLHP;
                cbMonLHP.DisplayMember = "TenMH";
                cbMonLHP.ValueMember = "MaMH";
                cbMonLHP.SelectedIndex = -1;
                cbMonLHP.DropDownStyle = ComboBoxStyle.DropDown;
            }

            private void LoadGiangVienLHP()
            {
                dtGiangVienLHP = busGV.LayDanhSachGiangVien();
                cbGVLHP.DataSource = dtGiangVienLHP;
                cbGVLHP.DisplayMember = "HoTen";
                cbGVLHP.ValueMember = "MaGV";
                cbGVLHP.SelectedIndex = -1;
                cbGVLHP.DropDownStyle = ComboBoxStyle.DropDown;
            }

            private void LoadHocKyLHP()
            {
                dtHocKyLHP = busHK.LayDanhSachHocKy(); // hoặc tự tạo DataTable
                cbHKLHP.DataSource = dtHocKyLHP;
                cbHKLHP.DisplayMember = "MaHK";
                cbHKLHP.ValueMember = "MaHK";
                cbHKLHP.SelectedIndex = -1;
                cbHKLHP.DropDownStyle = ComboBoxStyle.DropDown;
            }

            

            private void cbMonHocLHP_TextUpdate(object sender, EventArgs e)
            {
                FilterComboBox(cbMonLHP, dtMonLHP, "TenMH", "MaMH");
            }

            private void cbGiangVienLHP_TextUpdate(object sender, EventArgs e)
            {
                FilterComboBox(cbGVLHP, dtGiangVienLHP, "HoTen", "MaGV");
            }

            private void cbHocKyLP_TextUpdate(object sender, EventArgs e)
            {
                FilterComboBox(cbHKLHP, dtHocKyLHP, "MaHK", "MaHK");
            }

            private void FilterComboBox(ComboBox combo, DataTable source, string displayCol, string valueCol)
            {
            string filter = combo.Text;
            int cursorPos = combo.SelectionStart;

            DataTable filtered;
            if (string.IsNullOrEmpty(filter))
                filtered = source;
            else
            {
                var query = source.AsEnumerable()
                    .Where(row =>
                        row.Field<string>(displayCol).IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        row.Field<string>(valueCol).IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0);

                filtered = query.Any() ? query.CopyToDataTable() : source.Clone();
            }

            combo.DataSource = filtered;
            combo.DisplayMember = displayCol;
            combo.ValueMember = valueCol;
            combo.DroppedDown = true;
            combo.Text = filter;
            combo.SelectionStart = cursorPos;
            combo.SelectionLength = 0;
        }

        private void btnThemLHP_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaLHP.Text) ||
                cbMonLHP.SelectedIndex == -1 ||
                cbGVLHP.SelectedIndex == -1 ||
                cbHKLHP.SelectedIndex == -1 ||
                cbThu.SelectedIndex == -1 ||
                cbBatDau.SelectedIndex == -1 ||
                cbKetThuc.SelectedIndex == -1 ||
                cbPhongHoc.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maLHP = txtMaLHP.Text.Trim();
            string maMH = cbMonLHP.SelectedValue.ToString();
            string maGV = cbGVLHP.SelectedValue.ToString();
            string maHK = cbHKLHP.SelectedValue.ToString();
            int thu = int.Parse(cbThu.Text);
            int tietBD = int.Parse(cbBatDau.Text);
            int tietKT = int.Parse(cbKetThuc.Text);
            string phong = cbPhongHoc.SelectedItem.ToString();

            if (busLHP.KiemTraTonTai(maLHP))
            {
                MessageBox.Show("Mã lớp học phần đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (busLHP.KiemTraTrungLich(maGV, thu, tietBD, tietKT, maHK, phong))
            {
                MessageBox.Show("Lịch học bị trùng (giảng viên hoặc phòng)!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DTO_LopHocPhan lhp = new DTO_LopHocPhan(maLHP, maMH, maGV, maHK, tietBD, tietKT, thu, phong);
            if (busLHP.ThemLopHocPhan(lhp))
            {
                MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadLopHocPhan();
                ResetLopHocPhan();
            }
            else
            {
                MessageBox.Show("Thêm thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Sửa lớp học phần
        private void btnSuaLHP_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaLHP.Text))
            {
                MessageBox.Show("Vui lòng chọn lớp học phần cần sửa!", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maLHP = txtMaLHP.Text.Trim();
            string maMH = cbMonLHP.SelectedValue.ToString();
            string maGV = cbGVLHP.SelectedValue.ToString();
            string maHK = cbHKLHP.SelectedValue.ToString();
            int thu = int.Parse(cbThu.Text);
            int tietBD = int.Parse(cbBatDau.Text);
            int tietKT = int.Parse(cbKetThuc.Text);
            string phong = cbPhongHoc.SelectedItem.ToString();

            if (busLHP.KiemTraTrungLichSua(maLHP, maGV, thu, tietBD, tietKT, maHK, phong))
            {
                MessageBox.Show("Lịch học bị trùng (giảng viên hoặc phòng)!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DTO_LopHocPhan lhp = new DTO_LopHocPhan(maLHP, maMH, maGV, maHK, tietBD, tietKT, thu, phong);
            if (busLHP.SuaLopHocPhan(lhp))
            {
                MessageBox.Show("Sửa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadLopHocPhan();
                ResetLopHocPhan();
            }
            else
            {
                MessageBox.Show("Sửa thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Xóa lớp học phần
        private void btnXoaLHP_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaLHP.Text))
            {
                MessageBox.Show("Vui lòng chọn lớp học phần cần xóa!", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Bạn có chắc muốn xóa lớp học phần '{txtMaLHP.Text}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (busLHP.XoaLopHocPhan(txtMaLHP.Text.Trim()))
                {
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadLopHocPhan();
                    ResetLopHocPhan();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        //Tìm kiếm lớp học phần theo mã
        private void txtTimLHP_TextChanged(object sender, EventArgs e)
        {
            string ma = txtTimLHP.Text.Trim();
            if (string.IsNullOrEmpty(ma))
                LoadLopHocPhan(); // hiển thị toàn bộ
            else
                dgvLHP.DataSource = busLHP.TimLopHocPhan(ma);
        }

        // Chọn hàng DataGridView
        private void dgvLHP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvLHP.Rows[e.RowIndex];
                txtMaLHP.Text = row.Cells["MaLHP"].Value?.ToString();
                cbMonLHP.SelectedValue = row.Cells["MaMH"].Value?.ToString();
                cbGVLHP.SelectedValue = row.Cells["MaGV"].Value?.ToString();
                cbHKLHP.SelectedValue = row.Cells["MaHK"].Value?.ToString();
                cbThu.Text = row.Cells["Thu"].Value?.ToString();
                cbBatDau.Text = row.Cells["TietBatDau"].Value?.ToString();
                cbKetThuc.Text = row.Cells["TietKetThuc"].Value?.ToString();
                cbPhongHoc.SelectedItem = row.Cells["PhongHoc"].Value?.ToString();
            }
        }

        // Reset form
        private void ResetLopHocPhan()
        {
            txtMaLHP.Clear();
            cbMonLHP.SelectedIndex = -1;
            cbGVLHP.SelectedIndex = -1;
            cbHKLHP.SelectedIndex = -1;
            cbThu.SelectedIndex = -1;
            cbBatDau.SelectedIndex = -1;
            cbKetThuc.SelectedIndex = -1;
            cbPhongHoc.SelectedIndex = -1;
            txtMaLHP.Focus();
        }


    }
}

