using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bilten.Data;
using Bilten.Data.Models;
using Bilten.Web.Areas.AdministratorModul.ViewModels.Kategorije;
using Bilten.Web.Helper;
using Microsoft.AspNetCore.Mvc;

namespace Bilten.Web.Areas.AdministratorModul.Controllers
{
    [Area("AdministratorModul")]
    public class KategorijeController : Controller
    {
        private MojContext _context;

        public KategorijeController(MojContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 1)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
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
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 1)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }

            return View();
        }

        public IActionResult Snimi(string naziv)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 1)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }

            List<Kategorije> temp1 = _context.Kategorije.ToList();

            foreach (var item in temp1)
            {
                if (item.Naziv == naziv)
                    return Redirect("/AdministratorModul/Kategorije/Index");
            }

            Kategorije novo = new Kategorije();
            novo.Naziv = naziv;

            _context.Kategorije.Add(novo);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Obrisi(int KategorijeId)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 1)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }

            Dogadjaj temp1 = _context.Dogadjaj.Where(y => y.KategorijeId == KategorijeId).FirstOrDefault();

            if (temp1 != null)
            {
                _context.Dogadjaj.Remove(temp1);
                _context.SaveChanges();
            }

            Kategorije temp = _context.Kategorije.Where(x => x.Id == KategorijeId).FirstOrDefault();

            _context.Kategorije.Remove(temp);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Uredi(int KategorijeId)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 1)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }

            KategorijeDodajVM model = _context.Kategorije.Where(x => x.Id == KategorijeId).Select(x => new KategorijeDodajVM()
            {
                KategorijeID = x.Id,
                Naziv = x.Naziv
            }).FirstOrDefault();


            return View(model);
        }

        public IActionResult SnimiPromjene(int KategorijeId, string naziv)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 1)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }

            Kategorije novo = _context.Kategorije.Where(x => x.Id == KategorijeId).FirstOrDefault();

       
           
                _context.Kategorije.Update(novo);
                _context.SaveChanges();
          

            

            return RedirectToAction("Index");
        }
    }
}