using System;
using System.Data;
using System.Data.SqlClient;
using DTO_QuanLy;

namespace DAL_QuanLy
{
    public class DAL_DangKyHoc : DBConnect
    {
        public bool DangKyMonHoc(DTO_DangKyHoc dk)
        {
            string sql = "INSERT INTO DangKyHoc (MaSV, MaLHP) VALUES (@MaSV, @MaLHP)";
            SqlCommand cmd = new SqlCommand(sql, _conn);
            cmd.Parameters.AddWithValue("@MaSV", dk.MaSV);
            cmd.Parameters.AddWithValue("@MaLHP", dk.MaLHP);

            try
            {
                _conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
            catch
            {
                return false;
            }
            finally
            {
                _conn.Close();
            }
        }

        public bool DaDangKy(string maSV, string maLHP)
        {
            string sql = "SELECT COUNT(*) FROM DangKyHoc WHERE MaSV = @MaSV AND MaLHP = @MaLHP";
            SqlCommand cmd = new SqlCommand(sql, _conn);
            cmd.Parameters.AddWithValue("@MaSV", maSV);
            cmd.Parameters.AddWithValue("@MaLHP", maLHP);

            try
            {
                _conn.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
            catch
            {
                return false;
            }
            finally
            {
                _conn.Close();
            }
        }

        public DataTable LayDanhSachLHPDaDangKy(string maSV)
        {
            string sql = @"
                SELECT lhp.Thu, lhp.TietBatDau, lhp.TietKetThuc
                FROM DangKyHoc dk
                JOIN LopHocPhan lhp ON dk.MaLHP = lhp.MaLHP
                WHERE dk.MaSV = @MaSV";

            SqlCommand cmd = new SqlCommand(sql, _conn);
            cmd.Parameters.AddWithValue("@MaSV", maSV);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable LayThongTinLopHocPhan(string maLHP)
        {
            string sql = "SELECT Thu, TietBatDau, TietKetThuc FROM LopHocPhan WHERE MaLHP = @MaLHP";
            SqlCommand cmd = new SqlCommand(sql, _conn);
            cmd.Parameters.AddWithValue("@MaLHP", maLHP);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public string LayMaMonHoc(string maLHP)
        {
            string sql = "SELECT MaMH FROM LopHocPhan WHERE MaLHP = @MaLHP";
            SqlCommand cmd = new SqlCommand(sql, _conn);
            cmd.Parameters.AddWithValue("@MaLHP", maLHP);

            try
            {
                _conn.Open();
                object result = cmd.ExecuteScalar();
                return result != null ? result.ToString() : "";
            }
            catch
            {
                return "";
            }
            finally
            {
                _conn.Close();
            }
        }

        public string LayMonTienQuyet(string maMH)
        {
            string sql = "SELECT MonTienQuyet FROM MonHoc WHERE MaMH = @MaMH";
            SqlCommand cmd = new SqlCommand(sql, _conn);
            cmd.Parameters.AddWithValue("@MaMH", maMH);

            try
            {
                _conn.Open();
                object result = cmd.ExecuteScalar();
                return result != null ? result.ToString() : "";
            }
            catch
            {
                return "";
            }
            finally
            {
                _conn.Close();
            }
        }

        public bool DaHoanThanhMonTienQuyet(string maSV, string maMH)
        {
            string sql = @"
                SELECT d.Diem
                FROM DangKyHoc d
                JOIN LopHocPhan lhp ON d.MaLHP = lhp.MaLHP
                WHERE d.MaSV = @MaSV AND lhp.MaMH = @MaMH AND d.Diem >= 5.0";

            SqlCommand cmd = new SqlCommand(sql, _conn);
            cmd.Parameters.AddWithValue("@MaSV", maSV);
            cmd.Parameters.AddWithValue("@MaMH", maMH);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt.Rows.Count > 0;
        }
        public DataTable LayMonDaDangKy(string maSV, string maHK)
        {
            string sql = @"
        SELECT 
            dk.MaLHP,
            mh.TenMH,
            mh.SoTinChi,
            gv.HoTen AS GiangVien,
            lhp.TietBatDau,
            lhp.TietKetThuc,
            lhp.Thu,
            lhp.PhongHoc,
            hk.HocKySo,
            hk.NamHoc
        FROM DangKyHoc dk
        JOIN LopHocPhan lhp ON dk.MaLHP = lhp.MaLHP
        JOIN MonHoc mh ON lhp.MaMH = mh.MaMH
        JOIN GiangVien gv ON lhp.MaGV = gv.MaGV
        JOIN HocKy hk ON lhp.MaHK = hk.MaHK
        WHERE dk.MaSV = @MaSV AND hk.MaHK = @MaHK";

            SqlCommand cmd = new SqlCommand(sql, _conn);
            cmd.Parameters.AddWithValue("@MaSV", maSV);
            cmd.Parameters.AddWithValue("@MaHK", maHK);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        public bool HuyDangKy(string maSV, string maLHP)
        {
            string sql = "DELETE FROM DangKyHoc WHERE MaSV = @MaSV AND MaLHP = @MaLHP";
            SqlCommand cmd = new SqlCommand(sql, _conn);
            cmd.Parameters.AddWithValue("@MaSV", maSV);
            cmd.Parameters.AddWithValue("@MaLHP", maLHP);

            try
            {
                _conn.Open();
                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
            catch
            {
                return false;
            }
            finally
            {
                _conn.Close();
            }
        }
        public DataTable LayDiemSinhVien(string maSV, string maHK)
        {
            string sql = @"
        SELECT 
            mh.TenMH,
            dk.Diem
        FROM DangKyHoc dk
        JOIN LopHocPhan lhp ON dk.MaLHP = lhp.MaLHP
        JOIN MonHoc mh ON lhp.MaMH = mh.MaMH
        WHERE dk.MaSV = @MaSV AND lhp.MaHK = @MaHK";

            SqlCommand cmd = new SqlCommand(sql, _conn);
            cmd.Parameters.AddWithValue("@MaSV", maSV);
            cmd.Parameters.AddWithValue("@MaHK", maHK);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }



    }
}
