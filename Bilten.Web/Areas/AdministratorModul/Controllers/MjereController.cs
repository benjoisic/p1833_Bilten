using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bilten.Data;
using Bilten.Data.Models;
using Bilten.Web.Areas.AdministratorModul.ViewModels.Mjere;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Bilten.Web.Helper;

namespace Bilten.Web.Areas.AdministratorModul.Controllers
{
    [Area("AdministratorModul")]
    public class MjereController : Controller
    {
        private MojContext _context;

        public MjereController(MojContext context)
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

            MjereIndexVM model = new MjereIndexVM()
            {
                Rows = _context.Kategorije.Select(x => new MjereIndexVM.Row
                {
                    KategorijeId = x.Id,
                    NazivKategorije = x.Naziv
                }).ToList()
            };


            return View(model);
        }

        public IActionResult Prikazi(int KategorijeId)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 1)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }

            MjerePrikaziVM model = _context.Kategorije.Where(x => x.Id == KategorijeId).Select(x => new MjerePrikaziVM
            {
                KategorijeId = x.Id,
                NazivKategorije = x.Naziv,
                Rows = _context.Mjere.Where(y => y.KategorijeId == x.Id).Select(y => new MjerePrikaziVM.Row
                {
                    MjereId = y.Id,
                    OpisMjere = y.Opis
                }).ToList()
            }).FirstOrDefault();

            return View(model);
        }

        public IActionResult Dodaj(int kategorijeId)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 1)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }

            MjereDodajVM model = _context.Kategorije.Where(x => x.Id == kategorijeId).Select(x => new MjereDodajVM
            {
                KategorijeId = x.Id,
                NazivKategorije = x.Naziv
            }).FirstOrDefault();

            return View(model);
        }

        public IActionResult Snimi(string opisMjere, int kategorijeId)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 1)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }

            List<Mjere> temp1 = _context.Mjere.ToList();

            foreach (var item in temp1)
            {
                if (item.Opis == opisMjere)
                    return Redirect("/AdministratorModul/Mjere/Prikazi?=" + kategorijeId);
            }

            Mjere novo = new Mjere();
            novo.Opis = opisMjere;
            novo.KategorijeId = kategorijeId;

            _context.Mjere.Add(novo);
            _context.SaveChanges();

            return Redirect("/AdministratorModul/Mjere/Prikazi?=" + kategorijeId);
        }

        public IActionResult Obrisi(int mjeraId)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 1)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }
            List<DogadjajiMjere> temp1 = _context.DogadjajiMjere.Where(y => y.MjereId == mjeraId).ToList();

            foreach (var item in temp1)
            {
                if (temp1 != null)
                {
                    _context.DogadjajiMjere.Remove(item);
                    _context.SaveChanges();
                }
            }
            

            

            Mjere temp = _context.Mjere.Where(x => x.Id == mjeraId).FirstOrDefault();

            _context.Mjere.Remove(temp);
            _context.SaveChanges();



            return Redirect("/AdministratorModul/Mjere/Prikazi?=" + temp.KategorijeId);
        }

        public IActionResult Uredi(int mjereId)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 1)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }

            MjereDodajVM model = _context.Mjere.Where(x => x.Id == mjereId).Select(x => new MjereDodajVM
            {
                MjereId = mjereId,
                OpisMjere = x.Opis,
                KategorijeId = x.KategorijeId
            }).FirstOrDefault();

            return View(model);
        }

        public IActionResult SnimiPromjene(int kategorijeId, int mjereId, string opisMjere)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 1)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }

            Mjere novo = _context.Mjere.Where(x => x.Id == mjereId).FirstOrDefault();
            novo.Opis = opisMjere;
            novo.KategorijeId = kategorijeId;

            _context.Mjere.Update(novo);
            _context.SaveChanges();

            return Redirect("/AdministratorModul/Mjere/Prikazi?=" + kategorijeId);
        }
    }
}