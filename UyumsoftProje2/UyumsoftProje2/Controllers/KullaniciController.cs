using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UyumsoftProje2.Models;

namespace UyumsoftProje2.Controllers
{
    public class KullaniciController : Controller
    {
        Entities model = new Entities();
        // GET: Kullanici
        public ActionResult Index()
        {
            List<UYE> u = model.UYE.ToList();

            return View(u);
        }

        [HttpGet]
        public ActionResult KullaniciEkle()
        {
            UYE u = new UYE();
            return View(u);
        }

        [HttpPost]
        public ActionResult KullaniciEkle(UYE u)
        {
            model.UYE.AddOrUpdate(u);
            model.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult KullaniciGuncelle(int id)
        {
            UYE uye = model.UYE.FirstOrDefault(x => x.uyeId == id);

            return View("KullaniciEkle", uye);

        }

        [HttpPost]
        public ActionResult KullaniciSil(UYE u)
        {
            UYE uye = model.UYE.FirstOrDefault(x => x.uyeId == u.uyeId);

            if (uye != null)
            {
                model.UYE.Remove(uye);
                model.SaveChanges();
            }

            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult KullaniciSil(int id)
        {
            UYE uye = model.UYE.FirstOrDefault(x => x.uyeId == id);
            if (uye != null)
            {
                return View(uye);
            }

            return null;
        }


        public ActionResult Saticilar()
        {
            List<SATICI> saticilar = model.SATICI.ToList();

            List<UYE> uyeler = model.UYE.ToList();
            List<KITAP> kitaplar = model.KITAP.ToList();
            ViewBag.kitaplar = kitaplar;
            ViewBag.uyeler = uyeler;

            return View(saticilar);
        }

        public ActionResult Alicilar()
        {

            List<UYE> uyeler = model.UYE.Where(x => x.kullaniciRolü == "A").ToList();

            List<KITAP> kitaplar = model.KITAP.ToList();
            ViewBag.kitaplar = kitaplar;

            return View(uyeler);
        }
    }
}