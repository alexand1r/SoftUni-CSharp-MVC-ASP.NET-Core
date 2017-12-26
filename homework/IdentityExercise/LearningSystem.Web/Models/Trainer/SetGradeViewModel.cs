namespace LearningSystem.Web.Models.Trainer
{
    using System.Collections.Generic;
    using LearningSystem.Data.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class SetGradeViewModel
    {
        public Grade Grade { get; set; }

        public IEnumerable<SelectListItem> Grades { get; set; }
    }
}
