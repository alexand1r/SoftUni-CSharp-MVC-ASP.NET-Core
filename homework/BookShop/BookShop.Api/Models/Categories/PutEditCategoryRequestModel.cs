namespace BookShop.Api.Models.Categories
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class PutEditCategoryRequestModel
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(CategoryNameMaxLength)]
        public string Name { get; set; }
    }
}
