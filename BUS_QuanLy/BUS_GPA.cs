using System.Data;
using DAL_QuanLy;
using DTO_QuanLy;

namespace BUS_QuanLy
{
    public class BUS_GPA
    {
        private DAL_GPA dalGPA = new DAL_GPA();

        // Lấy GPA của một sinh viên
        public DataTable LayGPATheoSinhVien(string maSV)
        {
            return dalGPA.LayGPATheoSinhVien(maSV);
        }

        // Thêm GPA mới
        public bool ThemGPA(DTO_GPA gpa)
        {
            return dalGPA.ThemGPA(gpa);
        }

        // Cập nhật GPA
        public bool CapNhatGPA(DTO_GPA gpa)
        {
            return dalGPA.CapNhatGPA(gpa);
        }

        // Xóa GPA theo sinh viên và học kỳ
        public bool XoaGPA(string maSV, string maHK)
        {
            return dalGPA.XoaGPA(maSV, maHK);
        }
        public string LayGPA(string maSV, string maHK)
        {
            DataTable dt = dalGPA.LayGPATheoSinhVien(maSV);
            DataRow[] rows = dt.Select($"MaHK = '{maHK}'");

            if (rows.Length > 0)
                return rows[0]["GPA"].ToString();
            return string.Empty;
        }
    }
}
