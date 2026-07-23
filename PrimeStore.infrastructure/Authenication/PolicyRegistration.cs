using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using PrimeStore.infrastructure.Authenication.Handler;
using PrimeStore.infrastructure.Authenication.Requirement;

namespace PrimeStore.infrastructure.Authentication
{
    public static class PolicyRegistration
    {
        public static IServiceCollection AddPolicyRegistration(
            this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("OwnerPolicy", policy =>
                {
                    policy.Requirements.Add(new OwnershipRequirement());
                });

            });

            services.AddScoped<IAuthorizationHandler, OwnershipHandler>();

            return services;
        }
    }
}