namespace News.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using News.Services.Models;

    public interface INewsService
    {
        Task<int> Create(
            string title,
            string content,
            DateTime publishDate);

        Task<int> Edit(
            int id,
            string title,
            string content,
            DateTime publishDate);

        Task<string> Delete(int id);

        Task<bool> Exists(int id);

        Task<IEnumerable<NewsListingServiceModel>> AllAsync();
    }
}
