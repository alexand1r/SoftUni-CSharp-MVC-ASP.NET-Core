namespace LearningSystem.Web.Models.Admin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CourseFormViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        [Display(Name = "Trainer")]
        public string TrainerId { get; set; }

        public IEnumerable<SelectListItem> Trainers { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
    }
}
