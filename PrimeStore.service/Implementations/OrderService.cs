using Microsoft.EntityFrameworkCore;
using PrimeStore.data.Entities.Order;
using PrimeStore.data.Helper;
using PrimeStore.infrastructure.Abstracts;
using PrimeStore.service.Abstracts;

namespace PrimeStore.service.Implementations
{
    public class OrderService : IOrderService
    {
        #region Fields
        private readonly IOrderRepository _OrderRepository;
        private readonly IOrderItemRepository _OrderItemRepository;
        #endregion

        #region Constructor
        public OrderService(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository)
        {
            _OrderRepository = orderRepository;
            _OrderItemRepository = orderItemRepository;
        }
        #endregion

        #region Handle

        public async Task<ICollection<OrderItem>> GetOrderItemsByOrderId(int OrderId)
        {
            return await _OrderItemRepository.GetTableNoTracking()
                .Include(x => x.Product).ThenInclude(x => x.Brand)
                .Include(x => x.Product).ThenInclude(x => x.Category)
                .Where(x => x.OrderId == OrderId).ToListAsync();
        }

        public async Task<ICollection<Order>> GetUserOrdersByUserId(int UserId, int StatusId = 0)
        {
            var Query = _OrderRepository.GetTableNoTracking()
                .Where(x => x.UserId == UserId).AsQueryable();

            if (StatusId <= 0)
            {
                Query = Query.Where(x => x.StatusId == StatusId);
            }

            return await Query.ToListAsync();
        }

        public async Task<string> UpdateOrderStatus(int OrderId, int StatusId)
        {
            var Order_EX = await _OrderRepository.GetTableAsTracking().Where(x => x.OrderId == OrderId).FirstOrDefaultAsync();

            if (Order_EX == null)
            {
                return ResultString.NotFound;
            }

            Order_EX.StatusId = StatusId;

            await _OrderRepository.UpdateAsync(Order_EX);
            return ResultString.Success;
        }

        #endregion

    }
}
