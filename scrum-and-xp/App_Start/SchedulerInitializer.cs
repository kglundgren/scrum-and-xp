using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using scrum_and_xp.Models;

namespace scrum_and_xp.App_Start
{
    public class SchedulerInitializer : DropCreateDatabaseIfModelChanges<SchedulerContext>
    {
        protected override void Seed(SchedulerContext context)
        {
            List<SchedulerEvent> events = new List<SchedulerEvent>()
            {
                new SchedulerEvent()
                {
                    Id = 1,
                    Text = "Event 1",
                    StartDate = new DateTime(2020, 1, 15, 2, 0, 0),
                    EndDate = new DateTime(2020, 1, 15, 4, 0, 0)
                },
                new SchedulerEvent()
                {
                    Id = 2,
                    Text = "Event 2",
                    StartDate = new DateTime(2020, 1, 17, 3, 0, 0),
                    EndDate = new DateTime(2020, 1, 17, 6, 0, 0)
                },
                new SchedulerEvent()
                {
                    Id = 3,
                    Text = "Multiday event",
                    StartDate = new DateTime(2020, 1, 15, 0, 0, 0),
                    EndDate = new DateTime(2020, 1, 20, 0, 0, 0)
                }
            };

            events.ForEach(s => context.SchedulerEvents.Add(s));
            context.SaveChanges();

        }

    }
}
