using PrimeStore.data.Entities.Order;

namespace PrimeStore.service.Abstracts
{
    public interface IOrderService
    {
        Task<ICollection<Order>> GetUserOrdersByUserId(int UserId, int StatusId = 0);
        Task<ICollection<OrderItem>> GetOrderItemsByOrderId(int OrderId);
        Task<string> UpdateOrderStatus(int OrderId, int StatusId);
    }
}
