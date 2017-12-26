namespace LearningSystem.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using LearningSystem.Data.Models;
    using LearningSystem.Services;
    using LearningSystem.Web.Infrastructure;
    using LearningSystem.Web.Infrastructure.Extensions;
    using LearningSystem.Web.Models.Admin;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    [Authorize(Roles = GlobalConstants.AdministratorRole)]
    public class AdminController : Controller
    {
        private readonly IAdminService admins;
        private readonly UserManager<User> userManager;
        private readonly ICourseService courses;
        private readonly RoleManager<IdentityRole> roleManager;

        public AdminController(
            IAdminService admins,
            UserManager<User> userManager,
            ICourseService courses,
            RoleManager<IdentityRole> roleManager)
        {
            this.admins = admins;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.courses = courses;
        }

        public IActionResult Create()
        {
            return this.View(new CourseFormViewModel
            {
                Trainers = this.GetTrainers()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CourseFormViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.Trainers = this.GetTrainers();
                return this.View(model);
            }

            this.courses.Create(
                model.Name,
                model.Description,
                model.TrainerId,
                model.StartDate,
                model.EndDate);

            return this.RedirectToAction("Index", "Home");
        }

        private IEnumerable<SelectListItem> GetTrainers()
        {
            var trainers = this.userManager.GetUsersInRoleAsync(GlobalConstants.TrainerRole).Result;
            return trainers.Select(t => new SelectListItem
                {
                    Text = t.UserName,
                    Value = t.Id
                })
                .ToList();
        }

        public async Task<IActionResult> All()
        {
            var users = await this.admins.AllAsync();
            var roles = await this.roleManager
                .Roles
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                })
                .ToListAsync();

            return this.View(new AdminUserListingViewModel
            {
                Roles = roles,
                Users = users
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddToRole(AddUserToRoleFormModel model)
        {
            var roleExists = await this.roleManager.RoleExistsAsync(model.Role);
            var user = await this.userManager.FindByIdAsync(model.UserId);
            var userExists = user != null;

            if (!roleExists || !userExists)
            {
                this.ModelState.AddModelError(string.Empty, "Invalid identity details.");
            }

            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(this.All));
            }

            await this.userManager.AddToRoleAsync(user, model.Role);

            this.TempData.AddSuccessMessage($"User {user.UserName} has been successfully added to {model.Role} role.");
            return this.RedirectToAction(nameof(this.All));
        }
    }
}
