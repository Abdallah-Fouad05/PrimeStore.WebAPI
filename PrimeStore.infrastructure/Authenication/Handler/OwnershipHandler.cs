namespace PrimeStore.infrastructure.Authenication.Handler
{
    using Microsoft.AspNetCore.Authorization;
    using PrimeStore.infrastructure.Authenication.Requirement;
    using PrimeStore.Service.Implementations;

    public class OwnershipHandler : AuthorizationHandler<OwnershipRequirement, int>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            OwnershipRequirement requirement,
            int resource)
        {

            var userId = context.User.FindFirst(nameof(UserClaimModel.Id))?.Value;

            if (userId != null && int.Parse(userId) == resource)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
