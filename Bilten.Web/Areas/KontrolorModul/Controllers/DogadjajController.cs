using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Bilten.Data;
using Bilten.Data.Models;
using Bilten.Web.Areas.KontrolorModul.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rotativa.Core;
using Rotativa.AspNetCore;
using PagedList.Mvc;
using PagedList;
using Bilten.Web.Helper;

namespace Bilten.Web.Areas.KontrolorModul.Controllers
{
    [Area("KontrolorModul")]
    public class DogadjajController : Controller
    {
        private MojContext _context;

        public DogadjajController(MojContext context)
        {
            _context = context;
        }


        public IActionResult Index(string SearchString, string sortOrder)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 3)
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

        public IActionResult Odabran(int dogadjajId)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 3)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }
            Dogadjaj temp = _context.Dogadjaj.Where(x => x.Id == dogadjajId).FirstOrDefault();

            temp.Odabran = false;

            _context.Dogadjaj.Update(temp);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult NijeOdabran(int dogadjajId)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 3)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }
            Dogadjaj temp = _context.Dogadjaj.Where(x => x.Id == dogadjajId).FirstOrDefault();

            temp.Odabran = true;

            _context.Dogadjaj.Update(temp);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Detalji(int dogadjajId)
        {
          
            //Dogadjaj model = _context.Dogadjaj.Where(x => x.Id == dogadjajId).FirstOrDefault();

            Dogadjaj dogadjaj = _context.Dogadjaj
            .Include(x => x.Vrste).Include(y => y.Kategorije)
            .Include(a => a.OrganizacionaJedinica)
            .Include(z => z.PodorganizacionaJedinica).Where(s => s.Id == dogadjajId).FirstOrDefault();

            return View(dogadjaj);

        }


        public IActionResult Detalji2(int dogadjajId)
        {

            //Dogadjaj model = _context.Dogadjaj.Where(x => x.Id == dogadjajId).FirstOrDefault();

            Dogadjaj dogadjaj = _context.Dogadjaj
            .Include(x => x.Vrste).Include(y => y.Kategorije)
            .Include(a => a.OrganizacionaJedinica)
            .Include(z => z.PodorganizacionaJedinica).Where(s => s.Id == dogadjajId).FirstOrDefault();

            return View(dogadjaj);

        }


        public IActionResult ZvanicniBilten(string SearchString, string sortOrder)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 3)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }
            List<Dogadjaj> dogadjaji = _context.Dogadjaj.Where(x=>x.Odabran == true)
                .Include(x => x.Vrste).Include(y => y.Kategorije)
                .Include(a => a.OrganizacionaJedinica)
                .Include(z => z.PodorganizacionaJedinica).ToList();

            //if (!String.IsNullOrEmpty(SearchString))
            //{
            //    dogadjaji = dogadjaji.Where(s => s.Vrste.Naziv.Contains(SearchString)
            //    || s.MjestoDogadjaja.Contains(SearchString)).ToList();
            //}

            //ViewBag.Vrste = sortOrder == "Vrste" ? "Vrste_desc" : "Vrste";
            //ViewBag.DatumPrijave = sortOrder == "DatumPrijave" ? "DatumPrijave_desc" : "DatumPrijave";
            //ViewBag.Mjesto = sortOrder == "Mjesto" ? "Mjesto_desc" : "Mjesto";

            //switch (sortOrder)
            //{
            //    case "Vrste_desc":
            //        dogadjaji = dogadjaji.OrderByDescending(s => s.Vrste.Naziv).ToList();
            //        break;
            //    case "DatumPrijave_desc":
            //        dogadjaji = dogadjaji.OrderByDescending(s => s.DatumPrijave).ToList();
            //        break;
            //    case "Mjesto_desc":
            //        dogadjaji = dogadjaji.OrderByDescending(s => s.MjestoDogadjaja).ToList();
            //        break;
            //    default:
            //        dogadjaji = dogadjaji.OrderBy(s => s.Id).ToList();
            //        break;
            //}

            return View(dogadjaji);
        }

        //ViewAsPdf pdf = new ViewAsPdf("ZvanicniBilten", vmc)
        //{
        //    FileName = "File.pdf",
        //    PageSize = Rotativa.Options.Size.A4,
        //    PageMargins = { Left = 0, Right = 0 }
        //};

        //public IActionResult PrintViewToPdf()
        //{
        //    var pdf = new ViewAsPdf("ZvanicniBilten");

        //    return pdf;
        //}

        //public ActionResult PrintViewToPdf()
        //{
        //    return new ActionAsPdf("ZvanicniBilten");
        //}

        public ActionResult PrintViewToPdf()
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Korisnici k = _context.Korisnici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || k.VrstaKorisnikaId != 3)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }
            List<Dogadjaj> dogadjaji = _context.Dogadjaj.Where(x => x.Odabran == true)
              .Include(x => x.Vrste).Include(y => y.Kategorije)
              .Include(a => a.OrganizacionaJedinica)
              .Include(z => z.PodorganizacionaJedinica).ToList();

            ViewBag.SomeData = "Ciao";
            return new ViewAsPdf("ZvanicniBilten", dogadjaji);
        }

        //public actionresult printviewtopdf()
        //{
        //    var model = new generatepdfmodel();
        //    //code to get content
        //    return new rotativa.viewaspdf("generatepdf", model) { filename = "testviewaspdf.pdf" }
        //}

        ////public ActionResult PrintInvoice(int invoiceId)
        ////{
        ////    return new ActionAsPdf(
        ////                   "ZvanicniBilten",
        ////                   new { invoiceId = invoiceId })
        ////    { FileName = "ZvanicniBilten.pdf" };
        ////}
        //public IActionResult GeneratePDF()
        //{
        //    return new ActionAsPdf("Index") { FileName = "Test.pdf" };
        //}


    }
}