using Microsoft.EntityFrameworkCore;
using PrimeStore.data.Entities.Order;
using PrimeStore.data.Entities.Payment;
using PrimeStore.data.Helper;
using PrimeStore.data.Helper.Status;
using PrimeStore.data.Results;
using PrimeStore.infrastructure.Abstracts;
using PrimeStore.service.Abstracts;

namespace PrimeStore.service.Implementations
{
    public class PaymentService : IPaymentService
    {
        #region Fields
        private readonly IOrderRepository _OrderRepository;
        private readonly IOrderItemRepository _OrderItemRepository;
        private readonly IPaymentRepository _PaymentRepository;
        private readonly ICartService _CartService;
        #endregion

        #region Constructor
        public PaymentService(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository, IPaymentRepository paymentRepository, ICartService cartService)
        {
            _OrderRepository = orderRepository;
            _OrderItemRepository = orderItemRepository;
            _CartService = cartService;
            _PaymentRepository = paymentRepository;
        }

        #endregion

        #region Handle
        public async Task<string> AddUserPaymentAsync(PaymentResult payment)
        {
            var trans = await _PaymentRepository.BeginTransactionAsync();
            try
            {
                var NewOrder = new Order
                {
                    UserId = payment.UserId,
                    StatusId = (int)OrderStatusEnum.Processing,
                    CreatedAt = DateTime.UtcNow,
                    TotalPrice = 0
                };

                var Order_Ex = await _OrderRepository.AddAsync(NewOrder);

                var CartItems = await _CartService.GetUserCartItemsAsync(payment.UserId);

                var OrderItems = CartItems.
                    Select(x => new OrderItem
                    {
                        OrderId = Order_Ex.OrderId,
                        ProductId = x.ProductId,
                        Quantity = x.Quantity,
                        Total = x.Total,
                    }).ToList();

                await _OrderItemRepository.AddRangeAsync(OrderItems);

                Order_Ex.TotalPrice = OrderItems.Sum(x => x.Total);

                await _OrderRepository.UpdateAsync(Order_Ex);

                var NewPayment = new Payment
                {
                    OrderId = Order_Ex.OrderId,
                    MethodtId = (int)payment.MethodtId,
                    StatusId = (int)payment.StatusId,
                    Amount = payment.Amount,
                    PaidAt = DateTime.UtcNow,
                    TransactionId = payment.TransactionId
                };

                await _PaymentRepository.AddAsync(NewPayment);

                await trans.CommitAsync();
                return ResultString.Success;
            }
            catch
            {
                await trans.RollbackAsync();
                return ResultString.Failure;
            }
        }

        public async Task<ICollection<Payment>> GetPaymentsByPaymentIdAsync(int OrderId)
        {
            var Payments = await _PaymentRepository.GetTableNoTracking().Include(x => x.Status).Include(x => x.Method).Where(x => x.OrderId == OrderId).ToListAsync();

            return Payments;
        }

        public async Task<string> UpdatePaymentStatusAsync(int PaymentId, int StatusId)
        {
            var payment = await _PaymentRepository.GetTableAsTracking().Where(x => x.PaymentId == PaymentId).FirstOrDefaultAsync();

            if (payment == null)
            {
                return ResultString.NotFound;
            }

            payment.StatusId = StatusId;

            await _PaymentRepository.UpdateAsync(payment);

            return ResultString.Success;

        }
        #endregion
    }
}
