using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PrimeStore.data.Entities.Identity;
using PrimeStore.data.Helper.Role;
using PrimeStore.data.Helper.Status;

namespace PrimeStore.Infrustructure.Seeder
{
    public static class UserSeeder
    {
        public static async Task SeedAsync(UserManager<User> _userManager)
        {
            var usersCount = await _userManager.Users.CountAsync();
            if (usersCount <= 0)
            {
                var defaultuser = new User()
                {
                    UserName = "admin",
                    Email = "admin@project.com",
                    FullName = "schoolProject",
                    Country = "Egypt",
                    PhoneNumber = "123456",
                    Address = "Egypt",
                    EmailConfirmed = true,
                    StatusId = (int)UserStatusEnum.Active,
                    PhoneNumberConfirmed = true
                };
                await _userManager.CreateAsync(defaultuser, "M123_m");
                await _userManager.AddToRoleAsync(defaultuser, nameof(UserRoleEnum.ADMIN));
            }
        }
    }
}
