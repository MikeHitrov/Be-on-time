namespace BeOnTime.Web.Controllers
{
    using BeOnTime.Services.Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using BeOnTime.Web.ViewModels.Teams;
    using AspNetCoreTemplate.Web.Controllers;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Primitives;
    using System.Collections.Generic;
    using AspNetCoreTemplate.Data.Models;

    public class TeamsController : BaseController
    {
        private readonly IUsersService usersService;
        private readonly ITeamsService teamsService;

        public TeamsController(IUsersService usersService, ITeamsService teamsService)
        {
            this.usersService = usersService;
            this.teamsService = teamsService;
        }

        [Authorize]
        public IActionResult Add()
        {
            var users = this.usersService.GetAllUsers().Where(u => u.UserName != User.Identity.Name).Where(u => u.TeamId == null);
            var inputModel = new TeamInputModel();

            ViewBag.Users = users;
            ViewBag.Data = inputModel;
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(TeamInputModel inputModel)
        {
            var user = usersService.GetUserByUsername(User.Identity.Name);

            await this.teamsService.AddAsync(user.Id, user, inputModel.TeamName, inputModel.Users);

            return this.Redirect("/");
        }

        [Authorize]
        public IActionResult GetUserTeam()
        {
            var user = this.usersService.GetUserByUsername(User.Identity.Name);
            var team = this.teamsService.GetTeamByUser(user.UserName);
            var viewModel = new UserTeamViewModel();

            if (team != null)
            {
                viewModel.Id = team.Id;
                viewModel.isTeam = true;
                viewModel.ManagerName = this.usersService.GetUserById(team.ManagerId).UserName;
                viewModel.TeamName = team.TeamName;
                viewModel.MembersCount = team.Users.ToList().Count;
            }
            else
            {
                viewModel.isTeam = false;
            }
        
            return View(viewModel);
        }

        [Authorize]
        public IActionResult Edit(string id)
        {
            var team = this.teamsService.GetTeamById(id);
            var usersList = this.usersService.GetAllUsers().Where(u => u.UserName != User.Identity.Name).Where(u => u.TeamId == null);
            List<string> users = new List<string>();

            foreach (var user in usersList)
            {
                users.Add(user.UserName);
            }

            var inputModel = new TeamInputModel
            {
                Id = team.Id,
                TeamName = team.TeamName,
            };

            ViewBag.Users = users;
            ViewBag.Data = inputModel;
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(TeamInputModel inputModel)
        {
            await this.teamsService.UpdateAsync(inputModel.Id, inputModel.TeamName, inputModel.Users);

            return this.Redirect("/Teams/GetUserTeam");
        }

        [Authorize]
        public IActionResult Delete(string id)
        {
            this.teamsService.Delete(id);

            return this.Redirect("/Teams/GetUserTeam");
        }
    }
}
