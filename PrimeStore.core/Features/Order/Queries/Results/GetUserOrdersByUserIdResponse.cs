namespace PrimeStore.core.Features.Order.Queries.Results
{
    public class OrderStatusResponse
    {
        public int StatusId { get; set; }

        public string StatusName { get; set; }
    }
    public class GetUserOrdersByUserIdResponse
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public float TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public OrderStatusResponse Status { get; set; }
    }
}
