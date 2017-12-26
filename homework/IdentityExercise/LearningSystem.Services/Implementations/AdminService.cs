namespace LearningSystem.Services.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using LearningSystem.Data;
    using LearningSystem.Services.Models;
    using Microsoft.EntityFrameworkCore;

    public class AdminService : IAdminService
    {
        private readonly LearningSystemDbContext db;

        public AdminService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<UserModel>> AllAsync()
            => await this.db.Users
                .Select(u => new UserModel
                {
                    Email = u.Email,
                    Id = u.Id,
                    Username = u.UserName
                })
                .ToListAsync();
    }
}
