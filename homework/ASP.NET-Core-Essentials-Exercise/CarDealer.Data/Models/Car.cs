namespace CarDealer.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Range(0, long.MaxValue)]
        public long TravelledDistance { get; set; }

        public List<Sale> Sales { get; set; } = new List<Sale>();

        public List<CarPart> Parts { get; set; } = new List<CarPart>();
    }
}
