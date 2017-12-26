namespace LearningSystem.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using LearningSystem.Services;
    using Microsoft.AspNetCore.Mvc;
    using LearningSystem.Web.Models;
    using Microsoft.AspNetCore.Authorization;

    public class HomeController : Controller
    {
        private readonly ICourseService courses;
        private readonly IArticleService articles;

        public HomeController(
            ICourseService courses,
            IArticleService artcles)
        {
            this.courses = courses;
            this.articles = artcles;
        }

        public IActionResult Index()
        {
            var allCourses = this.courses.All();

            if (!allCourses.Any())
            {
                return this.View();
            }

            return this.View(allCourses);
        }

        [Authorize]
        public IActionResult Blog() => this.View(this.articles.All());
        
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
