namespace CarDealer.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CarDealer.Services.Contracts;
    using CarDealer.Services.Models.Sales;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Web.Models.Sales;
    using CarDealer.Data.Models;

    public class SalesController : Controller
    {
        private readonly ISaleService sales;
        private readonly ICustomerService customers;
        private readonly ICarService cars;
        private readonly ILogService logs;
       
        private const string SalesView = "Sales";
        private const string SalesWithDiscountView = "SalesWithDiscount";

        public SalesController(
            ISaleService sales,
            ICustomerService customers,
            ICarService cars,
            ILogService logs)
        {
            this.sales = sales;
            this.customers = customers;
            this.cars = cars;
            this.logs = logs;
        }

        // GET /sales/add
        public IActionResult Add() 
            => this.View(new AddSaleModel
        {
            Customers = this.GetCustomers(),
            Cars = this.GetCars()
        });

        // POST /sales/add
        [HttpPost]
        public IActionResult Add(AddSaleModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Customers = this.GetCustomers();
                model.Cars = this.GetCars();
                return this.View(model);
            }
            
            return this.RedirectToAction(
                nameof(this.Confirm),
                model);
        }

        // GET /sales/confirm
        public IActionResult Confirm(AddSaleModel model)
        {
            var customer = this.customers.FindCustomer(model.CustomerId);
            var car = this.cars.FindCar(model.CarId);

            return this.View(new SaleModel
            {
                Car = car,
                Customer = customer,
                Discount = model.Discount
            });
        }

        // POST /sales/confirm
        [HttpPost]
        public IActionResult Confirm(SaleModel model)
        {
            this.sales.Add(
                model.Car.Id, 
                model.Customer.Id,
                model.Discount);

            this.logs.Log(
                this.User.Identity.Name,
                OperationType.Create,
                "Sale",
                DateTime.Now);

            return this.RedirectToAction(nameof(this.All));
        }

        // GET /sales/all
        public IActionResult All()
        {            
            return this.View(SalesView, this.GetSales(false));
        }

        // GET /sales/discounted
        public IActionResult Discounted()
        {
            return this.View(SalesView, this.GetSales(true));
        }

        // GET /sales/discounted/{discount}
        [Route("sales/discounted/{discount}")]
        public IActionResult Discounted(string discount)
        {
            return this.View(SalesWithDiscountView
                , new SalesWithDiscount
                {
                    Sales = this.sales.AllWithEqualDiiscount(discount),
                    Discount = discount
                    
                });
        }

        private AllSalesModel GetSales(bool discount)
        {
            return new AllSalesModel
            {
                Sales = this.sales.All(discount),
                HasDiscount = (discount ? "With Discount" : "")
            };
        }

        private IEnumerable<SelectListItem> GetCustomers()
        {
            return this.customers
                .All()
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                });
        }

        private IEnumerable<SelectListItem> GetCars()
        {
            return this.cars
                .All()
                .Select(c => new SelectListItem
                {
                    Text = c.Make,
                    Value = c.Id.ToString()
                });
        }
    }
}
