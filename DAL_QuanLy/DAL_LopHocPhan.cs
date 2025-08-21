using System;
using System.Data;
using System.Data.SqlClient;
using DTO_QuanLy;

namespace DAL_QuanLy
{
    public class DAL_LopHocPhan : DBConnect
    {
        public DataTable LayLopHocPhanTheoHocKy(string maHK)
        {
            string sql = @"
        SELECT 
            lhp.MaLHP,
            mh.TenMH,
            mh.SoTinChi,
            gv.HoTen AS GiangVien,
            lhp.TietBatDau,
            lhp.TietKetThuc,
            lhp.Thu,
            lhp.PhongHoc
        FROM LopHocPhan lhp
        JOIN MonHoc mh ON lhp.MaMH = mh.MaMH
        JOIN GiangVien gv ON lhp.MaGV = gv.MaGV
        WHERE lhp.MaHK = @MaHK";

            SqlCommand cmd = new SqlCommand(sql, _conn);
            cmd.Parameters.AddWithValue("@MaHK", maHK);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable LayDanhSachLopHocPhan()
        {
            string sql = "SELECT * FROM LopHocPhan";
            SqlDataAdapter da = new SqlDataAdapter(sql, _conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable LayDanhSachLopHPTheoGiangVien(string maGV)
        {
            string sql = "SELECT * FROM LopHocPhan WHERE MaGV = @MaGV";
            SqlCommand cmd = new SqlCommand(sql, _conn);
            cmd.Parameters.AddWithValue("@MaGV", maGV);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public bool ThemLopHocPhan(DTO_LopHocPhan lhp)
        {
            string sql = @"INSERT INTO LopHocPhan (MaLHP, MaMH, MaGV, MaHK, TietBatDau, TietKetThuc, Thu, PhongHoc) 
                           VALUES (@MaLHP, @MaMH, @MaGV, @MaHK, @TietBatDau, @TietKetThuc, @Thu, @PhongHoc)";
            SqlCommand cmd = new SqlCommand(sql, _conn);
            cmd.Parameters.AddWithValue("@MaLHP", lhp.MaLHP);
            cmd.Parameters.AddWithValue("@MaMH", lhp.MaMH);
            cmd.Parameters.AddWithValue("@MaGV", lhp.MaGV);
            cmd.Parameters.AddWithValue("@MaHK", lhp.MaHK);
            cmd.Parameters.AddWithValue("@TietBatDau", lhp.TietBatDau);
            cmd.Parameters.AddWithValue("@TietKetThuc", lhp.TietKetThuc);
            cmd.Parameters.AddWithValue("@Thu", lhp.Thu);
            cmd.Parameters.AddWithValue("@PhongHoc", lhp.PhongHoc);

            _conn.Open();
            int rows = cmd.ExecuteNonQuery();
            _conn.Close();
            return rows > 0;
        }

        public bool CapNhatLopHocPhan(DTO_LopHocPhan lhp)
        {
            string sql = @"UPDATE LopHocPhan 
                           SET MaMH = @MaMH, MaGV = @MaGV, MaHK = @MaHK, TietBatDau = @TietBatDau, 
                               TietKetThuc = @TietKetThuc, Thu = @Thu, PhongHoc = @PhongHoc 
                           WHERE MaLHP = @MaLHP";
            SqlCommand cmd = new SqlCommand(sql, _conn);
            cmd.Parameters.AddWithValue("@MaLHP", lhp.MaLHP);
            cmd.Parameters.AddWithValue("@MaMH", lhp.MaMH);
            cmd.Parameters.AddWithValue("@MaGV", lhp.MaGV);
            cmd.Parameters.AddWithValue("@MaHK", lhp.MaHK);
            cmd.Parameters.AddWithValue("@TietBatDau", lhp.TietBatDau);
            cmd.Parameters.AddWithValue("@TietKetThuc", lhp.TietKetThuc);
            cmd.Parameters.AddWithValue("@Thu", lhp.Thu);
            cmd.Parameters.AddWithValue("@PhongHoc", lhp.PhongHoc);

            _conn.Open();
            int rows = cmd.ExecuteNonQuery();
            _conn.Close();
            return rows > 0;
        }

        public bool XoaLopHocPhan(string maLHP)
        {
            string sql = "DELETE FROM LopHocPhan WHERE MaLHP = @MaLHP";
            SqlCommand cmd = new SqlCommand(sql, _conn);
            cmd.Parameters.AddWithValue("@MaLHP", maLHP);

            _conn.Open();
            int rows = cmd.ExecuteNonQuery();
            _conn.Close();
            return rows > 0;
        }
        public DataTable LayTatCaLopHocPhan()
        {
            string sql = @"
        SELECT 
            lhp.MaLHP,
            lhp.MaMH,
            lhp.MaGV,
            lhp.MaHK,
            mh.TenMH,
            mh.SoTinChi,
            gv.HoTen AS GiangVien,
            lhp.TietBatDau,
            lhp.TietKetThuc,
            lhp.Thu,
            lhp.PhongHoc,
            hk.HocKySo,
            hk.NamHoc
        FROM LopHocPhan lhp
        JOIN MonHoc mh ON lhp.MaMH = mh.MaMH
        JOIN GiangVien gv ON lhp.MaGV = gv.MaGV
        JOIN HocKy hk ON lhp.MaHK = hk.MaHK";

            SqlDataAdapter da = new SqlDataAdapter(sql, _conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        public DataTable LayThongTinLopHocPhan(string maLHP)
        {
            string sql = @"
        SELECT MaMH, Thu, TietBatDau, TietKetThuc
        FROM LopHocPhan
        WHERE MaLHP = @MaLHP";

            SqlCommand cmd = new SqlCommand(sql, _conn);
            cmd.Parameters.AddWithValue("@MaLHP", maLHP);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        public bool KiemTraMonTienQuyet(string maSV, string maLHP)
        {
            // Truy ra môn học của lớp học phần
            string sqlMaMH = "SELECT MaMH FROM LopHocPhan WHERE MaLHP = @MaLHP";
            SqlCommand cmd1 = new SqlCommand(sqlMaMH, _conn);
            cmd1.Parameters.AddWithValue("@MaLHP", maLHP);

            _conn.Open();
            object maMHObj = cmd1.ExecuteScalar();
            _conn.Close();

            if (maMHObj == null) return false;
            string maMH = maMHObj.ToString();

            // Lấy môn tiên quyết
            string sqlTienQuyet = "SELECT MonTienQuyet FROM MonHoc WHERE MaMH = @MaMH";
            SqlCommand cmd2 = new SqlCommand(sqlTienQuyet, _conn);
            cmd2.Parameters.AddWithValue("@MaMH", maMH);

            _conn.Open();
            object monTienQuyetObj = cmd2.ExecuteScalar();
            _conn.Close();

            if (monTienQuyetObj == null || string.IsNullOrEmpty(monTienQuyetObj.ToString()))
                return false; // Không có môn tiên quyết

            string monTienQuyet = monTienQuyetObj.ToString();

            // Kiểm tra điểm >= 5 của môn tiên quyết
            string sqlKiemTra = @"
        SELECT COUNT(*) 
        FROM DangKyHoc d
        JOIN LopHocPhan lhp ON d.MaLHP = lhp.MaLHP
        WHERE d.MaSV = @MaSV AND lhp.MaMH = @MonTienQuyet AND d.Diem >= 5";

            SqlCommand cmd3 = new SqlCommand(sqlKiemTra, _conn);
            cmd3.Parameters.AddWithValue("@MaSV", maSV);
            cmd3.Parameters.AddWithValue("@MonTienQuyet", monTienQuyet);

            _conn.Open();
            int count = (int)cmd3.ExecuteScalar();
            _conn.Close();

            return count == 0; // true nếu chưa học hoặc không đạt
        }
        public DataTable LayThoiKhoaBieu(string maSV, string maHK)
        {
            string sql = @"
        SELECT 
            mh.TenMH,
            gv.HoTen AS GiangVien,
            lhp.Thu,
            lhp.TietBatDau,
            lhp.TietKetThuc,
            lhp.PhongHoc
        FROM DangKyHoc dk
        JOIN LopHocPhan lhp ON dk.MaLHP = lhp.MaLHP
        JOIN MonHoc mh ON lhp.MaMH = mh.MaMH
        JOIN GiangVien gv ON lhp.MaGV = gv.MaGV
        WHERE dk.MaSV = @MaSV AND lhp.MaHK = @MaHK";

            SqlCommand cmd = new SqlCommand(sql, _conn);
            cmd.Parameters.AddWithValue("@MaSV", maSV);
            cmd.Parameters.AddWithValue("@MaHK", maHK);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable TimLopHocPhan(string maLHP)
        {
            string sql = @"
        SELECT lhp.MaLHP, lhp.MaMH, mh.TenMH, lhp.MaGV, gv.HoTen AS TenGV,
               lhp.MaHK, hk.TenHK, lhp.Thu, lhp.TietBatDau, lhp.TietKetThuc, lhp.PhongHoc
        FROM LopHocPhan lhp
        INNER JOIN MonHoc mh ON lhp.MaMH = mh.MaMH
        INNER JOIN GiangVien gv ON lhp.MaGV = gv.MaGV
        INNER JOIN HocKy hk ON lhp.MaHK = hk.MaHK
        WHERE lhp.MaLHP LIKE @ma
    ";

            SqlDataAdapter da = new SqlDataAdapter(sql, _conn);
            da.SelectCommand.Parameters.AddWithValue("@ma", "%" + maLHP + "%");
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public bool KiemTraTrungLich(string maGV, int thu, int tietBD, int tietKT, string maHK, string phongHoc)
        {
            string sql = @"
                SELECT COUNT(*) FROM LopHocPhan
                WHERE MaHK = @MaHK
                  AND Thu = @Thu
                  AND (
                        MaGV = @MaGV
                        OR (PhongHoc IS NOT NULL AND PhongHoc = @Phong)
                      )
                  AND NOT (TietKetThuc < @TietBatDau OR TietBatDau > @TietKetThuc)
            ";
            using (SqlCommand cmd = new SqlCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@MaGV", maGV);
                cmd.Parameters.AddWithValue("@Thu", thu);
                cmd.Parameters.AddWithValue("@TietBatDau", tietBD);
                cmd.Parameters.AddWithValue("@TietKetThuc", tietKT);
                cmd.Parameters.AddWithValue("@MaHK", maHK);
                cmd.Parameters.AddWithValue("@Phong", (object)phongHoc ?? DBNull.Value);
                try
                {
                    _conn.Open();
                    int c = (int)cmd.ExecuteScalar();
                    return c > 0;
                }
                finally { _conn.Close(); }
            }
        }

        // Kiểm tra trùng lịch khi sửa (bỏ qua chính MaLHP)
        public bool KiemTraTrungLichSua(string maLHP, string maGV, int thu, int tietBD, int tietKT, string maHK, string phongHoc)
        {
            string sql = @"
                SELECT COUNT(*) FROM LopHocPhan
                WHERE MaHK = @MaHK
                  AND Thu = @Thu
                  AND MaLHP <> @MaLHP
                  AND (
                        MaGV = @MaGV
                        OR (PhongHoc IS NOT NULL AND PhongHoc = @Phong)
                      )
                  AND NOT (TietKetThuc < @TietKetThuc OR TietBatDau > @TietBatDau)
            ";
            using (SqlCommand cmd = new SqlCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@MaLHP", maLHP);
                cmd.Parameters.AddWithValue("@MaGV", maGV);
                cmd.Parameters.AddWithValue("@Thu", thu);
                cmd.Parameters.AddWithValue("@TietBatDau", tietBD);
                cmd.Parameters.AddWithValue("@TietKetThuc", tietKT);
                cmd.Parameters.AddWithValue("@MaHK", maHK);
                cmd.Parameters.AddWithValue("@Phong", (object)phongHoc ?? DBNull.Value);

                try
                {
                    _conn.Open();
                    int c = (int)cmd.ExecuteScalar();
                    return c > 0;
                }
                finally { _conn.Close(); }
            }
        }
        public bool KiemTraTonTai(string maLHP)
        {
            string sql = "SELECT COUNT(*) FROM LopHocPhan WHERE MaLHP = @MaLHP";
            SqlCommand cmd = new SqlCommand(sql, _conn);
            cmd.Parameters.AddWithValue("@MaLHP", maLHP);

            _conn.Open();
            int count = (int)cmd.ExecuteScalar();
            _conn.Close();

            return count > 0;
        }
    }
}
