using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bilten.Data;
using Bilten.Data.Models;
using Bilten.Web.Areas.AdministratorModul.ViewModels.Dogadjaj;
using Bilten.Web.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Bilten.Web.Areas.AdministratorModul.Controllers
{
    [Area("AdministratorModul")]
    public class DogadjajController : Controller
    {
        private MojContext _context;

        public DogadjajController(MojContext context)
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

        public IActionResult Detalji(int dogadjajId)
        {
            DogadjajDetaljiVM model = _context.Dogadjaj.Where(x => x.Id == dogadjajId).Select(x => new DogadjajDetaljiVM
            {
        //                    public string Kategorija { get; set; }
        //public int KategorijaID { get; set; }
        //public string OrganizacioneJedinice { get; set; }
        //public string PodorganizacioneJedinice { get; set; }
        //public string Vrste { get; set; }
        //public DateTime DatumDogadjaja { get; set; }
        //public string MjestoDogadjaja { get; set; }
        //public DateTime DatumPrijave { get; set; }
        //public string Prijavitelj { get; set; }
        //public string Opis { get; set; }
        //public List<string> mjere { get; set; }
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

        public IActionResult Snimi(int kategorijaId, int vrstaId, int orgJedinicaId,int PodorgJedinicaId,DateTime datumDog,string mjesto,DateTime datumPrijave,string prijavitelj,string opis)
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

            _context.Dogadjaj.Add(novi);
            _context.SaveChanges();

            //List<Student> s = db.Student.ToList();
            //foreach (var x in s)
            //{
            //    IspitStavka ist = new IspitStavka()
            //    {
            //        IspitId = i.Id,
            //        Pristupio = true,
            //        Ocjena = 6,
            //        StudentId = x.Id
            //    };
            //    db.IspitStavka.Add(ist);
            //    db.SaveChanges();

            List<Mjere> M = _context.Mjere.Where(z=>z.KategorijeId == kategorijaId).ToList();

            foreach(var x in M)
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

            return Redirect("/AdministratorModul/Dogadjaj/Prikazi?="+novi.KategorijeId);
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
                || s.MjestoDogadjaja.Contains(SearchString)
                || s.Opis.Contains(SearchString)).ToList();
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

        public IActionResult SnimiPromjene(int dogadjajId, int vrsteId, int orgJedId, int podOrgID, string mjesto, DateTime datumDog, string datumPrijave, string prijavitelj, string opis)
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

            _context.Dogadjaj.Update(novi);
            _context.SaveChanges();

            return Redirect("/AdministratorModul/Dogadjaj/Detalji?=" + novi.Id);
        }
    }
}