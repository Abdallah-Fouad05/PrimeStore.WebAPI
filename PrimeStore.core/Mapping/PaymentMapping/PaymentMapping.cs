using AutoMapper;
using PrimeStore.core.Features.Payments.Queries.Result;
using PrimeStore.data.Entities.Payment;

public class PaymentMapping : Profile
{
    public PaymentMapping()
    {
        CreateMap<Payment, GetPaymentByOrderIdResponse>()
            .ForMember(dest => dest.Method,
                opt => opt.MapFrom(src => src.Method))
            .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src => src.Status));

        CreateMap<PaymentStatus, PaymentStatusResponse>()
            .ForMember(dest => dest.StatusId,
                opt => opt.MapFrom(src => src.StatusId))
            .ForMember(dest => dest.StatusName,
                opt => opt.MapFrom(src => src.StatusName));

        CreateMap<PaymentMethod, PaymentMethodResponse>()
            .ForMember(dest => dest.MethodId,
                opt => opt.MapFrom(src => src.MethodId))
            .ForMember(dest => dest.MethodName,
                opt => opt.MapFrom(src => src.MethodString));
    }
}