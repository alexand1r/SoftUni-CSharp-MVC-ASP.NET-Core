namespace CameraBazaar.Web.Models.Cameras
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using CameraBazaar.Data.Models;

    public class DetailsCameraModel
    {
        public CameraMakeType Make { get; set; }
        
        public string Model { get; set; }

        public decimal Price { get; set; }
        
        public int Quantity { get; set; }

        public string Stock => (this.Quantity <= 0 ? "OUT OF STOCK" : "IN STOCK");

        [Display(Name = "Min Shutter Speed")]
        public int MinShutterSpeed { get; set; }
        
        [Display(Name = "Max Shutter Speed")]
        public int MaxShutterSpeed { get; set; }

        [Display(Name = "Mix ISO")]
        public MinISO MinISO { get; set; }

        [Display(Name = "Max ISO")]
        public int MaxISO { get; set; }

        [Display(Name = "Is Full Frame")]
        public bool IsFullFrame { get; set; }
        
        [Display(Name = "Video Resolution")]
        public string VideoResolution { get; set; }

        [Display(Name = "Light Meterings")]
        public LightMetering LightMetering { get; set; }
        
        [Display(Name = "Desctription")]
        public string Description { get; set; }
        
        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; }

        public string Username { get; set; }

        public string UserId { get; set; }
    }
}
