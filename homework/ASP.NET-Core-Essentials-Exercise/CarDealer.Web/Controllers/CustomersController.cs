namespace CarDealer.Web.Controllers
{
    using CarDealer.Services.Contracts;
    using CarDealer.Services.Models;
    using CarDealer.Web.Infrastructure.Extensions;
    using CarDealer.Web.Models.Customers;
    using Microsoft.AspNetCore.Mvc;

    [Route("customers")]
    public class CustomersController : Controller
    {
        private readonly ICustomerService customers;

        public CustomersController(ICustomerService customers)
        {
            this.customers = customers;
        }

        [Route("all/{order}")]
        public IActionResult All(string order)
        {
            var orderDirection = order.ToLower() == "ascending"
                ? OrderDirection.Ascending
                : OrderDirection.Descending;

            var customers = this.customers.OrderedCustomers(orderDirection);

            return View(new AllCustomersModel
            {
                Customers = customers,
                OrderDirection = orderDirection
            });
        }

        public IActionResult Customer(string id)
        {
            return this.View(this.customers.CustomerSales(id));
        }

        [Route(nameof(Create))]
        public IActionResult Create() => this.View();

        [HttpPost]
        [Route(nameof(Create))]
        public IActionResult Create(CustomerFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            this.customers.Create(
                model.Name,
                model.BirthDate,
                model.IsYoungDriver);

            return this.RedirectToAction(nameof(this.All), new {order = OrderDirection.Ascending.ToString()});
        }

        [Route(nameof(Edit) + "/{id}")]
        public IActionResult Edit(int id)
        {
            var customer = this.customers.ById(id);

            if (customer == null)
            {
                return this.NotFound();
            }

            return this.View(new CustomerFormModel
            {
                Name = customer.Name,
                BirthDate = customer.BirthDate,
                IsYoungDriver = customer.IsYoungDriver
            });
        }

        [HttpPost]
        [Route(nameof(Edit) + "/{id}")]
        public IActionResult Edit(int id, CustomerFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var customerExists = this.customers.Exists(id);

            if (!customerExists)
            {
                return this.NotFound();
            }

            this.customers.Edit(
                id,
                model.Name,
                model.BirthDate,
                model.IsYoungDriver);

            return this.RedirectToAction(nameof(this.All), new { order = OrderDirection.Ascending.ToString() });
        }
    }
}
