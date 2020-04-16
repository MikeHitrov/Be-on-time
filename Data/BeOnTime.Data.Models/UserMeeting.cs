namespace AspNetCoreTemplate.Data.Models
{
    using AspNetCoreTemplate.Data.Common.Models;
    using System.ComponentModel.DataAnnotations;

    public class UserMeeting : BaseDeletableModel<string>
    {
        [Key]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        
        [Key]
        public string MeetingId { get; set; }
        public Meeting Meeting { get; set; }
    }
}
