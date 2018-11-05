using JIC.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net.Mime;

namespace JIC.Utilities.Helpers
{
    public class EmailHelper
    {
        #region Properties

        #endregion

        #region Methods

        #region SendMailMehtods

        public static bool SendEmail(string To, string EmailSubject, string EmailContent, out string ErrorMessage)
        {
            return InternalSendMail(EmailFunctions.Default, new List<string> { To }, EmailSubject, EmailContent, new List<Attachment>(), out ErrorMessage);
        }

        public static bool SendEmail(List<string> To, string EmailSubject, string EmailContent, out string ErrorMessage)
        {
            return InternalSendMail(EmailFunctions.Default, To, EmailSubject, EmailContent, new List<Attachment>(), out ErrorMessage);
        }

        public static bool SendEmail(EmailFunctions EmailFunction, string To, string EmailSubject, string EmailContent, out string ErrorMessage)
        {
            return InternalSendMail(EmailFunction, new List<string> { To }, EmailSubject, EmailContent, new List<Attachment>(), out ErrorMessage);
        }

        public static bool SendEmail(EmailFunctions EmailFunction, List<string> To, string EmailSubject, string EmailContent, out string ErrorMessage)
        {
            return InternalSendMail(EmailFunction, To, EmailSubject, EmailContent, new List<Attachment>(), out ErrorMessage);
        }

        public static bool SendEmail(EmailFunctions EmailFunction, List<string> To, string EmailSubject, string EmailContent, out string ErrorMessage, List<Attachment> Attachments)
        {
            return InternalSendMail(EmailFunction, To, EmailSubject, EmailContent, Attachments, out ErrorMessage);
        }

        private static bool InternalSendMail(EmailFunctions EmailFunction, List<string> To, string EmailSubject, string EmailContent, List<Attachment> Attachments, out string ErrorMessage)
        {
            try
            {
                MailMessage _Message = new MailMessage();
                _Message.Subject = EmailSubject;
                _Message.SubjectEncoding = System.Text.Encoding.Unicode;
                _Message.Body = EmailContent;
                _Message.BodyEncoding = System.Text.Encoding.UTF8;
                _Message.IsBodyHtml = true;
                _Message.Priority = MailPriority.High;
                _Message.From = new MailAddress(SystemConfigurations.GetSMTPProperty(EmailFunction, SMTPProperties.From));
                To.ForEach(m => { _Message.Bcc.Add(m); });
                Attachments.ForEach(attachment => { _Message.Attachments.Add(attachment); });
                SmtpClient Client = GetSmtpClient(EmailFunction);

                Client.Send(_Message);
                ErrorMessage = string.Empty;
                return true;
            }
            catch (Exception exc)
            {
                ErrorMessage = exc.Message;
                return false;
            }
        }

        private static SmtpClient GetSmtpClient(EmailFunctions EmailFunction)
        {
            return new SmtpClient()
            {
                Host = SystemConfigurations.GetSMTPProperty(EmailFunction, SMTPProperties.Host),
                Port = int.Parse(SystemConfigurations.GetSMTPProperty(EmailFunction, SMTPProperties.Port)),
                EnableSsl = bool.Parse(SystemConfigurations.GetSMTPProperty(EmailFunction, SMTPProperties.EnableSSL)),
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(SystemConfigurations.GetSMTPProperty(EmailFunction, SMTPProperties.Username), SystemConfigurations.GetSMTPProperty(EmailFunction, SMTPProperties.Password)),
            };
        }

        #endregion

        #region BuildTemplatesMethods

        public static Attachment CreateAttachment(string _AttachmentPath)
        {
            string FileName = _AttachmentPath;
            Attachment attachment = new Attachment(FileName, MediaTypeNames.Application.Octet);
            ContentDisposition disposition = attachment.ContentDisposition;
            disposition.CreationDate = File.GetCreationTime(FileName);
            disposition.ModificationDate = File.GetLastWriteTime(FileName);
            disposition.ReadDate = File.GetLastAccessTime(FileName);
            disposition.FileName = Path.GetFileName(FileName);
            disposition.Size = new FileInfo(FileName).Length;
            disposition.DispositionType = DispositionTypeNames.Attachment;
            return attachment;
        }

        #endregion

        #endregion
    }
}
