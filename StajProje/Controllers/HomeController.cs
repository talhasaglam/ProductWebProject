using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using Microsoft.AspNet.Identity;
using StajProje.Entity;

namespace StajProje.Controllers
{
    
    public class HomeController : Controller
    {
        DataContext _context = new DataContext();
        // GET: Home
        public ActionResult Index()
        {
            
            return View(_context.Urunler.ToList());
        }

        public ActionResult Urunler(int? id)
        {
            IQueryable<Urun> urunler = _context.Urunler;
            if (id != null)
            {
                urunler = urunler.Where(i=>i.KategoriId==id);
            }
            return View(urunler.ToList());
        }

        public ActionResult Hakkimizda()
        {
            return View();
        }

        public ActionResult Iletisim()
        {
            return View();
        }

        public PartialViewResult KategoriAl()
        {
            
            return PartialView(_context.Kategoriler.ToList());
        }

    }
}