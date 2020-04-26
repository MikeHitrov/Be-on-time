namespace BeOnTime.Web.ViewModels.Teams
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class TeamInputModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string TeamName { get; set; }

        public IEnumerable<string> Users { get; set; }
    }
}
