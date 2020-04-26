namespace BeOnTime.Services.Data
{
    using AspNetCoreTemplate.Data.Models;
    using BeOnTime.Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUsersService
    {
        IEnumerable<ApplicationUser> GetAllUsers();

        ApplicationUser GetUserByUsername(string username);

        ApplicationUser GetUserById(string id);

        Task UpdateTeam(string teamId, Team team, string userId);
    }
}
