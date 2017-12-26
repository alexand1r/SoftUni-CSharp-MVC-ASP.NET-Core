namespace LearningSystem.Web.Controllers
{
    using LearningSystem.Data.Models;
    using LearningSystem.Services;
    using LearningSystem.Services.Models.Courses;
    using LearningSystem.Web.Infrastructure;
    using LearningSystem.Web.Models.Students;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.StudentRole)]
    public class StudentsController : Controller
    {
        private readonly ICourseService courses;
        private readonly UserManager<User> userManager;
        private readonly IStudentService students;

        public StudentsController(
            ICourseService courses,
            UserManager<User> userManager,
            IStudentService students)
        {
            this.courses = courses;
            this.userManager = userManager;
            this.students = students;
        }

        public IActionResult SignInCourse(int id)
        {
            var course = this.GetCourse(id);

            return this.View(course);
        }

        [HttpPost]
        [ActionName(nameof(SignInCourse))]
        public IActionResult SignUp(int id)
        {
            var userId = this.userManager.GetUserId(this.User);

            this.courses.SignUp(userId, id);

            this.TempData["SuccessMessage"] = "You have signed up successfully.";
            return this.RedirectToAction("Index", "Home");
        }

        public IActionResult SignOutOfCourse(int id)
        {
            var course = this.GetCourse(id);

            return this.View(course);
        }

        [HttpPost]
        [ActionName(nameof(SignOutOfCourse))]
        public IActionResult SignOut(int id)
        {
            var userId = this.userManager.GetUserId(this.User);

            this.courses.SignOut(userId, id);

            this.TempData["SuccessMessage"] = "You have successfully signed out.";
            return this.RedirectToAction("Index", "Home");
        }

        public IActionResult Profile()
        {
            var userId = this.userManager.GetUserId(this.User);
            var user = this.students.GetUserById(userId);

            return this.View(user);
        }

        public IActionResult Edit(string id)
        {
            var user = this.students.GetUserById(id);

            return this.View(new ProfileEditViewModel
            {
                BirthDate = user.BirthDate,
                Email = user.Email,
                Username = user.UserName
            });
        }

        [HttpPost]
        public IActionResult Edit(string id, ProfileEditViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            this.students.Edit(
                id,
                model.Email,
                model.Username,
                model.BirthDate);

            return this.RedirectToAction(nameof(this.Profile));
        }

        private CourseModel GetCourse(int id)
        {
            var user = this.userManager.GetUserId(this.User);
            var course = this.courses.GetCourseById(id);

            var isInCourse = this.courses.IsUserInCourse(user, id);
            if (!isInCourse)
            {
                this.ViewData["Signed"] = "no";
            }

            return course;
        }
    }
}
