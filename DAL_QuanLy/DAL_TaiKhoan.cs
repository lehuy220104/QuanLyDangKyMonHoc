using System;
using System.Data;
using System.Data.SqlClient;
using DTO_QuanLy;

namespace DAL_QuanLy
{
    public class DAL_TaiKhoan : DBConnect
    {
        public bool KiemTraDangNhap(string tenDangNhap, string matKhau)
        {
            string query = "SELECT COUNT(*) FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap AND MatKhau = @MatKhau";
            using (SqlCommand cmd = new SqlCommand(query, _conn))
            {
                cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                cmd.Parameters.AddWithValue("@MatKhau", matKhau);
                _conn.Open();
                int count = (int)cmd.ExecuteScalar();
                _conn.Close();
                return count > 0;
            }
        }

        public string LayVaiTro(string tenDangNhap)
        {
            string query = "SELECT VaiTro FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap";
            using (SqlCommand cmd = new SqlCommand(query, _conn))
            {
                cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                _conn.Open();
                object result = cmd.ExecuteScalar();
                _conn.Close();
                return result?.ToString();
            }
        }

        public bool DoiMatKhau(string tenDangNhap, string matKhauMoi)
        {
            string query = "UPDATE TaiKhoan SET MatKhau = @MatKhauMoi WHERE TenDangNhap = @TenDangNhap";
            using (SqlCommand cmd = new SqlCommand(query, _conn))
            {
                cmd.Parameters.AddWithValue("@MatKhauMoi", matKhauMoi);
                cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                _conn.Open();
                int rows = cmd.ExecuteNonQuery();
                _conn.Close();
                return rows > 0;
            }
        }

        public bool ThemTaiKhoan(DTO_TaiKhoan tk)
        {
            string query = "INSERT INTO TaiKhoan (TenDangNhap, MatKhau, VaiTro) VALUES (@TenDangNhap, @MatKhau, @VaiTro)";
            using (SqlCommand cmd = new SqlCommand(query, _conn))
            {
                cmd.Parameters.AddWithValue("@TenDangNhap", tk.TenDangNhap);
                cmd.Parameters.AddWithValue("@MatKhau", tk.MatKhau);
                cmd.Parameters.AddWithValue("@VaiTro", tk.VaiTro);
                _conn.Open();
                int rows = cmd.ExecuteNonQuery();
                _conn.Close();
                return rows > 0;
            }
        }

        public bool XoaTaiKhoan(string tenDangNhap)
        {
            string query = "DELETE FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap";
            using (SqlCommand cmd = new SqlCommand(query, _conn))
            {
                cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                _conn.Open();
                int rows = cmd.ExecuteNonQuery();
                _conn.Close();
                return rows > 0;
            }
        }

        public bool SuaTaiKhoan(DTO_TaiKhoan tk)
        {
            string query = "UPDATE TaiKhoan SET MatKhau = @MatKhau, VaiTro = @VaiTro WHERE TenDangNhap = @TenDangNhap";
            using (SqlCommand cmd = new SqlCommand(query, _conn))
            {
                cmd.Parameters.AddWithValue("@MatKhau", tk.MatKhau);
                cmd.Parameters.AddWithValue("@VaiTro", tk.VaiTro);
                cmd.Parameters.AddWithValue("@TenDangNhap", tk.TenDangNhap);

                _conn.Open();
                int rows = cmd.ExecuteNonQuery();
                _conn.Close();

                return rows > 0;
            }
        }

        public DataTable LayTatCaTaiKhoan()
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM TaiKhoan";
            using (SqlDataAdapter da = new SqlDataAdapter(query, _conn))
            {
                da.Fill(dt);
            }
            return dt;
        }

        public DataTable TimKiemTaiKhoan(string keyword)
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM TaiKhoan WHERE TenDangNhap LIKE @keyword";

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

        public bool KiemTraTonTai(string tenDangNhap)
        {
            string sql = "SELECT COUNT(*) FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap";
            SqlCommand cmd = new SqlCommand(sql, _conn);
            cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);

            _conn.Open();
            int count = (int)cmd.ExecuteScalar();
            _conn.Close();

            return count > 0;
        }
    }
}
