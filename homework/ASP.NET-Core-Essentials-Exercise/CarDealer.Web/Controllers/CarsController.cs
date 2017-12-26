namespace CarDealer.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CarDealer.Data.Models;
    using CarDealer.Services.Contracts;
    using CarDealer.Web.Models.Cars;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    [Route("cars")]
    public class CarsController : Controller
    {
        private readonly ICarService cars;
        private readonly IPartService parts;
        private readonly ILogService logs;

        public CarsController(
            ICarService cars,
            IPartService parts,
            ILogService logs)
        {
            this.cars = cars;
            this.parts = parts;
            this.logs = logs;
        }

        [Authorize]
        [Route(nameof(Create))]
        public IActionResult Create() 
            => this.View(new CarFormModel
            {
                AllParts = this.GetPartsSelectItems()
            });

        [Authorize]
        [HttpPost]
        [Route(nameof(Create))]
        public IActionResult Create(CarFormModel carModel)
        {
            if (!ModelState.IsValid)
            {
                carModel.AllParts = this.GetPartsSelectItems();
                return this.View(carModel);
            }

            this.cars.Create(
                carModel.Make,
                carModel.Model,
                carModel.TravelledDistance,
                carModel.SelectedItems);

            this.logs.Log(
                this.User.Identity.Name,
                OperationType.Create,
                "Car",
                DateTime.Now);

            return this.RedirectToAction(nameof(this.Parts));
        }

        [Route("{make}", Order = 2)]
        public IActionResult ByMake(string make)
        {
            var cars = this.cars.ByMake(make);

            return this.View(new CarsByMakeModel
            {
                Make = make,
                Cars = cars
            });
        }

        [Route("parts")]//, Order = 1)]
        public IActionResult Parts()
        {
            return this.View(this.cars.WithParts());
        }

        private IEnumerable<SelectListItem> GetPartsSelectItems()
            => this.parts
                .All()
                .Select(p => new SelectListItem
                {
                    Text = p.Name,
                    Value = p.Id.ToString()
                });
    }
}
