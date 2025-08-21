using System;
using System.Data;
using System.Data.SqlClient;
using DTO_QuanLy;

namespace DAL_QuanLy
{
    public class DAL_MonHoc : DBConnect
    {
        public DataTable LayDanhSachMonHoc()
        {
            string sql = "SELECT * FROM MonHoc";
            SqlDataAdapter da = new SqlDataAdapter(sql, _conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public bool ThemMonHoc(DTO_MonHoc mh)
        {
            string sql = @"INSERT INTO MonHoc (MaMH, TenMH, SoTinChi, MonTienQuyet) 
                           VALUES (@MaMH, @TenMH, @SoTinChi, @MonTienQuyet)";
            SqlCommand cmd = new SqlCommand(sql, _conn);
            cmd.Parameters.AddWithValue("@MaMH", mh.MaMH);
            cmd.Parameters.AddWithValue("@TenMH", mh.TenMH);
            cmd.Parameters.AddWithValue("@SoTinChi", mh.SoTinChi);
            cmd.Parameters.AddWithValue("@MonTienQuyet", string.IsNullOrEmpty(mh.MonTienQuyet) ? DBNull.Value : (object)mh.MonTienQuyet);

            _conn.Open();
            int rows = cmd.ExecuteNonQuery();
            _conn.Close();
            return rows > 0;
        }

        public bool CapNhatMonHoc(DTO_MonHoc mh)
        {
            string sql = @"UPDATE MonHoc 
                           SET TenMH = @TenMH, SoTinChi = @SoTinChi, MonTienQuyet = @MonTienQuyet 
                           WHERE MaMH = @MaMH";
            SqlCommand cmd = new SqlCommand(sql, _conn);
            cmd.Parameters.AddWithValue("@MaMH", mh.MaMH);
            cmd.Parameters.AddWithValue("@TenMH", mh.TenMH);
            cmd.Parameters.AddWithValue("@SoTinChi", mh.SoTinChi);
            cmd.Parameters.AddWithValue("@MonTienQuyet", string.IsNullOrEmpty(mh.MonTienQuyet) ? DBNull.Value : (object)mh.MonTienQuyet);

            _conn.Open();
            int rows = cmd.ExecuteNonQuery();
            _conn.Close();
            return rows > 0;
        }

        public bool XoaMonHoc(string maMH)
        {
            string sql = "DELETE FROM MonHoc WHERE MaMH = @MaMH";
            SqlCommand cmd = new SqlCommand(sql, _conn);
            cmd.Parameters.AddWithValue("@MaMH", maMH);

            _conn.Open();
            int rows = cmd.ExecuteNonQuery();
            _conn.Close();
            return rows > 0;
        }
        public DataTable TimKiemMonHoc(string keyword)
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM MonHoc WHERE MaMH LIKE @keyword OR TenMH LIKE @keyword";
            using (SqlCommand cmd = new SqlCommand(query, _conn))
            {
                cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }
        public bool KiemTraTonTai(string maMH)
        {
            string sql = "SELECT COUNT(*) FROM MonHoc WHERE MaMH = @MaMH";
            SqlCommand cmd = new SqlCommand(sql, _conn);
            cmd.Parameters.AddWithValue("@MaMH", maMH);


            _conn.Open();
            int count = (int)cmd.ExecuteScalar();
            _conn.Close();

            return count > 0; // true nếu đã tồn tại
        }
    }
}

