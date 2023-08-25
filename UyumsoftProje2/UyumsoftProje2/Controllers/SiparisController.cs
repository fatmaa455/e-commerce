using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using UyumsoftProje2.Models;

namespace UyumsoftProje2.Controllers
{
    public class SiparisController : Controller
    {
        Entities model = new Entities();
        // GET: Siparis
        public ActionResult Index()
        {

            var userId = System.Web.HttpContext.Current.User.Identity.Name;

            var kullanici = model.UYE.FirstOrDefault(x => x.kullaniciAdi == userId);

            List<SEPET> urunler = model.SEPET.Where(x => x.kullaniciId == kullanici.uyeId).ToList();
            List<KITAP> kitap = model.KITAP.ToList();
            ViewBag.kitap = kitap;
            ViewBag.urunler = urunler;

            List<STOK> stoklar = new List<STOK>();

            foreach (SEPET s2 in urunler)
            {
                stoklar = model.STOK.Where(x => x.kitapId == s2.kitapId).ToList();
            }

            SIPARIS siparis = new SIPARIS();

            return View(siparis);
        }

        public void siparisVer(int id)
        {
            SIPARIS s = model.SIPARIS.FirstOrDefault(x => x.siparisId == id);

            var userId = System.Web.HttpContext.Current.User.Identity.Name;

            var kullanici = model.UYE.FirstOrDefault(x => x.kullaniciAdi == userId);

            List<SEPET> urunler = model.SEPET.Where(x => x.kullaniciId == kullanici.uyeId).ToList();

            List<STOK> stoklar = new List<STOK>();

            foreach (SEPET s2 in urunler)
            {
                stoklar = model.STOK.Where(x => x.kitapId == s2.kitapId).ToList();
            }

            foreach (STOK s3 in stoklar)
            {
                if (s3.kalan > 0)
                {
                    s3.kalan--;
                }
            }

            foreach (SEPET sepet in urunler)
            {
                model.SEPET.Remove(sepet);
                model.SaveChanges();
            }

            if (s != null)
            {
                model.SIPARIS.Remove(s);
                model.SaveChanges();
            }
        }
    }
}