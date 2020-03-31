using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Be_on_time.Data.Models
{
    public class Feedback
    {
        public Feedback(){}

        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        [Required]
        public int Rating { get; set; }

        public string Description { get; set; }
    }
}
