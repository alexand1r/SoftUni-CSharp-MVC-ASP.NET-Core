namespace BookShop.Services
{
    using System.Threading.Tasks;
    using BookShop.Services.Models.Author;
    using BookShop.Services.Models.Book;
    using System.Collections.Generic;

    public interface IAuthorService
    {
        Task<int> Create(string firstName, string lastName);

        Task<AuthorDetailsServiceModel> Details(int id);

        Task<IEnumerable<BookWithCategoriesServiceModel>> Books(int authorId);

        Task<bool> Exists(int id);
    }
}
