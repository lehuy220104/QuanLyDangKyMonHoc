using System.Data;
using DAL_QuanLy;
using DTO_QuanLy;

namespace BUS_QuanLy
{
    public class BUS_GiangVien
    {
        DAL_GiangVien dal = new DAL_GiangVien();

        public DataTable LayDanhSachGiangVien()
        {
            return dal.LayDanhSachGiangVien();
        }

        public bool ThemGiangVien(DTO_GiangVien gv)
        {
            return dal.ThemGiangVien(gv);
        }

        public bool CapNhatGiangVien(DTO_GiangVien gv)
        {
            return dal.CapNhatGiangVien(gv);
        }

        public bool XoaGiangVien(string maGV)
        {
            return dal.XoaGiangVien(maGV);
        }

        public bool KiemTraTonTai(string maGV)
        {
            return dal.KiemTraTonTai(maGV);
        }

        public DataTable TimKiemGiangVien(string keyword)
        {
            return dal.TimKiemGiangVien(keyword);
        }
        public bool GuiEmailCanhCao(string toEmail, string subject, string body)
        {
            return dal.SendEmail(toEmail, subject, body);
        }

    }
}
