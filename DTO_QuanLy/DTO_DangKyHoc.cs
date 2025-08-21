namespace DTO_QuanLy
{
    public class DTO_DangKyHoc
    {
        public string MaSV { get; set; }
        public string MaLHP { get; set; }
        public float? Diem { get; set; }

        public DTO_DangKyHoc() { }

        public DTO_DangKyHoc(string maSV, string maLHP, float? diem = null)
        {
            MaSV = maSV;
            MaLHP = maLHP;
            Diem = diem;
        }
    }
}
