using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PrimeStore.data.Entities.Identity;
using PrimeStore.data.Helper;
using PrimeStore.data.Results;
using PrimeStore.infrastructure.Context;
using IAuthorizationService = PrimeStore.service.Abstracts.IAuthorizationService;

namespace PrimeStore.service.Implementations
{
    public class AuthorizationService : IAuthorizationService
    {
        #region Fields
        private readonly RoleManager<Role> _RoleManager;
        private readonly UserManager<User> _UserManager;
        private readonly ApplicationDbContext _DbContext;
        #endregion

        #region Constructor
        public AuthorizationService(RoleManager<Role> roleManager, UserManager<User> userManager, ApplicationDbContext applicationDbContext)
        {
            _RoleManager = roleManager;
            _UserManager = userManager;
            _DbContext = applicationDbContext;
        }

        public async Task<ICollection<Role>> GetRolesListAsync()
        {
            return await _RoleManager.Roles.ToListAsync();
        }

        public async Task<ManageUserRoleResult> GetUserRoles(int UserId)
        {

            ManageUserRoleResult response = new ManageUserRoleResult();

            var user = await _UserManager.FindByIdAsync(UserId.ToString());

            if (user == null)
            {
                return null;
            }

            var roles = await _RoleManager.Roles.ToListAsync();

            response.UserId = UserId;

            foreach (var role in roles)
            {
                UserRoles userRoles = new UserRoles { Id = role.Id, Name = role.Name };

                if (await _UserManager.IsInRoleAsync(user, role.Name))
                {
                    userRoles.HasRole = true;
                }
                else
                {
                    userRoles.HasRole = false;
                }
                response.Roles.Add(userRoles);
            }

            return response;
        }
        public async Task<Role> GetRoleByIdAsync(int Id)
        {
            return await _RoleManager.FindByIdAsync(Id.ToString());
        }

        public async Task<string> UpdateUserRoles(UpdateUserRolesResult request)
        {
            var transact = await _DbContext.Database.BeginTransactionAsync();
            try
            {
                ManageUserRoleResult response = new ManageUserRoleResult();

                var user = await _UserManager.FindByIdAsync(request.UserId.ToString());

                if (user == null)
                {
                    return "UserNotFound";
                }

                //reomove old roles
                var rolesList = await _UserManager.GetRolesAsync(user);

                var removeOldRoles = await _UserManager.RemoveFromRolesAsync(user, rolesList);

                if (!removeOldRoles.Succeeded)
                {
                    return "FailedToRemoveOldRoles";
                }

                var selectedRoles = request.Roles.Where(x => x.HasRole == true).Select(x => x.Name);

                //Add the Roles HasRole=True
                var addRolesresult = await _UserManager.AddToRolesAsync(user, selectedRoles);

                if (!addRolesresult.Succeeded)

                    return "FailedToAddNewRoles";

                transact.Commit();

                return ResultString.Success;
            }
            catch
            {
                transact.Rollback();
                return ResultString.Failure;
            }
        }

        public async Task<ManageUserClaimResult> GetUserClaims(int UserId)
        {

            ManageUserClaimResult response = new();

            var user = await _UserManager.FindByIdAsync(UserId.ToString());

            if (user == null)
            {
                return null;
            }

            var Userclaims = await _UserManager.GetClaimsAsync(user);

            response.UserId = UserId;

            foreach (var claim in ClaimsStore.claims)
            {
                UserClaim Claim = new();

                Claim.Type = claim.Type;

                if (Userclaims.Any(x => x.Type == claim.Type))
                {
                    Claim.Value = true;
                }

                response.Claims.Add(Claim);
            }

            return response;
        }

        public async Task<string> UpdateUserClaims(UpdateUserClaimsResult request)
        {
            var transact = await _DbContext.Database.BeginTransactionAsync();
            try
            {
                ManageUserRoleResult response = new ManageUserRoleResult();

                var user = await _UserManager.FindByIdAsync(request.UserId.ToString());

                if (user == null)
                {
                    return "UserNotFound";
                }

                //reomove old claims
                var claimsList = await _UserManager.GetClaimsAsync(user);

                var removeOldClaims = await _UserManager.RemoveClaimsAsync(user, claimsList);

                if (!removeOldClaims.Succeeded)
                {
                    return "FailedToRemoveOldClaims";
                }

                var selectedClaims = request.Claims.Where(x => x.Value == true).Select(x => new Claim(x.Type,x.Value.ToString()));

                //Add the Roles HasRole=True
                var addClaimsResult = await _UserManager.AddClaimsAsync(user,selectedClaims);

                if (!addClaimsResult.Succeeded)

                    return "FailedToAddNewClaims";

                transact.Commit();

                return ResultString.Success;
            }
            catch
            {
                transact.Rollback();
                return ResultString.Failure;
            }
        }


        #endregion

    }
}
