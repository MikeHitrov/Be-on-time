using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Be_on_time.Data.Models
{
    public class User
    {
        public User()
        {
            this.UsersMeetings = new List<UsersMeetings>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20), MinLength(5)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public List<UsersMeetings> UsersMeetings { get; set; }
    }
}
