namespace BeOnTime.Web.ViewModels.Meetings
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class UserMeetingsViewModel
    {
        public UserMeetingsViewModel(IEnumerable<MeetingsViewModel> meetings)
        {
            this.Meetings = meetings;
        }

        public IEnumerable<MeetingsViewModel> Meetings { get; set; }
    }
}
