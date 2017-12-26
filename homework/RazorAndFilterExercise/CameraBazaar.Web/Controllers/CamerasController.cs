namespace CameraBazaar.Web.Controllers
{
    using Data.Models;
    using Services;
    using Models.Cameras;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class CamerasController : Controller
    {
        private readonly ICameraService cameras;
        private readonly UserManager<User> userManager;
        private readonly IProfileService profiles;

        public CamerasController(
            ICameraService cameras,
            UserManager<User> userManager,
            IProfileService profiles)
        {
            this.userManager = userManager;
            this.cameras = cameras;
            this.profiles = profiles;
        }

        public IActionResult All() 
            => this.View(this.cameras.All());

        public IActionResult Details(int id)
        {
            var camera = this.cameras.Details(id);
            var user = this.profiles.GetUserById(camera.UserId);

            return this.View(new DetailsCameraModel
            {
                Description = camera.Description,
                ImageUrl = camera.ImageUrl,
                IsFullFrame = camera.IsFullFrame,
                LightMetering = camera.LightMetering,
                Make = camera.Make,
                MaxISO = camera.MaxISO,
                MaxShutterSpeed = camera.MaxShutterSpeed,
                MinISO = camera.MinISO,
                MinShutterSpeed = camera.MinShutterSpeed,
                VideoResolution = camera.VideoResolution,
                Quantity = camera.Quantity,
                Price = camera.Price,
                Model = camera.Model,
                Username = user.UserName,
                UserId = camera.UserId
            });
        }

        [Authorize]
        public IActionResult Add() => this.View();

        [Authorize]
        [HttpPost]
        public IActionResult Add(AddCameraViewModel cameraModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(cameraModel);
            }

            this.cameras.Create(
                cameraModel.Make,
                cameraModel.Model,
                cameraModel.Price,
                cameraModel.Quantity,
                cameraModel.MinShutterSpeed,
                cameraModel.MaxShutterSpeed,
                cameraModel.MinISO,
                cameraModel.MaxISO,
                cameraModel.IsFullFrame,
                cameraModel.VideoResolution,
                cameraModel.LightMetering,
                cameraModel.Description,
                cameraModel.ImageUrl,
                this.userManager.GetUserId(this.User));

            return this.RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
