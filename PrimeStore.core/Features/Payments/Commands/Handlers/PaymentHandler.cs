using MediatR;
using PrimeStore.core.Features.Payments.Commands.Models;
using PrimeStore.Core.Bases;
using PrimeStore.data.Helper;
using PrimeStore.service.Abstracts;

namespace PrimeStore.core.Features.Payments.Commands.Handlers
{
    public class PaymentHandler : IRequestHandler<AddPaymentCommand, Response<string>>,
                                  IRequestHandler<UpdatePaymentStatusCommand, Response<string>>
    {
        #region Fields
        private readonly IPaymentService _PaymentService;
        #endregion

        #region Constructor
        public PaymentHandler(IPaymentService paymentService)
        {
            _PaymentService = paymentService;
        }
        #endregion

        public async Task<Response<string>> Handle(AddPaymentCommand request, CancellationToken cancellationToken)
        {
            var response = await _PaymentService.AddUserPaymentAsync(request);

            if (response == ResultString.Failure)
            {
                ResponseHandler.BadRequest<string>("Try Again later");
            }

            return ResponseHandler.Success(response);
        }

        public async Task<Response<string>> Handle(UpdatePaymentStatusCommand request, CancellationToken cancellationToken)
        {
            var response = await _PaymentService.UpdatePaymentStatusAsync(request.PaymentId, (int)request.Status);

            if (response == ResultString.NotFound)
            {
                ResponseHandler.NotFound<string>("Payment Not Found");
            }

            return ResponseHandler.Success(response);
        }
    }
}
