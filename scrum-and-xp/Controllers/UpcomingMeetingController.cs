using scrum_and_xp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace scrum_and_xp.Controllers
{
    public class UpcomingMeetingController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult MeetingInvites()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            var meetings = db.UsersUpcomingMeetings.Where(m => m.UserId.Id == user.Id).Select(x => x.MeetingId).ToList();
            var model = new List<UpcomingMeetingViewModel>();
            foreach (var item in meetings)
            {
                model.Add(new UpcomingMeetingViewModel { Description = item.Description, Option1 = item.Option1, Option2 = item.Option2, Option3 = item.Option3, Author = item.Author, Duration = item.Duration });
            }
            //var meetings = db.UpcomingMeetings.Where(m => meetingIds.Contains(m.Id)).ToList();
            return View(model);
        }
    }
}