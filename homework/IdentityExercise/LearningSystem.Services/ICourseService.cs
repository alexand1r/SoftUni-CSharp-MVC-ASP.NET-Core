namespace LearningSystem.Services
{
    using System;
    using System.Collections.Generic;
    using LearningSystem.Data.Models;
    using LearningSystem.Services.Models.Courses;

    public interface ICourseService
    {
        IEnumerable<CourseModel> All();

        IEnumerable<Course> AllByTrainer(string id);

        void Create(
            string name,
            string description, 
            string trainerId,
            DateTime startDate,
            DateTime endDate);

        bool IsUserInCourse(string userId, int id);

        CourseModel GetCourseById(int id);

        void SignUp(string userId, int id);

        void SignOut(string userId, int id);
    }
}
