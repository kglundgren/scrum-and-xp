using scrum_and_xp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using scrum_and_xp.ViewModels;

namespace scrum_and_xp.Controllers
{
    public class UpcomingMeetingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult MeetingInvites()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            var meetings = db.UsersUpcomingMeetings.Where(m => m.UserId.Id == user.Id).Select(x => x.MeetingId).ToList();
            var model = new List<UpcomingMeetingViewModel>();
            foreach (var item in meetings)
            {
                model.Add(new UpcomingMeetingViewModel {Id =item.Id, Description = item.Description, Option1 = item.Option1, Option2 = item.Option2, Option3 = item.Option3, Author = item.Author, Duration = item.Duration });
            }
            //var meetings = db.UpcomingMeetings.Where(m => meetingIds.Contains(m.Id)).ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            var model = new CreateUpcomingMeetingViewModel();
            model.AllUsers = db.Users.ToList();
            //ViewBag.AllUsers = new SelectList(db.Users.ToList(), "Id", "Email");
            ViewBag.InvitedUsers = new List<SelectListItem>();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CreateUpcomingMeetingViewModel model, List<string> invitedUsers)
        {
            if (ModelState.IsValid)
            {
                var newMeeting = new UpcomingMeeting()
                {
                    Author = db.Users.Find(User.Identity.GetUserId()),
                    Description = model.Description,
                    Duration = model.Duration,
                    Option1 = model.Option1,
                    Option2 = model.Option2,
                    Option3 = model.Option3
                };
                db.UpcomingMeetings.Add(newMeeting);
                foreach (var id in invitedUsers)
                {
                    var user = db.Users.Find(id);
                    var attendee = new UsersUpcomingMeetings()
                    {
                        UserId = user,
                        MeetingId = newMeeting
                    };
                    db.UsersUpcomingMeetings.Add(attendee);
                }
                
                db.SaveChanges();
                return RedirectToAction("MeetingInvites");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult MeetingInvites(int Id, DateTime SelectedResponse)
        {
            var selectedOption = SelectedResponse;
            var meeting = db.UpcomingMeetings.Find(Id);
            //var meetingId = db.UsersUpcomingMeetings.FirstOrDefault()
            var meetingChange = new UsersUpcomingMeetings() { Answer = SelectedResponse, MeetingId = meeting, UserId = db.Users.Find(User.Identity.GetUserId()) };

            //db.Entry(meeting).CurrentValues.SetValues(meetingChange);
            return RedirectToAction("MeetingInvites");
        }
    }
}