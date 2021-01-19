﻿using System;
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
            try {
                List<Kryptomeny> list = JsonSerializer.Deserialize<List<Kryptomeny>>(str);
                Context.AddRange(list);
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

        //[HttpPost]
        public IActionResult PridajMedziOblubene(string id) {
            //string UserID = Context.Users.Where(p => p.Email == User.Identity.Name).Single().Id;
            string UserID = Context.Users.SingleOrDefault(p => p.Email == User.Identity.Name)?.Id;
            Console.WriteLine(id + " " + UserID + "\n");

            if (UserID == null) {
                return RedirectToAction("Index");
            }

            if (!Context.Oblubene.Any(temp => temp.UserId == UserID)) {
                Context.Oblubene.Add(new ZoznamOblubenych() {
                    UserId = UserID,
                    Oblubene = new List<string>()
                });
                Context.SaveChanges();
            }

            ZoznamOblubenych meny = Context.Oblubene.Single(temp => temp.UserId == UserID);
            //if (meny.Oblubene == null)
                //meny.Oblubene = new List<Kryptomeny>();

            if (!meny.Oblubene.Contains(id)) {
                meny.Oblubene.Add(id);
            } else {
                meny.Oblubene.Remove(id);
            }
            Context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Portfolio() {
            string UserID = Context.Users.Where(p => p.Email == User.Identity.Name).Single().Id;

            List<string> oblubene = Context.Oblubene.Single(p => p.UserId == UserID).Oblubene;
            List<Kryptomeny> result = new List<Kryptomeny>();
            foreach (string temp in oblubene) {
                Kryptomeny mena = Context.Kryptomeny.SingleOrDefault(p => p.Id == temp);
                result.Add(mena);
            }
            //IQueryable<string> result = from meny in Context.Kryptomeny
            //                            join fav in oblubene
            //                            on meny.Id equals fav
            //                            select meny.Id;

            result.ForEach(p => Console.WriteLine(p.Id));
            //return RedirectToAction("index", "Kryptomeny");
            return View("Index", result);
            //return View(portfolio);
        }

        public IActionResult Podrobnosti(string id) {
            Context.Kryptomeny.RemoveRange(Context.Kryptomeny);

            WebClient client = new WebClient();
            string str = client.DownloadString("https://api.coingecko.com/api/v3/coins/markets?vs_currency=eur&order=market_cap_desc&per_page=60&page=1&sparkline=false");
            try {
                List<Kryptomeny> list = JsonSerializer.Deserialize<List<Kryptomeny>>(str);
                Context.AddRange(list);
                Context.SaveChanges();
            } catch (JsonException exception) {
                Console.WriteLine(exception.ToString());
            }

            return View();
        }

        public IActionResult PotiahniPodrobneData() {
            //Context.Historia.RemoveRange(Context.Historia);

            //WebClient client = new WebClient();
            //string url = "https://api.coingecko.com/api/v3/coins/{0}/market_chart?vs_currency=eur&days=max";

            //foreach (var temp in Context.Kryptomeny) {
            //    string result = client.DownloadString(string.Format(url, temp.Id));
            //    try {
            //        HistoriaCeny historia = (HistoriaCeny)JsonSerializer.Deserialize<HistoriaCeny>(result);
            //        List<HistoriaDataZaznam> prices = new List<HistoriaDataZaznam>();
            //        List<HistoriaDataZaznam> cap = new List<HistoriaDataZaznam>();
            //        List<HistoriaDataZaznam> total = new List<HistoriaDataZaznam>();

            //        foreach (List<double> x in historia.Prices) {
            //            prices.Add(new HistoriaDataZaznam {
            //                Cas = x[0],
            //                Hodnota = x[1]
            //            });
            //        }
            //        foreach (List<double> x in historia.MarketCaps) {
            //            cap.Add(new HistoriaDataZaznam {
            //                Cas = x[0],
            //                Hodnota = x[1]
            //            });
            //        }
            //        foreach (List<double> x in historia.TotalVolumes) {
            //            total.Add(new HistoriaDataZaznam {
            //                Cas = x[0],
            //                Hodnota = x[1]
            //            });
            //        }

            //        Context.Historia.Add(new HistoriaData {
            //            IdMeny = temp.Id,
            //            Prices = prices,
            //            MarketCaps = cap,
            //            TotalVolumes = total
            //        });
            //            //});
            //            //historia.IdMeny = temp.Id;
            //            //Context.AddRange(historia);
            //            //Context.SaveChanges();
            //        } catch (JsonException exception) {
            //        Console.WriteLine(exception.ToString());
            //    }

            //    Context.SaveChanges();
            //}
            //Context.Kryptomeny.ForEachAsync(temp => {
            //        string result = client.GetStringAsync(string.Format(url, temp.Id)).Result;
            //        Console.WriteLine(string.Format(url, temp.Id));
            //        try {
            //            HistoriaCeny historia = (HistoriaCeny)JsonSerializer.Deserialize<HistoriaCeny>(result);
            //            //historia.IdMeny = temp.Id;
            //            //Context.AddRange(historia);
            //            //Context.SaveChanges();
            //        } catch (JsonException exception) {
            //            Console.WriteLine(exception.ToString());
            //        }
            //    });
            return RedirectToAction("Index", "Kryptomeny");
        }

        private void UpdateAll(Object source, ElapsedEventArgs args) {
            ReloadFromAPI();
        }
        
    }
}