namespace PrimeStore.service.Abstracts
{
    public interface IEmailService
    {
        Task<string> SendEmailAsync(string email, string message, string subject);
    }
}
