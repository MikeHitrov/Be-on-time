namespace BeOnTime.Web.ViewModels.Feedbacks
{
    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Services.Mapping;

    public class FeedbackInputModel : IMapTo<Feedback>
    {
        public string Id { get; set; }

        public int Rating { get; set; }

        public string Description { get; set; }

        public string MeetingTitle { get; set; }
    }
}
