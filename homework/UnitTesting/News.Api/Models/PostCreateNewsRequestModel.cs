namespace News.Api.Models
{
    using System;

    public class PostCreateNewsRequestModel
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime PublishDate { get; set; }
    }
}
