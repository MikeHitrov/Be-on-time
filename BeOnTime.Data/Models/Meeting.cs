using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Be_on_time.Data.Models
{
    public class Meeting
    {
        public Meeting()
        {
            this.UsersMeetings = new List<UsersMeetings>();
            this.Feedbacks = new List<Feedback>();
        }

        [Key]
        public int Id { get; set; }

        public int OrganiserId { get; set; }

        public User Organiser { get; set; }

        public List<UsersMeetings> UsersMeetings { get; set; }

        [Required]
        public DateTime MeetingStartTime { get; set; }

        [Required]
        public DateTime MeetingDuration { get; set; }

        [Required]
        public string Description { get; set; }

        public List<Feedback> Feedbacks { get; set; }
    }
}
