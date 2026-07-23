using PrimeStore.data.Entities.Identity;
using PrimeStore.data.Helper;
using PrimeStore.data.Results;

namespace PrimeStore.service.Abstracts
{
    public interface IAuthorizationService
    {
        Task<ICollection<Role>> GetRolesListAsync();
        Task<ManageUserRoleResult> GetUserRoles(int UserId);
        Task<Role> GetRoleByIdAsync(int Id);
        Task<string> UpdateUserRoles(UpdateUserRolesResult result);

        Task<ManageUserClaimResult> GetUserClaims(int UserId);
        Task<string> UpdateUserClaims(UpdateUserClaimsResult result);

    }
}
