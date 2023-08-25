using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UyumsoftProje2.Models;
using UyumsoftProje2.Security;

namespace UyumsoftProje2.Controllers
{
    public class GuvenlikController : Controller
    {
        [Authorize]
        // GET: Guvenlik

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(UYE u)
        {
            Entities model = new Entities();

            UYE uye = model.UYE.FirstOrDefault(x => x.kullaniciAdi == u.kullaniciAdi && x.sifre == u.sifre);

            if(uye == null)
            {
                ViewBag.Mesaj = "Kullanıcı Adı veya Kullanıcı Şifre Hatalı";
                return View();
            }
            else
            {
                FormsAuthentication.SetAuthCookie(uye.kullaniciAdi,false);

                return RedirectToAction("Index", "Anasayfa");
            }
        }

        [MyAuthorization(Roles = "Y,A,S")]
        public ActionResult Logout()
        {
            Entities model = new Entities();

            FormsAuthentication.SignOut();

            return RedirectToAction("Login");

            List<SEPET> sepet = model.SEPET.ToList();

            foreach (SEPET s in sepet)
            {
                model.SEPET.Remove(s);
            }


            model.SaveChanges();
        }

        public ActionResult Hata()
        {
            return View();
        }


        [AllowAnonymous]
        [HttpGet]
        public ActionResult Register()
        {
            UYE uye = new UYE();
            return View(uye);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(UYE u)
        {
            Entities model = new Entities();

            if (u == null)
            {
                ViewBag.Mesaj = "Zaten böyle bir kullanıcı var";
                return View();
            }
            else
            {
                u.kullaniciRolü = "A";
                model.UYE.Add(u);
                model.SaveChanges();
                FormsAuthentication.SetAuthCookie(u.kullaniciAdi, false);
                return RedirectToAction("Index", "Anasayfa");
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult forgotPassword()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult forgotPassword(UYE u)
        {
            Entities model = new Entities();

            UYE uye = model.UYE.FirstOrDefault(x => x.kullaniciAdi == u.kullaniciAdi);

            if (uye == null)
            {
                ViewBag.Mesaj = "Böyle bir mail adresi bulunmamakta.";
                return View();
            }
            else
            {
                uye.sifre = u.sifre;
                model.SaveChanges();

                FormsAuthentication.SetAuthCookie(uye.kullaniciAdi, false);
                return RedirectToAction("Index", "Anasayfa");
            }
        }

    }
}