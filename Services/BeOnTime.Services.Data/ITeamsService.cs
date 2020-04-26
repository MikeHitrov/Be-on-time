namespace BeOnTime.Services.Data
{
    using AspNetCoreTemplate.Data.Models;
    using BeOnTime.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface ITeamsService
    {
        Task AddAsync(string userId, ApplicationUser user, string name, IEnumerable<string> users);

        Team GetTeamByUser(string user);

        Team GetTeamById(string id);

        Task UpdateAsync(string id, string name, IEnumerable<string> users);

        Task Delete(string id);
    }
}
