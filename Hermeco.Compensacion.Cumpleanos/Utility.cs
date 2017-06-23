﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Hermeco.Compensacion.Cumpleanos
{
    public class Utility
    {
        public static void sendEmail(string smtpServer, string subject, string htmlPath, string body, string from, string to, string pathAttachment, bool isBodyHtml, string CC, Dictionary<string, string> parameters = null)
        {

            MailMessage mail = new MailMessage(from, to);

            SmtpClient client = new SmtpClient() { Port = 25, DeliveryMethod = SmtpDeliveryMethod.Network, UseDefaultCredentials = false, Host = smtpServer };            

            if (isBodyHtml)
            {
                using (StreamReader reader = File.OpenText(htmlPath))
                {
                    mail.Body = reader.ReadToEnd();                    
                }
            }
            else
            {
                mail.Body = body;
            }

            if (!string.IsNullOrEmpty(pathAttachment))
            {
                mail.Attachments.Add(new Attachment(pathAttachment));
            }

            if (!string.IsNullOrEmpty(CC))
            {
                var MailCC = new MailAddress(CC);
                mail.CC.Add(MailCC);
            }

            client.Send(mail);

        }
    }
}
