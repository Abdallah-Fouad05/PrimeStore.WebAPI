using System.Security.Claims;
using System.Text;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PrimeStore.data.Entities.Identity;
using PrimeStore.data.Helper;
using PrimeStore.Data.Helpers;
using PrimeStore.infrastructure.Context;
namespace PrimeStore.Infrustructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddServiceRegistration(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddIdentityCore<User>(option =>
            {

                // Password settings.
                option.Password.RequireDigit = true;
                option.Password.RequireLowercase = true;
                option.Password.RequireNonAlphanumeric = true;
                option.Password.RequireUppercase = true;
                option.Password.RequiredLength = 6;
                option.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                option.Lockout.MaxFailedAccessAttempts = 5;
                option.Lockout.AllowedForNewUsers = true;

                // User settings.
                option.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                option.User.RequireUniqueEmail = true;

                option.SignIn.RequireConfirmedEmail = true;
            })
            .AddRoles<Role>()
            .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();


            //JWT Authentication
            var jwtSettings = new JwtSettings();
            configuration.GetSection(nameof(jwtSettings)).Bind(jwtSettings);
            services.AddSingleton(jwtSettings);

            //Email 
            var emailSettings = new EmailSettings();
            configuration.GetSection(nameof(emailSettings)).Bind(emailSettings);
            services.AddSingleton(emailSettings);

            //Authentication (check Token)
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
         .AddJwtBearer(x =>
         {
             x.RequireHttpsMetadata = false;
             x.SaveToken = true;
             x.TokenValidationParameters = new TokenValidationParameters
             {
                 ValidateIssuer = jwtSettings.ValidateIssuer,
                 ValidIssuers = new[] { jwtSettings.Issuer },
                 ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                 ValidAudience = jwtSettings.Audience,
                 ValidateAudience = jwtSettings.ValidateAudience,
                 ClockSkew = TimeSpan.Zero,
                 ValidateLifetime = jwtSettings.ValidateLifeTime,
                 NameClaimType = ClaimTypes.Name,
                 RoleClaimType = ClaimTypes.Role
             };

             x.Events = new JwtBearerEvents
             {
                 OnMessageReceived = context =>
                 {
                     Console.WriteLine($"Authorization: {context.Request.Headers.Authorization}");
                     Console.WriteLine($"Token: {context.Token}");
                     return Task.CompletedTask;
                 },

                 OnAuthenticationFailed = context =>
                 {
                     Console.WriteLine("Authentication Failed");
                     Console.WriteLine(context.Exception.ToString());
                     return Task.CompletedTask;
                 },

                 OnTokenValidated = context =>
                 {
                     Console.WriteLine("Token Validated");
                     return Task.CompletedTask;
                 }
             };

         }


         );

            //Swagger Gn
            services.AddSwaggerGen(c =>
            {

                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PrimeStore.API", Version = "v1" });
                c.EnableAnnotations();

                c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
            {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            Array.Empty<string>()
            }
           });
            });

            //RateLimit
            services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                options.AddPolicy("AuthLimiter", httpContext =>
                {
                    var ip = httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";

                    return RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: ip,
                        factory: _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 5,
                            Window = TimeSpan.FromMinutes(1),
                            QueueLimit = 0
                        });
                });
            });

            //Poilcy Claim
            services.AddAuthorization(option =>
            {
                option.AddPolicy("CreateProduct", policy =>
                {
                    policy.RequireClaim("CreateProduct", "True");
                });
                option.AddPolicy("DeleteProduct", policy =>
                {
                    policy.RequireClaim("DeleteProduct", "True");
                });
                option.AddPolicy("EditProduct", policy =>
                {
                    policy.RequireClaim("UpdateProduct", "True");
                });
            });

            return services;
        }
    }
}
