﻿namespace LearningSystem.Data.Models
{
    public class UsersCourses
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }

        public Grade Grade { get; set; }
    }
}
