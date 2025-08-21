namespace DTO_QuanLy
{
    public class DTO_Khoa
    {
        public string MaKhoa { get; set; }
        public string TenKhoa { get; set; }

        public DTO_Khoa() { }

        public DTO_Khoa(string maKhoa, string tenKhoa)
        {
            MaKhoa = maKhoa;
            TenKhoa = tenKhoa;
        }
    }
}
