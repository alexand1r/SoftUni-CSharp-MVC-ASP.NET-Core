namespace CarDealer.Web.Models.Sales
{
    using System.Collections.Generic;
    using CarDealer.Services.Models.Sales;

    public class SalesWithDiscount
    {
        public IEnumerable<SaleModel> Sales { get; set; }

        public string Discount { get; set; }
    }
}
