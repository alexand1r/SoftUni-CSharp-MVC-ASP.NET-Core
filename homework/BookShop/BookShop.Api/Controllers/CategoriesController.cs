namespace BookShop.Api.Controllers
{
    using System.Threading.Tasks;
    using BookShop.Api.Infrastructure.Extensions;
    using BookShop.Api.Infrastructure.Filters;
    using BookShop.Api.Models.Categories;
    using BookShop.Services;
    using Microsoft.AspNetCore.Mvc;
    using static WebConstants;

    public class CategoriesController : BaseController
    {
        private readonly ICategoryService categories;

        public CategoriesController(ICategoryService categories)
        {
            this.categories = categories;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
            => this.Ok(await this.categories.All());

        [HttpGet(WithId)]
        public async Task<IActionResult> Get(int id)
            => this.OkOrNotFound(await this.categories.Details(id));

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Post([FromBody] PostCreateCategoryRequestModel model)
            => this.Ok(await this.categories.Create(model.Name));

        [HttpPut(WithId)]
        [ValidateModelState]
        public async Task<IActionResult> Put(int id, [FromBody]PutEditCategoryRequestModel model)
        {
            if (!await this.categories.Exists(id))
            {
                return this.BadRequest();
            }

            var categoryId = await this.categories.Edit(
                id,
                model.Name);

            return this.Ok(categoryId);
        }

        [HttpDelete(WithId)]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await this.categories.Exists(id))
            {
                return this.BadRequest();
            }

            var message = this.categories.Delete(id);

            return this.Ok(message);
        }
    }
}
