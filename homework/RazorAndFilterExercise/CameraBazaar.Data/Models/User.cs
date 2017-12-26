namespace CameraBazaar.Data.Models
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        public List<Camera> Cameras { get; set; } = new List<Camera>();

        public DateTime LastLogIn { get; set; }
    }
}
