namespace BookShop.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models.Book;

    public interface IBookService
    {
        Task<int> Create(
            string title,
            string description,
            decimal price,
            int copies,
            int? edition,
            int? ageRestriction,
            DateTime releaseDate,
            int authorId,
            string categories);

        Task<int> Edit(
            int id,
            string title,
            string description,
            decimal price,
            int copies,
            int? edition,
            int? ageRestriction,
            DateTime releaseDate,
            int authorId,
            string categories);

        Task<string> Delete(int id);

        Task<IEnumerable<BookListingServiceModel>> All(string searchText);

        Task<BookDetailsServiceModel> Details(int id);

        Task<bool> Exists(int id);
    }
}
