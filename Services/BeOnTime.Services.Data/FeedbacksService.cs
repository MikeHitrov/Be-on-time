﻿namespace BeOnTime.Services.Data
{
    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class FeedbacksService : IFeedbacksService
    {
        private readonly IDeletableEntityRepository<Feedback> feedbackRepository;

        public FeedbacksService(IDeletableEntityRepository<Feedback> feedbackRepository)
        {
            this.feedbackRepository = feedbackRepository;
        }

        public async Task AddAsync(string userId, ApplicationUser user, int rating, string description, string meetingId, Meeting meeting)
        {
            Feedback feedback = new Feedback
            {
                UserId = userId,
                User = user,
                Rating = rating,
                Description = description,
                MeetingId = meetingId,
                Meeting = meeting,
                CreatedOn = DateTime.Now,
            };

            feedback.Id = Guid.NewGuid().ToString();

            await this.feedbackRepository.AddAsync(feedback);
            await this.feedbackRepository.SaveChangesAsync();
        }

        public async Task Delete(Feedback feedback)
        {
            this.feedbackRepository.Delete(feedback);
            await this.feedbackRepository.SaveChangesAsync();
        }

        public IEnumerable<Feedback> GetAllFeedbacks()
        {
            return this.feedbackRepository.All().ToList();
        }

        public Feedback GetFeedbackById(string id)
        {
            return this.feedbackRepository.All().Where(f => f.Id == id).First();
        }

        public IEnumerable<Feedback> GetUserFeedbacks(string userId)
        {
            return this.feedbackRepository.All().Where(f => f.UserId == userId).ToList();
        }

        public async Task Update(int rating, string description, string id)
        {
            var feedback = this.GetFeedbackById(id);

            feedback.Rating = rating;
            feedback.Description = description;

            this.feedbackRepository.Update(feedback);
            await this.feedbackRepository.SaveChangesAsync();
        }
    }
}
