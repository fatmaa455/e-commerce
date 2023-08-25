using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using UyumsoftProje2.Models;
using System.Web.Security;

namespace UyumsoftProje2.Controllers
{
    [Authorize]
    public class SepetController : Controller
    {
        Entities model = new Entities();
        // GET: Sepet

        public ActionResult SepeteEkle(int adet, int id) // adet boş gelirse otomatik 1 yapıyoruz
        {

            var userId = System.Web.HttpContext.Current.User.Identity.Name;

            var kullanici = model.UYE.FirstOrDefault(x => x.kullaniciAdi == userId);

            SEPET sepettekiUrun = model.SEPET.FirstOrDefault(a => a.kitapId == id && a.kullaniciId==kullanici.uyeId);

            KITAP urun = model.KITAP.Find(id);

            if (adet == 0) adet = 1;

            if (sepettekiUrun == null) // sepette urun yoksa
            {
                SEPET yeniUrun = new SEPET()
                {
                    kullaniciId = kullanici.uyeId,
                    kitapId = id,
                    adet = adet, 
                    tutar = adet * urun.fiyat
                };
                model.SEPET.Add(yeniUrun);
            }
            else
            {
                sepettekiUrun.adet = sepettekiUrun.adet + adet;
                sepettekiUrun.tutar = sepettekiUrun.adet * urun.fiyat;
            }

            model.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Index()
        {


            var userId = System.Web.HttpContext.Current.User.Identity.Name;

            var kullanici = model.UYE.FirstOrDefault(x => x.ad == userId);

            var sepet = model.SEPET.Where(x => x.kullaniciId == kullanici.uyeId).ToList();

            List<KITAP> kitap = model.KITAP.ToList();
            ViewBag.kitap = kitap;


            return View(sepet);
        }

        public ActionResult Sil(int id)
        {
            SEPET sepet = model.SEPET.FirstOrDefault(x => x.sepetId == id);
            model.SEPET.Remove(sepet);
            model.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}