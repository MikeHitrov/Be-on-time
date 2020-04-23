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

    public class MeetingsServiceTest
    {
        [Fact]
        public void AddMeetingCorrectly()
        {
            var dbContext = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var meetingsRepository = new EfDeletableEntityRepository<Meeting>(new ApplicationDbContext(dbContext.Options));

            meetingsRepository.SaveChangesAsync().GetAwaiter().GetResult();

            var service = new MeetingsService(meetingsRepository);

            service.AddAsync(new DateTime(2020, 5, 5), new TimeSpan(12, 00, 00), new DateTime(2020, 5, 5), new TimeSpan(13, 00, 00), "Test title", "Test description", "Test place", new List<string>(), "mike");

            var list = meetingsRepository.All().ToList();
            Assert.Single(list);
            Assert.Equal("Test title", list[0].Title);
            Assert.Equal("Test description", list[0].Description);
            Assert.Equal("Test place", list[0].Place);
            Assert.Equal(new DateTime(2020,5,5,12,0,0), list[0].MeetingStartTime);
            Assert.Equal(new DateTime(2020, 5, 5, 13, 0, 0), list[0].MeetingEnding);
        }
    }
}
