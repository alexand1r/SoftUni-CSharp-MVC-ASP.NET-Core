namespace LearningSystem.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using LearningSystem.Data.Models;
    using LearningSystem.Services;
    using LearningSystem.Services.Models.Articles;
    using LearningSystem.Web.Infrastructure;
    using LearningSystem.Web.Models.Authors;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.BlogAuthorRole)]
    public class AuthorsController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IArticleService articles;

        public AuthorsController(
            UserManager<User> userManager,
            IArticleService articles)
        {
            this.userManager = userManager;
            this.articles = articles;
        }

        public IActionResult AddArticle() => this.View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddArticle(ArticleFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var username = this.User.Identity.Name;
            var user = await this.userManager.FindByNameAsync(username);

            this.articles.Create(
                model.Title,
                model.Content,
                DateTime.Now,
                user.Id);

            return this.RedirectToAction("Blog", "Home");
        }
    }
}
