namespace CarDealer.Services.Contracts
{
    using System;
    using System.Collections.Generic;
    using CarDealer.Data.Models;
    using CarDealer.Services.Models;
    using CarDealer.Services.Models.Customers;

    public interface ICustomerService
    {
        IEnumerable<CustomerModel> OrderedCustomers(OrderDirection order);

        CustomerSalesModel CustomerSales(string id);

        IEnumerable<CustomerModel> All();

        void Create(string name, DateTime birthDate, bool isYoungDriver);

        CustomerModel ById(int id);

        void Edit(int id, string name, DateTime birthDate, bool isYoungDriver);

        bool Exists(int id);

        Customer FindCustomer(int id);
    }
}
