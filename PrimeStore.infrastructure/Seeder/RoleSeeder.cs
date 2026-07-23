using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PrimeStore.data.Entities.Identity;


namespace PrimeStore.Infrustructure.Seeder
{
    public static class RoleSeeder
    {
        public static async Task SeedAsync(RoleManager<Role> _roleManager)
        {
            var rolesCount = await _roleManager.Roles.CountAsync();
            if (rolesCount <= 0)
            {
                await _roleManager.CreateAsync(new Role()
                {
                    Name = "OWNER"
                });

                await _roleManager.CreateAsync(new Role()
                {
                    Name = "ADMIN"
                });
                await _roleManager.CreateAsync(new Role()
                {
                    Name = "USER"
                });
            }
        }

    }
}
