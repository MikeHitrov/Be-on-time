namespace BeOnTime.Data.Models
{
    using AspNetCoreTemplate.Data.Common.Models;
    using AspNetCoreTemplate.Data.Models;
    using System.Collections.Generic;

    public class Team : BaseDeletableModel<string>
    {
        public Team()
        {
            this.Users = new HashSet<TeamUser>();
        }

        public string ManagerId { get; set; }

        public ApplicationUser Manager { get; set; }

        public string TeamName { get; set; }

        public ICollection<TeamUser> Users { get; set; }
    }
}
