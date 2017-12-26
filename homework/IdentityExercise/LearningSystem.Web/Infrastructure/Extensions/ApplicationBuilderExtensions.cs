namespace LearningSystem.Web.Infrastructure.Extensions
{
    using System.Threading.Tasks;
    using LearningSystem.Data;
    using LearningSystem.Data.Models;
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
                serviceScope.ServiceProvider.GetRequiredService<LearningSystemDbContext>().Database.Migrate();

                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();

                Task.Run(async () =>
                {
                    // CREATE ADMINISTRATOR ROLE
                    var roleAdmin = GlobalConstants.AdministratorRole;

                    var roleAdminExists = await roleManager.RoleExistsAsync(roleAdmin);

                    if (!roleAdminExists)
                    {
                        await roleManager.CreateAsync(new IdentityRole
                        {
                            Name = roleAdmin
                        });
                    }

                    // CREATE STUDENT ROLE
                    var roleStudent = GlobalConstants.StudentRole;

                    var roleStudentExists = await roleManager.RoleExistsAsync(roleStudent);

                    if (!roleStudentExists)
                    {
                        await roleManager.CreateAsync(new IdentityRole
                        {
                            Name = roleStudent
                        });
                    }

                    // CREATE BLOG AUTHOR ROLE
                    var roleBlogAuthor = GlobalConstants.BlogAuthorRole;

                    var roleBlogAuthorExists = await roleManager.RoleExistsAsync(roleBlogAuthor);

                    if (!roleBlogAuthorExists)
                    {
                        await roleManager.CreateAsync(new IdentityRole
                        {
                            Name = roleBlogAuthor
                        });
                    }

                    // CREATE TRAINER ROLE
                    var roleTrainer = GlobalConstants.TrainerRole;

                    var roleTrainerExists = await roleManager.RoleExistsAsync(roleTrainer);

                    if (!roleTrainerExists)
                    {
                        await roleManager.CreateAsync(new IdentityRole
                        {
                            Name = roleTrainer
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

                        await userManager.AddToRoleAsync(adminUser, roleAdmin);
                    }

                    // CREATE TRAINER
                    var trainerEmail = "trainer@mysite.com";

                    var trainerUser = await userManager.FindByNameAsync(trainerEmail);

                    if (trainerUser == null)
                    {
                        trainerUser = new User
                        {
                            Email = trainerEmail,
                            UserName = trainerEmail
                        };

                        await userManager.CreateAsync(trainerUser, "trainer12");

                        await userManager.AddToRoleAsync(trainerUser, roleTrainer);
                    }

                    // CREATE BLOG AUTHOR
                    var authorEmail = "author@mysite.com";

                    var authorUser = await userManager.FindByNameAsync(authorEmail);

                    if (authorUser == null)
                    {
                        authorUser = new User
                        {
                            Email = authorEmail,
                            UserName = authorEmail
                        };

                        await userManager.CreateAsync(authorUser, "author12");

                        await userManager.AddToRoleAsync(authorUser, roleBlogAuthor);
                    }
                })
                .Wait();
            }

            return app;
        }
    }
}
