namespace CarDealer.Web.Models.Suppliers
{
    using System.ComponentModel.DataAnnotations;

    public class SupplierFormModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Display(Name = "Importer")]
        public bool IsImporter { get; set; }
    }
}
