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
        public void AddMeetingCorrectly()
        {
            var dbContext = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var feedbackRepository = new EfDeletableEntityRepository<Feedback>(new ApplicationDbContext(dbContext.Options));

            feedbackRepository.SaveChangesAsync().GetAwaiter().GetResult();

            var service = new FeedbacksService(feedbackRepository);

            service.AddAsync(Guid.NewGuid().ToString(), new ApplicationUser(), 6, "Awesome", Guid.NewGuid().ToString(), new Meeting());

            var list = feedbackRepository.All().ToList();
            Assert.Single(list);
            Assert.Equal("Awesome", list[0].Description);
            Assert.Equal(6, list[0].Rating);
        }
    }
}
