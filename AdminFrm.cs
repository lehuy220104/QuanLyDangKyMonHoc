using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySinhVien
{
    public partial class AdminFrm : Form
    {
        private string maADDangNhap;
        public AdminFrm(string maAD)
        {
            InitializeComponent();
            maADDangNhap = maAD;
        }

        private void AdminFrm_Load(object sender, EventArgs e)
        {
            maAD.Text = maADDangNhap;
            cbVaiTro.Items.AddRange(new string[] { "Sinh viên", "Giảng viên", "Admin" });
            LoadTaiKhoan();
            LoadDanhSachBang();

        }

        private void LoadTaiKhoan()
        {
            string query = "SELECT * FROM TaiKhoan";
            DataTable dt = DataProvider.LoadCSDL(query);
            dgvTaiKhoan.DataSource = dt;
        }

        private void LoadDanhSachBang()
        {
            List<string> tables = new List<string>
    {
        "Khoa", "ChuyenNganh", "Lop", "GiangVien", "SinhVien", "TaiKhoan",
        "MonHoc", "HocKy", "LopHocPhan", "DangKyHoc", "GPA"
    };

            cbTable.DataSource = tables;
        }

        private void Reset()
        {
            // Load lại dữ liệu từ DB lên DataGridView
            LoadTaiKhoan();

            // Xoá nội dung ô nhập
            txtTenDN.Clear();
            txtMatKhau.Clear();
            cbVaiTro.SelectedIndex = -1;

            // Optional: đưa con trỏ về txtTenDN
            txtTenDN.Focus();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

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

        private void btnThem_Click(object sender, EventArgs e)
        {
            string tenDN = txtTenDN.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();
            string vaiTro = cbVaiTro.SelectedItem?.ToString();

            // Kiểm tra rỗng
            if (string.IsNullOrEmpty(tenDN) || string.IsNullOrEmpty(matKhau) || string.IsNullOrEmpty(vaiTro))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            // Kiểm tra trùng tên đăng nhập
            string checkQuery = $"SELECT COUNT(*) FROM TaiKhoan WHERE TenDangNhap = '{tenDN}'";
            int count = Convert.ToInt32(DataProvider.ExecuteScalar(checkQuery));

            if (count > 0)
            {
                MessageBox.Show("Tên đăng nhập đã tồn tại. Vui lòng chọn tên khác!", "Trùng tài khoản", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Nếu không trùng, thêm vào CSDL
            string insertQuery = $"INSERT INTO TaiKhoan VALUES ('{tenDN}', '{matKhau}', '{vaiTro}')";
            int rows = DataProvider.ExecuteNonQuery(insertQuery);

            if (rows > 0)
            {
                MessageBox.Show("Thêm tài khoản thành công!");
                LoadTaiKhoan(); // Load lại danh sách
                Reset();
            }
            else
            {
                MessageBox.Show("Thêm tài khoản thất bại!");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string ten = txtTenDN.Text;
            string mk = txtMatKhau.Text;
            string vaiTro = cbVaiTro.Text;

            string query = $"UPDATE TaiKhoan SET MatKhau = N'{mk}', VaiTro = N'{vaiTro}' WHERE TenDangNhap = N'{ten}'";
            if (DataProvider.ExecuteNonQuery(query) > 0)
            {
                MessageBox.Show("Cập nhật thành công");
                LoadTaiKhoan();
                Reset();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string ten = txtTenDN.Text;
            DialogResult result = MessageBox.Show("Bạn có chắc muốn xoá?", "Xác nhận", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                string query = $"DELETE FROM TaiKhoan WHERE TenDangNhap = N'{ten}'";
                if (DataProvider.ExecuteNonQuery(query) > 0)
                {
                    MessageBox.Show("Xoá thành công");
                    LoadTaiKhoan();
                    Reset();
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txtTimKiem.Text.Trim();

            if (string.IsNullOrEmpty(tenDangNhap))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập cần tìm!");
                return;
            }

            string query = $@"
        SELECT * FROM TaiKhoan 
        WHERE TenDangNhap LIKE N'%{tenDangNhap}%'";

            DataTable dt = DataProvider.LoadCSDL(query);

            if (dt.Rows.Count > 0)
            {
                dgvTaiKhoan.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Không tìm thấy tài khoản nào phù hợp!");
                dgvTaiKhoan.DataSource = null;
            }
        }

        private SqlDataAdapter adapter;
        private DataTable currentDataTable;
        private string currentTable;

        private void cbTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentTable = cbTable.SelectedItem.ToString();
            string query = $"SELECT * FROM {currentTable}";

            try
            {
                DataProvider.OpenConnection();

                adapter = new SqlDataAdapter(query, DataProvider.connection);
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter); // Tự sinh lệnh INSERT, UPDATE, DELETE

                currentDataTable = new DataTable();
                adapter.Fill(currentDataTable);
                dgvChinhSua.DataSource = currentDataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
            finally
            {
                DataProvider.CloseConnection();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                DataProvider.OpenConnection();
                adapter.Update(currentDataTable);
                MessageBox.Show("Cập nhật thành công!", "Thông báo");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật: " + ex.Message);
            }
            finally
            {
                DataProvider.CloseConnection();
            }
        }

        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            cbTable_SelectedIndexChanged(null, null);
        }
    }
}
