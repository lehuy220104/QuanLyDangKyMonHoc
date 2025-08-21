using DAL_QuanLy;
using DTO_QuanLy;
using System.Data;

namespace BUS_QuanLy
{
    public class BUS_HocKy
    {
        private DAL_HocKy dal = new DAL_HocKy();

        public DataTable LayDanhSachHocKy() => dal.LayDanhSachHocKy();

        public bool ThemHocKy(DTO_HocKy hk) => dal.ThemHocKy(hk);

        public bool SuaHocKy(DTO_HocKy hk) => dal.CapNhatHocKy(hk);

        public bool XoaHocKy(string maHK) => dal.XoaHocKy(maHK);
    }
}
