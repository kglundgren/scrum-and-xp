using DHTMLX.Scheduler.Data;
using Microsoft.AspNet.Identity;
using scrum_and_xp.App_Start;
using scrum_and_xp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Mapping;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace scrum_and_xp.Controllers
{
    [Authorize]
    public class SchedulerController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/scheduler
        public IEnumerable<WebAPIEvent> Get()
        {
            List<WebAPIEvent> listan = new List<WebAPIEvent>();

            foreach (var anEvent in db.SchedulerEvents.ToList())
            {
               
                var e = (WebAPIEvent)anEvent;
                e.creator_name = db.Users.Find(anEvent.Creator).FirstName + " " + db.Users.Find(anEvent.Creator).LastName;
                listan.Add(e);   
            }

            return listan;

        }



        // GET: api/scheduler/5
        public WebAPIEvent Get(int id)
        {
            return (WebAPIEvent)db.SchedulerEvents.Find(id);
        }

        // PUT: api/scheduler/5
        [HttpPut]
        public IHttpActionResult EditSchedulerEvent(int id, WebAPIEvent webAPIEvent)
        {
            var updatedSchedulerEvent = (SchedulerEvent)webAPIEvent;
            updatedSchedulerEvent.Id = id;

            if (updatedSchedulerEvent.Creator.Equals(User.Identity.GetUserId()) || User.IsInRole("Admin"))
            {
                db.Entry(updatedSchedulerEvent).State = EntityState.Modified;
                db.SaveChanges();
            }

            else {
                return Ok(new
                {
                    action = "failed" + updatedSchedulerEvent.Creator
                });
            }
            return Ok(new
            {
                action = "updated"
            });
        }

        // POST: api/scheduler/5
        [HttpPost]
        public IHttpActionResult CreateSchedulerEvent(WebAPIEvent webAPIEvent)
        {
            var Creator = User.Identity.GetUserId();
            var newSchedulerEvent = (SchedulerEvent)webAPIEvent;
            newSchedulerEvent.Creator = Creator;

            db.SchedulerEvents.Add(newSchedulerEvent);
            db.SaveChanges();

            return Ok(new
            {
                tid = newSchedulerEvent.Id,
                action = "inserted"
            });
        }

        // DELETE: api/scheduler/5
        [HttpDelete]
        public IHttpActionResult DeleteSchedulerEvent(int id)
        {
            var schedulerEvent = db.SchedulerEvents.Find(id);
            if (schedulerEvent != null)
            {
                if (schedulerEvent.Creator.Equals(User.Identity.GetUserId()) || User.IsInRole("Admin"))
                {
                    db.SchedulerEvents.Remove(schedulerEvent);
                    db.SaveChanges();
                }

                else
                {
                    return Ok(new
                    {
                        action = "failed"
                    });
                }
            }
                return Ok(new
                {
                    action = "deleted"
                });
            
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
