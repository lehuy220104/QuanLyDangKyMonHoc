namespace DTO_QuanLy
{
    public class DTO_ChuyenNganh
    {
        public string MaCN { get; set; }
        public string TenCN { get; set; }
        public string MaKhoa { get; set; }

        public DTO_ChuyenNganh() { }

        public DTO_ChuyenNganh(string maCN, string tenCN, string maKhoa)
        {
            MaCN = maCN;
            TenCN = tenCN;
            MaKhoa = maKhoa;
        }
    }
}
