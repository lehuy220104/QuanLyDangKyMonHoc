using System.Data;
using System.Data.SqlClient;
using DTO_QuanLy;

namespace DAL_QuanLy
{
    public class DAL_ChuyenNganh : DBConnect
    {
        public DataTable LayDanhSachChuyenNganh()
        {
            string sql = "SELECT * FROM ChuyenNganh";
            SqlDataAdapter da = new SqlDataAdapter(sql, _conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public bool ThemChuyenNganh(DTO_ChuyenNganh cn)
        {
            string sql = "INSERT INTO ChuyenNganh (MaCN, TenCN, MaKhoa) VALUES (@MaCN, @TenCN, @MaKhoa)";
            SqlCommand cmd = new SqlCommand(sql, _conn);
            cmd.Parameters.AddWithValue("@MaCN", cn.MaCN);
            cmd.Parameters.AddWithValue("@TenCN", cn.TenCN);
            cmd.Parameters.AddWithValue("@MaKhoa", cn.MaKhoa);

            _conn.Open();
            int rows = cmd.ExecuteNonQuery();
            _conn.Close();
            return rows > 0;
        }

        public bool CapNhatChuyenNganh(DTO_ChuyenNganh cn)
        {
            string sql = "UPDATE ChuyenNganh SET TenCN = @TenCN, MaKhoa = @MaKhoa WHERE MaCN = @MaCN";
            SqlCommand cmd = new SqlCommand(sql, _conn);
            cmd.Parameters.AddWithValue("@TenCN", cn.TenCN);
            cmd.Parameters.AddWithValue("@MaKhoa", cn.MaKhoa);
            cmd.Parameters.AddWithValue("@MaCN", cn.MaCN);

            _conn.Open();
            int rows = cmd.ExecuteNonQuery();
            _conn.Close();
            return rows > 0;
        }

        public bool XoaChuyenNganh(string maCN)
        {
            string sql = "DELETE FROM ChuyenNganh WHERE MaCN = @MaCN";
            SqlCommand cmd = new SqlCommand(sql, _conn);
            cmd.Parameters.AddWithValue("@MaCN", maCN);

            _conn.Open();
            int rows = cmd.ExecuteNonQuery();
            _conn.Close();
            return rows > 0;
        }
    }
}
