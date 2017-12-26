using CarDealer.Data.Models;

namespace CarDealer.Services.Models.Sales
{
    using System;
    using System.Linq;

    public class SaleModel
    {
        public Car Car { get; set; }

        public Customer Customer { get; set; }

        public decimal PriceWithDiscount 
            => (this.Discount < 1 
            ? this.Car.Parts.Sum(p => p.Part.Price) * Math.Ceiling(this.Discount) 
            : this.Car.Parts.Sum(p => p.Part.Price) * this.Discount);

        public decimal PriceWithoutDiscount => this.Car.Parts.Sum(p => p.Part.Price);

        public decimal Discount { get; set; }
    }
}
