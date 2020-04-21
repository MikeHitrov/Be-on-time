namespace BeOnTime.Services.Data
{
    using AspNetCoreTemplate.Data.Models;
    using System.Collections.Generic;

    public interface IUsersService
    {
        IEnumerable<ApplicationUser> GetAllUsers();

        ApplicationUser GetUserByUsername(string username);

        ApplicationUser GetUserById(string id);
    }
}
