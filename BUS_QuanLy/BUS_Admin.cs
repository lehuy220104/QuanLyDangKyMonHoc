using System.Data;
using DAL_QuanLy;

namespace BUS_QuanLy
{
    public class BUS_Admin
    {
        DAL_Admin dalAdmin = new DAL_Admin();

        public DataTable LayDanhSachBang()
        {
            return dalAdmin.LayDanhSachBang();
        }

        public DataTable LayDuLieuBang(string tenBang)
        {
            return dalAdmin.LayDuLieuBang(tenBang);
        }

        public void LuuChinhSua(string tenBang, DataTable dt)
        {
            dalAdmin.LuuChinhSua(tenBang, dt);
        }
    }
}
