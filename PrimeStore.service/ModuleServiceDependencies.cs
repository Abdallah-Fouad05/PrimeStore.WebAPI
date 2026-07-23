using Microsoft.Extensions.DependencyInjection;
using PrimeStore.service.Abstracts;
using PrimeStore.service.Implementations;
using PrimeStore.Service.Abstracts;
using PrimeStore.Service.AuthServices.Implementations;
using PrimeStore.Service.AuthServices.Interfaces;
using PrimeStore.Service.Implementations;
using SchoolProject.Service.Implementations;

namespace PrimeStore.service
{
    public static class ModuleServiceDependencies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IBrandService, BrandService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IReviewService, ReviewService>();
            services.AddTransient<IWishlistService, WishlistService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IAuthorizationService, AuthorizationService>();
            services.AddTransient<ICurrentUserService, CurrentUserService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICartService, CartServices>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddHttpContextAccessor();

            return services;
        }

    }
}
