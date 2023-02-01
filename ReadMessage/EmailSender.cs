using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadMessage
{
    public class EmailSender
    {

        public static void SendEmail(string to, string subject, string body)
        {
            Console.WriteLine("Email sending...");
            MimeMessage email = new MimeMessage();
            email.Sender = MailboxAddress.Parse("familhemidov71@gmail.com");
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = $@"<h1 style='text-align:center;'>{subject}</h1></br>
        <p style='text-align:center;color:blue;'>{body}</p>";
            email.Body = builder.ToMessageBody();
            using (SmtpClient smtp = new())
            {
                smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate("familhemidov71@gmail.com","famil777");
                smtp.Send(email);
                smtp.Disconnect(true);
            }
            Console.WriteLine("Email sent");
        }
        
    }
}
