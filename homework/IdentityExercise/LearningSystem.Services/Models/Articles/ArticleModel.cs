namespace LearningSystem.Services.Models.Articles
{
    using System;

    public class ArticleModel
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime PublishDate { get; set; }

        public string Author { get; set; }
    }
}
