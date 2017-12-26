namespace LearningSystem.Web.Models.Admin
{
    using System.Collections.Generic;
    using LearningSystem.Services.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class AdminUserListingViewModel
    {
        public IEnumerable<UserModel> Users { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}
