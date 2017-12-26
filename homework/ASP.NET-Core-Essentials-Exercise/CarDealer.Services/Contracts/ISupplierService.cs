namespace CarDealer.Services.Contracts
{
    using System.Collections.Generic;
    using CarDealer.Services.Models.Suppliers;

    public interface ISupplierService
    {
        IEnumerable<SupplierListingModel> AllListings(bool isImporter);

        IEnumerable<SupplierModel> All();

        IEnumerable<SupplierDetailsModel> AllWithDetails();

        void Add(string name, bool isImporter);

        void Edit(int id, string name, bool isImporter);

        void Delete(int id);

        SupplierDetailsModel ById(int id);
    }
}
