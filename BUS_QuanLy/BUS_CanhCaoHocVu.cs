using System.Data;
using DAL_QuanLy;

namespace BUS_QuanLy
{
    public class BUS_CanhCaoHocVu
    {
        private DAL_CanhCaoHocVu dal = new DAL_CanhCaoHocVu();

        public DataTable LayDSSVCanhCao(string namHoc, string maLop, int hocKySo)
        {
            return dal.LayDSSVCanhCao(namHoc, maLop, hocKySo);
        }

        public DataTable LayDanhSachNamHoc()
        {
            return dal.LayDanhSachNamHoc();
        }

        public DataTable LayDanhSachLop()
        {
            return dal.LayDanhSachLop();
        }

        public bool GuiEmailCanhCao(string toEmail, string subject, string body)
        {
            return dal.SendEmail(toEmail, subject, body);
        }
    }
}
