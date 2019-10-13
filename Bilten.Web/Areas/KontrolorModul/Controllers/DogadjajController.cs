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
using Rotativa;

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

        public IActionResult Odabran(int dogadjajId)
        {
            Dogadjaj temp = _context.Dogadjaj.Where(x => x.Id == dogadjajId).FirstOrDefault();

            temp.Odabran = false;

            _context.Dogadjaj.Update(temp);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult NijeOdabran(int dogadjajId)
        {
            Dogadjaj temp = _context.Dogadjaj.Where(x => x.Id == dogadjajId).FirstOrDefault();

            temp.Odabran = true;

            _context.Dogadjaj.Update(temp);
            _context.SaveChanges();

            return RedirectToAction("Index");
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

        public IActionResult ZvanicniBilten(string SearchString, string sortOrder)
        {
            List<Dogadjaj> dogadjaji = _context.Dogadjaj.Where(x=>x.Odabran == true)
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