namespace BeOnTime.Services.Data
{
    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models;
    using BeOnTime.Web.ViewModels.Meetings;
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

        public async Task AddAsync(DateTime meetingStartTime, TimeSpan meetingStartHour, DateTime meetingEnding, TimeSpan meetingEndHour, string title, string description, string place, IEnumerable<string> users, string organiserUsername)
        {
            DateTime startTime = new DateTime(meetingStartTime.Year, meetingStartTime.Month, meetingStartTime.Day, meetingStartHour.Hours, meetingStartHour.Minutes, meetingStartHour.Seconds);
            DateTime endTime = new DateTime(meetingEnding.Year, meetingEnding.Month, meetingEnding.Day, meetingEndHour.Hours, meetingEndHour.Minutes, meetingEndHour.Seconds);

            var meeting = new Meeting
            {
                MeetingStartTime = startTime,
                MeetingEnding = endTime,
                Title = title,
                Description = description,
                Place = place,
                OrganiserId = this.usersService.GetUserByUsername(organiserUsername).Id,
                Organiser = this.usersService.GetUserByUsername(organiserUsername),
            };

            meeting.Id = Guid.NewGuid().ToString();

            foreach (var user in users)
            {
                var userMeeting = new UserMeeting
                {
                    UserId = this.usersService.GetUserByUsername(user).Id,
                    User = this.usersService.GetUserByUsername(user),
                    MeetingId = meeting.Id,
                    Meeting = meeting,
                };

                meeting.UserMeeting.Add(userMeeting);
            }

            await this.meetingRepository.AddAsync(meeting);
            await this.meetingRepository.SaveChangesAsync();
        }

        public Meeting GetMeetingById(string Id)
        {
            return this.meetingRepository.All().Where(m => m.Id == Id).FirstOrDefault();
        }

        public IEnumerable<MeetingsViewModel> GetUserMeetings(string id)
        {
            var list = new List<MeetingsViewModel>();

            var meetings = this.userMeetingRepository.All().Where(m => m.UserId == id).ToList();

            foreach (var userMeeeting in meetings)
            {
                var meeting = this.GetMeetingById(userMeeeting.MeetingId);

                list.Add(new MeetingsViewModel
                {
                    Id = meeting.Id,
                    OrganiserId = meeting.OrganiserId,
                    Organiser = this.usersService.GetUserById(meeting.OrganiserId),
                    MeetingStartTime = meeting.MeetingStartTime,
                    MeetingEnding = meeting.MeetingEnding,
                    Title = meeting.Title,
                    Description = meeting.Description,
                    Place = meeting.Place,
                    CreatedOn = meeting.CreatedOn,
                });
            }

            return list;
        }
        public async Task UpdateAsync(DateTime meetingStartTime, TimeSpan meetingStartHour, DateTime meetingEnding, TimeSpan meetingEndHour, string title, string description, string place, string id)
        {
            DateTime startTime = new DateTime(meetingStartTime.Year, meetingStartTime.Month, meetingStartTime.Day, meetingStartHour.Hours, meetingStartHour.Minutes, meetingStartHour.Seconds);
            DateTime endTime = new DateTime(meetingEnding.Year, meetingEnding.Month, meetingEnding.Day, meetingEndHour.Hours, meetingEndHour.Minutes, meetingEndHour.Seconds);

            var meeting = this.GetMeetingById(id);

            meeting.MeetingStartTime = startTime;
            meeting.MeetingEnding = endTime;
            meeting.Title = title;
            meeting.Description = description;
            meeting.Place = place;

            this.meetingRepository.Update(meeting);
            await this.meetingRepository.SaveChangesAsync();
        }

        public void Delete(string id)
        {
            Meeting meeting = this.GetMeetingById(id);
            var userMeetings = this.userMeetingRepository.All().Where(m => m.MeetingId == id).ToList();

            this.meetingRepository.Delete(meeting);
            
            foreach (var userMeeting in userMeetings)
            {
                this.userMeetingRepository.Delete(userMeeting);
            }

            this.userMeetingRepository.SaveChangesAsync();
            this.meetingRepository.SaveChangesAsync();
        }

        public List<Meeting> GetAllOverMeetingsForUser(string id)
        {
            var userMeetings = this.userMeetingRepository
                .All()
                .Where(um => um.UserId == id)
                .Where(um => um.Meeting.MeetingEnding > um.Meeting.MeetingStartTime)
                .Where(um => um.Meeting.IsDeleted == false)
                .ToList();

            var meetings = new List<Meeting>();

            foreach (var meeting in userMeetings)
            {
                meetings.Add(this.GetMeetingById(meeting.MeetingId));
            }

            return meetings;
        }

        public Meeting GetMeetingByTitle(string title)
        {
            return this.meetingRepository.All().Where(m => m.Title == title).First();
        }
    }
}
