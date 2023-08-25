using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UyumsoftProje2.Models;
using UyumsoftProje2.Security;

namespace UyumsoftProje2.Controllers
{
    public class KitapController : Controller
    {
        // GET: Kitap

        Entities model = new Entities();

        [MyAuthorization(Roles="Y,A,S")]
        // GET: Anasayfa
        public ActionResult Index()
        {
            List<KATEGORI> kategori = model.KATEGORI.ToList();
            List<YAYINEVI> yayınevi = model.YAYINEVI.ToList();
            List<YAZAR> yazar = model.YAZAR.ToList();
            List<STOK> stok = model.STOK.ToList();

            var k = model.KITAP.Where(x => x.durum == true).ToList();

            ViewBag.kategori = kategori;
            ViewBag.yayınevi = yayınevi;
            ViewBag.yazar = yazar;
            ViewBag.stok = stok;

            return View(k);
        }

        [MyAuthorization(Roles = "Y,S")]
        [HttpGet]
        public ActionResult KitapEkle()
        {

            List<KATEGORI> kategori = model.KATEGORI.ToList();
            List<YAYINEVI> yayınevi = model.YAYINEVI.ToList();
            List<YAZAR> yazar = model.YAZAR.ToList();
            KITAP kitap = new KITAP();

            ViewBag.kategori = kategori;
            ViewBag.yayınevi = yayınevi;
            ViewBag.yazar = yazar;

            return View(kitap);
        }

        [MyAuthorization(Roles = "Y,S")]
        [HttpPost]
        public ActionResult KitapEkle(KITAP k)
        {


            var userId = System.Web.HttpContext.Current.User.Identity.Name;

            var kullanici = model.UYE.FirstOrDefault(x => x.kullaniciAdi == userId);

            var satici = model.SATICI.FirstOrDefault(x => x.uyeId == kullanici.uyeId);


            if (Request.Files.Count > 0)
            {
                string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                string yol = "~/Images/" + dosyaadi;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                k.resim = "/Images/" + dosyaadi;
            }
            
            k.durum = true;


            model.KITAP.AddOrUpdate(k);

            SATICI yeniSatici = new SATICI()
            {
                uyeId = kullanici.uyeId,
                kitapId = k.kitapId,
            };
            model.SATICI.Add(yeniSatici);

            model.SaveChanges();
            return RedirectToAction("Index");
        }

        [MyAuthorization(Roles = "Y,S")]
        [HttpPost]
        public int KitapSil(int id)
        {
            KITAP kitap = model.KITAP.FirstOrDefault(x => x.kitapId == id);
            try
            {
                kitap.durum = false;
                model.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        [MyAuthorization(Roles = "Y,S")]
        [HttpGet]
        public ActionResult KitapGuncelle(int id)
        {
            KITAP kitap = model.KITAP.FirstOrDefault(x => x.kitapId == id);
            List<KATEGORI> kategori = model.KATEGORI.ToList();
            List<YAYINEVI> yayınevi = model.YAYINEVI.ToList();
            List<YAZAR> yazar = model.YAZAR.ToList();
            ViewBag.kategori = kategori;
            ViewBag.yayınevi = yayınevi;
            ViewBag.yazar = yazar;

            return View("KitapEkle", kitap);
            // kitap ekleye kitap gönder
        }
    }
}