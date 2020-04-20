﻿namespace AspNetCoreTemplate.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using AspNetCoreTemplate.Data.Common.Models;

    public class Meeting : BaseDeletableModel<string>
    {
        public Meeting()
        {
            this.UserMeeting = new HashSet<UserMeeting>();
            this.Feedbacks = new HashSet<Feedback>();
        }

        public string OrganiserId { get; set; }

        public ApplicationUser Organiser { get; set; }

        public virtual ICollection<UserMeeting> UserMeeting { get; set; }

        [Required]
        public DateTime MeetingStartTime { get; set; }

        [Required]
        public DateTime MeetingEnding { get; set; }

        [Required]
        public string Description { get; set; }

        public ICollection<Feedback> Feedbacks { get; set; }
    }
}
