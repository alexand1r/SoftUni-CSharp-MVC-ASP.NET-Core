namespace BookShop.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data;
    using Models.Book;
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using BookShop.Common.Extensions;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class BookService : IBookService
    {
        private readonly BookShopDbContext db;

        public BookService(BookShopDbContext db)
        {
            this.db = db;
        }

        public async Task<int> Create(
            string title,
            string description,
            decimal price,
            int copies, 
            int? edition, 
            int? ageRestriction,
            DateTime releaseDate, 
            int authorId, 
            string categories)
        {
            var categoryNames = categories
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .ToHashSet();
            
            var existingCategories = await this.db
                .Categories
                .Where(c => categoryNames.Contains(c.Name))
                .ToListAsync();

            var allCategories = new List<Category>(existingCategories);

            foreach (var categoryName in categoryNames)
            {
                if (existingCategories.All(c => c.Name != categoryName))
                {
                    var category = new Category
                    {
                        Name = categoryName
                    };

                    this.db.Add(category);
                    
                    allCategories.Add(category);
                }
            }

            var book = new Book
            {
                Title = title,
                Description = description,
                Price = price,
                Copies = copies,
                Edition = edition,
                AgeRestriction = ageRestriction,
                ReleaseDate = releaseDate,
                AuthorId = authorId
            };

            allCategories.ForEach(c => book.Categories.Add(new BookCategory
            {
                CategoryId = c.Id
            }));

            this.db.Add(book);
            await this.db.SaveChangesAsync();

            return book.Id;
        }

        public async Task<int> Edit(
            int id, 
            string title,
            string description,
            decimal price, 
            int copies,
            int? edition,
            int? ageRestriction,
            DateTime releaseDate,
            int authorId,
            string categories)
        {
            var categoryNames = categories
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .ToHashSet();

            var existingCategories = await this.db
                .Categories
                .Where(c => categoryNames.Contains(c.Name))
                .ToListAsync();

            var allCategories = new List<Category>(existingCategories);

            foreach (var categoryName in categoryNames)
            {
                if (existingCategories.All(c => c.Name != categoryName))
                {
                    var category = new Category
                    {
                        Name = categoryName
                    };

                    this.db.Add(category);

                    allCategories.Add(category);
                }
            }

            var book = await this.db.Books.FindAsync(id);

            book.Title = title;
            book.Description = description;
            book.Price = price;
            book.Copies = copies;
            book.ReleaseDate = releaseDate;
            book.AgeRestriction = ageRestriction;
            book.Edition = edition;
            book.AuthorId = authorId;

            foreach (var bookCategory in book.Categories)
            {
                this.db.BooksInCategories.Remove(new BookCategory
                {
                    BookId = bookCategory.BookId,
                    CategoryId = bookCategory.CategoryId
                });
            }

            await this.db.SaveChangesAsync();

            allCategories.ForEach(c => book.Categories.Add(new BookCategory
            {
                CategoryId = c.Id
            }));
            
            await this.db.SaveChangesAsync();

            return book.Id;
        }

        public async Task<string> Delete(int id)
        {
            var book = this.db.Books.Find(id);

            this.db.Books.Remove(book);
            await this.db.SaveChangesAsync();

            return $"Book with id {id} was successufully deleted.";
        }

        public async Task<IEnumerable<BookListingServiceModel>> All(string searchText)
            => await this.db
                .Books
                .Where(b => b.Title.ToLower().Contains(searchText.ToLower()))
                .OrderBy(b => b.Title)
                .Take(10)
                .ProjectTo<BookListingServiceModel>()
                .ToListAsync();

        public async Task<BookDetailsServiceModel> Details(int id)
            => await this.db
                .Books
                .Where(b => b.Id == id)
                .ProjectTo<BookDetailsServiceModel>()
                .FirstOrDefaultAsync();

        public async Task<bool> Exists(int id)
            => await this.db.Books.AnyAsync(b => b.Id == id);
    }
}
