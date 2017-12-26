namespace News.Api.Models
{
    using System;

    public class PutEditNewsRequestModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime PublishDate { get; set; }
    }
}
