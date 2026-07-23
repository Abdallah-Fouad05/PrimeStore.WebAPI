using MediatR;
using PrimeStore.Core.Bases;
using PrimeStore.data.Results;

namespace PrimeStore.core.Features.Authorization.Queries.Models
{
    public class ManageUserRoleQuery : IRequest<Response<ManageUserRoleResult>>
    {
        public int UserId { get; set; }

        public ManageUserRoleQuery(int userId)
        {
            UserId = userId;
        }
    }
}
