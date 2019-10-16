using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bilten.Data;
using Bilten.Data.Models;
using Bilten.Web.Areas.AdministratorModul.ViewModels.Vrste;
using Microsoft.AspNetCore.Mvc;
using Bilten.Web.Helper;

namespace Bilten.Web.Areas.AdministratorModul.Controllers
{
    [Area("AdministratorModul")]
    public class VrsteController : Controller
    {
        private MojContext _context;

        public VrsteController(MojContext context)
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

            VrsteIndexVM model = new VrsteIndexVM()
            {
                Rows = _context.Kategorije.Select(x => new VrsteIndexVM.Row
                {
                    KategorijeId = x.Id,
                    NazivKategorije = x.Naziv
                }).ToList()
            };

            return View(model);
        }

        public IActionResult Odaberi(int KategorijeId)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 1)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }

            VrsteOdaberiVM model = _context.Kategorije.Where(x => x.Id == KategorijeId).Select(x => new VrsteOdaberiVM
            {
                KategorijeId = x.Id,
                NazivKategorije = x.Naziv,
                Rows = _context.Vrste.Where(y=>y.KategorijeId == x.Id).Select(y=> new VrsteOdaberiVM.Row
                {
                    VrsteId = y.Id,
                    Naziv = y.Naziv
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

            VrsteDodajVM model = _context.Kategorije.Where(x => x.Id == kategorijeId).Select(x => new VrsteDodajVM
            {
                KategorijeId = x.Id,
                NazivKategorije = x.Naziv
            }).FirstOrDefault();

            return View(model);
        }

        public IActionResult Snimi(string nazivVrste, int kategorijeId)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 1)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }

            List<Vrste> temp1 = _context.Vrste.ToList();

            foreach (var item in temp1)
            {
                if (item.Naziv == nazivVrste)
                    return Redirect("/AdministratorModul/Vrste/Odaberi?=" + kategorijeId);
            }

            Vrste novo = new Vrste();
            novo.Naziv = nazivVrste;
            novo.KategorijeId = kategorijeId;

            _context.Vrste.Add(novo);
            _context.SaveChanges();

            return Redirect("/AdministratorModul/Vrste/Odaberi?=" + kategorijeId);
        }

        public IActionResult Obrisi(int vrsteId)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 1)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/AdministratorModul/Autentifikacija/Index");
            }

            Dogadjaj temp1 = _context.Dogadjaj.Where(y => y.VrsteId == vrsteId).FirstOrDefault();

            if (temp1 != null)
            {
                _context.Dogadjaj.Remove(temp1);
                _context.SaveChanges();
            }

            Vrste temp = _context.Vrste.Where(x => x.Id == vrsteId).FirstOrDefault();

            _context.Vrste.Remove(temp);
            _context.SaveChanges();

            return Redirect("/AdministratorModul/Vrste/Odaberi?=" + temp.KategorijeId);
        }

        public IActionResult Uredi(int vrsteId)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 1)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }

            VrsteDodajVM model = _context.Vrste.Where(x => x.Id == vrsteId).Select(x => new VrsteDodajVM
            {
                VrsteId = vrsteId,
                Naziv = x.Naziv,
                KategorijeId = x.KategorijeId
            }).FirstOrDefault();

            return View(model);
        }

        public IActionResult SnimiPromjene(int kategorijeId, int vrsteId, string nazivVrste)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 1)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }

            Vrste novo = _context.Vrste.Where(x => x.Id == vrsteId).FirstOrDefault();
            novo.Naziv = nazivVrste;
            novo.KategorijeId = kategorijeId;

            _context.Vrste.Update(novo);
            _context.SaveChanges();

            return Redirect("/AdministratorModul/Vrste/Odaberi?=" + kategorijeId);
        }
    }
}