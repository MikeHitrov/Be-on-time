namespace BeOnTime.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Data;
    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Data.Repositories;

    using Microsoft.EntityFrameworkCore;

    using Moq;

    using Xunit;

    public class FeedbacksServiceTests
    {
        [Fact]
        public async void AddFeedbackCorrectly()
        {
            var dbContext = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var feedbackRepository = new EfDeletableEntityRepository<Feedback>(new ApplicationDbContext(dbContext.Options));

            feedbackRepository.SaveChangesAsync().GetAwaiter().GetResult();

            var service = new FeedbacksService(feedbackRepository);

            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
            };

            var meeting = new Meeting
            {
                Id = Guid.NewGuid().ToString(),
            };

            await service.AddAsync(user.Id, user, 6, "Awesome", meeting.Id, meeting);

            var list = feedbackRepository.All().ToList();
            Assert.Single(list);
            Assert.Equal("Awesome", list[0].Description);
            Assert.Equal(6, list[0].Rating);
        }

        [Fact]
        public async void ListAllFeedbackCorrectly()
        {
            var dbContext = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var feedbackRepository = new EfDeletableEntityRepository<Feedback>(new ApplicationDbContext(dbContext.Options));

            feedbackRepository.SaveChangesAsync().GetAwaiter().GetResult();

            var service = new FeedbacksService(feedbackRepository);

            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
            };

            var meeting = new Meeting
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Test 1",
            };

            var secondMeeting = new Meeting
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Test 2",
            };

            await service.AddAsync(user.Id, user, 6, "Awesome", meeting.Id, meeting);
            await service.AddAsync(user.Id, user, 1, "Poor", secondMeeting.Id, secondMeeting);

            var list = service.GetAllFeedbacks().ToList();
            Assert.Equal(2, list.Count);
        }

        [Fact]
        public async void ListAllUserFeedbackCorrectly()
        {
            var dbContext = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var feedbackRepository = new EfDeletableEntityRepository<Feedback>(new ApplicationDbContext(dbContext.Options));

            feedbackRepository.SaveChangesAsync().GetAwaiter().GetResult();

            var service = new FeedbacksService(feedbackRepository);

            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
            };

            var meeting = new Meeting
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Test 1",
            };

            var secondMeeting = new Meeting
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Test 2",
            };

            await service.AddAsync(user.Id, user, 6, "Awesome", meeting.Id, meeting);
            await service.AddAsync("Not user id", new ApplicationUser { }, 1, "Poor", secondMeeting.Id, secondMeeting);

            var list = service.GetUserFeedbacks(user.Id).ToList();
            Assert.Single(list);
        }

        [Fact]
        public async void GetFeedbackByIdCorrectly()
        {
            var dbContext = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var feedbackRepository = new EfDeletableEntityRepository<Feedback>(new ApplicationDbContext(dbContext.Options));

            feedbackRepository.SaveChangesAsync().GetAwaiter().GetResult();

            var service = new FeedbacksService(feedbackRepository);

            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
            };

            var meeting = new Meeting
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Test 1",
            };

            var secondMeeting = new Meeting
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Test 2",
            };

            await service.AddAsync(user.Id, user, 6, "Awesome", meeting.Id, meeting);
            await service.AddAsync("Not user id", new ApplicationUser { }, 1, "Poor", secondMeeting.Id, secondMeeting);

            var id = feedbackRepository.All().Where(f => f.MeetingId == secondMeeting.Id).First().Id;
            var feedback = service.GetFeedbackById(id);

            Assert.Equal(1, feedback.Rating);
            Assert.Equal("Poor", feedback.Description);
        }

        [Fact]
        public async void UpdateFeedbackCorrectly()
        {
            var dbContext = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var feedbackRepository = new EfDeletableEntityRepository<Feedback>(new ApplicationDbContext(dbContext.Options));

            feedbackRepository.SaveChangesAsync().GetAwaiter().GetResult();

            var service = new FeedbacksService(feedbackRepository);

            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
            };

            var meeting = new Meeting
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Test 1",
            };

            await service.AddAsync(user.Id, user, 6, "Awesome", meeting.Id, meeting);

            var id = feedbackRepository.All().Where(f => f.MeetingId == meeting.Id).First().Id;

            await service.Update(5, "Very good", id);

            var feedback = service.GetFeedbackById(id);

            Assert.Equal(5, feedback.Rating);
            Assert.Equal("Very good", feedback.Description);
        }

        [Fact]
        public async void DeleteFeedbackCorrectly()
        {
            var dbContext = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var feedbackRepository = new EfDeletableEntityRepository<Feedback>(new ApplicationDbContext(dbContext.Options));

            feedbackRepository.SaveChangesAsync().GetAwaiter().GetResult();

            var service = new FeedbacksService(feedbackRepository);

            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
            };

            var meeting = new Meeting
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Test 1",
            };

            await service.AddAsync(user.Id, user, 6, "Awesome", meeting.Id, meeting);

            var id = feedbackRepository.All().Where(f => f.MeetingId == meeting.Id).First().Id;
            var feedback = service.GetFeedbackById(id);

            await service.Delete(feedback);

            var list = service.GetAllFeedbacks();

            Assert.Empty(list);
        }
    }
}
