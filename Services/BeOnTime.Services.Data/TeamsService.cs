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
        private readonly IDeletableEntityRepository<TeamUser> teamUsersRepository;
        private readonly IUsersService usersService;

        public TeamsService(IDeletableEntityRepository<Team> teamRepository, IUsersService usersService, IDeletableEntityRepository<TeamUser> teamUsersRepository)
        {
            this.teamRepository = teamRepository;
            this.usersService = usersService;
            this.teamRepository = teamRepository;
        }

        public async Task AddAsync(string userId, ApplicationUser user, string name, IEnumerable<string> users)
        {
            var userList = new HashSet<TeamUser>();

            Team team = new Team
            {
                ManagerId = userId,
                Manager = user,
                TeamName = name,
                CreatedOn = DateTime.Now,
            };

            team.Id = Guid.NewGuid().ToString();

            foreach (var usrname in users)
            {
                userList.Add(new TeamUser
                {
                    Id = Guid.NewGuid().ToString(),
                    Username = usrname,
                    TeamId = team.Id,
                });
            }

            userList.Add(new TeamUser
            {
                Id = Guid.NewGuid().ToString(),
                Username = this.usersService.GetUserById(userId).UserName,
                TeamId = team.Id,
            });

            team.Users = userList;

            foreach (var us in userList)
            {
                await this.usersService.UpdateTeam(team.Id, team, this.usersService.GetUserByUsername(us.Username).Id);
            }

            await this.teamRepository.AddAsync(team);
            await this.teamRepository.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var team = this.GetTeamById(id);

            foreach (var user in team.Users)
            {
                await Task.Run(() => this.teamUsersRepository.Delete(user));
                await this.teamUsersRepository.SaveChangesAsync();

                await this.usersService.UpdateTeam(null, team, this.usersService.GetUserByUsername(user.Username).Id);
            }

            await this.usersService.UpdateTeam(null, team, team.ManagerId);

            await Task.Run(() => this.teamRepository.Delete(team));
            await this.teamRepository.SaveChangesAsync();
        }

        public Team GetTeamById(string id)
        {
            return this.teamRepository.All().Where(t => t.Id == id).First();
        }

        public Team GetTeamByUser(string username)
        {
            var teamUsers = this.teamRepository.All().ToList();

            foreach (var team in teamUsers)
            {
                foreach (var user in team.Users)
                {
                    if (user.Username == username)
                        return team;
                }
            }

            return null;
        }

        public async Task UpdateAsync(string id, string name, IEnumerable<string> users)
        {
            var team = this.GetTeamById(id);

            team.TeamName = name;

            var userList = team.Users.ToList();

            foreach (var username in users)
            {
                var user = this.usersService.GetUserByUsername(username);

                if(user.TeamId == null)
                {
                    await Task.Run(() => this.usersService.UpdateTeam(id, team, user.Id));
                    userList.Add(new TeamUser {
                        Username = user.UserName,
                        TeamId = user.TeamId,
                    });
                }
            }

            team.Users = userList;

            await Task.Run(() => this.teamRepository.Update(team));
            await this.teamRepository.SaveChangesAsync();
        }
    }
}
