namespace BeOnTime.Services.Data
{
    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models;
    using BeOnTime.Web.ViewModels.Meetings;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class MeetingsService : IMeetingsService
    {
        private readonly IDeletableEntityRepository<Meeting> meetingRepository;
        private readonly IDeletableEntityRepository<UserMeeting> userMeetingRepository;
        private readonly IUsersService usersService;

        public MeetingsService(IDeletableEntityRepository<Meeting> meetingRepository, IDeletableEntityRepository<UserMeeting> userMeetingRepository, IUsersService usersService)
        {
            this.meetingRepository = meetingRepository;
            this.userMeetingRepository = userMeetingRepository;
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

        public Meeting GetMeetingById(string Id)
        {
            return meetingRepository.All().Where(m => m.Id == Id).FirstOrDefault();
        }

        public IEnumerable<MeetingsViewModel> GetUserMeetings(string id)
        {
            var list = new List<MeetingsViewModel>();

            var meetings = userMeetingRepository.All().Where(m => m.UserId == id).ToList();

            foreach (var userMeeeting in meetings)
            {
                var meeting = GetMeetingById(userMeeeting.MeetingId);

                list.Add(new MeetingsViewModel
                {
                    OrganiserId = userMeeeting.UserId,
                    Organiser = userMeeeting.User,
                    MeetingStartTime = meeting.MeetingStartTime,
                    MeetingEnding = meeting.MeetingEnding,
                    Description = meeting.Description,
                    Feedbacks = meeting.Feedbacks,
                    CreatedOn = meeting.CreatedOn
                });
            }

            return list;
        }

    }
}
