namespace BeOnTime.Web.Controllers
{
    using BeOnTime.Services.Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using BeOnTime.Web.ViewModels.Feedbacks;
    using global::AspNetCoreTemplate.Web.Controllers;
    using System.Linq;
    using System.Threading.Tasks;

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
            var user = this.usersService.GetUserByUsername(User.Identity.Name);
            var meetings = this.meetingsService.GetAllOverMeetingsForUser(user.Id);

            ViewBag.Data = inputModel;
            ViewBag.Meetings = meetings;
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(FeedbackInputModel inputModel)
        {
            var meetingTitle = inputModel.MeetingTitle;
            var meeting = this.meetingsService.GetMeetingByTitle(meetingTitle);
            var user = this.usersService.GetUserByUsername(User.Identity.Name);

            await this.feedbackService.AddAsync(user.Id, user, inputModel.Rating, inputModel.Description, meeting.Id, meeting);

            return this.Redirect("/");
        }

        [Authorize]
        public IActionResult GetUserFeedbacks()
        {
            var userId = this.usersService.GetUserByUsername(User.Identity.Name).Id;
            var viewModel = new FeedbackUserViewModel();

            var feedbacks = this.feedbackService.GetUserFeedbacks(userId);

            foreach (var feedback in feedbacks)
            {
                viewModel.Feedbacks.Add(new FeedbackViewModel { 
                    Id = feedback.Id,
                    Rating = feedback.Rating,
                    Description = feedback.Description,
                    Username = this.usersService.GetUserById(feedback.UserId).UserName,
                    MeetingTitle = this.meetingsService.GetMeetingById(feedback.MeetingId).Title,
                    CreatedOn = feedback.CreatedOn,
                });
            }

            return View(viewModel);
        }

        [Authorize]
        public IActionResult GetAllFeedbacks()
        {
            var viewModel = new FeedbackUserViewModel();

            var feedbacks = this.feedbackService.GetAllFeedbacks();

            foreach (var feedback in feedbacks)
            {
                viewModel.Feedbacks.Add(new FeedbackViewModel
                {
                    Id = feedback.Id,
                    Rating = feedback.Rating,
                    Description = feedback.Description,
                    Username = this.usersService.GetUserById(feedback.UserId).UserName,
                    MeetingTitle = this.meetingsService.GetMeetingById(feedback.MeetingId).Title,
                    CreatedOn = feedback.CreatedOn,
                });
            }

            return View(viewModel);
        }

        [Authorize]
        public IActionResult Edit(string id)
        {
            var user = this.usersService.GetUserByUsername(User.Identity.Name);
            var feedback = this.feedbackService.GetFeedbackById(id);
            var meetings = this.meetingsService.GetAllOverMeetingsForUser(user.Id);


            var inputModel = new FeedbackInputModel
            {
                Id = id,
                Rating = feedback.Rating,
                Description = feedback.Description,
                MeetingTitle = this.meetingsService.GetMeetingById(feedback.MeetingId).Title,
            };

            ViewBag.Data = inputModel;
            ViewBag.Meetings = meetings;
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(FeedbackInputModel inputModel)
        {
            await this.feedbackService.Update(inputModel.Rating, inputModel.Description, inputModel.Id);

            return this.Redirect("/Feedbacks/GetUserFeedbacks");
        }

        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            var feedback = this.feedbackService.GetFeedbackById(id);

            await this.feedbackService.Delete(feedback);

            return this.Redirect("/Feedbacks/GetUserFeedbacks");
        }
    }
}