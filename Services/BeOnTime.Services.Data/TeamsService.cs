namespace BeOnTime.Services.Data
{
    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models;
    using BeOnTime.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TeamsService : ITeamsService
    {
        private readonly IDeletableEntityRepository<Team> teamRepository;
        private readonly IUsersService usersService;

        public TeamsService(IDeletableEntityRepository<Team> teamRepository, IUsersService usersService)
        {
            this.teamRepository = teamRepository;
            this.usersService = usersService;
        }

        public async Task AddAsync(string userId, ApplicationUser user, string name, IEnumerable<ApplicationUser> users)
        {
            Team team = new Team
            {
                Id = Guid.NewGuid().ToString(),
                ManagerId = userId,
                Manager = user,
                TeamName = name,
                Users = users,
                CreatedOn = DateTime.Now,
            };

            users.ToList().Add(team.Manager);

            foreach (var us in users)
            {
                await this.usersService.UpdateTeam(team.Id, team);
            }

            await this.teamRepository.AddAsync(team);
            await this.teamRepository.SaveChangesAsync();
        }
    }
}
