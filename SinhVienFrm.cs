using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySinhVien
{
    public partial class SinhVienFrm : Form
    {
        private string maSVDangNhap;
        DataTable dt = new DataTable();
        public SinhVienFrm(string maSV)
        {
            InitializeComponent();
            maSVDangNhap = maSV;
        }

        private void btnALL_Click(object sender, EventArgs e)
        {
            LoadTableMonHoc();
        }
        private void LoadTableMonHoc()
        {
            string query = @"
        SELECT 
            lhp.MaLHP,
            mh.TenMH,
            mh.SoTinChi,
            gv.HoTen AS GiangVien,
            lhp.TietBatDau,
            lhp.TietKetThuc,
            lhp.Thu,
            lhp.PhongHoc,
            hk.HocKySo,
            hk.NamHoc
        FROM LopHocPhan lhp
        JOIN MonHoc mh ON lhp.MaMH = mh.MaMH
        JOIN GiangVien gv ON lhp.MaGV = gv.MaGV
        JOIN HocKy hk ON lhp.MaHK = hk.MaHK
    ";

            dt.Clear();
            dt = DataProvider.LoadCSDL(query);
            dgvDK.DataSource = dt;
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            if (dgvDK.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn lớp học phần để đăng ký!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maLHP = dgvDK.SelectedRows[0].Cells["MaLHP"].Value.ToString();

            if (KiemTraTrungLich(maSVDangNhap, maLHP))
            {
                MessageBox.Show("Lớp này bị trùng lịch với môn đã đăng ký!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (KiemTraMonTienQuyet(maSVDangNhap, maLHP))
            {
                MessageBox.Show("Bạn chưa hoàn thành môn tiên quyết!", "Không thể đăng ký", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra đã đăng ký chưa
            string checkQuery = $"SELECT COUNT(*) FROM DangKyHoc WHERE MaSV = '{maSVDangNhap}' AND MaLHP = '{maLHP}'";
            int count = Convert.ToInt32(DataProvider.ExecuteScalar(checkQuery));

            if (count > 0)
            {
                MessageBox.Show("Bạn đã đăng ký lớp học phần này rồi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Đăng ký mới
            string insertQuery = $"INSERT INTO DangKyHoc (MaSV, MaLHP) VALUES ('{maSVDangNhap}', '{maLHP}')";
            int result = DataProvider.ExecuteNonQuery(insertQuery);

            if (result > 0)
                MessageBox.Show("Đăng ký thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Đăng ký thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void LoadHocKyVaoComboBox()
        {
            string query = "SELECT MaHK, NamHoc + ' - HK' + CAST(HocKySo AS VARCHAR) AS TenHocKy FROM HocKy";
            DataTable dtHocKy = DataProvider.LoadCSDL(query);

            cbHocKy.DisplayMember = "TenHocKy"; // Hiển thị đẹp
            cbHocKy.ValueMember = "MaHK";       // Dùng để truy vấn
            cbHocKy.DataSource = dtHocKy;

            cbHocKy2.DataSource = dtHocKy;
            cbHocKy2.DisplayMember = "TenHocKy";
            cbHocKy2.ValueMember = "MaHK";
            cbHocKy2.SelectedIndex = -1;

            cbHocKy3.DataSource = dtHocKy.Copy();
            cbHocKy3.DisplayMember = "TenHocKy";
            cbHocKy3.ValueMember = "MaHK";
            cbHocKy3.SelectedIndex = -1;

            cbHocKy4.DataSource = dtHocKy.Copy();
            cbHocKy4.DisplayMember = "TenHocKy";
            cbHocKy4.ValueMember = "MaHK";
            cbHocKy4.SelectedIndex = -1;
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            LoadHocKyVaoComboBox();
            LoadMonDaDangKy();
        }
        private void LoadMonTheoHocKy(string maHK)
        {
            string query = $@"
        SELECT 
            lhp.MaLHP,
            mh.TenMH,
            mh.SoTinChi,
            gv.HoTen AS GiangVien,
            lhp.TietBatDau,
            lhp.TietKetThuc,
            lhp.Thu,
            lhp.PhongHoc
        FROM LopHocPhan lhp
        JOIN MonHoc mh ON lhp.MaMH = mh.MaMH
        JOIN GiangVien gv ON lhp.MaGV = gv.MaGV
        WHERE lhp.MaHK = '{maHK}'";

            dt.Clear();
            dt = DataProvider.LoadCSDL(query);
            dgvDK.DataSource = dt;
        }
        private void LoadMonDaDangKy(string maHK = "")
        {
            string query = $@"
        SELECT 
            dk.MaLHP,
            mh.TenMH,
            mh.SoTinChi,
            gv.HoTen AS GiangVien,
            lhp.TietBatDau,
            lhp.TietKetThuc,
            lhp.Thu,
            lhp.PhongHoc,
            hk.HocKySo,
            hk.NamHoc
        FROM DangKyHoc dk
        JOIN LopHocPhan lhp ON dk.MaLHP = lhp.MaLHP
        JOIN MonHoc mh ON lhp.MaMH = mh.MaMH
        JOIN GiangVien gv ON lhp.MaGV = gv.MaGV
        JOIN HocKy hk ON lhp.MaHK = hk.MaHK
        WHERE dk.MaSV = '{maSVDangNhap}'";

            // Nếu có chọn học kỳ, thì thêm điều kiện
            if (!string.IsNullOrEmpty(maHK))
            {
                query += $" AND hk.MaHK = '{maHK}'";
            }

            DataTable dtMonDaDK = DataProvider.LoadCSDL(query);
            dgvXemDK.DataSource = dtMonDaDK;
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

        private void btnHuyDangKy_Click(object sender, EventArgs e)
        {
            if (dgvXemDK.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một môn học để hủy đăng ký.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy MaLHP từ dòng được chọn
            string maLHP = dgvXemDK.CurrentRow.Cells["MaLHP"].Value.ToString();

            // Xác nhận hủy đăng ký
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn hủy đăng ký môn này?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                string query = $"DELETE FROM DangKyHoc WHERE MaSV = '{maSVDangNhap}' AND MaLHP = '{maLHP}'";
                int rows = DataProvider.ExecuteNonQuery(query); // Gọi hàm thực thi không trả về bảng

                if (rows > 0)
                {
                    MessageBox.Show("Hủy đăng ký thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    string selectedMaHK = cbHocKy2.SelectedValue?.ToString() ?? "";
                    LoadMonDaDangKy(selectedMaHK); // Tải lại danh sách
                }
                else
                {
                    MessageBox.Show("Hủy đăng ký thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool KiemTraTrungLich(string maSV, string maLHPMoi)
        {
            // Lấy thông tin lịch học của lớp mới
            string queryLHP = $@"
        SELECT Thu, TietBatDau, TietKetThuc
        FROM LopHocPhan
        WHERE MaLHP = '{maLHPMoi}'";

            DataTable dtMoi = DataProvider.LoadCSDL(queryLHP);
            if (dtMoi.Rows.Count == 0) return false;

            int thuMoi = Convert.ToInt32(dtMoi.Rows[0]["Thu"]);
            int tietBDMoi = Convert.ToInt32(dtMoi.Rows[0]["TietBatDau"]);
            int tietKTMoi = Convert.ToInt32(dtMoi.Rows[0]["TietKetThuc"]);

            // Lấy các lớp SV đã đăng ký
            string queryDK = $@"
        SELECT lhp.Thu, lhp.TietBatDau, lhp.TietKetThuc
        FROM DangKyHoc dkh
        JOIN LopHocPhan lhp ON dkh.MaLHP = lhp.MaLHP
        WHERE dkh.MaSV = '{maSV}'";

            DataTable dtDK = DataProvider.LoadCSDL(queryDK);

            foreach (DataRow row in dtDK.Rows)
            {
                int thu = Convert.ToInt32(row["Thu"]);
                int tietBD = Convert.ToInt32(row["TietBatDau"]);
                int tietKT = Convert.ToInt32(row["TietKetThuc"]);

                if (thu == thuMoi &&
                    !(tietKT < tietBDMoi || tietBD > tietKTMoi)) // có giao nhau
                {
                    return true; // Bị trùng lịch
                }
            }

            return false;
        }

        private bool KiemTraMonTienQuyet(string maSV, string maLHP)
        {
            // Lấy mã môn học từ lớp học phần
            string queryMH = $"SELECT MaMH FROM LopHocPhan WHERE MaLHP = '{maLHP}'";
            DataTable dt = DataProvider.LoadCSDL(queryMH);
            if (dt.Rows.Count == 0) return false;

            string maMH = dt.Rows[0]["MaMH"].ToString();

            // Lấy mã môn tiên quyết của môn này
            string queryTienQuyet = $"SELECT MonTienQuyet FROM MonHoc WHERE MaMH = '{maMH}'";
            DataTable dt2 = DataProvider.LoadCSDL(queryTienQuyet);
            if (dt2.Rows.Count == 0) return false;

            string monTienQuyet = dt2.Rows[0]["MonTienQuyet"].ToString();
            if (string.IsNullOrEmpty(monTienQuyet)) return false; // Không có môn tiên quyết → OK

            // Kiểm tra sinh viên đã học và qua môn tiên quyết chưa
            string queryKiemTra = $@"
        SELECT d.Diem
        FROM DangKyHoc d
        JOIN LopHocPhan lhp ON d.MaLHP = lhp.MaLHP
        WHERE d.MaSV = '{maSV}' AND lhp.MaMH = '{monTienQuyet}' AND d.Diem >= 5.0";

            DataTable dt3 = DataProvider.LoadCSDL(queryKiemTra);
            return dt3.Rows.Count == 0; // Nếu không có dòng nào => chưa học/không qua
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
            string query = $@"
        SELECT 
            mh.TenMH,
            gv.HoTen AS GiangVien,
            lhp.Thu,
            lhp.TietBatDau,
            lhp.TietKetThuc,
            lhp.PhongHoc
        FROM DangKyHoc dk
        JOIN LopHocPhan lhp ON dk.MaLHP = lhp.MaLHP
        JOIN MonHoc mh ON lhp.MaMH = mh.MaMH
        JOIN GiangVien gv ON lhp.MaGV = gv.MaGV
        WHERE dk.MaSV = '{maSVDangNhap}' AND lhp.MaHK = '{maHK}'
    ";

            DataTable dtTKB = DataProvider.LoadCSDL(query);
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
            string queryDiem = $@"
        SELECT 
            mh.TenMH,
            dk.Diem
        FROM DangKyHoc dk
        JOIN LopHocPhan lhp ON dk.MaLHP = lhp.MaLHP
        JOIN MonHoc mh ON lhp.MaMH = mh.MaMH
        WHERE dk.MaSV = '{maSVDangNhap}' AND lhp.MaHK = '{maHK}'";

            DataTable dtDiem = DataProvider.LoadCSDL(queryDiem);
            dgvDiem.DataSource = dtDiem;

            // Lấy GPA
            string queryGPA = $"SELECT GPA FROM GPA WHERE MaSV = '{maSVDangNhap}' AND MaHK = '{maHK}'";
            DataTable dtGPA = DataProvider.LoadCSDL(queryGPA);

            if (dtGPA.Rows.Count > 0)
                lbGPA.Text = dtGPA.Rows[0]["GPA"].ToString();
            else
                lbGPA.Text = "None";
        }
    }
}
