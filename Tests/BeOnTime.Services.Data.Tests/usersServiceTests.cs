namespace BeOnTime.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Data;
    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Migrations;
    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Data.Repositories;
    using BeOnTime.Data.Models;
    using Microsoft.EntityFrameworkCore;

    using Moq;

    using Xunit;

    public class UsersServiceTests
    {
        public List<ApplicationUser> GetTestUsersProfile()
        {
            return new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    Id = "MikelId",
                    UserName = "Mikel",
                    Email = "mike.hitrov@abv.bg",
                },
                new ApplicationUser
                {
                    Id = "StamatId",
                    UserName = "Stamat",
                    Email = "stamat.hitrov@abv.bg",
                },
                new ApplicationUser
                {
                    Id = "GeorgiId",
                    UserName = "Georgi",
                    Email = "georgi.hitrov@abv.bg",
                },
            };
        }

        [Fact]
        public void GetAllUsersCorrectNumber()
        {
            var repository = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            repository.Setup(f => f.All())
                .Returns(this.GetTestUsersProfile()
                .AsQueryable());

            var service = new UsersService(repository.Object);
            var list = service.GetAllUsers().ToList();

            Assert.Equal(3, list.Count());
        }

        [Fact]
        public void GetUserByUsernameCorrectly()
        {
            var repository = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            repository.Setup(f => f.All())
                .Returns(this.GetTestUsersProfile()
                .AsQueryable());

            var service = new UsersService(repository.Object);
            var user = service.GetUserByUsername("Georgi");

            Assert.Equal("GeorgiId", this.GetTestUsersProfile().Where(u => u.UserName == "Georgi").First().Id);
        }

        [Fact]
        public void GetUserByIdCorrectly()
        {
            var repository = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            repository.Setup(f => f.All())
                .Returns(this.GetTestUsersProfile()
                .AsQueryable());

            var service = new UsersService(repository.Object);
            var user = service.GetUserById("StamatId");

            Assert.Equal("Stamat", user.UserName);
        }

        [Fact]
        public async void UpdateTeamCorrectly()
        {
            var repository = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            repository.Setup(f => f.All())
                .Returns(this.GetTestUsersProfile()
                .AsQueryable());

            var service = new UsersService(repository.Object);

            var user = service.GetUserByUsername("Georgi");

            Assert.Equal(null, user.TeamId);

            BeOnTime.Data.Models.Team team = new BeOnTime.Data.Models.Team
            {
                Id = Guid.NewGuid().ToString(),
                ManagerId = user.Id,
                Manager = user,
                TeamName = "Happy team",
            };

            await service.UpdateTeam(team.Id, team, user.Id);

            Assert.Equal(team.Id, user.TeamId);
        }
    }
}