namespace BeOnTime.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Data;
    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Data.Repositories;

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
                    UserName = "Mikel",
                    Email = "mike.hitrov@abv.bg",
                },
                new ApplicationUser
                {
                   UserName = "Stamat",
                    Email = "stamat.hitrov@abv.bg",
                },
                new ApplicationUser
                {
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

            Assert.Equal(this.GetTestUsersProfile().Where(u => u.UserName == "Georgi").First(), user);
        }
    }
}