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
    [Authorize(Roles = "Users,Admin")]
    public class UpcomingMeetingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult MeetingInvites()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            var model = new MeetingInvitesViewModel
            {
                UpcomingMeetingViewModels = new List<UpcomingMeetingViewModel>(),
                CreatedInvites = new List<CreatedInviteViewModel>()
            };

            // Meetings the user have been invited to.
            var pendingInvites = db.UsersUpcomingMeetings.Where(m => m.UserId.Id == user.Id && m.Answer == null).Select(x => x.MeetingId).ToList();
            foreach (var item in pendingInvites)
            {
                model.UpcomingMeetingViewModels.Add(new UpcomingMeetingViewModel
                {
                    Id = item.Id,
                    Description = item.Description,
                    Option1 = item.Option1,
                    Option2 = item.Option2,
                    Option3 = item.Option3,
                    Author = item.Author,
                    Duration = item.Duration
                });
            }

            // Meeting invites the user has created.
            var createdUpcomingMeetings = db.UpcomingMeetings.Where(m => m.Author.Id == user.Id).ToList();
            foreach (var item in createdUpcomingMeetings)
            {
                model.CreatedInvites.Add(new CreatedInviteViewModel
                {
                    Id = item.Id,
                    Description = item.Description,
                    Option1 = item.Option1,
                    Option2 = item.Option2,
                    Option3 = item.Option3,
                    Option1Votes = db.UsersUpcomingMeetings.Where(m => m.MeetingId.Id == item.Id && m.Answer == item.Option1 && item.Option1 != null).Count(),
                    Option2Votes = db.UsersUpcomingMeetings.Where(m => m.MeetingId.Id == item.Id && m.Answer == item.Option2 && item.Option2 != null).Count(),
                    Option3Votes = db.UsersUpcomingMeetings.Where(m => m.MeetingId.Id == item.Id && m.Answer == item.Option3 && item.Option3 != null).Count(),
                    TotalInvited = db.UsersUpcomingMeetings.Where(m => m.MeetingId.Id == item.Id).Count(),
                    Author = item.Author,
                    Duration = item.Duration
                });
            }
            //var meetings = db.UpcomingMeetings.Where(m => meetingIds.Contains(m.Id)).ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult MakeSchedulerEvent(int id, DateTime selectedTime)
        {
            var meeting = db.UpcomingMeetings.Include("Author")
                .Where(m => m.Id == id).FirstOrDefault();
            var schedulerEvent = new SchedulerEvent
            {
                Text = meeting.Description,
                StartDate = selectedTime,
                EndDate = selectedTime.AddHours(meeting.Duration.Hours),
                Creator = meeting.Author.Id
            };
            db.SchedulerEvents.Add(schedulerEvent);
            db.UpcomingMeetings.Remove(meeting);
            var result = db.UsersUpcomingMeetings.Where(m => m.MeetingId.Id == meeting.Id);
            foreach (var item in result)
            {
                db.UsersUpcomingMeetings.Remove(item);

            }
            db.SaveChanges();
            return RedirectToAction("MeetingInvites");
        }

        [HttpPost]
        // Reply to meeting invite.
        public ActionResult MeetingInvites(int Id, DateTime SelectedResponse)
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            var invite = db.UsersUpcomingMeetings.Include("MeetingId").Include("UserId")
                .Where(m => m.MeetingId.Id == Id && m.UserId.Id == user.Id).FirstOrDefault();
            invite.Answer = SelectedResponse;

            var meeting = db.UpcomingMeetings.Find(Id);
            meeting.TotalAnswered += 1;

            var invitedUsers = db.UsersUpcomingMeetings.Include("MeetingId").Include("UserId")
                .Where(m => m.MeetingId.Id == Id && m.Answer != null).Count();
            var totalAnswered = db.UpcomingMeetings.Find(Id).TotalAnswered;

            db.SaveChanges();
            return RedirectToAction("MeetingInvites");
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
            // Check if the time options are the same time.
            var options = new List<DateTime?> { model.Option1, model.Option2, model.Option3 };
            for (int i = 0; i < options.Count; i++)
            {
                if ((i + 1) != options.Count)
                {
                    if (options[i] == options[i + 1] && options[i] != null && options[i + 1] != null)
                    {
                        if (ModelState.ContainsKey("Option3"))
                        {
                            ModelState["Option3"].Errors.Clear();
                            ModelState.AddModelError("Option3", "All option times need to be different.");
                        }
                    }
                }
            }

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
            ViewBag.InvitedUsers = new List<SelectListItem>();
            return View(model);
        }
    }
}