namespace LearningSystem.Services
{
    using System;
    using System.Collections.Generic;
    using LearningSystem.Services.Models.Articles;

    public interface IArticleService
    {
        IEnumerable<ArticleModel> All();

        void Create(string title, string content, DateTime date, string userId);
    }
}
