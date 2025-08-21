namespace DTO_QuanLy
{
    public class DTO_Diem
    {
        public string MaSV { get; set; }
        public string MaLHP { get; set; }
        public float Diem { get; set; }

        public DTO_Diem() { }

        public DTO_Diem(string maSV, string maLHP, float diem)
        {
            MaSV = maSV;
            MaLHP = maLHP;
            Diem = diem;
        }
    }
}
