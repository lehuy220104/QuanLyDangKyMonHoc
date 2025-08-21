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
    public partial class GUI_DangNhap : Form
    {
        BUS_TaiKhoan busTK = new BUS_TaiKhoan();
        public GUI_DangNhap()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txtMaTK.Text.Trim();
            string matKhau = txtMatKhau.Text;
            string loaiHienThi = cbLoaiTaiKhoan.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(loaiHienThi))
            {
                MessageBox.Show("Vui lòng chọn loại tài khoản.");
                return;
            }

            // Mapping combobox sang DB role
            Dictionary<string, string> roleMap = new Dictionary<string, string>()
            {
                { "Sinh Viên", "SV" },
                { "Giảng Viên", "GV" },
                { "Admin", "AD" }
            };

            string vaiTro = roleMap[loaiHienThi];

            // Tạo DTO để kiểm tra
            DTO_TaiKhoan tk = new DTO_TaiKhoan(tenDangNhap, matKhau);

            // Kiểm tra đăng nhập
            if (busTK.DangNhap(tk))
            {
                string vaiTroDB = busTK.LayVaiTro(tk.TenDangNhap);

                if (vaiTroDB == vaiTro)
                {
                    // Mở form tương ứng
                    Form nextForm = null;

                    if (vaiTro == "SV")
                        nextForm = new GUI_SinhVien(tenDangNhap);
                    else if (vaiTro == "GV")
                        nextForm = new GUI_GiangVien(tenDangNhap);
                    else if (vaiTro == "AD")
                        nextForm = new GUI_Admin(tenDangNhap);

                    this.Hide();
                    nextForm.ShowDialog();
                    this.Show();
                }
                else
                {
                    MessageBox.Show("Vai trò không đúng!", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu sai!", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void GUI_DangNhap_Load(object sender, EventArgs e)
        {
            cbLoaiTaiKhoan.SelectedIndex = 0;
        }
    } 
    
}
