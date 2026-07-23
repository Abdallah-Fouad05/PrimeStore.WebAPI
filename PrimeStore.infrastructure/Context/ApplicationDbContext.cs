using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PrimeStore.data.Entities;
using PrimeStore.data.Entities.Identity;
using PrimeStore.data.Entities.Order;
using PrimeStore.data.Entities.Payment;
using PrimeStore.data.Entities.Status;

namespace PrimeStore.infrastructure.Context
{
    public class ApplicationDbContext : IdentityDbContext<data.Entities.Identity.User, Role, int, IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<UserStatus> UserStatuses { get; set; }
        public DbSet<GenericStatus> GenericStatuses { get; set; }

        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<PaymentStatus> PaymentStatuses { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderStatus>().HasData
                (
                    new OrderStatus { StatusId = 1, StatusName = "Pending" },
                    new OrderStatus { StatusId = 2, StatusName = "Confirmed" },
                    new OrderStatus { StatusId = 3, StatusName = "Processing" },
                    new OrderStatus { StatusId = 4, StatusName = "Shipped" },
                    new OrderStatus { StatusId = 5, StatusName = "Delivered" },
                    new OrderStatus { StatusId = 6, StatusName = "Cancelled" },
                    new OrderStatus { StatusId = 7, StatusName = "Returned" }
                );

            modelBuilder.Entity<PaymentMethod>().HasData
                (
                    new PaymentMethod { MethodId = 1, MethodString = "Cash on Delivery" },
                    new PaymentMethod { MethodId = 2, MethodString = "Credit Card" },
                    new PaymentMethod { MethodId = 3, MethodString = "PayPal" },
                    new PaymentMethod { MethodId = 4, MethodString = "Bank Transfer" }
                );

            modelBuilder.Entity<PaymentStatus>().HasData
                (
                    new PaymentStatus { StatusId = 1, StatusName = "Pending" },
                    new PaymentStatus { StatusId = 2, StatusName = "Paid" },
                    new PaymentStatus { StatusId = 3, StatusName = "Failed" },
                    new PaymentStatus { StatusId = 4, StatusName = "Refunded" }
                );

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
