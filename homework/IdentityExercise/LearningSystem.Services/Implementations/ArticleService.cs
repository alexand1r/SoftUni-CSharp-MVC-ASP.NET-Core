namespace LearningSystem.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using LearningSystem.Data;
    using LearningSystem.Data.Models;
    using LearningSystem.Services.Models.Articles;

    public class ArticleService : IArticleService
    {
        private readonly LearningSystemDbContext db;

        public ArticleService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<ArticleModel> All()
            => this.db.Articles
                .OrderByDescending(a => a.Id)
                .Select(a => new ArticleModel
                {
                    Author = a.Author.UserName,
                    Content = a.Content,
                    Title = a.Title,
                    PublishDate = a.PublishDate
                })
                .ToList();

        public void Create(string title, string content, DateTime date, string userId)
        {
            var article = new Article
            {
                Title = title,
                Content = content,
                PublishDate = date,
                AuthorId = userId
            };

            this.db.Add(article);
            this.db.SaveChanges();
        }
    }
}
