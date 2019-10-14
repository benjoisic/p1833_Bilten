using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bilten.Data;
using Bilten.Data.Models;
using Bilten.Web.Areas.KontrolorModul.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Bilten.Web.Helper;

namespace Bilten.Web.Areas.KontrolorModul.Controllers
{
    [Area("KontrolorModul")]
    public class KorisnikController : Controller
    {
        private MojContext _context;

        public KorisnikController(MojContext context)
        {
            _context = context;
        }

        public IActionResult Index(int korisnikId)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 3)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }
            KorisnikIndexVM model = _context.Korisnici.Where(x => x.Id == korisnikId).Select(x => new KorisnikIndexVM
            {
                KorisnikId = x.Id,
                ImePrezime = x.ImePrezime,
                JMBG = x.JMBG,
                email = x.email,
                username = x.KorisnickiNalog.Username,
                vrstaZaposlenika = x.VrstaKorisnika.Naziv,
                password = x.KorisnickiNalog.Lozinka
            }).FirstOrDefault();

            return View(model);
        }


        public IActionResult UrediProfil(int korisnikId)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 3)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }
            KorisnikIndexVM model = _context.Korisnici.Where(x => x.Id == korisnikId).Select(x => new KorisnikIndexVM
            {
                KorisnikId = x.Id,
                KorisnickiNalogId = x.KorisnickiNalogId,
                username = x.KorisnickiNalog.Username,
                password = x.KorisnickiNalog.Lozinka
            }).FirstOrDefault();

            return View(model);
        }

        public IActionResult SnimiPromjene(int korisnikId, int nalogId, string username)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 3)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }
            KorisnickiNalog temp = _context.KorisnickiNalog.Where(x => x.Id == nalogId).FirstOrDefault();

            temp.Username = username;

            _context.KorisnickiNalog.Update(temp);
            _context.SaveChanges();

            return Redirect("/KontrolorModul/Korisnik/Index?=" + korisnikId);
        }

        public IActionResult PromjeniPassword(int korisnikId)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 3)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }
            KorisnikPromjeniPasswordVM model = _context.Korisnici.Where(x => x.Id == korisnikId).Select(x => new KorisnikPromjeniPasswordVM
            {
                KorisnikId = x.Id,
                KorisnickiNalogId = x.KorisnickiNalogId,
                StariPass = x.KorisnickiNalog.Lozinka
            }).FirstOrDefault();

            return View(model);
        }

        public IActionResult SnimiPassword(int korisnikId, int nalogId, string StariPass, string UnosStariPass, string NoviPass, string NoviPassProvjera)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 3)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }
            KorisnickiNalog temp = _context.KorisnickiNalog.Where(x => x.Id == nalogId).FirstOrDefault();

            if (temp.Lozinka == UnosStariPass && NoviPass == NoviPassProvjera)
            {
                temp.Lozinka = NoviPass;

                _context.KorisnickiNalog.Update(temp);
                _context.SaveChanges();
            }
            else
            {
                return Redirect("/KontrolorModul/Korisnik/Index?=" + korisnikId);
            }

            return Redirect("/KontrolorModul/Korisnik/Index?=" + korisnikId);
        }

    }
}