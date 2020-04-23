namespace BeOnTime.Web.ViewModels.Feedbacks
{
    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Services.Mapping;

    public class FeedbackInputModel : IMapTo<Feedback>
    {
        public int UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int Rating { get; set; }

        public string Description { get; set; }
    }
}
