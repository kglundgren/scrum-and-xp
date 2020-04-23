using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace scrum_and_xp.Models
{
    public class UsersUpcomingMeetings
    {
        public ApplicationUser User { get; set; }
        public UpcomingMeeting Meeting { get; set; }
        public DateTime Answer { get; set; }
    }
}