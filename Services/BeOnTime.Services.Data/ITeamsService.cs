namespace BeOnTime.Services.Data
{
    using AspNetCoreTemplate.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface ITeamsService
    {
        Task AddAsync(string userId, ApplicationUser user, string name, IEnumerable<ApplicationUser> users);
    }
}
