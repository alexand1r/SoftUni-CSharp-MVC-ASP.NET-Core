namespace CarDealer.Services.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using CarDealer.Data;
    using CarDealer.Data.Models;
    using CarDealer.Services.Contracts;
    using CarDealer.Services.Models.Sales;

    public class SaleService : ISaleService
    {
        private readonly CarDealerDbContext db;

        public SaleService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<SaleModel> All(bool discount)
        {
            if (discount)
            {
                return this.db.Sales
                    .Where(s => s.Discount > 0L)
                    .Select(s => new SaleModel
                    {
                        Car = s.Car,
                        Customer = s.Customer,
                        Discount = s.Discount
                    }).ToList();
            }
            else
            {
                return this.db.Sales
                   .Select(s => new SaleModel
                   {
                       Car = s.Car,
                       Customer = s.Customer,
                       Discount = s.Discount
                   }).ToList();
            }        
        }

        public IEnumerable<SaleModel> AllWithEqualDiiscount(string discount)
            => this.db.Sales
                .Where(s => s.Discount == decimal.Parse(discount))
                .Select(s => new SaleModel
                {
                    Car = s.Car,
                    Customer = s.Customer,
                    Discount = s.Discount
                }).ToList();

        public void Add(int carId, int customerId, decimal discount)
        {
            var sale = new Sale
            {
                CarId = carId,
                CustomerId = customerId,
                Discount = discount
            };

            this.db.Add(sale);
            this.db.SaveChanges();
        }
    }
}
