using DAL_QuanLy;
using DTO_QuanLy;
using System.Data;

namespace BUS_QuanLy
{
    public class BUS_Khoa
    {
        private DAL_Khoa dal = new DAL_Khoa();

        public DataTable LayDanhSachKhoa() => dal.LayDanhSachKhoa();

        public bool ThemKhoa(DTO_Khoa khoa) => dal.ThemKhoa(khoa);

        public bool SuaKhoa(DTO_Khoa khoa) => dal.CapNhatKhoa(khoa);

        public bool XoaKhoa(string maKhoa) => dal.XoaKhoa(maKhoa);
    }
}
