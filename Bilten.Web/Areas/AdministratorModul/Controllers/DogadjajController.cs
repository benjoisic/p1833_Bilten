using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Bilten.Data;
using Bilten.Data.Models;
using Bilten.Web.Areas.AdministratorModul.ViewModels.Dogadjaj;
using Bilten.Web.Helper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Bilten.Web.Areas.AdministratorModul.Controllers
{
    [Area("AdministratorModul")]
    public class DogadjajController : Controller
    {
        private MojContext _context;
        private readonly IHostingEnvironment he;

        public DogadjajController(MojContext context, IHostingEnvironment e)
        {
            _context = context;
            he = e;
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

        public IActionResult Prikazi(int kategorijeId)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 1)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }
            DogadjajPrikaziVM model = _context.Kategorije.Where(x => x.Id == kategorijeId).Select(x => new DogadjajPrikaziVM
            {
                KategorijaID = x.Id,
                Kategorija = x.Naziv,
                Rows = _context.Dogadjaj.Where(y => y.KategorijeId == x.Id).Select(y => new DogadjajPrikaziVM.Row
                {
                    DogadjajID = y.Id,
                    OrganizacioneJedinice = y.OrganizacionaJedinica.Naziv,
                    PodorganizacioneJedinice = y.PodorganizacionaJedinica.Naziv,
                    Vrste = y.Vrste.Naziv,
                    mjere = _context.DogadjajiMjere.Where(m => m.DogadjajId == y.Id && m.MjeraPoduzeta == true).Select(m => m.Mjere.Opis).ToList(),
                    DatumDogadjaja = (DateTime)y.DatumDogadjaja,
                    MjestoDogadjaja = y.MjestoDogadjaja,
                    DatumPrijave = (DateTime)y.DatumPrijave,
                    Prijavitelj = y.Prijavitelj,
                    Opis = y.Opis
                }).ToList()

            }).FirstOrDefault();


            return View(model);
        }



        public IActionResult Obrisi(int dogadjajId)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 1)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }
            Dogadjaj temp = _context.Dogadjaj.Where(x => x.Id == dogadjajId).FirstOrDefault();

            _context.Dogadjaj.Remove(temp);
            _context.SaveChanges();

            return Redirect("/AdministratorModul/Dogadjaj/Lista2");

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
                Vrste = _context.Vrste.Where(z=>z.KategorijeId == kategorijeId).Select(z => new SelectListItem
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

        public IActionResult Uredi(int dogadjajId)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 1)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }
            DogadjajUrediVM model = _context.Dogadjaj.Where(x => x.Id == dogadjajId).Select(x => new DogadjajUrediVM
            {
                DogadjajID = x.Id,
                KategorijaID = x.KategorijeId,
                DatumDogadjaja = (DateTime)x.DatumDogadjaja,
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
                Slika = x.SlikaPath

            }).FirstOrDefault();

            model.OrganizacioneJedinice = _context.OrganizacionaJedinica.Where(a => a.Id != model.OrgJedID).Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Naziv
            }).ToList();
            model.PodorganizacioneJedinice = _context.PodorganizacionaJedinica.Where(s => s.OrganizacionaJedinicaId == model.OrgJedID && s.Id != model.PodOrgID).Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Naziv
            }).ToList();
            model.Vrste = _context.Vrste.Where(z => z.KategorijeId == model.KategorijaID && z.Id != model.VrsteID).Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Naziv
            }).ToList();


            return View(model);
        }

        [HttpPost]
        public IActionResult Snimi(int kategorijaId, int vrstaId, int orgJedinicaId, int PodorgJedinicaId, DateTime datumDog,
            string mjesto, DateTime datumPrijave, string prijavitelj, string opis, IFormFile SlikaFF)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 1)
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

            string uniqueFileName = null;
            if (SlikaFF != null)
            {
                string uploadsFolder = Path.Combine(he.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + SlikaFF.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                SlikaFF.CopyTo(new FileStream(filePath, FileMode.Create));

                novi.SlikaPath = uniqueFileName;

            }


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

            return Redirect("/AdministratorModul/Dogadjaj/Lista2");
        }

        public IActionResult Lista2(string SearchString, string sortOrder)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 1)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }
            List<Dogadjaj> dogadjaji = _context.Dogadjaj
                .Include(x => x.Vrste).Include(y => y.Kategorije)
                .Include(a => a.OrganizacionaJedinica)
                .Include(z => z.PodorganizacionaJedinica).ToList();

            if (!String.IsNullOrEmpty(SearchString))
            {
                List<Dogadjaj> dogadjaji1 = _context.Dogadjaj
                .Include(x => x.Vrste).Include(y => y.Kategorije)
                .Include(a => a.OrganizacionaJedinica)
                .Include(z => z.PodorganizacionaJedinica).Where(s => s.Vrste.Naziv.Contains(SearchString) || s.MjestoDogadjaja.Contains(SearchString)
                || s.Opis.Contains(SearchString)).ToList();

                return View(dogadjaji1);
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

        [HttpPost]
        public IActionResult SnimiPromjene(int dogadjajId, int vrsteId, int orgJedId, int podOrgID, string mjesto, 
            DateTime datumDog, string datumPrijave, string prijavitelj, string opis, IFormFile SlikaFF)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 1)
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

            string uniqueFileName = null;
            if (SlikaFF != null)
            {
                string uploadsFolder = Path.Combine(he.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + SlikaFF.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                SlikaFF.CopyTo(new FileStream(filePath, FileMode.Create));

                novi.SlikaPath = uniqueFileName;

            }
           

            _context.Dogadjaj.Update(novi);
            _context.SaveChanges();

            return Redirect("/AdministratorModul/Dogadjaj/Detalji2?=" + novi.Id);
        }

        public IActionResult Detalji2(int dogadjajId)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 1)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }
            //Dogadjaj model = _context.Dogadjaj.Where(x => x.Id == dogadjajId).FirstOrDefault();

            Dogadjaj dogadjaj = _context.Dogadjaj
            .Include(x => x.Vrste).Include(y => y.Kategorije)
            .Include(a => a.OrganizacionaJedinica)
            .Include(z => z.PodorganizacionaJedinica).Where(s=>s.Id == dogadjajId).FirstOrDefault();

            return View(dogadjaj);
        }

        public IActionResult ObrisiSliku(int dogadjajId)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 1)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }
            Dogadjaj temp = _context.Dogadjaj.Where(x => x.Id == dogadjajId).FirstOrDefault();

            if (temp.SlikaPath != null)
            {
                string uploadsFolder = Path.Combine(he.WebRootPath, "images");
                string filePath = Path.Combine(uploadsFolder, temp.SlikaPath);
                //string putanja = filePath;
                //var imagesFolder = System.IO.File.Exists(filePath);

                //if (System.IO.File.Exists(filePath))
                //{

                //    System.IO.File.Delete(putanja);


                //}


                if (System.IO.File.Exists(filePath))
                {
                    try
                    {
                        System.GC.Collect();
                        System.GC.WaitForPendingFinalizers();
                        System.IO.File.Delete(filePath);

                    }
                    catch (Exception e) { }
                }
            }

            temp.SlikaPath = null;

            _context.Dogadjaj.Update(temp);
            _context.SaveChanges();


            return Redirect("/AdministratorModul/Dogadjaj/Uredi?=" + dogadjajId);
        }
    }
}