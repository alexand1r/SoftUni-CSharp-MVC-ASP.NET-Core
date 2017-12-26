namespace BookShop.Api.Models.Categories
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class PostCreateCategoryRequestModel
    {
        [Required]
        [MaxLength(CategoryNameMaxLength)]
        public string Name { get; set; }
    }
}
