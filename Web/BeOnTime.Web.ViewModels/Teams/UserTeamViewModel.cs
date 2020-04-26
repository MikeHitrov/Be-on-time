namespace BeOnTime.Web.ViewModels.Teams
{
    using BeOnTime.Data.Models;

    public class UserTeamViewModel
    {
        public bool isTeam { get; set; }

        public string Id { get; set; }

        public string TeamName { get; set; }

        public string ManagerName { get; set; }

        public int MembersCount { get; set; }
    }
}
