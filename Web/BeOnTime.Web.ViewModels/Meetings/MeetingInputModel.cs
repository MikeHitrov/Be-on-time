namespace BeOnTime.Web.ViewModels.Meetings
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Services.Mapping;

    public class MeetingInputModel : IMapTo<Meeting>
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime MeetingStartTime { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan MeetingStartHour { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime MeetingEnding { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan MeetingEndHour { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Place { get; set; }

        [Required]
        public string Description { get; set; }

        public IEnumerable<string> Users { get; set; }
    }
}