namespace CameraBazaar.Web.Models.Home
{
    using System.Collections.Generic;

    public class UserRolesViewModel
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}
