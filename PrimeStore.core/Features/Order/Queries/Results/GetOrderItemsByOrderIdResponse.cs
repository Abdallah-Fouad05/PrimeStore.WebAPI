using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeStore.core.Features.Products.Queries.Results;
using PrimeStore.data.Entities;

namespace PrimeStore.core.Features.Order.Queries.Results
{
    public class GetOrderItemsByOrderIdResponse
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public GetProductListResponse Product { get; set; }
        public int Quantity { get; set; }
        public float Total { get; set; }
    }
}
