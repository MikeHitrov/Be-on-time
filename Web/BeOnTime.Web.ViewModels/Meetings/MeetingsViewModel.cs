namespace BeOnTime.Web.ViewModels.Meetings
{
    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Services.Mapping;
    using System;
    using System.Collections.Generic;

    public class MeetingsViewModel : IMapFrom<Meeting>
    {
        public MeetingsViewModel()
        {
            this.Feedbacks = new HashSet<Feedback>();
        }

        public string Id { get; set; }

        public string OrganiserId { get; set; }

        public ApplicationUser Organiser { get; set; }

        public DateTime MeetingStartTime { get; set; }

        public DateTime MeetingEnding { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Place { get; set; }

        public ICollection<Feedback> Feedbacks { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
