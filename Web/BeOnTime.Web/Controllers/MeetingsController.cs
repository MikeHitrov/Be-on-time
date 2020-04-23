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
    using System.Collections.Generic;

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

            var users = inputModel.Users.ToList();
            users.Add(User.Identity.Name);

            await this.meetingsService.AddAsync(inputModel.MeetingStartTime, inputModel.MeetingStartHour, inputModel.MeetingEnding, inputModel.MeetingEndHour, inputModel.Title, inputModel.Description, inputModel.Place, users, (string)User.Identity.Name);

            return this.Redirect("/");
        }

        [Authorize]
        public async Task<IActionResult> GetUserMeetings()
        {
            var user = this.usersService.GetUserByUsername(this.User.Identity.Name);

            var meetings = this.meetingsService.GetUserMeetings(user.Id);

            UserMeetingsViewModel viewModel = new UserMeetingsViewModel(meetings);

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Edit(string id)
        {
            var meeting = this.meetingsService.GetMeetingById(id);
            var usersList = this.usersService.GetAllUsers().Where(u => u.UserName != User.Identity.Name);
            List<string> users = new List<string>();

            foreach (var user in usersList)
            {
                users.Add(user.UserName);
            }

            var startTime = new DateTime(meeting.MeetingStartTime.Year, meeting.MeetingStartTime.Month, meeting.MeetingStartTime.Day);
            var startHour = new TimeSpan(meeting.MeetingStartTime.Hour, meeting.MeetingStartTime.Minute, meeting.MeetingStartTime.Second);
            var endTime = new DateTime(meeting.MeetingEnding.Year, meeting.MeetingEnding.Month, meeting.MeetingEnding.Day);
            var endHour = new TimeSpan(meeting.MeetingEnding.Hour, meeting.MeetingEnding.Minute, meeting.MeetingEnding.Second);

            var inputModel = new MeetingInputModel
            { 
                Id = meeting.Id,
                Users = users,
                MeetingStartTime = startTime,
                MeetingStartHour = startHour,
                MeetingEnding = endTime,
                MeetingEndHour = endHour,
                Title = meeting.Title,
                Description = meeting.Description,
                Place = meeting.Place,
            };

            ViewBag.Users = users;
            ViewBag.Data = inputModel;
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(MeetingInputModel inputModel)
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

            var users = inputModel.Users.ToList();
            users.Add(User.Identity.Name);

            await this.meetingsService.UpdateAsync(inputModel.MeetingStartTime, inputModel.MeetingStartHour, inputModel.MeetingEnding, inputModel.MeetingEndHour, inputModel.Title, inputModel.Description, inputModel.Place, inputModel.Id);

            return this.Redirect("/Meetings/GetUserMeetings");
        }

        [Authorize]
        public IActionResult Delete(string id)
        {
            var meeting = this.meetingsService.GetMeetingById(id);

            this.meetingsService.Delete(id);

            return this.Redirect("/Meetings/GetUserMeetings");
        }
    }
}