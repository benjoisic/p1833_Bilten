using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bilten.Data;
using Bilten.Data.Models;
using Bilten.Web.Areas.OperaterModul.ViewModels.Dogadjaj;
using Bilten.Web.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Bilten.Web.Areas.OperaterModul.Controllers
{
    [Area("OperaterModul")]
    public class DogadjajController : Controller
    {
        private MojContext _context;

        public DogadjajController(MojContext context)
        {
            _context = context;
        }
        public IActionResult Index(string SearchString)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 2)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }

            DogadjajIndexVM model = new DogadjajIndexVM()
            {
                Rows = _context.Kategorije.Select(x => new DogadjajIndexVM.Row
                {
                    KategorijeId = x.Id,
                    NazivKategorije = x.Naziv
                }).ToList()
            };


            return View(model);
        }

        public IActionResult Dodaj(int kategorijeId)
        {
            DogadjajDodajVM model = _context.Kategorije.Where(x => x.Id == kategorijeId).Select(x => new DogadjajDodajVM
            {
                KategorijaID = x.Id,
                Kategorija = x.Naziv,
                OrganizacioneJedinice = _context.OrganizacionaJedinica.Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Naziv
                }).ToList(),
                PodorganizacioneJedinice = _context.PodorganizacionaJedinica.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Naziv
                }).ToList(),
                Vrste = _context.Vrste.Where(z => z.KategorijeId == kategorijeId).Select(z => new SelectListItem
                {
                    Value = z.Id.ToString(),
                    Text = z.Naziv
                }).ToList()
                //mjere = _context.DogadjajiMjere.Where(m => m.DogadjajId == x.Id).Select(m => m.Mjere.Opis).ToList(),
                //DatumDogadjaja = (DateTime)x.DatumDogadjaja,
                //MjestoDogadjaja = x.MjestoDogadjaja,
                //DatumPrijave = (DateTime)x.DatumPrijave,
                //Prijavitelj = x.Prijavitelj,
                //Opis = x.Opis
            }).FirstOrDefault();


            return View(model);
        }

        public IActionResult Snimi(int kategorijaId, int vrstaId, int orgJedinicaId, int PodorgJedinicaId, DateTime datumDog, string mjesto, DateTime datumPrijave, string prijavitelj, string opis)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 2)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }

            Dogadjaj novi = new Dogadjaj();
            novi.KategorijeId = kategorijaId;
            novi.VrsteId = vrstaId;
            novi.OrganizacionaJedinicaId = orgJedinicaId;
            novi.PodorganizacionaJedinicaId = PodorgJedinicaId;
            novi.DatumDogadjaja = datumDog;
            novi.MjestoDogadjaja = mjesto;
            novi.DatumPrijave = datumPrijave;
            novi.Prijavitelj = prijavitelj;
            novi.Opis = opis;

            _context.Dogadjaj.Add(novi);
            _context.SaveChanges();

            List<Mjere> M = _context.Mjere.Where(z => z.KategorijeId == kategorijaId).ToList();

            foreach (var x in M)
            {
                DogadjajiMjere DM = new DogadjajiMjere()
                {
                    DogadjajId = novi.Id,
                    MjeraPoduzeta = false,
                    MjereId = x.Id
                };
                _context.DogadjajiMjere.Add(DM);
                _context.SaveChanges();
            }

            return Redirect("/OperaterModul/Dogadjaj/Lista2");
        }

        public IActionResult Lista()
        {
            DogadjajListaVM model = _context.Dogadjaj.Select(x => new DogadjajListaVM()
            {
                Rows = _context.Dogadjaj.Select(y => new DogadjajListaVM.Row
                {
                    DogadjajID = y.Id,
                    Kategorije = y.Kategorije.Naziv,
                    Vrste = y.Vrste.Naziv,
                    DatumPrijave = (DateTime)y.DatumPrijave,
                    MjestoDogadjaja = y.MjestoDogadjaja,
                    Prijavitelj = y.Prijavitelj,
                    Opis = y.Opis,
                    podorg = y.PodorganizacionaJedinica.Naziv
                }).ToList()
            }).FirstOrDefault();


            return View(model);
        }

        public IActionResult Pretraga(string searchString)
        {
            var dogadjaji = from d in _context.Dogadjaj
                            select d;

            if (!String.IsNullOrEmpty(searchString))
            {
                dogadjaji = dogadjaji.Where(a => a.Vrste.Naziv.Contains(searchString));
            }


            return View(dogadjaji);
        }

        public IActionResult Uredi(int dogadjajId)
        {
            
            DogadjajUrediVM model = _context.Dogadjaj.Where(x => x.Id == dogadjajId).Select(x => new DogadjajUrediVM
            {
                DogadjajID = x.Id,
                KategorijaID = x.KategorijeId,
                DatumDogadjaja = x.DatumDogadjaja.ToString(),
                MjestoDogadjaja = x.MjestoDogadjaja,
                DatumPrijave = x.DatumPrijave.ToString(),
                Prijavitelj = x.Prijavitelj,
                Opis = x.Opis,
                OrgJedTekst = x.OrganizacionaJedinica.Naziv,
                OrgJedID = x.OrganizacionaJedinicaId,
                PodOrgTekst = x.PodorganizacionaJedinica.Naziv,
                PodOrgID = x.PodorganizacionaJedinicaId,
                VrsteTekst = x.Vrste.Naziv,
                VrsteID = x.VrsteId,

        }).FirstOrDefault();

            model.OrganizacioneJedinice = _context.OrganizacionaJedinica.Where(a=>a.Id != model.OrgJedID).Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Naziv
            }).ToList();
            model.PodorganizacioneJedinice = _context.PodorganizacionaJedinica.Where(s => s.OrganizacionaJedinicaId == model.OrgJedID && s.Id != model.PodOrgID).Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Naziv
            }).ToList();
            model.Vrste = _context.Vrste.Where(z=>z.KategorijeId == model.KategorijaID && z.Id != model.VrsteID).Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Naziv
            }).ToList();


            return View(model);
        }

        public IActionResult Detalji(int dogadjajId)
        {
            DogadjajDetaljiVM model = _context.Dogadjaj.Where(x => x.Id == dogadjajId).Select(x => new DogadjajDetaljiVM
            {
                DogadjajID = x.Id,
                Kategorija = x.Kategorije.Naziv,
                OrganizacioneJedinice = x.OrganizacionaJedinica.Naziv,
                PodorganizacioneJedinice = x.PodorganizacionaJedinica.Naziv,
                Vrste = x.Vrste.Naziv,
                DatumDogadjaja = (DateTime)x.DatumDogadjaja,
                MjestoDogadjaja = x.MjestoDogadjaja,
                DatumPrijave = (DateTime)x.DatumPrijave,
                Prijavitelj = x.Prijavitelj,
                Opis = x.Opis


            }).FirstOrDefault();

            return View(model);

        }

        public IActionResult Obrisi(int dogadjajId)
        {
            Dogadjaj temp = _context.Dogadjaj.Where(x => x.Id == dogadjajId).FirstOrDefault();

            _context.Dogadjaj.Remove(temp);
            _context.SaveChanges();

            return Redirect("/OperaterModul/Dogadjaj/Lista2");

        }


        public IActionResult SnimiPromjene(int dogadjajId, int vrsteId, int orgJedId, int podOrgID, string mjesto, DateTime datumDog, string datumPrijave, string prijavitelj, string opis)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 2)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }

            Dogadjaj novi = _context.Dogadjaj.Where(x => x.Id == dogadjajId).FirstOrDefault();
            novi.VrsteId = vrsteId;
            novi.OrganizacionaJedinicaId = orgJedId;
            novi.PodorganizacionaJedinicaId = podOrgID;
            novi.DatumDogadjaja = datumDog;
            novi.MjestoDogadjaja = mjesto;
            novi.DatumPrijave = DateTime.Now;
            novi.Prijavitelj = prijavitelj;
            novi.Opis = opis;

            _context.Dogadjaj.Update(novi);
            _context.SaveChanges();

            return Redirect("/OperaterModul/Dogadjaj/Detalji?=" + novi.Id);
        }

        public IActionResult Lista2(string SearchString, string sortOrder)
        {
            List<Dogadjaj> dogadjaji = _context.Dogadjaj
                .Include(x => x.Vrste).Include(y => y.Kategorije)
                .Include(a => a.OrganizacionaJedinica)
                .Include(z => z.PodorganizacionaJedinica).ToList();

            if (!String.IsNullOrEmpty(SearchString))
            {
                dogadjaji = dogadjaji.Where(s => s.Vrste.Naziv.Contains(SearchString) 
                || s.MjestoDogadjaja.Contains(SearchString)).ToList();
            }

            ViewBag.Vrste = sortOrder == "Vrste" ? "Vrste_desc" : "Vrste";
            ViewBag.DatumPrijave = sortOrder == "DatumPrijave" ? "DatumPrijave_desc" : "DatumPrijave";
            ViewBag.Mjesto = sortOrder == "Mjesto" ? "Mjesto_desc" : "Mjesto";

            switch (sortOrder)
            {
                case "Vrste_desc":
                    dogadjaji = dogadjaji.OrderByDescending(s => s.Vrste.Naziv).ToList();
                    break;
                case "DatumPrijave_desc":
                    dogadjaji = dogadjaji.OrderByDescending(s => s.DatumPrijave).ToList();
                    break;
                case "Mjesto_desc":
                    dogadjaji = dogadjaji.OrderByDescending(s => s.MjestoDogadjaja).ToList();
                    break;
                default:
                    dogadjaji = dogadjaji.OrderBy(s => s.Id).ToList();
                    break;
            }

            return View(dogadjaji);
        }



        //public IActionResult SnimiPromjene(DogadjajUrediVM model)
        //{
        //    KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
        //    Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
        //    if (korisnik == null || k.VrstaKorisnikaId != 2)
        //    {
        //        TempData["error_poruka"] = "Nemate pravo pristupa!";
        //        return Redirect("/Autentifikacija/Index");
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        model.OrganizacioneJedinice = _context.OrganizacionaJedinica.Select(a => new SelectListItem
        //        {
        //            Value = a.Id.ToString(),
        //            Text = a.Naziv
        //        }).ToList();
        //        model.PodorganizacioneJedinice = _context.PodorganizacionaJedinica.Select(s => new SelectListItem
        //        {
        //            Value = s.Id.ToString(),
        //            Text = s.Naziv
        //        }).ToList();
        //        model.Vrste = _context.Vrste.Where(z => z.KategorijeId == model.KategorijaID).Select(z => new SelectListItem
        //        {
        //            Value = z.Id.ToString(),
        //            Text = z.Naziv
        //        }).ToList();

        //        return View("Uredi", model);
        //    }

        //    Dogadjaj novi = _context.Dogadjaj.Where(x => x.Id == model.DogadjajID).FirstOrDefault();
        //    novi.VrsteId = model.VrsteID;
        //    novi.OrganizacionaJedinicaId = model.OrgJedID;
        //    novi.PodorganizacionaJedinicaId = model.PodOrgID;
        //    novi.MjestoDogadjaja = model.MjestoDogadjaja;
        //    novi.DatumPrijave = DateTime.Now;
        //    novi.Prijavitelj = model.Prijavitelj;
        //    novi.Opis = model.Opis;

        //    _context.Dogadjaj.Update(novi);
        //    _context.SaveChanges();

        //    return Redirect("/OperaterModul/Dogadjaj/Detalji?=" + novi.Id);
        //}
    }
}