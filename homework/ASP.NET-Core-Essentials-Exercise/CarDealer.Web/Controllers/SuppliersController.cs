namespace CarDealer.Web.Controllers
{
    using System;
    using CarDealer.Data.Models;
    using CarDealer.Services.Contracts;
    using CarDealer.Web.Models.Suppliers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class SuppliersController : Controller
    {
        private const string SuppliersView = "Suppliers";

        private readonly ISupplierService suppliers;
        private readonly ILogService logs;

        public SuppliersController(
            ISupplierService suppliers,
            ILogService logs)
        {
            this.suppliers = suppliers;
            this.logs = logs;
        }

        [Authorize]
        public IActionResult Add()
            => this.View();

        [Authorize]
        [HttpPost]
        public IActionResult Add(SupplierFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            this.suppliers.Add(
                model.Name,
                model.IsImporter);

            this.logs.Log(
                this.User.Identity.Name,
                OperationType.Create,
                "Supplier",
                DateTime.Now);

            return this.RedirectToAction(nameof(this.All));
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var supplier = this.suppliers.ById(id);

            if (supplier == null)
            {
                return this.NotFound();
            }

            return this.View(new SupplierFormModel
            {
                Name = supplier.Name,
                IsImporter = supplier.IsImporter
            });
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id, SupplierFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            this.suppliers.Edit(
                id,
                model.Name,
                model.IsImporter);

            this.logs.Log(
                this.User.Identity.Name,
                OperationType.Edit,
                "Supplier",
                DateTime.Now);

            return this.RedirectToAction(nameof(this.All));
        }

        [Authorize]
        public IActionResult Delete(int id) => this.View(id);

        [Authorize]
        public IActionResult Destroy(int id)
        {
            this.suppliers.Delete(id);

            this.logs.Log(
                this.User.Identity.Name,
                OperationType.Delete,
                "Supplier",
                DateTime.Now);

            return this.RedirectToAction(nameof(this.All));
        }

        public IActionResult All()
            => this.View(this.suppliers.AllWithDetails());
        
        public IActionResult Local()
        {
            return this.View(SuppliersView, this.GetSuppliers(false));
        }

        public IActionResult Importers()
        {
            return this.View(SuppliersView, this.GetSuppliers(true));
        }

        private SuppliersModel GetSuppliers(bool importers)
        {
            var type = importers ? "Importer" : "Local";

            var suppliers = this.suppliers.AllListings(importers);

            return new SuppliersModel
            {
                Type = type,
                Suppliers = suppliers
            };
        }
    }
}
