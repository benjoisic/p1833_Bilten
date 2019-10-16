using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bilten.Data;
using Bilten.Data.Models;
using Bilten.Web.Areas.AdministratorModul.ViewModels.PodorganizacionaJedinica;
using Bilten.Web.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bilten.Web.Areas.AdministratorModul.Controllers
{
    [Area("AdministratorModul")]

    public class PodorganizacionaJedinicaController : Controller
    {
        private MojContext _context;

        public PodorganizacionaJedinicaController(MojContext context)
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

            PodorganizacionaJedinicaIndexVM model = new PodorganizacionaJedinicaIndexVM()
            {
                Rows = _context.PodorganizacionaJedinica.Select(x => new PodorganizacionaJedinicaIndexVM.Row
                {
                    PodorganizacionaJedinicaID = x.Id,
                    PodorganizacionaJedinica = x.Naziv
                }).ToList()
            };


            return View(model);
        }

        public IActionResult Detalji(int PodorganizacionaJedinicaID)
        {
            //<a href="/PodorganizacionaJedinica/Odaberi?=@x.OrganizacionaJedinicaID">Odaberi</a>
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 1)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }

            PodorganizacionaJedinicaDetaljiVM model = _context.PodorganizacionaJedinica.Where(x => x.Id == PodorganizacionaJedinicaID).Select(x => new PodorganizacionaJedinicaDetaljiVM
            {
                OrganizacionaJedinicaID = x.OrganizacionaJedinicaId,
                OrganizacionaJedinica = x.OrganizacionaJedinica.Naziv,
                PodOrgNaziv = x.Naziv,
                PodorganizacionaJedinicaID = x.Id
                
             
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

            PodorganizacionaJedinicaDodajVM model = _context.OrganizacionaJedinica.Select(x => new PodorganizacionaJedinicaDodajVM
            {
                orgJedinice = _context.OrganizacionaJedinica.Select(y=> new SelectListItem
                {
                    Value = y.Id.ToString(),
                    Text = y.Naziv
                }).ToList()
            }).FirstOrDefault();

            return View(model);
        }

        public IActionResult Snimi(int orgJedId,  string podOrgNaziv)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 1)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }


            PodorganizacionaJedinica novo = new PodorganizacionaJedinica();
            novo.OrganizacionaJedinicaId = orgJedId;
            novo.Naziv = podOrgNaziv;

            _context.PodorganizacionaJedinica.Add(novo);
            _context.SaveChanges();

            return Redirect("/AdministratorModul/PodorganizacionaJedinica/Index");
        }

        public IActionResult Obrisi(int PodorganizacionaJedinicaID)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 1)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }
            //<a href="/PodorganizacionaJedinica/Obrisi?=@x.PodorganizacionaJedinicaID">Obriši</a>
            PodorganizacionaJedinica temp = _context.PodorganizacionaJedinica.Where(x => x.Id == PodorganizacionaJedinicaID).FirstOrDefault();


            int OrganizacionaJedinicaID = temp.OrganizacionaJedinicaId;

            _context.PodorganizacionaJedinica.Remove(temp);
            _context.SaveChanges();

            return Redirect("/AdministratorModul/PodorganizacionaJedinica/Index");
        }

        public IActionResult Uredi(int PodorganizacionaJedinicaID)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 1)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }
            //<a href="/PodorganizacionaJedinica/Uredi?=@x.PodorganizacionaJedinicaID">Uredi</a>

            PodorganizacionaJedinicaDodajVM model = _context.PodorganizacionaJedinica.Where(x => x.Id == PodorganizacionaJedinicaID).Select(x => new PodorganizacionaJedinicaDodajVM
            {
                OrganizacionaJedinicaID = x.OrganizacionaJedinicaId,
                OrganizacionaJedinica = x.Naziv,
                PodorganizacionaJedinicaID = x.Id,
                Naziv = x.Naziv
            }).FirstOrDefault();


            return View(model);
        }

        public IActionResult SnimiPromjene(string Naziv, int OrganizacionaJedinicaID, int PodorganizacionaJedinicaID)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 1)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }
            ///PodorganizacionaJedinica/SnimiPromjene?Naziv=podorganizacionaaa+jed1&PodorganizacionaJedinicaID=2

            PodorganizacionaJedinica novo = _context.PodorganizacionaJedinica.Where(x => x.Id == PodorganizacionaJedinicaID).FirstOrDefault();
            novo.Naziv = Naziv;
            novo.OrganizacionaJedinicaId = OrganizacionaJedinicaID;


            _context.PodorganizacionaJedinica.Update(novo);
            _context.SaveChanges();

            return Redirect("/AdministratorModul/PodorganizacionaJedinica/Index");
        }
    }
}