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
    public partial class DangNhapFrm : Form
    {
        public DangNhapFrm()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txtMaTK.Text;
            string matKhau = txtMatKhau.Text;
            string loaiHienThi = cbLoaiTaiKhoan.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(loaiHienThi))
            {
                MessageBox.Show("Vui lòng chọn loại tài khoản.");
                return;
            }

            // Ánh xạ từ ComboBox sang VaiTro trong CSDL
            Dictionary<string, string> roleMap = new Dictionary<string, string>()
    {
        { "Sinh Viên", "SV" },
        { "Giảng Viên", "GV" },
        { "Admin", "AD" }
    };

            string vaiTro = roleMap[loaiHienThi];

            bool isFound = false;

            foreach (var dn in DataProvider.DangNhaps)
            {
                if (dn.MaSV == tenDangNhap && dn.MatKhau == matKhau && dn.VaiTro == vaiTro)
                {
                    isFound = true;
                    break;
                }
            }

            if (isFound)
            {
                // Mở form tương ứng theo vai trò
                if (vaiTro == "SV")
                {
                    SinhVienFrm f = new SinhVienFrm(tenDangNhap);
                    f.ShowDialog();
                }
                else if (vaiTro == "GV")
                {
                    GiangVienFrm f = new GiangVienFrm(tenDangNhap);
                    f.ShowDialog();
                }
                else if (vaiTro == "AD")
                {
                    AdminFrm f = new AdminFrm(tenDangNhap);
                    f.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Tên đăng nhập, mật khẩu hoặc vai trò không đúng!", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DangNhapFrm_Load(object sender, EventArgs e)
        {
            DataProvider.GetAllDangNhap();
        }

    }
}
