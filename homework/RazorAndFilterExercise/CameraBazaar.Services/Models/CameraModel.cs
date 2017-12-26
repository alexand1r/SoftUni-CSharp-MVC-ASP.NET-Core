namespace CameraBazaar.Services.Models
{
    using CameraBazaar.Data.Models;

    public class CameraModel
    {
        public int Id { get; set; }

        public CameraMakeType Make { get; set; }
        
        public string Model { get; set; }

        public decimal Price { get; set; }
        
        public int Quantity { get; set; }

        public string ImageUrl { get; set; }

        public string Stock => (this.Quantity <= 0 ? "OUT OF STOCK" : "IN STOCK");
    }
}
