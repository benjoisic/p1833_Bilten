using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bilten.Data;
using Microsoft.AspNetCore.Mvc;
using Bilten.Web.Helper;
using Bilten.Web.Areas.AdministratorModul.ViewModels.Korisnici;
using Bilten.Data.Models;

namespace Bilten.Web.Areas.AdministratorModul.Controllers
{
    [Area("AdministratorModul")]
    public class KorisniciController : Controller
    {
        private MojContext _context;

        public KorisniciController(MojContext context)
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
            KorisniciIndexVM model = _context.Korisnici.Select(x => new KorisniciIndexVM
            {
                Rows = _context.Korisnici.Select(y=> new KorisniciIndexVM.Row
                {
                KorisnikId = y.Id,
                KorisnickiNalogId = y.KorisnickiNalogId,
                ImePrezime = y.ImePrezime,
                JMBG = y.JMBG,
                email = y.email,
                username = y.KorisnickiNalog.Username,
                vrstaZaposlenika = y.VrstaKorisnika.Naziv
                }).ToList()
            }).FirstOrDefault();

            return View(model);
        }

        public IActionResult Detalji(int korisnikId)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 1)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }
            KorisniciUrediVM model = _context.Korisnici.Where(x => x.Id == korisnikId).Select(y => new KorisniciUrediVM
            {
                KorisnikId = y.Id,
                KorisnickiNalogId = y.KorisnickiNalogId,
                ImePrezime = y.ImePrezime,
                JMBG = y.JMBG,
                email = y.email,
                username = y.KorisnickiNalog.Username,
                password = y.KorisnickiNalog.Lozinka,
                vrstaZaposlenika = y.VrstaKorisnika.Naziv
            }).FirstOrDefault();

            return View(model);
        }

        public IActionResult Dodaj()
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 1)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }
            KorisniciDodajVM model = new KorisniciDodajVM();

            model.vrsteZaposlenika = _context.VrstaKorisnika.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Naziv
            }).ToList();

            return View(model);
        }

        public IActionResult Snimi(string imeiprezime, int jmbg, string email, int vrstaZaposlenikaId, string username, string password)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 1)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }
            //imeiprezime = &jmbg = &email = &vrstaZaposlenikaId = 1 & username = &password =

            KorisnickiNalog KN = new KorisnickiNalog();

            KN.Username = username;
            KN.Lozinka = password;
            _context.KorisnickiNalog.Add(KN);
            _context.SaveChanges();

            Korisnici K = new Korisnici();

            K.KorisnickiNalogId = KN.Id;
            K.ImePrezime = imeiprezime;
            K.JMBG = jmbg;
            K.email = email;
            K.VrstaKorisnikaId = vrstaZaposlenikaId;
            _context.Korisnici.Add(K);
            _context.SaveChanges();



            return Redirect("/AdministratorModul/Korisnici/Index");
        }

        public IActionResult UrediKorisnika(int korisnikId)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 1)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }
            KorisniciUrediVM model = _context.Korisnici.Where(x=>x.Id==korisnikId).Select(y => new KorisniciUrediVM
            {
                    KorisnikId = y.Id,
                    KorisnickiNalogId = y.KorisnickiNalogId,
                    ImePrezime = y.ImePrezime,
                    JMBG = y.JMBG,
                    email = y.email,
                    username = y.KorisnickiNalog.Username,
                    password = y.KorisnickiNalog.Lozinka,
                    vrstaZaposlenika = y.VrstaKorisnika.Naziv
            }).FirstOrDefault();

            return View(model);
        }

        public IActionResult SnimiPromjene(int korisnikId, int nalogId, string imeiprezime, int jmbg, string email, string username)
        //?korisnikId=1&nalogId=1&imeiprezime=Samra+Buri%C4%87&jmbg=2905997&email=samra%40edu.fit.ba&username=samrab
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 1)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }
            Korisnici temp = _context.Korisnici.Where(x => x.Id == korisnikId).FirstOrDefault();
            KorisnickiNalog temp1 = _context.KorisnickiNalog.Where(y => y.Id == nalogId).FirstOrDefault();

            temp1.Username = username;

            _context.KorisnickiNalog.Update(temp1);
            _context.SaveChanges();

            temp.KorisnickiNalogId = temp1.Id;
            temp.ImePrezime = imeiprezime;
            temp.JMBG = jmbg;
            temp.email = email;

            _context.Korisnici.Update(temp);
            _context.SaveChanges();



            return Redirect("/AdministratorModul/Korisnici/Index");

        }
        
        public IActionResult Obrisi(int korisnikId)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 1)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }
            Korisnici k1 = _context.Korisnici.Where(x => x.Id == korisnikId).FirstOrDefault();

            _context.Korisnici.Remove(k1);
            _context.SaveChanges();

            return Redirect("/AdministratorModul/Korisnici/Index");
        }

        public IActionResult PromjeniPassword(int korisnikId)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 1)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }
            KorisniciPromjeniPasswordVM model = _context.Korisnici.Where(x => x.Id == korisnikId).Select(x => new KorisniciPromjeniPasswordVM
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
            if (korisnik == null || k.VrstaKorisnikaId != 1)
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
                return Redirect("/AdministratorModul/Korisnici/Index");
            }

            return Redirect("/AdministratorModul/Korisnici/Index");
        }
    }
}