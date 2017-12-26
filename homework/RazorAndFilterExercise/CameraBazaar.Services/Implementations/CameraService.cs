namespace CameraBazaar.Services.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using CameraBazaar.Data;
    using CameraBazaar.Data.Models;
    using CameraBazaar.Services.Models;

    public class CameraService : ICameraService
    {
        private readonly CameraBazaarDbContext db;

        public CameraService(CameraBazaarDbContext db)
        {
            this.db = db;
        }

        public Camera Details(int id)
            => this.db
                .Cameras
                .Find(id);

        public IEnumerable<CameraModel> All()
            => this.db
                .Cameras
                .OrderBy(c => c.Id)
                .Select(c => new CameraModel
                {
                    Id = c.Id,
                    ImageUrl = c.ImageUrl,
                    Make = c.Make,
                    Model = c.Model,
                    Price = c.Price,
                    Quantity = c.Quantity
                })
                .ToList();

        public IEnumerable<CameraModel> AllById(string id)
            => this.db.Cameras
                .Where(c => c.UserId == id)
                .Select(c => new CameraModel
                {
                    Id = c.Id,
                    ImageUrl = c.ImageUrl,
                    Make = c.Make,
                    Model = c.Model,
                    Price = c.Price,
                    Quantity = c.Quantity
                })
                .ToList();

        public void Create(
            CameraMakeType make, 
            string model, 
            decimal price, 
            int quantity, 
            int minShutterSpeed, 
            int maxShutterSpeed,
            MinISO minISO, 
            int maxISO, 
            bool isFrame, 
            string videoResolution, 
            IEnumerable<LightMetering> lightMeterings,
            string description, 
            string imageUrl,
            string userId)
        {
            var camera = new Camera
            {
                Make = make,
                Model = model,
                Price = price,
                Quantity = quantity,
                MinShutterSpeed = minShutterSpeed,
                MaxShutterSpeed = maxShutterSpeed,
                MinISO = minISO,
                MaxISO = maxISO,
                IsFullFrame = isFrame,
                VideoResolution = videoResolution,
                LightMetering = (LightMetering) lightMeterings.Cast<int>().Sum(),
                Description = description,
                ImageUrl = imageUrl,
                UserId = userId
            };

            this.db.Add(camera);
            this.db.SaveChanges();
        }
    }
}
