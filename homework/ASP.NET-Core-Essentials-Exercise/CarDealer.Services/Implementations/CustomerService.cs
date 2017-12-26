namespace CarDealer.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CarDealer.Data;
    using CarDealer.Data.Models;
    using CarDealer.Services.Contracts;
    using CarDealer.Services.Models;
    using CarDealer.Services.Models.Customers;

    public class CustomerService : ICustomerService
    {
        private readonly CarDealerDbContext db;

        public CustomerService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CustomerModel> All()
        {
           return this.db.Customers
                .Select(c => new CustomerModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    BirthDate = c.BirthDate,
                    IsYoungDriver = c.IsYoungDriver
                }).ToList();
        }

        public IEnumerable<CustomerModel> OrderedCustomers(OrderDirection order)
        {
            var customersQuery = this.db.Customers.AsQueryable();

            switch (order)
            {
                case OrderDirection.Ascending:
                    customersQuery = customersQuery
                        .OrderBy(c => c.BirthDate)
                        .ThenBy(c => c.Name);
                    break;
                case OrderDirection.Descending:
                    customersQuery = customersQuery
                        .OrderByDescending(c => c.BirthDate)
                        .ThenBy(c => c.Name);
                    break;
                default:
                    throw new InvalidOperationException("Invalid order direction.");
            }

            return customersQuery
                .Select(c => new CustomerModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    BirthDate = c.BirthDate,
                    IsYoungDriver = c.IsYoungDriver
                }).ToList();
        }

        public CustomerSalesModel CustomerSales(string id)
        {
            return this.db.Customers.Where(c => c.Id == int.Parse(id))
            .Select(c => new CustomerSalesModel
            {
                Name = c.Name,
                CarsCount = c.Sales.Count,
                TotalPrice = c.Sales.Sum(s => s.Car.Parts.Sum(p => p.Part.Price))
            }).FirstOrDefault();
        }
        
        public void Create(string name, DateTime birthDate, bool isYoungDriver)
        {
            var customer = new Customer
            {
                Name = name,
                BirthDate = birthDate,
                IsYoungDriver = isYoungDriver
            };

            this.db.Add(customer);
            this.db.SaveChanges();
        }

        public CustomerModel ById(int id)
            => this.db
            .Customers
            .Where(c => c.Id == id)
            .Select(c => new CustomerModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    BirthDate = c.BirthDate,
                    IsYoungDriver = c.IsYoungDriver
                }).FirstOrDefault();

        public void Edit(int id, string name, DateTime birthDate, bool isYoungDriver)
        {
            var existingCustomer = this.db.Customers.Find(id);

            if (existingCustomer == null)
            {
                return;
            }

            existingCustomer.Name = name;
            existingCustomer.BirthDate = birthDate;
            existingCustomer.IsYoungDriver = isYoungDriver;

            this.db.SaveChanges();
        }

        public bool Exists(int id)
            => this.db.Customers.Any(c => c.Id == id);

        public Customer FindCustomer(int id)
            => this.db.Customers.Find(id);
    }
}
