namespace DTO_QuanLy
{
    public class DTO_HocKy
    {
        public string MaHK { get; set; }
        public string NamHoc { get; set; }
        public int HocKySo { get; set; }

        public DTO_HocKy() { }

        public DTO_HocKy(string maHK, string namHoc, int hocKySo)
        {
            MaHK = maHK;
            NamHoc = namHoc;
            HocKySo = hocKySo;
        }

        public string TenHocKy => $"{NamHoc} - Học kỳ {HocKySo}";
    }
}
