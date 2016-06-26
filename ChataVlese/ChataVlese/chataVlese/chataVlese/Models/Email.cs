using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace chataVlese.Models
{
    public class Email
    {
        public static void sendEmail(String subject,String message,String addressTo)
        {
            try
            {
                MailMessage mailMessage = new MailMessage("chatavlese2@gmail.com",addressTo);
                mailMessage.Subject = subject;
                mailMessage.Body = message;

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com",587);
                smtpClient.Credentials = new System.Net.NetworkCredential()
                {
                    UserName = "chatavlese2@gmail.com",
                    Password = "terchova"
                };
                smtpClient.EnableSsl = true;
                smtpClient.Send(mailMessage);
            }
            catch(Exception ex)
            {

            }
        }

    }

}