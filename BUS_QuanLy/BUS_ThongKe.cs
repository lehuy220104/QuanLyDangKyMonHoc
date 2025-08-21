using System.Data;
using DAL_QuanLy;

namespace BUS_QuanLy
{
    public class BUS_ThongKe
    {
        private DAL_ThongKe dalThongKe = new DAL_ThongKe();

        public DataTable ThongKeSoLuongSVTheoLHP(string maHK)
        {
            return dalThongKe.ThongKeSoLuongSVTheoLHP(maHK);
        }

        public DataTable ThongKeDiemTBTheoLHP(string maHK)
        {
            return dalThongKe.ThongKeDiemTBTheoLHP(maHK);
        }

        public DataTable ThongKeTiLeDat(string maHK)
        {
            return dalThongKe.ThongKeTiLeDat(maHK);
        }
    }
}
