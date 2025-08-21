using System.Data;
using System.Data.SqlClient;
using DTO_QuanLy;

namespace DAL_QuanLy
{
    public class DAL_Lop : DBConnect
    {
        public DataTable LayDanhSachLop()
        {
            string sql = "SELECT * FROM Lop";
            SqlDataAdapter da = new SqlDataAdapter(sql, _conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public bool ThemLop(DTO_Lop lop)
        {
            string sql = "INSERT INTO Lop (MaLop, TenLop, MaCN) VALUES (@MaLop, @TenLop, @MaCN)";
            SqlCommand cmd = new SqlCommand(sql, _conn);
            cmd.Parameters.AddWithValue("@MaLop", lop.MaLop);
            cmd.Parameters.AddWithValue("@TenLop", lop.TenLop);
            cmd.Parameters.AddWithValue("@MaCN", lop.MaCN);

            _conn.Open();
            int rows = cmd.ExecuteNonQuery();
            _conn.Close();
            return rows > 0;
        }

        public bool CapNhatLop(DTO_Lop lop)
        {
            string sql = "UPDATE Lop SET TenLop = @TenLop, MaCN = @MaCN WHERE MaLop = @MaLop";
            SqlCommand cmd = new SqlCommand(sql, _conn);
            cmd.Parameters.AddWithValue("@TenLop", lop.TenLop);
            cmd.Parameters.AddWithValue("@MaCN", lop.MaCN);
            cmd.Parameters.AddWithValue("@MaLop", lop.MaLop);

            _conn.Open();
            int rows = cmd.ExecuteNonQuery();
            _conn.Close();
            return rows > 0;
        }

        public bool XoaLop(string maLop)
        {
            string sql = "DELETE FROM Lop WHERE MaLop = @MaLop";
            SqlCommand cmd = new SqlCommand(sql, _conn);
            cmd.Parameters.AddWithValue("@MaLop", maLop);

            _conn.Open();
            int rows = cmd.ExecuteNonQuery();
            _conn.Close();
            return rows > 0;
        }
    }
}
