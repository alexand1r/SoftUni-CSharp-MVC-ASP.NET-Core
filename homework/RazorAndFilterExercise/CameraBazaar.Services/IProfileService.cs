namespace CameraBazaar.Services
{
    using System.Collections.Generic;
    using CameraBazaar.Data.Models;
    using CameraBazaar.Services.Models;

    public interface IProfileService
    {
        User GetUser(string username);

        User GetUserById(string id);

        void Edit(string id, string email, string password, string phone);

        void LogDate(string username);

        IEnumerable<ProfileModel> All();
    }
}
