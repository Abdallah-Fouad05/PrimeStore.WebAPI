using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PrimeStore.core;
using PrimeStore.data.Entities.Identity;
using PrimeStore.infrastructure;
using PrimeStore.infrastructure.Authentication;
using PrimeStore.infrastructure.Context;
using PrimeStore.Infrustructure;
using PrimeStore.Infrustructure.Seeder;
using PrimeStore.service;
using SchoolProject.Core.MiddleWare;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//xml
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("PrimeStoreApiCorsPolicy", policy =>
    {
        policy
            .WithOrigins(
                "https://localhost:7217"
            //Front-end origin(Development)
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

#region DI
builder.Services.AddInfrastructureDependencies()
    .AddServiceDependencies().AddCoreDependencies().AddServiceRegistration(builder.Configuration).AddPolicyRegistration();
#endregion

//Connection To SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("dbcontext"));
});


//SeriLog
Log.Logger = new LoggerConfiguration()
              .ReadFrom.Configuration(builder.Configuration).CreateLogger();
builder.Services.AddSerilog();




var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
    await RoleSeeder.SeedAsync(roleManager);
    await UserSeeder.SeedAsync(userManager);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseStaticFiles();

app.UseHttpsRedirection();

//Apply CORS middleware (pipeline)
app.UseCors("PrimeStoreApiCorsPolicy");

app.UseRateLimiter();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
