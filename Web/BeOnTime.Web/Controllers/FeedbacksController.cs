namespace BeOnTime.Web.Controllers
{
    using BeOnTime.Services.Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using BeOnTime.Web.ViewModels.Feedbacks;
    using global::AspNetCoreTemplate.Web.Controllers;
    using System.Linq;

    public class FeedbacksController : BaseController
    {
        private readonly IUsersService usersService;
        private readonly IMeetingsService meetingsService;
        private readonly IFeedbacksService feedbackService;

        public FeedbacksController(IUsersService usersService, IMeetingsService meetingsService, IFeedbacksService feedbackService)
        {
            this.usersService = usersService;
            this.feedbackService = feedbackService;
            this.meetingsService = meetingsService;
        }

        [Authorize]
        public IActionResult Add()
        {
            var inputModel = new FeedbackInputModel();
            var user = usersService.GetUserByUsername(User.Identity.Name);
            var meetings = meetingsService.GetAllOverMeetingsForUser(user.Id);

            ViewBag.Data = inputModel;
            ViewBag.Meetings = meetings;
            return View();
        }
    }
}