using AutoMapper;
using PrimeStore.core.Features.Order.Queries.Results;
using PrimeStore.data.Entities.Order;

public class OrderMapping : Profile
{
    public OrderMapping()
    {
        CreateMap<OrderItem, GetOrderItemsByOrderIdResponse>()
            .ForMember(dest => dest.Product,
                opt => opt.MapFrom(src => src.Product));

        CreateMap<Order, GetUserOrdersByUserIdResponse>()
            .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src => src.Status));

        CreateMap<OrderStatus, OrderStatusResponse>()
              .ForMember(dest => dest.StatusId,
                opt => opt.MapFrom(src => src.StatusId))
          .ForMember(dest => dest.StatusName,
                opt => opt.MapFrom(src => src.StatusName));

    }
}