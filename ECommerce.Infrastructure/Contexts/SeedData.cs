using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Contexts
{
    using Microsoft.AspNetCore.Identity;

    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = { "Admin", "User" };

            foreach (var roleName in roleNames)
            {
                var roleExists = await roleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
            var user = await userManager.FindByEmailAsync("admin@example.com");

            if (user == null)
            {
                user = new IdentityUser()
                {
                    UserName = "admin@example.com",
                    Email = "admin@example.com",
                };
                await userManager.CreateAsync(user, "Admin@123");
            }
            await userManager.AddToRoleAsync(user, "Admin");
        }
    }

}
