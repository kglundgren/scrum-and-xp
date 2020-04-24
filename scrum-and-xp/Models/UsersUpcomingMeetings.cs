using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace scrum_and_xp.Models
{
    public class UsersUpcomingMeetings
    {
        public int Id { get; set; }
        public ApplicationUser UserId { get; set; }
        public UpcomingMeeting MeetingId { get; set; }
        public DateTime? Answer { get; set; }
    }
}