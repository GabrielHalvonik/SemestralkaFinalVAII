using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using SemestralkaVAII.Models;
using System.Timers;
using SemestralkaFinalVAII.Data;

namespace SemestralkaVAII.Controllers {

    public class KryptomenyController : Controller {

        private readonly ApplicationDbContext Context;

        public KryptomenyController(ApplicationDbContext context) {
            Context = context;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index() {
            List<Kryptomeny> kryptomenyList = Context.Kryptomeny.ToList();
            return View(kryptomenyList);
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

            HttpClient client = new HttpClient();
            string str = client.GetStringAsync("https://api.coingecko.com/api/v3/coins/markets?vs_currency=eur&order=market_cap_desc&per_page=60&page=1&sparkline=false").Result;

            List<Kryptomeny> list = JsonSerializer.Deserialize<List<Kryptomeny>>(str);
            Context.AddRange(list);

            Context.SaveChanges();
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
            //context.Kryptomeny.Update(krypto);
            Context.SaveChanges();
            return RedirectToAction("Index", "Kryptomeny");
        }

        public IActionResult Zmazat(string id) {
            Kryptomeny mena = Context.Kryptomeny.Single(temp => temp.Id == id);
            Context.Kryptomeny.Remove(mena);
            Context.SaveChanges();

            return RedirectToAction("Index", "Kryptomeny");
        }

        public void PridajMedziOblubene(string id) {
            //if (Context.Prihlaseny != null) {
            //    Context.Prihlaseny.OblubeneMeny.Add(id);
            //}
        }

        private void UpdateAll(Object source, ElapsedEventArgs args) {
            ReloadFromAPI();
        }
    }
}