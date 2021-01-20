using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SemestralkaFinalVAII.Data;
using SemestralkaFinalVAII.Models;
using SemestralkaVAII.Models;

namespace SemestralkaFinalVAII.Controllers {

    public class ZmenarneController : Controller {
        private readonly ApplicationDbContext Context;

        public ZmenarneController(ApplicationDbContext context) {
            Context = context;
        }

        public IActionResult Index() {
            List<ZmenarenData> kryptomenyList = Context.Zmenarne.ToList();
            return View(kryptomenyList.OrderBy(p => p.TrustScoreRank).ToList());
        }

        public IActionResult LoadZmenarne() {
            ReloadFromApi();
            return RedirectToAction("index", "Zmenarne");
        }

        [HttpGet]
        public void ReloadFromApi() {
            Context.Zmenarne.RemoveRange(Context.Zmenarne);
            const string url = "https://api.coingecko.com/api/v3/exchanges";
            WebClient client = new WebClient();
            string str = client.DownloadString(url);
            try {
                List<ZmenarenData> list = JsonSerializer.Deserialize<List<ZmenarenData>>(str);
                Context.Zmenarne.AddRange(list);
                Context.SaveChanges();
            } catch (JsonException exception) {
                Console.WriteLine(exception.ToString());
            }
        }
    }
}
