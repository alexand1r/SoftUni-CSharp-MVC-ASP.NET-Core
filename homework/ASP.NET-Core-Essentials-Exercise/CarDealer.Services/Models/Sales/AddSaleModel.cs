namespace CarDealer.Services.Models.Sales
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AddSaleModel
    {
        [Display(Name = "Car")]
        public int CarId { get; set; }

        public IEnumerable<SelectListItem> Cars { get; set; }

        [Display(Name = "Customer")]
        public int CustomerId { get; set; }

        public IEnumerable<SelectListItem> Customers { get; set; }

        public decimal Discount { get; set; }
    }
}
