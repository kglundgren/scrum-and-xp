using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace scrum_and_xp.Models
{
    public abstract class SuperCat
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
    }
}