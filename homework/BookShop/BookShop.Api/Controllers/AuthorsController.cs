namespace BookShop.Api.Controllers
{
    using System.Threading.Tasks;
    using Infrastructure.Extensions;
    using Infrastructure.Filters;
    using Models.Authors;
    using Services;
    using Microsoft.AspNetCore.Mvc;
    using static WebConstants;

    public class AuthorsController : BaseController
    {
        private readonly IAuthorService authors;

        public AuthorsController(IAuthorService authors)
        {
            this.authors = authors;
        }

        [HttpGet(WithId)]
        public async Task<IActionResult> Get(int id)
            => this.OkOrNotFound(await this.authors.Details(id));

        [HttpGet(WithId + "/books")]
        public async Task<IActionResult> GetBooks(int id)
            => this.Ok(await this.authors.Books(id));

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Post([FromBody]PostAuthorRequestModel model)
            => this.Ok(await this.authors
                .Create(model.FirstName, model.LastName));
    }
}
