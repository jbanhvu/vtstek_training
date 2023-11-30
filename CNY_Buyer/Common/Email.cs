using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;
using RazorEngine;
using System.IO;
using RazorEngine.Templating;

namespace CNY_Buyer.Common
{
    public static class Email
    {
        public static void Send(string recipientEmail, string subject, string HtmlTemplate)
        {
            string senderEmail = "vuda.vtstek@gmail.com"; // Thay thế bằng địa chỉ email của bạn
            string senderPassword = "bmitpcidautdcyhx"; // Thay thế bằng mật khẩu email của bạn

            // Dữ liệu mẫu để truyền vào template
            List<UserInfo> users = new List<UserInfo>
            {
                new UserInfo { Name = "Người 1", PhoneNumber = "0123456789" },
                new UserInfo { Name = "Người 2", PhoneNumber = "0987654321" }
            };
            string confirmLink = "https://example.com/confirm";
            string rejectLink = "https://example.com/reject";

            // Tạo EmailTemplateModel và truyền dữ liệu vào
            EmailTemplateModel model = new EmailTemplateModel
            {
                Users = users,
                ConfirmLink = confirmLink,
                RejectLink = rejectLink
            };

            // Gọi template và truyền dữ liệu vào bằng Razor Engine
            string body;
            try
            {
                //body = Engine.Razor.RunCompile(File.ReadAllText("EmailTemplate.cshtml"), "templateKey2", null, model);
                body = HtmlTemplate;
            }
            catch(Exception ex)
            {
                 body = ex.ToString();
            }
            


            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);

            MailMessage mailMessage = new MailMessage(senderEmail, recipientEmail, subject, body);
            mailMessage.IsBodyHtml = true;

            smtpClient.Send(mailMessage);
            //MessageBox.Show("Email to " + recipientEmail + " sent successfully!");

        }
    }
    public class EmailTemplateModel
    {
        public List<UserInfo> Users { get; set; }
        public string ConfirmLink { get; set; }
        public string RejectLink { get; set; }
    }

    public class UserInfo
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
}
