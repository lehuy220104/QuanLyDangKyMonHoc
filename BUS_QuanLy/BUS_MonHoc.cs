using DAL_QuanLy;
using DTO_QuanLy;
using System.Data;

namespace BUS_QuanLy
{
    public class BUS_MonHoc
    {
        private DAL_MonHoc dal = new DAL_MonHoc();

        public DataTable LayDanhSachMonHoc() => dal.LayDanhSachMonHoc();

        public bool ThemMonHoc(DTO_MonHoc mh) => dal.ThemMonHoc(mh);

        public bool SuaMonHoc(DTO_MonHoc mh) => dal.CapNhatMonHoc(mh);

        public bool XoaMonHoc(string maMH) => dal.XoaMonHoc(maMH);

        public DataTable TimKiemMonHoc(string keyword) => dal.TimKiemMonHoc(keyword);

        public bool KiemTraTonTai(string maMH) => dal.KiemTraTonTai(maMH);
    }
}
