using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveCharts.Wpf;
using LiveCharts;

namespace QuanLySinhVien
{
    public partial class GiangVienFrm : Form
    {
        private string maGVDangNhap;
        public GiangVienFrm(string maGV)
        {
            InitializeComponent();
            maGVDangNhap = maGV;
        }

        private void GiangVienFrm_Load(object sender, EventArgs e)
        {
            maGV.Text = maGVDangNhap;
            LoadLopHP(maGVDangNhap);
            LoadHocKy();
        }

        private void LoadLopHP(string maGV)
        {
            string query = $"SELECT MaLHP FROM LopHocPhan WHERE MaGV = '{maGV}'";
            DataTable dt = DataProvider.LoadCSDL(query);
            cbLopHP.DataSource = dt;
            cbLopHP.DisplayMember = "MaLHP";
            cbLopHP.ValueMember = "MaLHP";
        }

        private void LoadDSSinhVien(string maLHP)
        {
            string query = $@"
        SELECT dk.MaSV, sv.HoTen, dk.Diem 
        FROM DangKyHoc dk 
        JOIN SinhVien sv ON dk.MaSV = sv.MaSV 
        WHERE dk.MaLHP = '{maLHP}'";

            DataTable dt = DataProvider.LoadCSDL(query);
            dgvDSSV.DataSource = dt;
        }

        private void ResetNut()
        {
            btnNhap.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            lbMaSV.Text = "...";
            lbTenSV.Text = "...";
            txtDiem.Clear();
        }


        private void btnNhap_Click_1(object sender, EventArgs e)
        {
            string maSV = lbMaSV.Text;
            string maLHP = cbLopHP.SelectedValue.ToString();
            float diem;
            if (!float.TryParse(txtDiem.Text, out diem) || diem < 0 || diem > 10)
            {
                MessageBox.Show("Điểm không hợp lệ!");
                return;
            }

            string query = $"UPDATE DangKyHoc SET Diem = {diem} WHERE MaSV = '{maSV}' AND MaLHP = '{maLHP}'";
            DataProvider.ExecuteNonQuery(query);
            MessageBox.Show("Nhập điểm thành công!");
            LoadDSSinhVien(maLHP);
            ResetNut();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maSV = lbMaSV.Text;
            string maLHP = cbLopHP.SelectedValue.ToString();
            float diem;
            if (!float.TryParse(txtDiem.Text, out diem) || diem < 0 || diem > 10)
            {
                MessageBox.Show("Điểm không hợp lệ!");
                return;
            }

            string query = $"UPDATE DangKyHoc SET Diem = {diem} WHERE MaSV = '{maSV}' AND MaLHP = '{maLHP}'";
            DataProvider.ExecuteNonQuery(query);
            MessageBox.Show("Sửa điểm thành công!");
            LoadDSSinhVien(maLHP);
            ResetNut();
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            string maSV = lbMaSV.Text;
            string maLHP = cbLopHP.SelectedValue.ToString();

            DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa điểm này?", "Xác nhận", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                string query = $"UPDATE DangKyHoc SET Diem = NULL WHERE MaSV = '{maSV}' AND MaLHP = '{maLHP}'";
                DataProvider.ExecuteNonQuery(query);
                MessageBox.Show("Đã xóa điểm!");
                LoadDSSinhVien(maLHP);
                ResetNut();
            }
        }

        private void dgvDSSV_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDSSV.SelectedRows.Count > 0)
            {
                var dongDuocChon = dgvDSSV.SelectedRows[0];
                lbMaSV.Text = dongDuocChon.Cells["MaSV"].Value.ToString();
                lbTenSV.Text = dongDuocChon.Cells["HoTen"].Value.ToString();
                txtDiem.Text = dongDuocChon.Cells["Diem"].Value.ToString();

                btnNhap.Enabled = true;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
        }

        private void cbLopHP_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cbLopHP.SelectedValue != null)
            {
                string maLHP = cbLopHP.SelectedValue.ToString();
                LoadDSSinhVien(maLHP);
            }
        }

        private void cbThongKe_SelectedIndexChanged(object sender, EventArgs e)
        {
            string loaiTK = cbThongKe.SelectedItem.ToString();

            switch (loaiTK)
            {
                case "Điểm TB toàn trường":
                    ThongKeDiemTrungBinh("truong");
                    break;
                case "Điểm TB theo khoa":
                    ThongKeDiemTrungBinh("khoa");
                    break;
                case "Điểm TB theo lớp":
                    ThongKeDiemTrungBinh("lop");
                    break;
                case "Số lượng SV cảnh cáo học vụ":
                    ThongKeCanhCaoHocVu();
                    break;
                case "Top 10 GPA toàn trường":
                    ThongKeTop10GPA();
                    break;
                case "Top 10 GPA theo học kỳ":
                    ThongKeTop10GPATheoHK();
                    break;
            }
        }

        private void ThongKeDiemTrungBinh(string cap)
        {
            string query = "";

            switch (cap)
            {
                case "truong":
                    query = "SELECT 'Toan truong' AS Ten, AVG(GPA) AS DiemTB FROM GPA";
                    break;
                case "khoa":
                    query = @"
                SELECT k.TenKhoa AS Ten, AVG(g.GPA) AS DiemTB
                FROM GPA g
                JOIN SinhVien sv ON g.MaSV = sv.MaSV
                JOIN Lop l ON sv.MaLop = l.MaLop
                JOIN ChuyenNganh cn ON l.MaCN = cn.MaCN
                JOIN Khoa k ON cn.MaKhoa = k.MaKhoa
                GROUP BY k.TenKhoa";
                    break;
                case "lop":
                    query = @"
                SELECT l.MaLop AS Ten, AVG(g.GPA) AS DiemTB
                FROM GPA g
                JOIN SinhVien sv ON g.MaSV = sv.MaSV
                JOIN Lop l ON sv.MaLop = l.MaLop
                GROUP BY l.MaLop";
                    break;
            }

            DataTable dt = DataProvider.LoadCSDL(query);
            List<string> labels = new List<string>();
            ChartValues<double> values = new ChartValues<double>();

            foreach (DataRow row in dt.Rows)
            {
                labels.Add(row["Ten"].ToString());
                values.Add(Convert.ToDouble(row["DiemTB"]));
            }

            cartesianChart1.Series = new SeriesCollection
    {
        new ColumnSeries
        {
            Title = "Điểm TB",
            Values = values
        }
    };

            cartesianChart1.AxisX.Clear();
            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Đối tượng",
                Labels = labels
            });

            cartesianChart1.AxisY.Clear();
            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "Điểm TB",
                MinValue = 0,
                MaxValue = 4
            });
        }

        private void ThongKeCanhCaoHocVu()
        {
            string query = @"
        SELECT sv.HoTen, COUNT(*) AS SoLanCanhCao
        FROM GPA g
        JOIN SinhVien sv ON g.MaSV = sv.MaSV
        WHERE GPA < 2.0
        GROUP BY sv.MaSV, sv.HoTen
        HAVING COUNT(*) >= 2";

            DataTable dt = DataProvider.LoadCSDL(query);

            List<string> labels = new List<string>();
            ChartValues<int> values = new ChartValues<int>();

            foreach (DataRow row in dt.Rows)
            {
                labels.Add(row["HoTen"].ToString());
                values.Add(Convert.ToInt32(row["SoLanCanhCao"]));
            }

            cartesianChart1.Series = new SeriesCollection
    {
        new ColumnSeries
        {
            Title = "Số lần cảnh cáo",
            Values = values
        }
    };

            cartesianChart1.AxisX.Clear();
            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Sinh viên",
                Labels = labels
            });

            cartesianChart1.AxisY.Clear();
            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "Lần",
                MinValue = 0
            });
        }

        private void ThongKeTop10GPA()
        {
            string query = @"
        SELECT TOP 10 sv.HoTen, AVG(g.GPA) AS GPA_TB
        FROM GPA g
        JOIN SinhVien sv ON g.MaSV = sv.MaSV
        GROUP BY sv.HoTen
        ORDER BY GPA_TB DESC";

            DataTable dt = DataProvider.LoadCSDL(query);

            List<string> labels = new List<string>();
            ChartValues<double> values = new ChartValues<double>();

            foreach (DataRow row in dt.Rows)
            {
                labels.Add(row["HoTen"].ToString());
                values.Add(Convert.ToDouble(row["GPA_TB"]));
            }

            cartesianChart1.Series = new SeriesCollection
    {
        new ColumnSeries
        {
            Title = "GPA",
            Values = values
        }
    };

            cartesianChart1.AxisX.Clear();
            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Sinh viên",
                Labels = labels
            });

            cartesianChart1.AxisY.Clear();
            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "GPA",
                MinValue = 0,
                MaxValue = 4
            });
        }

        private void ThongKeTop10GPATheoHK()
        {
            string maHK = cbHocKyTK.SelectedValue.ToString();
            string query = $@"
        SELECT TOP 10 sv.HoTen, g.GPA
        FROM GPA g
        JOIN SinhVien sv ON g.MaSV = sv.MaSV
        WHERE g.MaHK = '{maHK}'
        ORDER BY g.GPA DESC";

            DataTable dt = DataProvider.LoadCSDL(query);

            List<string> labels = new List<string>();
            ChartValues<double> values = new ChartValues<double>();

            foreach (DataRow row in dt.Rows)
            {
                labels.Add(row["HoTen"].ToString());
                values.Add(Convert.ToDouble(row["GPA"]));
            }

            cartesianChart1.Series = new SeriesCollection
    {
        new ColumnSeries
        {
            Title = "GPA",
            Values = values
        }
    };

            cartesianChart1.AxisX.Clear();
            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Sinh viên",
                Labels = labels
            });

            cartesianChart1.AxisY.Clear();
            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "GPA",
                MinValue = 0,
                MaxValue = 4
            });
        }

        private void LoadHocKy()
        {
            string query = "SELECT MaHK, NamHoc + ' - Học kỳ ' + CAST(HocKySo AS NVARCHAR) AS TenHK FROM HocKy";
            DataTable dt = DataProvider.LoadCSDL(query);

            cbHocKyTK.DataSource = dt;
            cbHocKyTK.DisplayMember = "TenHK"; // Hiển thị cho người dùng
            cbHocKyTK.ValueMember = "MaHK";    // Giá trị thực dùng trong xử lý
            cbHocKyTK.SelectedIndex = -1; // Không chọn sẵn
        }


    }




}
