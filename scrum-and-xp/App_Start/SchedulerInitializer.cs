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
                    Id = 10,
                    Text = "Event asdf",
                    StartDate = new DateTime(2020, 4, 22, 2, 0, 0),
                    EndDate = new DateTime(2020, 4, 22, 4, 0, 0)
                },
                new SchedulerEvent()
                {
                    Id = 2,
                    Text = "Event 2",
                    StartDate = new DateTime(2020, 4, 24, 3, 0, 0),
                    EndDate = new DateTime(2020, 4, 24, 6, 0, 0)
                },
                new SchedulerEvent()
                {
                    Id = 3,
                    Text = "Multiday event",
                    StartDate = new DateTime(2020, 5, 15, 0, 0, 0),
                    EndDate = new DateTime(2020, 5, 20, 0, 0, 0)
                }
            };

            events.ForEach(s => context.SchedulerEvents.Add(s));
            context.SaveChanges();

        }

    }
}
