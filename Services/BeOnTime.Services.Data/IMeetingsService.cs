namespace BeOnTime.Services.Data
{
    using AspNetCoreTemplate.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMeetingsService
    {
        Task AddAsync(DateTime meetingStartTime, DateTime meetingEnding, string description, IEnumerable<string> users, string name);
    }
}
