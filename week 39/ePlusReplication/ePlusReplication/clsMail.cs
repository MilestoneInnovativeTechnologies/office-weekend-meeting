using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;

namespace ePlusReplication
{
    class clsMail
    {
        public static bool sendEmail(string fromID, string pwd, string toID, string subject, string body)
        {
            /*try
            {
                var fromAddress = new MailAddress(fromID);
                var toAddress = new MailAddress(toID);
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, pwd)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
            }
            catch
            {
                return false;
            }*/
            return true;
        }
    }
}
