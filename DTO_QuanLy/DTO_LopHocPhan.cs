namespace DTO_QuanLy
{
    public class DTO_LopHocPhan
    {
        public string MaLHP { get; set; }
        public string MaMH { get; set; }
        public string MaGV { get; set; }
        public string MaHK { get; set; }
        public int TietBatDau { get; set; }
        public int TietKetThuc { get; set; }
        public int Thu { get; set; }
        public string PhongHoc { get; set; }

        public DTO_LopHocPhan() { }

        public DTO_LopHocPhan(string maLHP, string maMH, string maGV, string maHK,
                              int tietBatDau, int tietKetThuc, int thu, string phongHoc)
        {
            MaLHP = maLHP;
            MaMH = maMH;
            MaGV = maGV;
            MaHK = maHK;
            TietBatDau = tietBatDau;
            TietKetThuc = tietKetThuc;
            Thu = thu;
            PhongHoc = phongHoc;
        }
    }
}
