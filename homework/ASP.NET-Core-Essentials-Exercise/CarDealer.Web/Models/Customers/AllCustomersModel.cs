namespace CarDealer.Web.Models.Customers
{
    using System.Collections.Generic;
    using CarDealer.Services.Models;

    public class AllCustomersModel
    {
        public IEnumerable<CustomerModel> Customers { get; set; }

        public OrderDirection OrderDirection { get; set; }
    }
}
