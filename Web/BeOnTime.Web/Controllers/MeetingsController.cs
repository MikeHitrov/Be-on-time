namespace AspNetCoreTemplate.Web.Controllers
{
    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models;
    using BeOnTime.Services.Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using BeOnTime.Web.ViewModels.Meetings;

    public class MeetingsController : BaseController
    {
        private readonly IUsersService usersService;
        private readonly IDeletableEntityRepository<Meeting> repository;

        public MeetingsController(IUsersService usersService, IDeletableEntityRepository<Meeting> repository)
        {
            this.usersService = usersService;
            this.repository = repository;
        }

        [Authorize]
        public IActionResult Add()
        {
            var users = this.usersService.GetAllUsers();
            
            ViewBag.Users = users;
            return View();
        }
    }
}