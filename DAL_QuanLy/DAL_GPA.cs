using System.Data;
using System.Data.SqlClient;
using DTO_QuanLy;

namespace DAL_QuanLy
{
    public class DAL_GPA : DBConnect
    {
        public DataTable LayGPATheoSinhVien(string maSV)
        {
            string sql = "SELECT * FROM GPA WHERE MaSV = @MaSV";
            SqlDataAdapter da = new SqlDataAdapter(sql, _conn);
            da.SelectCommand.Parameters.AddWithValue("@MaSV", maSV);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public bool ThemGPA(DTO_GPA gpa)
        {
            string sql = @"INSERT INTO GPA (MaSV, MaHK, GPA) 
                           VALUES (@MaSV, @MaHK, @GPA)";
            SqlCommand cmd = new SqlCommand(sql, _conn);
            cmd.Parameters.AddWithValue("@MaSV", gpa.MaSV);
            cmd.Parameters.AddWithValue("@MaHK", gpa.MaHK);
            cmd.Parameters.AddWithValue("@GPA", gpa.GPA);

            _conn.Open();
            int rows = cmd.ExecuteNonQuery();
            _conn.Close();
            return rows > 0;
        }

        public bool CapNhatGPA(DTO_GPA gpa)
        {
            string sql = @"UPDATE GPA 
                           SET GPA = @GPA 
                           WHERE MaSV = @MaSV AND MaHK = @MaHK";
            SqlCommand cmd = new SqlCommand(sql, _conn);
            cmd.Parameters.AddWithValue("@MaSV", gpa.MaSV);
            cmd.Parameters.AddWithValue("@MaHK", gpa.MaHK);
            cmd.Parameters.AddWithValue("@GPA", gpa.GPA);

            _conn.Open();
            int rows = cmd.ExecuteNonQuery();
            _conn.Close();
            return rows > 0;
        }

        public bool XoaGPA(string maSV, string maHK)
        {
            string sql = "DELETE FROM GPA WHERE MaSV = @MaSV AND MaHK = @MaHK";
            SqlCommand cmd = new SqlCommand(sql, _conn);
            cmd.Parameters.AddWithValue("@MaSV", maSV);
            cmd.Parameters.AddWithValue("@MaHK", maHK);

            _conn.Open();
            int rows = cmd.ExecuteNonQuery();
            _conn.Close();
            return rows > 0;
        }

    }
}
