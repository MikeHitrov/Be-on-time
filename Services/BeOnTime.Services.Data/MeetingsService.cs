namespace BeOnTime.Services.Data
{
    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public class MeetingsService : IMeetingsService
    {
        private readonly IDeletableEntityRepository<Meeting> meetingRepository;

        public MeetingsService(IDeletableEntityRepository<Meeting> meetingRepository)
        {
            this.meetingRepository = meetingRepository;
        }
        public async Task<string> AddAsync(DateTime meetingStart, DateTime meetingEnd, string description, IEnumerable<string> users)
        {
            var meeting = new Meeting
            {
                MeetingStartTime = meetingStart,
                MeetingEnding = meetingEnd,
                Description = description,
            };

            await this.meetingRepository.AddAsync(meeting);
            await this.meetingRepository.SaveChangesAsync();
            return meeting.Id;
        }
    }
}
