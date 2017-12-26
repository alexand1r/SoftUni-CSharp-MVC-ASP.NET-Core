namespace BookShop.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BookShop.Services.Models.Category;

    public interface ICategoryService
    {
        Task<int> Create(string name);

        Task<int> Edit(int id, string name);

        Task<string> Delete(int id);

        Task<IEnumerable<CategoryListingServiceModel>> All();

        Task<CategoryDetailsServiceModel> Details(int id);

        Task<bool> Exists(int id);
    }
}
