﻿namespace LearningSystem.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Course
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string TrainerId { get; set; }

        public User Trainer { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public List<UsersCourses> Users { get; set; } = new List<UsersCourses>();
    }
}
