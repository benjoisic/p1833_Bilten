using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bilten.Data;
using Bilten.Data.Models;
using Bilten.Web.Helper;
using Bilten.Web.ViewModels.Kategorije;
using Microsoft.AspNetCore.Mvc;

namespace Bilten.Web.Controllers
{
    public class KategorijeController : Controller
    {
        private MojContext _context;

        public KategorijeController(MojContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            Korisnici korisnik = HttpContext.GetLogiraniKorisnik();
            if (korisnik == null)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa";
                return RedirectToAction("Index", "Autentifikacija");
            }
            KategorijeIndexVM model = new KategorijeIndexVM()
            {
                Rows = _context.Kategorije.Select(x => new KategorijeIndexVM.Row
                {
                    KategorijeId = x.Id,
                    Naziv = x.Naziv
                }).ToList()
            };

            return View(model);
        }

        public IActionResult Dodaj()
        {
            //KategorijeDodajVM model = new KategorijeDodajVM();

            return View();
        }

        public IActionResult Snimi(string naziv)
        {
            Kategorije novo = new Kategorije();
            novo.Naziv = naziv;

            _context.Kategorije.Add(novo);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Obrisi(int KategorijeId)
        {
            Kategorije temp = _context.Kategorije.Where(x => x.Id == KategorijeId).FirstOrDefault();

            _context.Kategorije.Remove(temp);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Uredi(int KategorijeId)
        {
            KategorijeDodajVM model = _context.Kategorije.Where(x => x.Id == KategorijeId).Select(x => new KategorijeDodajVM()
            {
                KategorijeID = x.Id,
                Naziv = x.Naziv
            }).FirstOrDefault();


            return View(model);
        }

        public IActionResult SnimiPromjene(int KategorijeId, string naziv)
        {
            Kategorije novo = _context.Kategorije.Where(x => x.Id == KategorijeId).FirstOrDefault();
            novo.Naziv = naziv;

            _context.Kategorije.Update(novo);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}