namespace News.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using News.Data;
    using News.Data.Models;
    using News.Services.Models;

    public class NewsService : INewsService
    {
        private readonly NewsDbContext db;

        public NewsService(NewsDbContext db)
        {
            this.db = db;
        }

        public async Task<int> Create(string title, string content, DateTime publishDate)
        {
            var news = new News
            {
                Title = title,
                Content = content,
                PublishDate = publishDate
            };

            this.db.Add(news);
            await this.db.SaveChangesAsync();

            return news.Id;
        }

        public async Task<int> Edit(int id, string title, string content, DateTime publishDate)
        {
            var news = await this.db.News.FindAsync(id);

            news.Title = title;
            news.Content = content;
            news.PublishDate = publishDate;

            await this.db.SaveChangesAsync();

            return news.Id;
        }

        public async Task<string> Delete(int id)
        {
            var news = this.db.News.Find(id);

            this.db.News.Remove(news);
            await this.db.SaveChangesAsync();

            return $"News with id {id} was successufully deleted.";
        }

        public async Task<bool> Exists(int id)
            => await this.db.News.AnyAsync(b => b.Id == id);

        public async Task<IEnumerable<NewsListingServiceModel>> AllAsync()
            => await this.db
                .News
                .OrderByDescending(n => n.Id)
                .ProjectTo<NewsListingServiceModel>()
                .ToListAsync();
    }
}
