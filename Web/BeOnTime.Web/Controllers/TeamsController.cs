namespace BeOnTime.Web.Controllers
{
    using BeOnTime.Services.Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using BeOnTime.Web.ViewModels.Teams;
    using AspNetCoreTemplate.Web.Controllers;

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
            var users = this.usersService.GetAllUsers().Where(u => u.UserName != User.Identity.Name).Where(u => u.TeamId == "");
            var inputModel = new TeamInputModel();

            ViewBag.Users = users;
            ViewBag.Data = inputModel;
            return View();
        }
    }
}
