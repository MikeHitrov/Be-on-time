namespace AspNetCoreTemplate.Web.Areas.Administration.Controllers
{
    using AspNetCoreTemplate.Web.ViewModels.Administration.Dashboard;
    using BeOnTime.Services.Data;
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : AdministrationController
    {
        private readonly IFeedbacksService feedbacksService;

        public DashboardController(IFeedbacksService feedbacksService)
        {
            this.feedbacksService = feedbacksService;
        }

        public IActionResult Index()
        {
            return this.View();
        }
    }
}
