using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL_QuanLy
{
    public class DAL_Admin : DBConnect
    {
        // Lấy danh sách tên các bảng trong CSDL
        public DataTable LayDanhSachBang()
        {
            string sql = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME <> 'sysdiagrams' AND TABLE_NAME <> 'TaiKhoan'";
            SqlDataAdapter da = new SqlDataAdapter(sql, _conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        // Lấy dữ liệu của 1 bảng
        public DataTable LayDuLieuBang(string tenBang)
        {
            string sql = $"SELECT * FROM {tenBang}";
            SqlDataAdapter da = new SqlDataAdapter(sql, _conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        // Lưu chỉnh sửa DataTable về DB
        public void LuuChinhSua(string tenBang, DataTable dt)
        {
            string sql = $"SELECT * FROM {tenBang}";
            SqlDataAdapter da = new SqlDataAdapter(sql, _conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            da.Update(dt);
        }
    }
}
