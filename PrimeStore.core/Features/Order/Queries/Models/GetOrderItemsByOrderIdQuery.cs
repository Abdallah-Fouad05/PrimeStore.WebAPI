using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Org.BouncyCastle.Bcpg.OpenPgp;
using PrimeStore.core.Features.Order.Queries.Results;
using PrimeStore.Core.Bases;

namespace PrimeStore.core.Features.Order.Queries.Models
{
    public class GetOrderItemsByOrderIdQuery : IRequest<Response<List<GetOrderItemsByOrderIdResponse>>>
    {
        public int OrderId { get; set; }

        public GetOrderItemsByOrderIdQuery(int orderId)
        {
            OrderId = orderId;
        }
    }
}
