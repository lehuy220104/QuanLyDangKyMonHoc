using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySinhVien
{
    public class DangNhap
    {
        public string MaSV { get; set; }
        public string MatKhau { get; set; }
        public string VaiTro { get; set; }
        public DangNhap() { }
        public DangNhap(string masv, string matKhau, string vaitro)
        {
            MaSV = masv;
            MatKhau = matKhau;
            VaiTro = vaitro;
        }
    }
}
