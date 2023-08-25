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
    public class YazarController : Controller
    {
        Entities model = new Entities();
        // GET: Yazar

        [MyAuthorization(Roles = "Y,A,S")]
        public ActionResult Index()
        {
            List<YAZAR> y = model.YAZAR.ToList();

            return View(y);
        }

        [MyAuthorization(Roles = "Y,S")]
        [HttpGet]
        public ActionResult YazarEkle()
        {
            YAZAR y = new YAZAR();
            return View(y);
        }

        [MyAuthorization(Roles = "Y,S")]
        [HttpPost]
        public ActionResult YazarEkle(YAZAR y)
        {
            model.YAZAR.AddOrUpdate(y);
            model.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}