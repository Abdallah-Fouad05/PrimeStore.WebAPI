using Microsoft.Extensions.DependencyInjection;
using PrimeStore.infrastructure.Abstracts;
using PrimeStore.infrastructure.InfrastructureBases;
using PrimeStore.infrastructure.Repositories;
using PrimeStore.Infrustructure.Abstracts;
using SchoolProject.Infrustructure.Repositories;

namespace PrimeStore.infrastructure
{
    public static class ModuleInfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IBrandRepository, BrandRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IProductAttributeRepository, ProductAttributeRepository>();
            services.AddTransient<IProductImageRepository, ProductImageRepository>();
            services.AddTransient<IReviewRepository, ReviewRepository>();
            services.AddTransient<IWishlistRepository, WishlistRepository>();
            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddTransient<IImageRepository, ImageRepository>();
            services.AddTransient<ICartRepository, CartRepository>();
            services.AddTransient<ICartItemRepository, CartItemRepository>();
            services.AddTransient<IPaymentRepository, PaymentRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderItemRepository, OrderItemRepository>();
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            return services;
        }
    }
}