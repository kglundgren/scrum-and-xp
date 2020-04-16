using scrum_and_xp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace scrum_and_xp.Controllers
{
    public class CatFilterApiController : ApiController
    {
        private readonly ApplicationDbContext _dbContext;

        public CatFilterApiController()
        {
            _dbContext = new ApplicationDbContext();
        }

        [HttpGet]
        public IEnumerable<Category> List(string chosenCat)
        {
            var lista = new CategorySubViewModel() { sub = _dbContext.Categories.Where(c => c.Type.Equals(chosenCat)).ToList() };
            return lista.sub;
        }
        
    }

}

    

