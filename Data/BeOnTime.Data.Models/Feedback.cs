namespace AspNetCoreTemplate.Data.Models
{
    using AspNetCoreTemplate.Data.Common.Models;
    using System.ComponentModel.DataAnnotations;

    public class Feedback : BaseDeletableModel<string>
    {
        public Feedback() {}

        public int UserId { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        public int Rating { get; set; }

        public string Description { get; set; }
    }
}
