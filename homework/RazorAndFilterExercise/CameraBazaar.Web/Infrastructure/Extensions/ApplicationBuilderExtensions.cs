namespace CameraBazaar.Web.Infrastructure.Extensions
{
    using System.Threading.Tasks;
    using CameraBazaar.Data.Models;
    using Data;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<CameraBazaarDbContext>().Database.Migrate();

                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();

                Task.Run(async () =>
                    {
                        // CREATE ADMINISTRATOR ROLE
                        var roleName = GlobalConstants.AdministratorRole;

                        var roleExists = await roleManager.RoleExistsAsync(roleName);

                        if (!roleExists)
                        {
                            await roleManager.CreateAsync(new IdentityRole
                            {
                                Name = roleName
                            });
                        }

                        // CREATE USER ROLE
                        var roleRestrictedUser = GlobalConstants.RestrictedUserRole;

                        var roleRestrictedUserExists = await roleManager.RoleExistsAsync(roleRestrictedUser);

                        if (!roleRestrictedUserExists)
                        {
                            await roleManager.CreateAsync(new IdentityRole
                            {
                                Name = roleRestrictedUser
                            });
                        }

                        // CREATE RESTRICTED USER ROLE
                        var roleUser = GlobalConstants.UserRole;

                        var roleUserExists = await roleManager.RoleExistsAsync(roleUser);

                        if (!roleUserExists)
                        {
                            await roleManager.CreateAsync(new IdentityRole
                            {
                                Name = roleUser
                            });
                        }

                        // CREATE ADMIN
                        var email = "admin@mysite.com";

                        var adminUser = await userManager.FindByNameAsync(email);

                        if (adminUser == null)
                        {
                            adminUser = new User
                            {
                                Email = email,
                                UserName = email
                            };

                            await userManager.CreateAsync(adminUser, "admin12");

                            await userManager.AddToRoleAsync(adminUser, roleName);
                        }
                    })
                    .Wait();
            }

            return app;
        }
    }
}
