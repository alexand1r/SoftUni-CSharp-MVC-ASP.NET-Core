namespace LearningSystem.Services
{
    using System;
    using LearningSystem.Data.Models;

    public interface IStudentService
    {
        User GetUserById(string id);

        void Edit(string id, string email, string username, DateTime birthdate);
    }
}
