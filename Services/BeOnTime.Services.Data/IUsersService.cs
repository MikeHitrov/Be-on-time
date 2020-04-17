namespace BeOnTime.Services.Data
{
    using AspNetCoreTemplate.Data.Models;
    using System.Collections.Generic;

    public interface IUsersService
    {
        IEnumerable<ApplicationUser> GetAll();
    }
}
