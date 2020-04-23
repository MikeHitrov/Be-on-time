namespace BeOnTime.Services.Data
{
    using AspNetCoreTemplate.Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IFeedbacksService
    {
        Task AddAsync(string userId, ApplicationUser user, int rating, string description, string meetingId, Meeting meeting);

        IEnumerable<Feedback> GetUserFeedbacks(string userId);

        IEnumerable<Feedback> GetAllFeedbacks();
    }
}