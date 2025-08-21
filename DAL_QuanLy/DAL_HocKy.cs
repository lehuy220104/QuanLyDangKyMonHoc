using System.Data;
using System.Data.SqlClient;
using DTO_QuanLy;

namespace DAL_QuanLy
{
    public class DAL_HocKy : DBConnect
    {
        public DataTable LayDanhSachHocKy()
        {
            string sql = @"
        SELECT MaHK, 
               'HK' + CAST(HocKySo AS VARCHAR) + '/' + NamHoc AS TenHocKy
        FROM HocKy";
            SqlDataAdapter da = new SqlDataAdapter(sql, _conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public bool ThemHocKy(DTO_HocKy hk)
        {
            string sql = @"INSERT INTO HocKy (MaHK, NamHoc, HocKySo) 
                           VALUES (@MaHK, @NamHoc, @HocKySo)";
            SqlCommand cmd = new SqlCommand(sql, _conn);
            cmd.Parameters.AddWithValue("@MaHK", hk.MaHK);
            cmd.Parameters.AddWithValue("@NamHoc", hk.NamHoc);
            cmd.Parameters.AddWithValue("@HocKySo", hk.HocKySo);

            _conn.Open();
            int rows = cmd.ExecuteNonQuery();
            _conn.Close();
            return rows > 0;
        }

        public bool CapNhatHocKy(DTO_HocKy hk)
        {
            string sql = @"UPDATE HocKy 
                           SET NamHoc = @NamHoc, HocKySo = @HocKySo 
                           WHERE MaHK = @MaHK";
            SqlCommand cmd = new SqlCommand(sql, _conn);
            cmd.Parameters.AddWithValue("@MaHK", hk.MaHK);
            cmd.Parameters.AddWithValue("@NamHoc", hk.NamHoc);
            cmd.Parameters.AddWithValue("@HocKySo", hk.HocKySo);

            _conn.Open();
            int rows = cmd.ExecuteNonQuery();
            _conn.Close();
            return rows > 0;
        }

        public bool XoaHocKy(string maHK)
        {
            string sql = "DELETE FROM HocKy WHERE MaHK = @MaHK";
            SqlCommand cmd = new SqlCommand(sql, _conn);
            cmd.Parameters.AddWithValue("@MaHK", maHK);

            _conn.Open();
            int rows = cmd.ExecuteNonQuery();
            _conn.Close();
            return rows > 0;
        }
    }
}
