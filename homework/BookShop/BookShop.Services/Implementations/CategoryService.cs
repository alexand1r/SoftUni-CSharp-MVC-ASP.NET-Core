namespace BookShop.Services.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper.QueryableExtensions;
    using BookShop.Data;
    using BookShop.Data.Models;
    using BookShop.Services.Models.Category;
    using Microsoft.EntityFrameworkCore;

    public class CategoryService : ICategoryService
    {
        private readonly BookShopDbContext db;

        public CategoryService(BookShopDbContext db)
        {
            this.db = db;
        }

        public async Task<int> Create(string name)
        {
            var categoryExists = await this.db
                .Categories
                .AnyAsync(c => c.Name == name);

            if (categoryExists) return -1;

            var category = new Category
            {
                Name = name
            };

            this.db.Add(category);
            await this.db.SaveChangesAsync();

            return category.Id;
        }

        public async Task<int> Edit(int id, string name)
        {
            var category = await this.db
                .Categories
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null) return -1;

            category.Name = name;
            
            await this.db.SaveChangesAsync();

            return category.Id;
        }

        public async Task<string> Delete(int id)
        {
            var category = this.db.Categories.Find(id);

            this.db.Categories.Remove(category);
            await this.db.SaveChangesAsync();

            return $"Category with id {id} was successufully deleted.";
        }

        public async Task<IEnumerable<CategoryListingServiceModel>> All()
            => await this.db
                .Categories
                .OrderBy(c => c.Name)
                .ProjectTo<CategoryListingServiceModel>()
                .ToListAsync();

        public async Task<CategoryDetailsServiceModel> Details(int id)
            => await this.db
                .Categories
                .Where(c => c.Id == id)
                .ProjectTo<CategoryDetailsServiceModel>()
                .FirstOrDefaultAsync();

        public async Task<bool> Exists(int id)
            => await this.db.Categories.AnyAsync(b => b.Id == id);
    }
}
