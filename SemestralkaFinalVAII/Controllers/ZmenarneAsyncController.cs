using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SemestralkaFinalVAII.Data;
using SemestralkaVAII.Models;

namespace SemestralkaFinalVAII.Controllers {

    [Route("api/[controller]")]
    public class ZmenarneAsyncController : Controller {
        private readonly ApplicationDbContext Context;

        public ZmenarneAsyncController(ApplicationDbContext context) {
            Context = context;
        }

        [HttpDelete("{id}")]
        public void Delete(string id) {
            ZmenarenData zmenaren = Context.Zmenarne.SingleOrDefault(temp => temp.Id == id);
            if (zmenaren != null) {
                Context.Zmenarne.Remove(zmenaren);
                Context.SaveChanges();
            }
        }
    }
}
