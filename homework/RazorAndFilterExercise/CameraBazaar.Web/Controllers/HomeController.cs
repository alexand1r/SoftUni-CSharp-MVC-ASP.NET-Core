namespace CameraBazaar.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using CameraBazaar.Data.Models;
    using CameraBazaar.Services;
    using CameraBazaar.Services.Models;
    using CameraBazaar.Web.Infrastructure;
    using CameraBazaar.Web.Models.Home;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.Data.OData.Query.SemanticAst;
    using Models;

    public class HomeController : Controller
    {
        private readonly IProfileService profiles;
        private readonly ICameraService cameras;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public HomeController(
            IProfileService profiles,
            ICameraService cameras,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.profiles = profiles;
            this.cameras = cameras;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = GlobalConstants.AdministratorRole)]
        public IActionResult All()
        {
            var users = this.profiles.All();

            return this.View(users);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRole)]
        public async Task<IActionResult> Roles(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);

            if (user == null)
                return this.NotFound();

            var roles = await this.userManager.GetRolesAsync(user);

            return this.View(new UserRolesViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Roles = roles
            });
        }

        [Authorize(Roles = GlobalConstants.AdministratorRole)]
        public IActionResult AddToRole(string id)
        {
            var rolesDropDown = this.roleManager
                .Roles
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                })
                .ToList();

            return this.View(rolesDropDown);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToRole(string id, string role)
        {
            var user = await this.userManager.FindByIdAsync(id);
            var roleExists = await this.roleManager.RoleExistsAsync(role);

            if (user == null || !roleExists)
            {
                return this.NotFound();
            }

            await this.userManager.AddToRoleAsync(user, role);

            this.TempData["SuccessMessage"] = $"User {user.Email} added to {role} role!";
            return this.RedirectToAction(nameof(this.All));
        }

        [Authorize(Roles = GlobalConstants.UserRole)]
        public IActionResult Profile(string id)
        {
            var loggedUser = this.profiles.GetUser(this.User.Identity.Name);
            var user = this.profiles.GetUserById(id);
            var profile = new ProfileViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Phone = user.PhoneNumber,
                Username = user.UserName,
                LastLogIn = user.LastLogIn,
                Cameras = this.cameras.AllById(user.Id),
                LoggedUserId = loggedUser.Id
            };

            return this.View(profile);
        }
        
        [Authorize(Roles = GlobalConstants.UserRole)]
        public IActionResult Edit(string id)
        {
            var user = this.profiles.GetUserById(id);

            return this.View(new ProfileFormViewModel
            {
                Id = id,
                Email = user.Email,
                Password = user.PasswordHash,
                Phone = user.PhoneNumber,
                LastLogIn = user.LastLogIn
            });
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.UserRole)]
        public IActionResult Edit(string id, ProfileFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            this.profiles.Edit(id, model.Email, model.Password, model.Phone);

            return this.RedirectToAction(nameof(this.Profile));
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
