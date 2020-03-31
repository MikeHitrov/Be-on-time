using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Be_on_time.Data.Models
{
    public class UsersMeetings
    {
        [Key]
        public int UserId { get; set; }
        public User User { get; set; }
        [Key]
        public int MeetingId { get; set; }
        public User Meeting { get; set; }
    }
}
