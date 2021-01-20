using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using SemestralkaVAII.Models;
using System.Timers;
using SemestralkaFinalVAII.Data;
using SemestralkaFinalVAII.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Net;

namespace SemestralkaVAII.Controllers {

    public class KryptomenyController : Controller {
        private readonly ApplicationDbContext Context;

        public KryptomenyController(ApplicationDbContext context) {
            Context = context;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index() {
            List<Kryptomeny> kryptomenyList = Context.Kryptomeny.ToList();
            return View(kryptomenyList.OrderByDescending(p => p.CenaCelkovo).ToList());
        }

        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public IActionResult CreateKryptomeny(Kryptomeny kryptomeny) {
            Context.Kryptomeny.Add(kryptomeny);
            Context.SaveChanges();
            return RedirectToAction("index", "Kryptomeny");
        }

        public IActionResult LoadKryptomeny() {
            ReloadFromAPI();
            return RedirectToAction("index", "Kryptomeny");
        }

        public IActionResult Navrat() {
            return RedirectToAction("index", "Kryptomeny");
        }

        [HttpGet]
        private void ReloadFromAPI() {
        Context.Kryptomeny.RemoveRange(Context.Kryptomeny);
        string RequestUri = "https://api.coingecko.com/api/v3/coins/markets?vs_currency=eur&order=market_cap_desc&per_page=60&page=1&sparkline=false";
            WebClient client = new WebClient();
            string str = client.DownloadString(RequestUri);
            try {
                List<Kryptomeny> list = JsonSerializer.Deserialize<List<Kryptomeny>>(str);
                Context.Kryptomeny.AddRange(list);
                Context.SaveChanges();
            } catch (JsonException exception) {
                Console.WriteLine(exception.ToString());
            }
        }

        public IActionResult Uprav(string id) {
            Kryptomeny mena = Context.Kryptomeny.Single(temp => temp.Id == id);
            return View(mena);
        }

        [HttpPost]
        public IActionResult Uprav(Kryptomeny menena) {
            Kryptomeny krypto = Context.Kryptomeny.Single(temp => menena.Id == temp.Id);
            krypto.Nazov = menena.Nazov;
            krypto.Cena = menena.Cena;
            krypto.CenaCelkovo = menena.CenaCelkovo;
            krypto.Zmena = menena.Zmena;
            krypto.ZmenaPercent = menena.ZmenaPercent;
            krypto.PocetTokenov = menena.PocetTokenov;
            Context.SaveChanges();
            return RedirectToAction("Index", "Kryptomeny");
        }

        public IActionResult Portfolio() {
            string UserID = Context.Users.Where(p => p.Email == User.Identity.Name).Single().Id;

            List<string> oblubene = Context.Oblubene.SingleOrDefault(p => p.UserId == UserID).Oblubene;

            List<Kryptomeny> result = new List<Kryptomeny>();
            foreach (string temp in oblubene) {
                Kryptomeny mena = Context.Kryptomeny.SingleOrDefault(p => p.Id == temp);
                if (mena != null)
                    result.Add(mena);
            }
            //IQueryable<string> result = from meny in Context.Kryptomeny
            //                            join fav in oblubene
            //                            on meny.Id equals fav
            //                            select meny.Id;

            return View("Index", result);
        }

        public IActionResult Podrobnosti(string id) {
            string url = "https://api.coingecko.com/api/v3/coins/{0}/market_chart?vs_currency=eur&days=max";
            WebClient client = new WebClient();
            string result = client.DownloadString(string.Format(url, id));
            HistoriaCeny historia = new HistoriaCeny();
            try {
                historia = (HistoriaCeny)JsonSerializer.Deserialize<HistoriaCeny>(result);
            } catch (JsonException exception) {
                Console.WriteLine(exception.ToString());
            }

            return View(historia);
        }

        public IActionResult VytvorRoluUzivatela() {
            //Context.Roles.Add(new Microsoft.AspNetCore.Identity.IdentityRole {
                //Name = "Admin"
            //});
            //Context.UserRoles.Add(new Microsoft.AspNetCore.Identity.IdentityUserRole<string> {
                //UserId = "2fda2e10-dc0b-4b20-a89a-e8207ceb6777",
                //RoleId = "e82b5ca0-fad1-4d35-a169-f8b0baba78c2"
            //});
            //Context.SaveChanges();

            return RedirectToAction("Index", "Kryptomeny");
        }

        private void UpdateAll(Object source, ElapsedEventArgs args) {
            ReloadFromAPI();
        }
        
    }
}