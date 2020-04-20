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
        private readonly IUsersService usersService;

        public MeetingsService(IDeletableEntityRepository<Meeting> meetingRepository, IUsersService usersService)
        {
            this.meetingRepository = meetingRepository;
            this.usersService = usersService;
        }

        public async Task AddAsync(DateTime meetingStartTime, DateTime meetingEnding, string description, IEnumerable<string> users, string organiserUsername)
        {
            var meeting = new Meeting
            {
                MeetingStartTime = meetingStartTime,
                MeetingEnding = meetingEnding,
                Description = description,
                OrganiserId = usersService.GetUserByUsername(organiserUsername).Id,
                Organiser = usersService.GetUserByUsername(organiserUsername)
            };

            meeting.Id = Guid.NewGuid().ToString();

            foreach (var user in users)
            {
                var userMeeting = new UserMeeting
                {
                    UserId = usersService.GetUserByUsername(user).Id,
                    User = usersService.GetUserByUsername(user),
                    MeetingId = meeting.Id,
                    Meeting = meeting
                };

                meeting.UserMeeting.Add(userMeeting);
            }

            await this.meetingRepository.AddAsync(meeting);
            await this.meetingRepository.SaveChangesAsync();
        }
    }
}
