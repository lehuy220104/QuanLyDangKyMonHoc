using System.Data;
using System.Data.SqlClient;
using DTO_QuanLy;

namespace DAL_QuanLy
{
    public class DAL_Khoa : DBConnect
    {
        public DataTable LayDanhSachKhoa()
        {
            string sql = "SELECT * FROM Khoa";
            SqlDataAdapter da = new SqlDataAdapter(sql, _conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public bool ThemKhoa(DTO_Khoa khoa)
        {
            string sql = "INSERT INTO Khoa (MaKhoa, TenKhoa) VALUES (@MaKhoa, @TenKhoa)";
            SqlCommand cmd = new SqlCommand(sql, _conn);
            cmd.Parameters.AddWithValue("@MaKhoa", khoa.MaKhoa);
            cmd.Parameters.AddWithValue("@TenKhoa", khoa.TenKhoa);

            _conn.Open();
            int rows = cmd.ExecuteNonQuery();
            _conn.Close();
            return rows > 0;
        }

        public bool CapNhatKhoa(DTO_Khoa khoa)
        {
            string sql = "UPDATE Khoa SET TenKhoa = @TenKhoa WHERE MaKhoa = @MaKhoa";
            SqlCommand cmd = new SqlCommand(sql, _conn);
            cmd.Parameters.AddWithValue("@TenKhoa", khoa.TenKhoa);
            cmd.Parameters.AddWithValue("@MaKhoa", khoa.MaKhoa);

            _conn.Open();
            int rows = cmd.ExecuteNonQuery();
            _conn.Close();
            return rows > 0;
        }

        public bool XoaKhoa(string maKhoa)
        {
            string sql = "DELETE FROM Khoa WHERE MaKhoa = @MaKhoa";
            SqlCommand cmd = new SqlCommand(sql, _conn);
            cmd.Parameters.AddWithValue("@MaKhoa", maKhoa);

            _conn.Open();
            int rows = cmd.ExecuteNonQuery();
            _conn.Close();
            return rows > 0;
        }
    }
}
