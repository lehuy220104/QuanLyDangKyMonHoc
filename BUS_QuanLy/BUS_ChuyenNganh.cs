using DAL_QuanLy;
using DTO_QuanLy;
using System.Data;

namespace BUS_QuanLy
{
    public class BUS_ChuyenNganh
    {
        private DAL_ChuyenNganh dal = new DAL_ChuyenNganh();

        public DataTable LayDanhSachChuyenNganh() => dal.LayDanhSachChuyenNganh();

        public bool ThemChuyenNganh(DTO_ChuyenNganh cn) => dal.ThemChuyenNganh(cn);

        public bool SuaChuyenNganh(DTO_ChuyenNganh cn) => dal.CapNhatChuyenNganh(cn);

        public bool XoaChuyenNganh(string maCN) => dal.XoaChuyenNganh(maCN);
    }
}
