using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using DTO_QuanLy;

namespace DAL_QuanLy
{
    public class DAL_GiangVien : DBConnect
    {
        public DataTable LayDanhSachGiangVien()
        {
            string sql = "SELECT * FROM GiangVien";
            SqlDataAdapter da = new SqlDataAdapter(sql, _conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public bool ThemGiangVien(DTO_GiangVien gv)
        {
            string sql = "INSERT INTO GiangVien (MaGV, HoTen, Email, MaKhoa, GioiTinh) VALUES (@MaGV, @HoTen, @Email, @MaKhoa, @GioiTinh)";
            SqlCommand cmd = new SqlCommand(sql, _conn);
            cmd.Parameters.AddWithValue("@MaGV", gv.MaGV);
            cmd.Parameters.AddWithValue("@HoTen", gv.HoTen);
            cmd.Parameters.AddWithValue("@Email", gv.Email);
            cmd.Parameters.AddWithValue("@MaKhoa", gv.MaKhoa);
            cmd.Parameters.AddWithValue("@GioiTinh", gv.GioiTinh);

            _conn.Open();
            int rows = cmd.ExecuteNonQuery();
            _conn.Close();
            return rows > 0;
        }

        public bool CapNhatGiangVien(DTO_GiangVien gv)
        {
            string sql = "UPDATE GiangVien SET HoTen = @HoTen, Email = @Email, MaKhoa = @MaKhoa, GioiTinh = @GioiTinh WHERE MaGV = @MaGV";
            SqlCommand cmd = new SqlCommand(sql, _conn);
            cmd.Parameters.AddWithValue("@HoTen", gv.HoTen);
            cmd.Parameters.AddWithValue("@Email", gv.Email);
            cmd.Parameters.AddWithValue("@MaKhoa", gv.MaKhoa);
            cmd.Parameters.AddWithValue("@GioiTinh", gv.GioiTinh);
            cmd.Parameters.AddWithValue("@MaGV", gv.MaGV);

            _conn.Open();
            int rows = cmd.ExecuteNonQuery();
            _conn.Close();
            return rows > 0;
        }

        public bool XoaGiangVien(string maGV)
        {
            string sql = "DELETE FROM GiangVien WHERE MaGV = @MaGV";
            SqlCommand cmd = new SqlCommand(sql, _conn);
            cmd.Parameters.AddWithValue("@MaGV", maGV);

            _conn.Open();
            int rows = cmd.ExecuteNonQuery();
            _conn.Close();
            return rows > 0;
        }

        public bool KiemTraTonTai(string maGV)
        {
            string sql = "SELECT COUNT(*) FROM GiangVien WHERE MaGV = @MaGV";
            SqlCommand cmd = new SqlCommand(sql, _conn);
            cmd.Parameters.AddWithValue("@MaGV", maGV);

            _conn.Open();
            int count = (int)cmd.ExecuteScalar();
            _conn.Close();
            return count > 0;
        }

        public DataTable TimKiemGiangVien(string keyword)
        {
            string sql = "SELECT * FROM GiangVien WHERE MaGV LIKE @kw OR HoTen LIKE @kw";
            SqlDataAdapter da = new SqlDataAdapter(sql, _conn);
            da.SelectCommand.Parameters.AddWithValue("@kw", "%" + keyword + "%");
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        // Email mặc định để gửi cảnh cáo
        private static string defaultSenderEmail = "ousender2025@gmail.com";
        // App Password 16 ký tự, bỏ khoảng trắng
        private static string defaultSenderPassword = "aelvojvysyeagyrg";

        /// <summary>
        /// Gửi email cảnh cáo học vụ
        /// </summary>
        public bool SendEmail(string toEmail, string subject, string body)
        {
            try
            {
                using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
                {
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(defaultSenderEmail, defaultSenderPassword);
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.Timeout = 20000; // 20 giây

                    using (MailMessage mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress(defaultSenderEmail);
                        mailMessage.To.Add(toEmail);
                        mailMessage.Subject = subject;
                        mailMessage.Body = body;
                        mailMessage.IsBodyHtml = false; // Đặt true nếu muốn gửi HTML

                        client.Send(mailMessage);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi gửi email: " + ex.Message);
            }
        }

    }
}
