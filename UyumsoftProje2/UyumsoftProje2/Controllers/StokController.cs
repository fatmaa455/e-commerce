using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UyumsoftProje2.Models;

namespace UyumsoftProje2.Controllers
{
    public class StokController : Controller
    {
        Entities model = new Entities();
        // GET: Stok
        public ActionResult Index()
        {
            List<STOK> s = model.STOK.ToList();
            List<KITAP> kitap = model.KITAP.ToList();
            ViewBag.kitap = kitap;
            return View(s);
        }

        [HttpGet]
        public ActionResult StokEkle()
        {
            List<KITAP> kitap = model.KITAP.ToList();
            ViewBag.kitap = kitap;
            STOK s = new STOK();
            return View(s);
        }

        [HttpPost]
        public ActionResult StokEkle(STOK s)
        {
            model.STOK.AddOrUpdate(s);
            model.SaveChanges();
            return RedirectToAction("Index");
            
        }

        /*
        [HttpPost]
        public int StokSil(int id)
        {
            STOK stok = model.STOK.FirstOrDefault(x => x.stokId == id);

            try
            {
                model.STOK.Remove(stok);
                model.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        
        [HttpGet]
        public ActionResult StokSil(int id)
        {
            STOK stok = model.STOK.FirstOrDefault(x => x.stokId == id);
            if (stok != null)
            {
                return View(stok);
            }

            return null;
        }
        */
    }
}