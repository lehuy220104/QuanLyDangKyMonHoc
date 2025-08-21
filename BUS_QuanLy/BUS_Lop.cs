using DAL_QuanLy;
using DTO_QuanLy;
using System.Data;

namespace BUS_QuanLy
{
    public class BUS_Lop
    {
        private DAL_Lop dal = new DAL_Lop();

        public DataTable LayDanhSachLop() => dal.LayDanhSachLop();

        public bool ThemLop(DTO_Lop lop) => dal.ThemLop(lop);

        public bool SuaLop(DTO_Lop lop) => dal.CapNhatLop(lop);

        public bool XoaLop(string maLop) => dal.XoaLop(maLop);
    }
}
