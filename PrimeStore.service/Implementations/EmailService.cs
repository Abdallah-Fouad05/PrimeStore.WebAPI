using MailKit.Net.Smtp;
using MimeKit;
using PrimeStore.data.Helper;
using PrimeStore.service.Abstracts;

namespace PrimeStore.service.Implementations
{
    public class EmailService : IEmailService
    {
        #region Fields
        private readonly EmailSettings _EmailSettings;
        #endregion
        #region Constructors
        public EmailService(EmailSettings emailSettings)
        {
            _EmailSettings = emailSettings;
        }

        #endregion

        public async Task<string> SendEmailAsync(string email, string Message, string subject)
        {
            try
            {
                //sending the Message of passwordResetLink
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_EmailSettings.host, _EmailSettings.port, true);
                    client.Authenticate(_EmailSettings.FromEmail, _EmailSettings.password);
                    var bodybuilder = new BodyBuilder
                    {
                        HtmlBody = $"{Message}",
                        TextBody = "Welcome",
                    };
                    var message = new MimeMessage
                    {
                        Body = bodybuilder.ToMessageBody()
                    };
                    message.From.Add(new MailboxAddress("Prime Store Team", _EmailSettings.FromEmail));
                    message.To.Add(new MailboxAddress("testing", email));
                    message.Subject = subject == null ? "No Submitted" : subject;
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
                //end of sending email
                return ResultString.Success;
            }
            catch (Exception ex)
            {
                return ResultString.Failure;
            }
        }
    }
}
