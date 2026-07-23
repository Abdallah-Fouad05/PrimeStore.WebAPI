using MediatR;
using PrimeStore.Core.Bases;
using PrimeStore.data.Results;

namespace PrimeStore.core.Features.Authorization.Commands.Models
{
    public class UpdateUserRolesCommand : UpdateUserRolesResult, IRequest<Response<string>>
    {

    }
}
