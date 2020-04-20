namespace AspNetCoreTemplate.Web.Controllers
{
    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models;
    using BeOnTime.Services.Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using BeOnTime.Web.ViewModels.Meetings;
    using System.Linq;
    using System.Threading.Tasks;
    using System;

    public class MeetingsController : BaseController
    {
        private readonly IUsersService usersService;
        private readonly IMeetingsService meetingsService;

        public MeetingsController(IUsersService usersService, IMeetingsService meetingsService)
        {
            this.usersService = usersService;
            this.meetingsService = meetingsService;
        }

        [Authorize]
        public IActionResult Add()
        {
            var users = this.usersService.GetAllUsers().Where(u => u.UserName != User.Identity.Name);
            var inputModel = new MeetingInputModel();

            ViewBag.Users = users;
            ViewBag.Data = inputModel;
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(MeetingInputModel inputModel)
        {
            DateTime dateNow = DateTime.UtcNow;   
            if (inputModel.MeetingStartTime < dateNow
                || inputModel.MeetingEnding < dateNow
                || inputModel.MeetingEnding < inputModel.MeetingStartTime
                || !inputModel.Users.Any())
            {
                ViewBag.Users = this.usersService.GetAllUsers().Where(u => u.UserName != User.Identity.Name);
                ViewBag.Data = inputModel;
                return this.View(inputModel);
            }

            Console.WriteLine(inputModel);
            await this.meetingsService.AddAsync(inputModel.MeetingStartTime, inputModel.MeetingEnding, inputModel.Description, inputModel.Users, (string)User.Identity.Name);

            return this.Redirect("/");
        }
    }
}