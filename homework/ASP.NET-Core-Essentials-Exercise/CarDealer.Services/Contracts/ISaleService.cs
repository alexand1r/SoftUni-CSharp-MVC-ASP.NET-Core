namespace CarDealer.Services.Contracts
{
    using Models.Sales;
    using System.Collections.Generic;

    public interface ISaleService
    {
        IEnumerable<SaleModel> All(bool discount);

        IEnumerable<SaleModel> AllWithEqualDiiscount(string discount);

        void Add(int carId, int customerId, decimal discount);
    }
}
