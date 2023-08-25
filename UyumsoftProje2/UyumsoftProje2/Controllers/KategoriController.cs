using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UyumsoftProje2.Models;
using UyumsoftProje2.Security;

namespace UyumsoftProje2.Controllers
{
    public class KategoriController : Controller
    {
        Entities model = new Entities();
        // GET: Kategori

        [MyAuthorization(Roles = "Y,A,S")]
        public ActionResult Index()
        {
            List<KATEGORI> k = model.KATEGORI.ToList();

            return View(k);
        }

        [MyAuthorization(Roles = "Y,A,S")]
        public ActionResult KategoriDetay(int id)
        {
            var degerler = model.KITAP.Where(x => x.durum == true && x.kategoriId == id).ToList();

            List<KATEGORI> kategori = model.KATEGORI.ToList();
            List<YAYINEVI> yayınevi = model.YAYINEVI.ToList();
            List<YAZAR> yazar = model.YAZAR.ToList();

            ViewBag.kategori = kategori;
            ViewBag.yayınevi = yayınevi;
            ViewBag.yazar = yazar;

            return View(degerler);
        }

        [MyAuthorization(Roles = "Y,S")]
        [HttpGet]
        public ActionResult KategoriEkle()
        {
            KATEGORI kategori = new KATEGORI();
            return View(kategori);
        }

        [MyAuthorization(Roles = "Y,S")]
        [HttpPost]
        public ActionResult KategoriEkle(KATEGORI k)
        {
            model.KATEGORI.AddOrUpdate(k);
            model.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}