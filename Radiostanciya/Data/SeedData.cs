using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Radiostanciya.Models;
using System;
using System.Threading.Tasks;

namespace Radiostanciya.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            var roleNames = new string[] { "Admin", "User" };

            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);

                if (!roleExist)
                {
                    // Создаем роль
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Проверяем, существует ли пользователь с именем "admin"
            var adminUser = await userManager.FindByNameAsync("admin");

            if (adminUser == null)
            {
                var admin = new User
                {
                    UserName = "admin",
                };

                // Устанавливаем хеш пароля для пользователя
                var passwordHash = userManager.PasswordHasher.HashPassword(admin, "admin");

                // Устанавливаем хеш пароля для пользователя
                admin.PasswordHash = passwordHash;

                var createAdmin = await userManager.CreateAsync(admin);

                if (createAdmin.Succeeded)
                {
                    // Присваиваем роль "Admin" пользователю
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}
