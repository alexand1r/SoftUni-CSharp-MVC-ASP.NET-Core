namespace News.Api.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using News.Api.Infrastructure.Filters;
    using News.Api.Models;
    using News.Services;

    [Route("api/[controller]")]
    public class NewsController : Controller
    {
        private readonly INewsService news;

        public NewsController(INewsService news)
        {
            this.news = news;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
            => this.Ok(await this.news.AllAsync());

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Post([FromBody] PostCreateNewsRequestModel model)
        {
            var id = await this.news.Create(
                model.Title,
                model.Content,
                model.PublishDate);

            return this.Ok(id);
        }

        [HttpPut("{id}")]
        [ValidateModelState]
        public async Task<IActionResult> Put(int id, [FromBody]PutEditNewsRequestModel model)
        {
            if (!await this.news.Exists(id))
            {
                return this.BadRequest();
            }

            var newsId = await this.news.Edit(
                id,
                model.Title,
                model.Content,
                model.PublishDate);

            return this.Ok(newsId);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await this.news.Exists(id))
            {
                return this.BadRequest();
            }

            var message = this.news.Delete(id);

            return this.Ok(message);
        }
    }
}
