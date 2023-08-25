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
    public class YayineviController : Controller
    {
        Entities model = new Entities();
        // GET: Yayinevi

        [MyAuthorization(Roles = "Y,A,S")]
        public ActionResult Index()
        {
            List<YAYINEVI> y = model.YAYINEVI.ToList();

            return View(y);
        }

        [MyAuthorization(Roles = "Y,S")]
        [HttpGet]
        public ActionResult YayineviEkle()
        {
            YAYINEVI y = new YAYINEVI();
            return View(y);
        }

        [MyAuthorization(Roles = "Y,S")]
        [HttpPost]
        public ActionResult YayineviEkle(YAYINEVI y)
        {
            model.YAYINEVI.AddOrUpdate(y);
            model.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}