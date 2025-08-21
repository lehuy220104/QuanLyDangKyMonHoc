using System.Data;
using System.Data.SqlClient;
using DTO_QuanLy;

namespace DAL_QuanLy
{
    public class DAL_SinhVien : DBConnect
    {
        public DataTable LayDanhSachSinhVien()
        {
            string sql = "SELECT * FROM SinhVien";
            SqlDataAdapter da = new SqlDataAdapter(sql, _conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public bool ThemSinhVien(DTO_SinhVien sv)
        {
            string sql = "INSERT INTO SinhVien (MaSV, HoTen, NgaySinh, GioiTinh, MaLop, TrangThai, Email) " +
                         "VALUES (@MaSV, @HoTen, @NgaySinh, @GioiTinh, @MaLop, @TrangThai, @Email)";
            SqlCommand cmd = new SqlCommand(sql, _conn);
            cmd.Parameters.AddWithValue("@MaSV", sv.MaSV);
            cmd.Parameters.AddWithValue("@HoTen", sv.HoTen);
            cmd.Parameters.AddWithValue("@NgaySinh", sv.NgaySinh);
            cmd.Parameters.AddWithValue("@GioiTinh", sv.GioiTinh);
            cmd.Parameters.AddWithValue("@MaLop", sv.MaLop);
            cmd.Parameters.AddWithValue("@TrangThai", sv.TrangThai);
            cmd.Parameters.AddWithValue("@Email", sv.Email);

            _conn.Open();
            int rows = cmd.ExecuteNonQuery();
            _conn.Close();

            return rows > 0;    
        }

        public bool CapNhatSinhVien(DTO_SinhVien sv)
        {
            string sql = "UPDATE SinhVien SET HoTen = @HoTen, NgaySinh = @NgaySinh, GioiTinh = @GioiTinh, " +
                         "MaLop = @MaLop, TrangThai = @TrangThai, Email = @Email WHERE MaSV = @MaSV";
            SqlCommand cmd = new SqlCommand(sql, _conn);
            cmd.Parameters.AddWithValue("@HoTen", sv.HoTen);
            cmd.Parameters.AddWithValue("@NgaySinh", sv.NgaySinh);
            cmd.Parameters.AddWithValue("@GioiTinh", sv.GioiTinh);
            cmd.Parameters.AddWithValue("@MaLop", sv.MaLop);
            cmd.Parameters.AddWithValue("@TrangThai", sv.TrangThai);
            cmd.Parameters.AddWithValue("@Email", sv.Email);
            cmd.Parameters.AddWithValue("@MaSV", sv.MaSV);

            _conn.Open();
            int rows = cmd.ExecuteNonQuery();
            _conn.Close();

            return rows > 0;
        }

        public bool XoaSinhVien(string maSV)
        {
            string sql = "DELETE FROM SinhVien WHERE MaSV = @MaSV";
            SqlCommand cmd = new SqlCommand(sql, _conn);
            cmd.Parameters.AddWithValue("@MaSV", maSV);

            _conn.Open();
            int rows = cmd.ExecuteNonQuery();
            _conn.Close();
            return rows > 0;
        }
        public DataTable TimKiemSinhVien(string keyword)
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM SinhVien WHERE HoTen LIKE @keyword OR MaSV LIKE @keyword";
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
        public bool KiemTraTonTai(string maSV)
        {
            string sql = "SELECT COUNT(*) FROM SinhVien WHERE MaSV = @MaSV";
            SqlCommand cmd = new SqlCommand(sql, _conn);
            cmd.Parameters.AddWithValue("@MaSV", maSV);

            _conn.Open();
            int count = (int)cmd.ExecuteScalar();
            _conn.Close();

            return count > 0;
        }
    }
}
