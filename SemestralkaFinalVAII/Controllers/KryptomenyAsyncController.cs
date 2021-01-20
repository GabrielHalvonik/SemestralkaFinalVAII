using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SemestralkaFinalVAII.Data;
using SemestralkaFinalVAII.Models;
using SemestralkaVAII.Models;

namespace SemestralkaFinalVAII.Controllers {

    [Route("api/[controller]")]
    public class KryptomenyAsyncController : Controller {

    private readonly ApplicationDbContext Context;

        public KryptomenyAsyncController(ApplicationDbContext context) {
            Context = context;
        }

        [HttpPut("{id}")]
        public void Put(string id) {
            string UserID = Context.Users.SingleOrDefault(p => p.Email == User.Identity.Name)?.Id;
            if (UserID == null) {
                return;
            }

            if (!Context.Oblubene.Any(temp => temp.UserId == UserID)) {
                Context.Oblubene.Add(new ZoznamOblubenych() {
                    UserId = UserID,
                    Oblubene = new List<string>()
                });
                Context.SaveChanges();
            }

            ZoznamOblubenych meny = Context.Oblubene.Single(temp => temp.UserId == UserID);

            if (!meny.Oblubene.Contains(id)) {
                meny.Oblubene.Add(id);
            } else {
                meny.Oblubene.Remove(id);
            }
            Context.SaveChanges();
        }

        [HttpDelete("{id}")]
        public void Delete(string id) {
            Kryptomeny mena = Context.Kryptomeny.SingleOrDefault(temp => temp.Id == id);
            if (mena != null) {
                Context.Kryptomeny.Remove(mena);
                Context.SaveChanges();
            }
        }
    }
}
