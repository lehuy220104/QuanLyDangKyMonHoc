using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS_QuanLy;
using DTO_QuanLy;

namespace GUI_QuanLy
{
    public partial class GUI_GiangVien : Form
    {
        private string maGVDangNhap;
        private BUS_LopHocPhan busLHP = new BUS_LopHocPhan();
        private BUS_Diem busDiem = new BUS_Diem();
        BUS_GiangVien busGV = new BUS_GiangVien();
        BUS_ThongKe busTK = new BUS_ThongKe();
        BUS_HocKy busHK = new BUS_HocKy();
        private BUS_GPA busGPA = new BUS_GPA();
        BUS_CanhCaoHocVu busCanhCao = new BUS_CanhCaoHocVu();

        public GUI_GiangVien(string maGV)
        {
            InitializeComponent();
            maGVDangNhap = maGV;
            LoadLoaiThongKe();
            LoadHocKyThongKe();
            LoadNamHoc();
            LoadLop();
            LoadHK();
        }
        private void LoadNamHoc()
        {
            // Load năm học từ DB
            DataTable dt = busCanhCao.LayDanhSachNamHoc();
            cbNamHoc.DataSource = dt;
            cbNamHoc.DisplayMember = "NamHoc";
            cbNamHoc.ValueMember = "NamHoc";
            cbNamHoc.SelectedIndex = -1; // để trống mặc định
        }

        private void LoadLop()
        {
            DataTable dt = busCanhCao.LayDanhSachLop();
            cbLop.DataSource = dt;
            cbLop.DisplayMember = "TenLop";
            cbLop.ValueMember = "MaLop";
            cbLop.SelectedIndex = -1;
        }

        private void LoadHK()
        {
            DataTable dtHocKy = new DataTable();
            dtHocKy.Columns.Add("HocKyValue", typeof(int));
            dtHocKy.Columns.Add("HocKyText", typeof(string));
            dtHocKy.Rows.Add(1, "1");
            dtHocKy.Rows.Add(2, "2");
            cbHK.DataSource = dtHocKy;
            cbHK.DisplayMember = "HocKyText";
            cbHK.ValueMember = "HocKyValue";
            cbHK.SelectedIndex = -1;
        }

        private void LoadLoaiThongKe()
        {
            cbLoaiThongKe.Items.Clear();
            cbLoaiThongKe.Items.Add("Số lượng sinh viên theo LHP");
            cbLoaiThongKe.Items.Add("Điểm trung bình theo LHP");
            cbLoaiThongKe.Items.Add("Tỉ lệ SV đạt >= 5");
            cbLoaiThongKe.SelectedIndex = -1; // để trống
        }
        private void LoadHocKyThongKe()
        {
            cbHocKyThongKe.DataSource = busHK.LayDanhSachHocKy();
            cbHocKyThongKe.DisplayMember = "TenHocKy";
            cbHocKyThongKe.ValueMember = "MaHK";
            cbHocKyThongKe.SelectedIndex = -1; // để trống
        }


        private void btnNhap_Click(object sender, EventArgs e)
        {
            DTO_Diem diem = TaoDiemTuForm();
            if (busDiem.ThemDiem(diem))
            {
                MessageBox.Show("Nhập điểm thành công!");
                LoadDSSinhVien(cbLopHP.SelectedValue.ToString());
            }
            else
            {
                MessageBox.Show("Nhập điểm thất bại!");
            }
        }

        private void GUI_GiangVien_Load(object sender, EventArgs e)
        {
            maGV.Text = "Mã Giảng Viên: "+ maGVDangNhap;
            LoadLopHocPhan(maGVDangNhap);
            LoadHocKy();
        }
        private void LoadLopHocPhan(string maGV)
        {
            cbLopHP.DataSource = busLHP.LayDanhSachLopHP(maGV);
            cbLopHP.DisplayMember = "MaLHP";
            cbLopHP.ValueMember = "MaLHP";
            cbLopHP.SelectedIndex = -1;
        }

        private void LoadHocKy()
        {
            cbHocKyThongKe.DataSource = new BUS_HocKy().LayDanhSachHocKy();
            cbHocKyThongKe.DisplayMember = "TenHocKy";
            cbHocKyThongKe.ValueMember = "MaHK";
            cbHocKyThongKe.SelectedIndex = -1;
        }

        private void cbLopHP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLopHP.SelectedValue != null)
            {
                string maLHP = cbLopHP.SelectedValue.ToString();
                LoadDSSinhVien(maLHP);
            }
        }
        private void LoadDSSinhVien(string maLHP)
        {
            dgvDSSV.DataSource = busDiem.LayDanhSachDiem(maLHP);
            ResetNut();
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

        private void dgvDSSV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnNhap.Enabled = true;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;

                lbMaSV.Text = dgvDSSV.Rows[e.RowIndex].Cells["MaSV"].Value.ToString();
                lbTenSV.Text = dgvDSSV.Rows[e.RowIndex].Cells["HoTen"].Value.ToString();
                txtDiem.Text = dgvDSSV.Rows[e.RowIndex].Cells["Diem"].Value.ToString();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            DTO_Diem diem = TaoDiemTuForm();
            if (busDiem.SuaDiem(diem))
            {
                MessageBox.Show("Cập nhật điểm thành công!");
                LoadDSSinhVien(cbLopHP.SelectedValue.ToString());
            }
            else
            {
                MessageBox.Show("Cập nhật điểm thất bại!");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maSV = lbMaSV.Text;
            string maLHP = cbLopHP.SelectedValue.ToString();

            if (busDiem.XoaDiem(new DTO_Diem { MaSV = maSV, MaLHP = maLHP }))
            {
                MessageBox.Show("Xóa điểm thành công!");
                LoadDSSinhVien(maLHP);
            }
            else
            {
                MessageBox.Show("Xóa điểm thất bại!");
            }
        }
        private DTO_Diem TaoDiemTuForm()
        {
            return new DTO_Diem
            {
                MaSV = lbMaSV.Text,
                MaLHP = cbLopHP.SelectedValue.ToString(),
                Diem = float.TryParse(txtDiem.Text, out float d) ? d : 0
            };
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            if (cbLoaiThongKe.SelectedIndex == -1 || cbHocKyThongKe.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn loại thống kê và học kỳ!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maHK = cbHocKyThongKe.SelectedValue.ToString();
            DataTable dtThongKe = null;

            switch (cbLoaiThongKe.SelectedItem.ToString())
            {
                case "Số lượng sinh viên theo LHP":
                    dtThongKe = busTK.ThongKeSoLuongSVTheoLHP(maHK);
                    break;

                case "Điểm trung bình theo LHP":
                    dtThongKe = busTK.ThongKeDiemTBTheoLHP(maHK);
                    break;

                case "Tỉ lệ SV đạt >= 5":
                    dtThongKe = busTK.ThongKeTiLeDat(maHK);
                    break;
            }

            if (dtThongKe != null && dtThongKe.Rows.Count > 0)
            {
                HienThiBieuDo(dtThongKe);
            }
            else
            {
                MessageBox.Show("Không có dữ liệu thống kê!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void HienThiBieuDo(DataTable dt)
        {
            chartThongKe.Series.Clear();
            chartThongKe.Titles.Clear();

            string seriesName = cbLoaiThongKe.SelectedItem.ToString();
            chartThongKe.Series.Add(seriesName);
            chartThongKe.Series[seriesName].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            foreach (DataRow row in dt.Rows)
            {
                string tenLHP = row[0].ToString();
                double value = Convert.ToDouble(row[1]);
                chartThongKe.Series[seriesName].Points.AddXY(tenLHP, value);
            }

            chartThongKe.Titles.Add(seriesName);

            // Giới hạn trục Y nếu là điểm trung bình hoặc tỉ lệ
            if (seriesName.Contains("Điểm trung bình"))
            {
                chartThongKe.ChartAreas[0].AxisY.Minimum = 0;
                chartThongKe.ChartAreas[0].AxisY.Maximum = 10;
                chartThongKe.ChartAreas[0].AxisY.Interval = 1;
            }
            else if (seriesName.Contains("Tỉ lệ"))
            {
                chartThongKe.ChartAreas[0].AxisY.Minimum = 0;
                chartThongKe.ChartAreas[0].AxisY.Maximum = 100;
                chartThongKe.ChartAreas[0].AxisY.Interval = 10;
            }
        }
        private void btnLoc_Click(object sender, EventArgs e)
        {
            if (cbLop.SelectedIndex == -1 || cbHK.SelectedIndex == -1 || cbNamHoc.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return;
            }

            string maLop = cbLop.SelectedValue.ToString();
            string namHoc = cbNamHoc.SelectedValue.ToString();
            int hocKySo = int.Parse(cbHK.SelectedValue.ToString());

            DataTable dt = busCanhCao.LayDSSVCanhCao(namHoc, maLop, hocKySo);
            dgvCanhCao.DataSource = dt;
            // Xóa cột cũ nếu đã tồn tại
            if (dgvCanhCao.Columns.Contains("btnGuiEmail"))
            {
                dgvCanhCao.Columns.Remove("btnGuiEmail");
            }
            // Tạo cột nút gửi email
            DataGridViewButtonColumn btnEmail = new DataGridViewButtonColumn();
            btnEmail.Name = "btnGuiEmail";
            btnEmail.HeaderText = "Thông báo";
            btnEmail.Text = "Gửi Email";
            btnEmail.UseColumnTextForButtonValue = true;
            btnEmail.Width = 100;
            dgvCanhCao.Columns.Add(btnEmail);
        }

        private void dgvCanhCao_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvCanhCao.Columns[e.ColumnIndex].Name == "btnGuiEmail")
            {
                string toEmail = dgvCanhCao.Rows[e.RowIndex].Cells["Email"].Value.ToString();
                string hoTen = dgvCanhCao.Rows[e.RowIndex].Cells["HoTen"].Value.ToString();
                string gpa = dgvCanhCao.Rows[e.RowIndex].Cells["GPA"].Value.ToString();

                // Mở hộp thoại nhập nội dung
                string subject = "Cảnh cáo học vụ";
                string body = Microsoft.VisualBasic.Interaction.InputBox(
                    $"Nhập nội dung email cảnh cáo cho {hoTen} (GPA: {gpa}):",
                    "Nhập nội dung cảnh cáo",
                    $"Xin chào {hoTen},\n\nBạn đang có GPA {gpa}, dưới 2.0. Vui lòng cải thiện kết quả học tập."
                );

                if (!string.IsNullOrWhiteSpace(body))
                {
                    try
                    {
                        bool result = busGV.GuiEmailCanhCao(toEmail, subject, body);
                        if (result)
                            MessageBox.Show($"Đã gửi email cảnh cáo đến {hoTen} ({toEmail})");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi gửi email: " + ex.Message);
                    }
                }
            }
        }
    }
}
