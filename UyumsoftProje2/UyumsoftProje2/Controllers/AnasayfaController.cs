using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UyumsoftProje2.Models;
using UyumsoftProje2.Security;

namespace UyumsoftProje2.Controllers
{
    
    public class AnasayfaController : Controller
    {
        Entities model = new Entities();

        // GET: Anasayfa
        [MyAuthorization(Roles = "Y,A,S")]
        public ActionResult Index()
        {
            KATEGORI k = new KATEGORI();
            return View(k);
        }

        [MyAuthorization(Roles = "Y,A,S")]
        public ActionResult Iletisim()
        {
            return View();
        }

        [MyAuthorization(Roles = "Y,A,S")]
        public ActionResult Hakkimizda()
        {
            return View();
        }
    }
}