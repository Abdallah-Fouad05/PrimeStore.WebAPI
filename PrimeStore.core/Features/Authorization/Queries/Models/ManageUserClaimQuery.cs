using MediatR;
using PrimeStore.Core.Bases;
using PrimeStore.data.Results;

namespace PrimeStore.core.Features.Authorization.Queries.Models
{
    public class ManageUserClaimQuery : IRequest<Response<ManageUserClaimResult>>
    {
        public int UserId { get; set; }

        public ManageUserClaimQuery(int userId)
        {
            UserId = userId;
        }
    }
}
