namespace DTO_QuanLy
{
    public class DTO_GPA
    {
        public string MaSV { get; set; }
        public string MaHK { get; set; }
        public double GPA { get; set; }

        public DTO_GPA() { }

        public DTO_GPA(string maSV, string maHK, float gpa)
        {
            MaSV = maSV;
            MaHK = maHK;
            GPA = gpa;
        }
    }
}
