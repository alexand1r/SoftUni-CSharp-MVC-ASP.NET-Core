namespace News.Test
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using News.Api.Controllers;
    using News.Api.Models;
    using News.Data;
    using News.Data.Models;
    using News.Services;
    using Xunit;

    public class NewsControllerTest
    {
        [Fact]
        public async Task TestGetAll_ShouldReturn200StatusCode()
        {
            //Arrange
            var newsService = new Mock<INewsService>();
            
            var controller = new NewsController(
                newsService.Object);

            // Act
            var result = await controller.Get();

            //Assert
            result
                .Should()
                .BeOfType<OkObjectResult>()
                .Subject
                .StatusCode
                .Should()
                .Be(200);
        }

        [Fact]
        public async Task TestGetAll_ShouldReturnCorrectData()
        {
            //Arrange
            var db = this.GetDatabase();
            foreach (var news in this.GetTestData())
            {
                db.News.Add(news);
            }
            db.SaveChanges();

            var newsService = new Mock<INewsService>();

            var controller = new NewsController(
                newsService.Object);

            // Act
            var returnedData = (await controller.Get() as OkObjectResult).Value as IEnumerable<News>;
            var testData = this.GetTestData();

            //Assert
            foreach (var returnedModel in returnedData)
            {
                var testModel = testData.First(n => n.Id == returnedModel.Id);

                Assert.NotNull(testModel);
                Assert.True(this.CompareNewsExact(returnedModel, testModel));
            }
        }

        [Fact]
        public async Task TestCreateWithCorrectData()
        {
            //Arrange
            var db = this.GetDatabase();
            db.News.AddRange(this.GetTestData());
            db.SaveChanges();

            var newsService = new Mock<INewsService>();

            var controller = new NewsController(
                newsService.Object);

            // Act
            var result = await controller
                .Post(new PostCreateNewsRequestModel
            {
                Title = "Test",
                Content = "Test",
                PublishDate = DateTime.Now
            });
            
            //Assert
            result
                .Should()
                .BeOfType<OkObjectResult>()
                .Subject
                .StatusCode
                .Should()
                .Be(201);
        }

        private NewsDbContext GetDatabase()
        {
            var dbOptions = new DbContextOptionsBuilder<NewsDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new NewsDbContext(dbOptions);
        }

        private IEnumerable<News> GetTestData()
        {
            return new List<News>
            {
                new News{Id = 1, Content = "Content1", Title = "Title1", PublishDate = DateTime.ParseExact("01/06/2017", "dd/MM/yyyy", CultureInfo.InvariantCulture)},
                new News{Id = 2, Content = "Content2", Title = "Title2", PublishDate = DateTime.ParseExact("02/06/2017", "dd/MM/yyyy", CultureInfo.InvariantCulture)},
                new News{Id = 3, Content = "Content3", Title = "Title3", PublishDate = DateTime.ParseExact("03/06/2017", "dd/MM/yyyy", CultureInfo.InvariantCulture)},
                new News{Id = 4, Content = "Content4", Title = "Title4", PublishDate = DateTime.ParseExact("04/06/2017", "dd/MM/yyyy", CultureInfo.InvariantCulture)},
                new News{Id = 5, Content = "Content5", Title = "Title5", PublishDate = DateTime.ParseExact("05/06/2017", "dd/MM/yyyy", CultureInfo.InvariantCulture)}
            };
        }

        private bool CompareNewsExact(News thisNews, News otherNews)
            => thisNews.Id == otherNews.Id
               && thisNews.Title == otherNews.Title
               && thisNews.Content == otherNews.Content
               && thisNews.PublishDate == otherNews.PublishDate;
    }
}
