using System;

namespace DTO_QuanLy
{
    public class DTO_SinhVien
    {
        public string MaSV { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string MaLop { get; set; }
        public string TrangThai { get; set; }

        public string Email  { get; set; }

        public DTO_SinhVien() { }

        public DTO_SinhVien(string maSV, string hoTen, DateTime ngaySinh, string gioiTinh, string maLop, string trangThai, string email)
        {
            MaSV = maSV;
            HoTen = hoTen;
            NgaySinh = ngaySinh;
            GioiTinh = gioiTinh;
            MaLop = maLop;
            TrangThai = trangThai;
            Email = email;
        }
    }
}
