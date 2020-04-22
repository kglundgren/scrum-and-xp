using DHTMLX.Scheduler.Data;
using Microsoft.AspNet.Identity;
using scrum_and_xp.App_Start;
using scrum_and_xp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

            return db.SchedulerEvents
                .ToList()
                .Select(e => (WebAPIEvent)e);
               
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
            db.Entry(updatedSchedulerEvent).State = EntityState.Modified;
            db.SaveChanges();

            return Ok(new
            {
                action = "updated"
            });
        }

        // POST: api/scheduler/5
        [HttpPost]
        public IHttpActionResult CreateSchedulerEvent(WebAPIEvent webAPIEvent)
        {
            var CreatorId = db.Users.Find(User.Identity.GetUserId());
            var newSchedulerEvent = (SchedulerEvent)webAPIEvent;
            newSchedulerEvent.CreatorId = CreatorId;

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
                db.SchedulerEvents.Remove(schedulerEvent);
                db.SaveChanges();
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
