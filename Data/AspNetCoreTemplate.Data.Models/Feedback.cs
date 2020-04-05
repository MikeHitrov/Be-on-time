namespace BeOnTime.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Feedback
    {
        public Feedback() {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        public int UserId { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        public int Rating { get; set; }

        public string Description { get; set; }
    }
}
