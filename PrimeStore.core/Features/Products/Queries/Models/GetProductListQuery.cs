using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using PrimeStore.core.Features.Products.Queries.Results;
using PrimeStore.data.Entities;
using PrimeStore.Core.Bases;

namespace PrimeStore.core.Features.Products.Queries.Models
{
    public class GetProductListQuery : IRequest<Response<List<GetProductListResponse>>>
    {
    
    }
}
