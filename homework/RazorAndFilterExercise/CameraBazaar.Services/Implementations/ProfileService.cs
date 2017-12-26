namespace CameraBazaar.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CameraBazaar.Data;
    using CameraBazaar.Data.Models;
    using CameraBazaar.Services.Models;

    public class ProfileService : IProfileService
    {
        private readonly CameraBazaarDbContext db;

        public ProfileService(CameraBazaarDbContext db)
        {
            this.db = db;
        }

        public User GetUser(string username)
            => this.db.Users.FirstOrDefault(u => u.UserName == username);

        public User GetUserById(string id)
            => this.db.Users.FirstOrDefault(u => u.Id == id);

        public void Edit(string id, string email, string password, string phone)
        {
            var user = this.GetUserById(id);

            user.Email = email;
            user.PasswordHash = password;
            user.PhoneNumber = phone;

            this.db.SaveChanges();
        }

        public void LogDate(string username)
        {
            var user = this.GetUser(username);
            user.LastLogIn = DateTime.UtcNow;
            this.db.SaveChanges();
        }

        public IEnumerable<ProfileModel> All()
            => this.db.Users.OrderBy(u => u.Email)
                .Select(u => new ProfileModel
                {
                    Id = u.Id,
                    Username = u.UserName,
                    Email = u.Email
                })
                .ToList();
    }
}
