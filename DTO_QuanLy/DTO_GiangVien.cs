using System;

namespace DTO_QuanLy
{
    public class DTO_GiangVien
    {
        public string MaGV { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string MaKhoa { get; set; }
        public string GioiTinh { get; set; }


        public DTO_GiangVien() { }

        public DTO_GiangVien(string maGV, string hoTen, string email, string maKhoa, string gioiTinh)
        {
            MaGV = maGV;
            HoTen = hoTen;
            Email = email;
            MaKhoa = maKhoa;
            GioiTinh = gioiTinh;
        }
    }
}
