namespace CarDealer.Web.Models.Sales
{
    using CarDealer.Services.Models.Sales;
    using System.Collections.Generic;

    public class AllSalesModel
    {
        public IEnumerable<SaleModel> Sales { get; set; }

        public string HasDiscount { get; set; }
    }
}
