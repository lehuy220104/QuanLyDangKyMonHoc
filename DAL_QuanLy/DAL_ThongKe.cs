using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL_QuanLy
{
    public class DAL_ThongKe : DBConnect
    {
        // 1. Thống kê số lượng sinh viên theo LHP
        public DataTable ThongKeSoLuongSVTheoLHP(string maHK)
        {
            string sql = @"
                SELECT lhp.MaLHP, COUNT(dkh.MaSV) AS SoLuong
                FROM LopHocPhan lhp
                LEFT JOIN DangKyHoc dkh ON lhp.MaLHP = dkh.MaLHP
                WHERE lhp.MaHK = @MaHK
                GROUP BY lhp.MaLHP";

            using (SqlCommand cmd = new SqlCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@MaHK", maHK);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // 2. Thống kê điểm trung bình theo LHP
        public DataTable ThongKeDiemTBTheoLHP(string maHK)
        {
            string sql = @"
                SELECT lhp.MaLHP, 
                       ROUND(AVG(dkh.Diem), 2) AS DiemTrungBinh
                FROM LopHocPhan lhp
                INNER JOIN DangKyHoc dkh ON lhp.MaLHP = dkh.MaLHP
                WHERE lhp.MaHK = @MaHK
                GROUP BY lhp.MaLHP";

            using (SqlCommand cmd = new SqlCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@MaHK", maHK);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // 3. Thống kê tỉ lệ SV đạt >= 5 theo LHP
        public DataTable ThongKeTiLeDat(string maHK)
        {
            string sql = @"
                SELECT lhp.MaLHP,
                       CAST(SUM(CASE WHEN dkh.Diem >= 5 THEN 1 ELSE 0 END) AS FLOAT) /
                       NULLIF(COUNT(dkh.MaSV), 0) * 100 AS TiLeDat
                FROM LopHocPhan lhp
                INNER JOIN DangKyHoc dkh ON lhp.MaLHP = dkh.MaLHP
                WHERE lhp.MaHK = @MaHK
                GROUP BY lhp.MaLHP";

            using (SqlCommand cmd = new SqlCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@MaHK", maHK);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}
