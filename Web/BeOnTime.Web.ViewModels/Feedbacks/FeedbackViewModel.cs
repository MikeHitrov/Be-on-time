namespace BeOnTime.Web.ViewModels.Feedbacks
{
    using System;

    public class FeedbackViewModel
    {
        public string Id { get; set; }

        public int Rating { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
