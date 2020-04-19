namespace BeOnTime.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMeetingsService
    {
        Task<string> AddAsync(DateTime meetingStart, DateTime meetingEnd, string description, IEnumerable<string> users);
    }
}
