using DataContext.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace AppService
{
    public class HelperService : IHelper
    {
        public HelperService()
        {
        }

        public void InvoiceEmailSender(string email, string title, MemoryStream stream)
        {

            var fromEmail = new MailAddress("pawel.jeziorski.97@gmail.com", "COMPANY");
            var fromEmailPass = "serwerycod4";

            var toEmail = new MailAddress(email);

            var subject = title;

            var body = "<br/><br/>NEW ORDER" + title + "<br/>"
                    + "FAKTURA";

            var smtp = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPass)
            };



            var MS = new MailMessage(fromEmail, toEmail);
            MS.Subject = subject;
            MS.Body = body;
            MS.IsBodyHtml = true;
            MS.Attachments.Add(new Attachment(stream,"INVOICE.pdf"));


                smtp.Send(MS);
        }

        public void WarehouseEmailSender(string email, string title, string content)
        {
            {
                //var veryfyURL = "/User/VerifyAccount/" + activationCode;
                // var link = "https://localhost:44347" + veryfyURL;

                var fromEmail = new MailAddress("pawel.jeziorski.97@gmail.com", "COMPANY");
                var fromEmailPass = "serwerycod4";

                var toEmail = new MailAddress(email);

                var subject = title;

                var body = "<br/><br/>NEW ORDER" + title + "<br/>"
                        + content;

                var smtp = new SmtpClient()
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new NetworkCredential(fromEmail.Address, fromEmailPass)
                };

                using (var msg = new MailMessage(fromEmail, toEmail)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                })
                    smtp.Send(msg);
            }

        }
    }
}
