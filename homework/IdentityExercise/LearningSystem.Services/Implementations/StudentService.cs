namespace LearningSystem.Services.Implementations
{
    using System;
    using System.Linq;
    using LearningSystem.Data;
    using LearningSystem.Data.Models;
    using LearningSystem.Services.Models;

    public class StudentService : IStudentService
    {
        private readonly LearningSystemDbContext db;

        public StudentService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public User GetUserByUsername(string username)
            => this.db
                .Users
                .FirstOrDefault(u => u.UserName == username);

        public User GetUserById(string id)
            => this.db
                .Users
                .Find(id);

        public void Edit(string id, string email, string username, DateTime birthdate)
        {
            var user = this.GetUserById(id);

            user.Email = email;
            user.UserName = username;
            user.BirthDate = birthdate;

            this.db.SaveChanges();
        }
    }
}
