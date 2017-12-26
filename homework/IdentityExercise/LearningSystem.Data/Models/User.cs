namespace LearningSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;
    
    public class User : IdentityUser
    {
        public DateTime BirthDate { get; set; }

        public List<Course> Trainings { get; set; } = new List<Course>();

        public List<UsersCourses> Courses { get; set; } = new List<UsersCourses>();

        public List<Article> Articles { get; set; } = new List<Article>();
    }
}
