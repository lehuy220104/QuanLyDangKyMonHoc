using DAL_QuanLy;
using DTO_QuanLy;
using System.Data;

namespace BUS_QuanLy
{
    public class BUS_LopHocPhan
    {
        private DAL_LopHocPhan dal = new DAL_LopHocPhan();

        public DataTable LayDanhSachLopHP(string maGV) => dal.LayDanhSachLopHPTheoGiangVien(maGV);

        public bool ThemLopHocPhan(DTO_LopHocPhan lhp) => dal.ThemLopHocPhan(lhp);

        public bool SuaLopHocPhan(DTO_LopHocPhan lhp) => dal.CapNhatLopHocPhan(lhp);

        public bool XoaLopHocPhan(string maLHP) => dal.XoaLopHocPhan(maLHP);
        public DataTable LayLopHocPhanTheoHocKy(string maHK)
        {
            return dal.LayLopHocPhanTheoHocKy(maHK);
        }
        public DataTable LayTatCaLopHocPhan()
        {
            return dal.LayTatCaLopHocPhan();
        }
        public DataTable LayThongTinLopHocPhan(string maLHP)
        {
            return dal.LayThongTinLopHocPhan(maLHP);
        }

        public bool KiemTraMonTienQuyet(string maSV, string maLHP)
        {
            return dal.KiemTraMonTienQuyet(maSV, maLHP);
        }
        public DataTable LayThoiKhoaBieu(string maSV, string maHK)
        {
            return dal.LayThoiKhoaBieu(maSV, maHK);
        }

        public DataTable TimLopHocPhan(string maLHP)
        {
            return dal.TimLopHocPhan(maLHP);
        }
        public bool KiemTraTonTai(string maLHP) => dal.KiemTraTonTai(maLHP);

        public bool KiemTraTrungLich(string maGV, int thu, int tietBD, int tietKT, string maHK, string phongHoc)
            => dal.KiemTraTrungLich(maGV, thu, tietBD, tietKT, maHK, phongHoc);

        public bool KiemTraTrungLichSua(string maLHP, string maGV, int thu, int tietBD, int tietKT, string maHK, string phongHoc)
            => dal.KiemTraTrungLichSua(maLHP, maGV, thu, tietBD, tietKT, maHK, phongHoc);
    }
}
