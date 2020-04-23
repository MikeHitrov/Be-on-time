namespace BeOnTime.Services.Data
{
    using AspNetCoreTemplate.Data.Models;
    using System.Threading.Tasks;

    public interface IFeedbacksService
    {
        Task AddAsync(string userId, ApplicationUser user, int rating, string description);
    }
}