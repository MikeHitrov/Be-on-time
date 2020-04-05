namespace BeOnTime.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class UserMeeting
    {
        [Key]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        
        [Key]
        public string MeetingId { get; set; }
        public Meeting Meeting { get; set; }
    }
}
