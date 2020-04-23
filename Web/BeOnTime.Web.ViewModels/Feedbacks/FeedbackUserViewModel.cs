namespace BeOnTime.Web.ViewModels.Feedbacks
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class FeedbackUserViewModel
    {
        public FeedbackUserViewModel()
        {
            this.Feedbacks = new List<FeedbackViewModel>();
        }

        public List<FeedbackViewModel> Feedbacks { get; set; }
    }
}
