namespace CameraBazaar.Web.Models.Home
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CameraBazaar.Services.Models;

    public class ProfileViewModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime LastLogIn { get; set; }

        public IEnumerable<CameraModel> Cameras { get; set; }

        public int Count => this.Cameras.Count(); 

        public int CamerasInStock => this.Cameras.Count(c => c.Quantity > 0);

        public int CamerasOutOfStock => this.Cameras.Count(c => c.Quantity < 1);

        public string LoggedUserId { get; set; }
    }
}
