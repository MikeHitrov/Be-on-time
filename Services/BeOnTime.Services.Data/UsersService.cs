namespace BeOnTime.Services.Data
{
    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Services.Mapping;
    using BeOnTime.Data.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        public UsersService(IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            return this.usersRepository.All().ToList();
        }

        public ApplicationUser GetUserByUsername(string username)
        { 
            return this.usersRepository.All().Where(u => u.UserName == username).First();
        }

        public ApplicationUser GetUserById(string id)
        {
            return this.usersRepository.All().Where(u => u.Id == id).FirstOrDefault();
        }

        public async Task UpdateTeam(string teamId, Team team, string userId)
        {
            var user = this.GetUserById(userId);

            user.TeamId = teamId;
            user.Team = team;

            await Task.Run(() => this.usersRepository.Update(user));
            await this.usersRepository.SaveChangesAsync();
        }
    }
}
