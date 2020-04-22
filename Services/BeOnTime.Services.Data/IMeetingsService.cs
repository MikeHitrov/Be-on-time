namespace BeOnTime.Services.Data
{
    using AspNetCoreTemplate.Data.Models;
    using BeOnTime.Web.ViewModels.Meetings;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMeetingsService
    {
        Task AddAsync(DateTime meetingStartTime, TimeSpan meetingStartHour, DateTime meetingEnding, TimeSpan meetingEndHour, string title, string description, string place, IEnumerable<string> users, string organiserUsername);

        IEnumerable<MeetingsViewModel> GetUserMeetings(string id);

        Meeting GetMeetingById(string id);

        Task UpdateAsync(DateTime meetingStartTime, TimeSpan meetingStartHour, DateTime meetingEnding, TimeSpan meetingEndHour, string title, string description, string place, string id);

        void Delete(string id);
    }
}
