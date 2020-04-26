namespace BeOnTime.Data.Models
{
    using AspNetCoreTemplate.Data.Common.Models;

    public class TeamUser : BaseDeletableModel<string>
    {
        public string Username { get; set; }

        public string TeamId { get; set; }
    }
}
