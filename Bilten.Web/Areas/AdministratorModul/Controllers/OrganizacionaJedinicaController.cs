using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bilten.Data;
using Bilten.Data.Models;
using Bilten.Web.Areas.AdministratorModul.ViewModels.OrganizacionaJedinica;
using Bilten.Web.Helper;
using Microsoft.AspNetCore.Mvc;

namespace Bilten.Web.Areas.AdministratorModul.Controllers
{

    [Area("AdministratorModul")]

    public class OrganizacionaJedinicaController : Controller
    {
            private MojContext _context;

            public OrganizacionaJedinicaController(MojContext context)
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
            OrganizacionaJedinicaIndexVM model = new OrganizacionaJedinicaIndexVM()
            {
                Rows = _context.OrganizacionaJedinica.Select(x => new OrganizacionaJedinicaIndexVM.Row
                {
                    OrganizacionaJedinicaID = x.Id,
                    Naziv = x.Naziv
                }).ToList()
            };

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

            return View();
        }

        public IActionResult Snimi(string Naziv)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 1)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }

            List<OrganizacionaJedinica> temp1 = _context.OrganizacionaJedinica.ToList();

            foreach (var item in temp1)
            {
                if (item.Naziv == Naziv)
                    return Redirect("/AdministratorModul/OrganizacionaJedinica/Index");
            }


            ///OrganizacionaJedinica/Snimi?Naziv=&name=Snimi
            OrganizacionaJedinica novo = new OrganizacionaJedinica();
            novo.Naziv = Naziv;

            _context.OrganizacionaJedinica.Add(novo);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Obrisi(int OrganizacionaJedinicaID)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 1)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }
            // <a href="/OrganizacionaJedinica/Obrisi?=@x.OrganizacionaJedinicaID">Obrisi | </a>
            OrganizacionaJedinica temp = _context.OrganizacionaJedinica.Where(x => x.Id == OrganizacionaJedinicaID).FirstOrDefault();

            PodorganizacionaJedinica temp1 = _context.PodorganizacionaJedinica.Where(y => y.OrganizacionaJedinicaId == temp.Id).FirstOrDefault();

            List<Dogadjaj> temp2 = _context.Dogadjaj.Where(s => s.OrganizacionaJedinicaId == temp.Id).ToList();


            foreach (var item in temp2)
            {
                if (temp2 != null)
                {
                    _context.Dogadjaj.Remove(item);
                    _context.SaveChanges();
                }
            }
           


            if (temp1 != null)
            {
                _context.PodorganizacionaJedinica.Remove(temp1);
                _context.SaveChanges();
            }


            _context.OrganizacionaJedinica.Remove(temp);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Uredi(int OrganizacionaJedinicaID)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 1)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }
            //<a href="/OrganizacionaJedinica/Uredi?=@x.OrganizacionaJedinicaID">Uredi</a>
            OrganizacionaJedinicaDodajVM model = _context.OrganizacionaJedinica.Where(x => x.Id == OrganizacionaJedinicaID).Select(x => new OrganizacionaJedinicaDodajVM
            {
                OrganizacionaJedinicaID = x.Id,
                Naziv = x.Naziv
            }).FirstOrDefault();


            return View(model);
        }

        public IActionResult SnimiPromjene(string Naziv, int OrganizacionaJedinicaID)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 1)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }

            ///OrganizacionaJedinica/SnimiPromjene?Naziv=organizaciona+jedinica+temp&OrganizacionaJedinicaID=2

            OrganizacionaJedinica novo = _context.OrganizacionaJedinica.Where(x => x.Id == OrganizacionaJedinicaID).FirstOrDefault();
            novo.Naziv = Naziv;

            _context.OrganizacionaJedinica.Update(novo);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}