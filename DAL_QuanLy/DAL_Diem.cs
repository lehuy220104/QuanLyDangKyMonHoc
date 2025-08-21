using System.Data;
using System.Data.SqlClient;
using DTO_QuanLy;

namespace DAL_QuanLy
{
    public class DAL_Diem : DBConnect
    {
        public DataTable LayDSSinhVien(string maLHP)
        {
            string query = @"
                SELECT dk.MaSV, sv.HoTen, dk.Diem 
                FROM DangKyHoc dk 
                JOIN SinhVien sv ON dk.MaSV = sv.MaSV 
                WHERE dk.MaLHP = @maLHP";

            SqlCommand cmd = new SqlCommand(query, _conn);
            cmd.Parameters.AddWithValue("@maLHP", maLHP);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public bool CapNhatDiem(DTO_Diem diem)
        {
            try
            {
                _conn.Open();
                string query = "UPDATE DangKyHoc SET Diem = @diem WHERE MaSV = @maSV AND MaLHP = @maLHP";
                SqlCommand cmd = new SqlCommand(query, _conn);
                cmd.Parameters.AddWithValue("@diem", diem.Diem);
                cmd.Parameters.AddWithValue("@maSV", diem.MaSV);
                cmd.Parameters.AddWithValue("@maLHP", diem.MaLHP);
                return cmd.ExecuteNonQuery() > 0;
            }
            finally
            {
                _conn.Close();
            }
        }

        public bool XoaDiem(DTO_Diem diem)
        {
            try
            {
                _conn.Open();
                string query = "UPDATE DangKyHoc SET Diem = NULL WHERE MaSV = @maSV AND MaLHP = @maLHP";
                SqlCommand cmd = new SqlCommand(query, _conn);
                cmd.Parameters.AddWithValue("@maSV", diem.MaSV);
                cmd.Parameters.AddWithValue("@maLHP", diem.MaLHP);
                return cmd.ExecuteNonQuery() > 0;
            }
            finally
            {
                _conn.Close();
            }
        }

        public DataTable ThongKeDiemTheoLop(string maLHP)
        {
            string query = "SELECT MaSV, Diem FROM DangKyHoc WHERE MaLHP = @MaLHP";
            SqlCommand cmd = new SqlCommand(query, _conn);
            cmd.Parameters.AddWithValue("@MaLHP", maLHP);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

    }
}
