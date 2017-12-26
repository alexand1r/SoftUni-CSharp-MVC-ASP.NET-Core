namespace CameraBazaar.Web.Models.Home
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ProfileFormViewModel
    {
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"\+\d{10,12}", ErrorMessage = "Phone must start with a '+' sign and contain between 10 to 12 symbols.")]
        public string Phone { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public DateTime LastLogIn { get; set; }
    }
}
