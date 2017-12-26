namespace CarDealer.Services.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using CarDealer.Data;
    using CarDealer.Data.Models;
    using CarDealer.Services.Contracts;
    using CarDealer.Services.Models.Suppliers;

    public class SupplierService : ISupplierService
    {
        private readonly CarDealerDbContext db;

        public SupplierService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<SupplierListingModel> AllListings(bool isImporter)
            => this.db
                .Suppliers
                .Where(s => s.IsImporter == isImporter)
                .Select(s => new SupplierListingModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    TotalParts = s.Parts.Count
                })
                .ToList();

        public IEnumerable<SupplierModel> All()
            => this.db
                .Suppliers
                .OrderBy(s => s.Name)
                .Select(s => new SupplierModel
                {
                    Id = s.Id,
                    Name = s.Name
                })
            .ToList();

        public IEnumerable<SupplierDetailsModel> AllWithDetails()
            => this.db.Suppliers
                .OrderByDescending(s => s.Id)
                .Select(s => new SupplierDetailsModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    IsImporter = s.IsImporter
                }).ToList();

        public void Add(string name, bool isImporter)
        {
            var supplier = new Supplier
            {
                Name = name,
                IsImporter = isImporter
            };

            this.db.Add(supplier);
            this.db.SaveChanges();
        }

        public void Edit(int id, string name, bool isImporter)
        {
            var supplier = this.db.Suppliers.Find(id);

            if (supplier == null)
            {
                return;
            }

            supplier.Name = name;
            supplier.IsImporter = isImporter;

            this.db.SaveChanges();
        }

        public void Delete(int id)
        {
            var supplier = this.db.Suppliers.Find(id);

            if (supplier == null)
            {
                return;
            }

            var parts = supplier.Parts;
            foreach (var part in parts)
            {
                this.db.Parts.Remove(part);
            }
            this.db.Suppliers.Remove(supplier);
            this.db.SaveChanges();
        }

        public SupplierDetailsModel ById(int id)
            => this.db
                .Suppliers
                .Where(s => s.Id == id)
                .Select(s => new SupplierDetailsModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    IsImporter = s.IsImporter
                })
                .FirstOrDefault();
    }
}
