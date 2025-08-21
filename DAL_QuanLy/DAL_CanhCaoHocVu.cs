using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;

namespace DAL_QuanLy
{
    public class DAL_CanhCaoHocVu : DBConnect
    {
        public DataTable LayDSSVCanhCao(string namHoc, string maLop, int hocKySo)
        {
            string sql = @"
        SELECT sv.MaSV, sv.HoTen, sv.Email, gpa.GPA
        FROM SinhVien sv
        INNER JOIN Lop l ON sv.MaLop = l.MaLop
        INNER JOIN GPA gpa ON sv.MaSV = gpa.MaSV
        INNER JOIN HocKy hk ON gpa.MaHK = hk.MaHK
        WHERE hk.NamHoc = @NamHoc
          AND hk.HocKySo = @HocKySo
          AND sv.MaLop = @MaLop
          AND gpa.GPA < 2.0";

            using (SqlCommand cmd = new SqlCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@NamHoc", namHoc);
                cmd.Parameters.AddWithValue("@HocKySo", hocKySo);
                cmd.Parameters.AddWithValue("@MaLop", maLop);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
        public DataTable LayDanhSachNamHoc()
        {
            string sql = "SELECT DISTINCT NamHoc FROM HocKy ORDER BY NamHoc DESC";
            SqlDataAdapter da = new SqlDataAdapter(sql, _conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable LayDanhSachLop()
        {
            string sql = "SELECT MaLop, TenLop FROM Lop ORDER BY TenLop";
            SqlDataAdapter da = new SqlDataAdapter(sql, _conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        public bool SendEmail(string toEmail, string subject, string body)
        {
            try
            {
                string defaultSenderEmail = "ousender2025@gmail.com";
                string defaultSenderPassword = "aelvojvysyeagyrg";

                using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
                {
                    client.EnableSsl = true;
                    client.Credentials = new System.Net.NetworkCredential(defaultSenderEmail, defaultSenderPassword);

                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(defaultSenderEmail);
                    mailMessage.To.Add(toEmail);
                    mailMessage.Subject = subject;
                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = false;

                    client.Send(mailMessage);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
