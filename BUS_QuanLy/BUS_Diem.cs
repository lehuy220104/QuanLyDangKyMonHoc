using System.Data;
using DTO_QuanLy;
using DAL_QuanLy;

namespace BUS_QuanLy
{
    public class BUS_Diem
    {
        private DAL_Diem dalDiem = new DAL_Diem();

        // Lấy danh sách sinh viên trong lớp học phần (kèm điểm)
        public DataTable LayDanhSachDiem(string maLHP)
        {
            return dalDiem.LayDSSinhVien(maLHP);
        }

        // Cập nhật điểm cho sinh viên
        public bool SuaDiem(DTO_Diem diem)
        {
            return dalDiem.CapNhatDiem(diem);
        }

        // Xóa điểm (đặt về NULL)
        public bool XoaDiem(DTO_Diem diem)
        {
            return dalDiem.XoaDiem(diem);
        }

        // Nhập điểm mới (giống như cập nhật, dùng lại phương thức cập nhật)
        public bool ThemDiem(DTO_Diem diem)
        {
            return dalDiem.CapNhatDiem(diem);
        }

        // Thống kê điểm theo lớp học phần
        public DataTable ThongKeDiemTheoLop(string maLHP)
        {
            return dalDiem.ThongKeDiemTheoLop(maLHP);
        }
    }
}
