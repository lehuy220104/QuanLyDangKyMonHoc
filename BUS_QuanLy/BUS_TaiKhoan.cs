using DAL_QuanLy;
using DTO_QuanLy;
using System.Data;

namespace BUS_QuanLy
{
    public class BUS_TaiKhoan
    {
        DAL_TaiKhoan dalTaiKhoan = new DAL_TaiKhoan();

        // Hàm kiểm tra đăng nhập
        public bool DangNhap(DTO_TaiKhoan tk)
        {
            return dalTaiKhoan.KiemTraDangNhap(tk.TenDangNhap, tk.MatKhau);
        }

        public string LayVaiTro(string tenDangNhap)
        {
            return dalTaiKhoan.LayVaiTro(tenDangNhap);
        }

        public bool DoiMatKhau(DTO_TaiKhoan tk)
        {
            return dalTaiKhoan.DoiMatKhau(tk.TenDangNhap, tk.MatKhau);
        }

        public bool ThemTaiKhoan(DTO_TaiKhoan tk)
        {
            return dalTaiKhoan.ThemTaiKhoan(tk);
        }

        public bool XoaTaiKhoan(string tenDangNhap)
        {
            return dalTaiKhoan.XoaTaiKhoan(tenDangNhap);
        }
        public bool SuaTaiKhoan(DTO_TaiKhoan tk)
        {
            return dalTaiKhoan.SuaTaiKhoan(tk);
        }

        public DataTable LayTatCaTaiKhoan()
        {
            return dalTaiKhoan.LayTatCaTaiKhoan();
        }

        public DataTable TimKiemTaiKhoan(string tenDangNhap)
        {
            return dalTaiKhoan.TimKiemTaiKhoan(tenDangNhap);
        }

        public bool KiemTraTonTai(string tenDangNhap)
        {
            return dalTaiKhoan.KiemTraTonTai(tenDangNhap);
        }
    }
}
