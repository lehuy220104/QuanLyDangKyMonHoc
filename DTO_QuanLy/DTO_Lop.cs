namespace DTO_QuanLy
{
    public class DTO_Lop
    {
        public string MaLop { get; set; }
        public string TenLop { get; set; }
        public string MaCN { get; set; }

        public DTO_Lop() { }

        public DTO_Lop(string maLop, string tenLop, string maCN)
        {
            MaLop = maLop;
            TenLop = tenLop;
            MaCN = maCN;
        }
    }
}
