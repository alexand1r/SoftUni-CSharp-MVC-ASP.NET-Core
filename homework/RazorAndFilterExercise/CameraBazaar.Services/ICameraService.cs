namespace CameraBazaar.Services
{
    using System.Collections.Generic;
    using CameraBazaar.Data;
    using CameraBazaar.Data.Models;
    using CameraBazaar.Services.Models;

    public interface ICameraService
    {
        Camera Details(int id);

        IEnumerable<CameraModel> All();

        IEnumerable<CameraModel> AllById(string id);

        void Create(
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
            string userId);
    }
}
