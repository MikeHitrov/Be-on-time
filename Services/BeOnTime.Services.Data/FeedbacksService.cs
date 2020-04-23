namespace BeOnTime.Services.Data
{
    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models;
    using System;
    using System.Threading.Tasks;

    public class FeedbacksService : IFeedbacksService
    {
        private readonly IDeletableEntityRepository<Feedback> feedbackRepository;

        public FeedbacksService(IDeletableEntityRepository<Feedback> feedbackRepository)
        {
            this.feedbackRepository = feedbackRepository;
        }

        public async Task AddAsync(string userId, ApplicationUser user, int rating, string description)
        {
            Feedback feedback = new Feedback
            {
                UserId = userId,
                User = user,
                Rating = rating,
                Description = description,
            };

            feedback.Id = Guid.NewGuid().ToString();

            await this.feedbackRepository.AddAsync(feedback);
            await this.feedbackRepository.SaveChangesAsync();
        }
    }
}
