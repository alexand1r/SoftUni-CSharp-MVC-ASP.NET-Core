namespace LearningSystem.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using LearningSystem.Data.Models;
    using LearningSystem.Services;
    using LearningSystem.Web.Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.TrainerRole)]
    public class TrainerController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly ICourseService courses;

        public TrainerController(
            UserManager<User> userManager,
            ICourseService courses)
        {
            this.userManager = userManager;
            this.courses = courses;
        }

        public IActionResult TrainerProfile()
        {
            var userId =  this.userManager.GetUserId(this.User);
            
            return this.View(this.courses.AllByTrainer(userId));
        }
        
    }
}
