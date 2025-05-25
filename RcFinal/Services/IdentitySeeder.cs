using Microsoft.AspNetCore.Identity;
using RcFinal.Data;
using RcFinal.Models;

namespace RcFinal.Services
{
    public static class IdentitySeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var settings = configuration.GetSection("IdentitySeed").Get<IdentitySeedSettings>();

            if (settings == null)
            {
                throw new InvalidOperationException("IdentitySeed section is missing in appsettings.json.");
            }

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            foreach (var role in settings.Roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var adminEmail = settings.AdminUser.Email;
            var adminPassword = settings.AdminUser.Password;

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
                else
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new Exception($"Failed to create admin user: {errors}");
                }
            }
        }
    }
}
