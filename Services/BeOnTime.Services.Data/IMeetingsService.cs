namespace BeOnTime.Services.Data
{
    using AspNetCoreTemplate.Data.Models;
    using BeOnTime.Web.ViewModels.Meetings;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMeetingsService
    {
        Task AddAsync(DateTime meetingStartTime, DateTime meetingEnding, string description, IEnumerable<string> users, string name);

        IEnumerable<MeetingsViewModel> GetUserMeetings(string id);

        Meeting GetMeetingById(string Id);
    }
}
