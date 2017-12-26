namespace BookShop.Api.Controllers
{
    using System.Threading.Tasks;
    using BookShop.Api.Infrastructure.Extensions;
    using BookShop.Api.Infrastructure.Filters;
    using BookShop.Api.Models.Books;
    using Services;
    using Microsoft.AspNetCore.Mvc;
    using static WebConstants;

    public class BooksController : BaseController
    {
        private readonly IBookService books;
        private readonly IAuthorService authors;

        public BooksController(
            IBookService books,
            IAuthorService authors)
        {
            this.books = books;
            this.authors = authors;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]string search = "")
            => this.Ok(await this.books.All(search));

        [HttpGet(WithId)]
        public async Task<IActionResult> Get(int id)
            => this.OkOrNotFound(await this.books.Details(id));

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Post([FromBody] PostCreateBookRequestModel model)
        {
            if (!await this.authors.Exists(model.AuthorId))
            {
                return this.BadRequest("Author does not exist.");
            }

            var id = await this.books.Create(
                model.Title,
                model.Description,
                model.Price,
                model.Copies,
                model.Edition,
                model.AgeRestriction,
                model.ReleaseDate,
                model.AuthorId,
                model.Categories);

            return this.Ok(id);
        }

        [HttpPut(WithId)]
        [ValidateModelState]
        public async Task<IActionResult> Put(int id, [FromBody]PutEditBookRequestModel model)
        {
            if (!await this.books.Exists(id))
            {
                return this.BadRequest();
            }

            var bookId = await this.books.Edit(
                id,
                model.Title,
                model.Description,
                model.Price,
                model.Copies,
                model.Edition,
                model.AgeRestriction,
                model.ReleaseDate,
                model.AuthorId,
                model.Categories);

            return this.Ok(bookId);
        }

        [HttpDelete(WithId)]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await this.books.Exists(id))
            {
                return this.BadRequest();
            }

            var message = this.books.Delete(id);

            return this.Ok(message);
        }
    }
}
