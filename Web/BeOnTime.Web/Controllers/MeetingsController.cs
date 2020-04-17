namespace AspNetCoreTemplate.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Services.Data;
    using BeOnTime.Services.Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

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
            var users = usersService.GetAll();
            return View(users);
        }
    }
}