using MediatR;
using PrimeStore.Core.Bases;
using PrimeStore.data.Helper;
using PrimeStore.service.Abstracts;
using SchoolProject.Core.Features.Emails.Commands.Models;
namespace PrimeStore.Core.Features.Emails.Commands.Handlers
{
    public class EmailsCommandHandler : IRequestHandler<SendEmailCommand, Response<string>>
    {
        #region Fields
        private readonly IEmailService _EmailService;
        #endregion
        #region Constructors
        public EmailsCommandHandler(IEmailService emailsService)
        {
            _EmailService = emailsService;
        }
        #endregion
        #region Handle Functions
        public async Task<Response<string>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var response = await _EmailService.SendEmailAsync(request.Email, request.Message, null);
            if (response == ResultString.Success)
                return ResponseHandler.Success<string>("");
            return ResponseHandler.BadRequest<string>("Failed Submit");
        }
        #endregion
    }
}
