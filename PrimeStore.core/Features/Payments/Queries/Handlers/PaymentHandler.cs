using AutoMapper;
using MediatR;
using PrimeStore.core.Features.Payments.Queries.Models;
using PrimeStore.core.Features.Payments.Queries.Result;
using PrimeStore.Core.Bases;
using PrimeStore.service.Abstracts;

namespace PrimeStore.core.Features.Payments.Queries.Handlers
{
    public class PaymentHandler : IRequestHandler<GetPaymentByOrderIdQuery, Response<List<GetPaymentByOrderIdResponse>>>
    {
        #region Fields
        private readonly IPaymentService _PaymentService;
        private readonly IMapper _Mapper;
        #endregion

        #region Constructor
        public PaymentHandler(IPaymentService paymentService, IMapper mapper)
        {
            _PaymentService = paymentService;
            _Mapper = mapper;
        }
        #endregion

        public async Task<Response<List<GetPaymentByOrderIdResponse>>> Handle(GetPaymentByOrderIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _PaymentService.GetPaymentsByPaymentIdAsync(request.OrderId);

            var Payments = _Mapper.Map<List<GetPaymentByOrderIdResponse>>(response);

            return ResponseHandler.Success(Payments);
        }
    }
}
