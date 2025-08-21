using System;
using System.Net;
using System.Net.Mail;

namespace Utils
{
    public static class EmailHelper
    {
        // Email mặc định để gửi cảnh cáo
        private static string defaultSenderEmail = "ousender2025@gmail.com";
        // Mật khẩu ứng dụng (App Password) 16 ký tự, bỏ khoảng trắng
        private static string defaultSenderPassword = "aelvojvysyeagyrg";

        public static void SendEmail(string toEmail, string subject, string body)
        {
            try
            {
                using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
                {
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(defaultSenderEmail, defaultSenderPassword);

                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(defaultSenderEmail);
                    mailMessage.To.Add(toEmail);
                    mailMessage.Subject = subject;
                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = false; // hoặc true nếu muốn hỗ trợ HTML

                    client.Send(mailMessage);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi gửi email: " + ex.Message);
            }
        }
    }
}
