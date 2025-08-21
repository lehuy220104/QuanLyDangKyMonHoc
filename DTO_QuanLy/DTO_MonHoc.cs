namespace DTO_QuanLy
{
    public class DTO_MonHoc
    {
        public string MaMH { get; set; }
        public string TenMH { get; set; }
        public int SoTinChi { get; set; }
        public string MonTienQuyet { get; set; }

        public DTO_MonHoc() { }

        public DTO_MonHoc(string maMH, string tenMH, int soTinChi, string monTienQuyet = "")
        {
            MaMH = maMH;
            TenMH = tenMH;
            SoTinChi = soTinChi;
            MonTienQuyet = monTienQuyet;
        }
    }
}
