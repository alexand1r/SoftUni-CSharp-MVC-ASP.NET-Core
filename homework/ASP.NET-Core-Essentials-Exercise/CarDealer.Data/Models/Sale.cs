namespace CarDealer.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Sale
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public decimal Discount { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public int CarId { get; set; }

        public Car Car { get; set; }
    }
}
