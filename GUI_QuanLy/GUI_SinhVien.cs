using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS_QuanLy;
using DTO_QuanLy;

namespace GUI_QuanLy
{
    public partial class GUI_SinhVien : Form
    {
        private string maSVDangNhap;
        DataTable dt = new DataTable();

        private BUS_HocKy busHocKy = new BUS_HocKy();
        private BUS_LopHocPhan busLHP = new BUS_LopHocPhan();
        private BUS_DangKyHoc busDK = new BUS_DangKyHoc();
        private BUS_GPA busGPA = new BUS_GPA();
        public GUI_SinhVien(string maSV)
        {
            InitializeComponent();
            maSVDangNhap = maSV;
        }
        public GUI_SinhVien()
        {
            InitializeComponent();
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            if (dgvDK.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn lớp học phần để đăng ký!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maLHP = dgvDK.SelectedRows[0].Cells["MaLHP"].Value.ToString();

            // Kiểm tra đã đăng ký lớp học phần này chưa
            if (busDK.KiemTraDaDangKy(maSVDangNhap, maLHP))
            {
                MessageBox.Show("Bạn đã đăng ký lớp học phần này rồi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Kiểm tra trùng lịch
            if (busDK.KiemTraTrungLich(maSVDangNhap, maLHP))
            {
                MessageBox.Show("Lớp này bị trùng lịch với môn đã đăng ký!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra môn tiên quyết
            if (busDK.KiemTraMonTienQuyet(maSVDangNhap, maLHP))
            {
                MessageBox.Show("Bạn chưa hoàn thành môn tiên quyết!", "Không thể đăng ký", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tạo đối tượng đăng ký
            DTO_DangKyHoc dk = new DTO_DangKyHoc
            {
                MaSV = maSVDangNhap,
                MaLHP = maLHP
            };

            // Thêm đăng ký
            bool kq = busDK.ThemDangKy(dk);
            if (kq)
            {
                MessageBox.Show("Đăng ký thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadMonDaDangKy(); // Tải lại danh sách
            }
            else
            {
                MessageBox.Show("Đăng ký thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GUI_SinhVien_Load(object sender, EventArgs e)
        {
            LoadHocKyVaoComboBox();
            LoadMonDaDangKy();
        }
        private void LoadHocKyVaoComboBox()
        {
            DataTable dtHK = busHocKy.LayDanhSachHocKy();

            cbHocKy.DataSource = dtHK.Copy();
            cbHocKy.DisplayMember = "TenHocKy";
            cbHocKy.ValueMember = "MaHK";
            cbHocKy.SelectedIndex = -1;

            cbHocKy2.DataSource = dtHK.Copy();
            cbHocKy2.DisplayMember = "TenHocKy";
            cbHocKy2.ValueMember = "MaHK";
            cbHocKy2.SelectedIndex = -1;

            cbHocKy3.DataSource = dtHK.Copy();
            cbHocKy3.DisplayMember = "TenHocKy";
            cbHocKy3.ValueMember = "MaHK";
            cbHocKy3.SelectedIndex = -1;

            cbHocKy4.DataSource = dtHK.Copy();
            cbHocKy4.DisplayMember = "TenHocKy";
            cbHocKy4.ValueMember = "MaHK";
            cbHocKy4.SelectedIndex = -1;
        }

        private void LoadMonTheoHocKy(string maHK)
        {
            DataTable dt = busLHP.LayLopHocPhanTheoHocKy(maHK);
            dgvDK.DataSource = dt;
        }

        private void LoadMonDaDangKy(string maHK = "")
        {
            dgvXemDK.DataSource = busDK.LayMonDaDangKy(maSVDangNhap, maHK);
        }

        private void cbHocKy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbHocKy.SelectedValue != null)
            {
                string maHK = cbHocKy.SelectedValue.ToString();
                LoadMonTheoHocKy(maHK);
            }
        }

        private void cbHocKy2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbHocKy2.SelectedValue != null)
            {
                string maHK = cbHocKy2.SelectedValue.ToString();
                LoadMonDaDangKy(maHK);
            }
        }

        private void btnALL_Click(object sender, EventArgs e)
        {
            dgvDK.DataSource = busLHP.LayTatCaLopHocPhan();
        }

        private void btnHuyDangKy_Click(object sender, EventArgs e)
        {
            if (dgvXemDK.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một môn học để hủy đăng ký.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maLHP = dgvXemDK.CurrentRow.Cells["MaLHP"].Value.ToString();

            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn hủy đăng ký môn này?",
                "Xác nhận hủy",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                BUS_DangKyHoc busDK = new BUS_DangKyHoc();
                bool success = busDK.HuyDangKy(maSVDangNhap, maLHP);

                if (success)
                {
                    MessageBox.Show("Hủy đăng ký thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Tải lại danh sách môn đã đăng ký
                    string selectedMaHK = cbHocKy2.SelectedValue?.ToString() ?? "";
                    dgvXemDK.DataSource = busDK.LayMonDaDangKy(maSVDangNhap, selectedMaHK);
                }
                else
                {
                    MessageBox.Show("Hủy đăng ký thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cbHocKy3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbHocKy3.SelectedValue != null)
            {
                string maHK = cbHocKy3.SelectedValue.ToString();
                LoadThoiKhoaBieu(maHK);
            }
        }
        private void LoadThoiKhoaBieu(string maHK)
        {
            BUS_LopHocPhan busLHP = new BUS_LopHocPhan();
            DataTable dtTKB = busLHP.LayThoiKhoaBieu(maSVDangNhap, maHK);
            dgvTKB.DataSource = dtTKB;
        }

        private void cbHocKy4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbHocKy4.SelectedValue != null)
            {
                string maHK = cbHocKy4.SelectedValue.ToString();
                LoadDiemVaGPA(maHK);
            }
        }
        private void LoadDiemVaGPA(string maHK)
        {
            BUS_DangKyHoc busDK = new BUS_DangKyHoc();
            DataTable dtDiem = busDK.LayDiemSinhVien(maSVDangNhap, maHK);
            dgvDiem.DataSource = dtDiem;

            // Lấy GPA
            BUS_GPA busGPA = new BUS_GPA();
            string gpa = busGPA.LayGPA(maSVDangNhap, maHK);
            lbGPA.Text = string.IsNullOrEmpty(gpa) ? "None" : gpa;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            DataTable dtSource = dgvDK.DataSource as DataTable;
            if (dtSource == null) return;

            string keyword = RemoveDiacritics(txtTimKiem.Text.Trim().ToLower());

            if (string.IsNullOrEmpty(keyword))
            {
                dgvDK.DataSource = dtSource;
            }
            else
            {
                var filtered = dtSource.AsEnumerable()
                    .Where(row => RemoveDiacritics(row["TenMH"].ToString().ToLower()).Contains(keyword));

                if (filtered.Any())
                    dgvDK.DataSource = filtered.CopyToDataTable();
                else
                    dgvDK.DataSource = dtSource.Clone(); // Trả về bảng rỗng nếu không có kết quả
            }
        }
        private string RemoveDiacritics(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
