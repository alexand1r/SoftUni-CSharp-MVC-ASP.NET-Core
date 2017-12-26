namespace LearningSystem.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using LearningSystem.Data;
    using LearningSystem.Data.Models;
    using LearningSystem.Services.Models.Courses;
    using Microsoft.EntityFrameworkCore;

    public class CourseService : ICourseService
    {
        private readonly LearningSystemDbContext db;

        public CourseService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CourseModel> All()
            => this.db
                .Courses
                .Include(c => c.Users)
                .OrderByDescending(c => c.Id)
                .Select(c => new CourseModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    Trainer = c.Trainer.UserName
                })
                .ToList();

        public IEnumerable<Course> AllByTrainer(string id)
        {
            var user = this.db.Users.Find(id);

            return user.Trainings;
        }

        public void Create(
            string name, 
            string description, 
            string trainerId, 
            DateTime startDate, 
            DateTime endDate)
        {
            var course = new Course
            {
                Name = name,
                Description = description,
                TrainerId = trainerId,
                StartDate = startDate,
                EndDate = endDate
            };

            this.db.Add(course);
            this.db.SaveChanges();
        }

        public bool IsUserInCourse(string userId, int id)
        {
            return this.db
                .Courses
                .Any(c => c.Id == id 
                    && c.Users.Any(u => u.User.Id == userId));
        }

        public CourseModel GetCourseById(int id)
            => this.db
                .Courses
                .Where(c => c.Id == id)
                .Select(c => new CourseModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate
                })
                .FirstOrDefault();

        public void SignUp(string userId, int id)
        {
            var user = this.db.Users.Include(u => u.Courses).FirstOrDefault(u => u.Id == userId);
            var course = this.db.Courses.Include(c => c.Users).FirstOrDefault(c => c.Id == id);

            course.Users.Add(new UsersCourses
            {
                CourseId = id,
                UserId = userId,
                Course = course,
                User = user
            });

            this.db.SaveChanges();
        }

        public void SignOut(string userId, int id)
        {
            var user = this.db.Users.Include(u => u.Courses).FirstOrDefault(u => u.Id == userId);
            var course = this.db.Courses.Include(c => c.Users).FirstOrDefault(c => c.Id == id);
            
            course.Users.Remove(new UsersCourses
            {
                User = user,
                Course = course
            });

            this.db.SaveChanges();
        }
    }
}
