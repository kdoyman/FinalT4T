using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Tip4Trip_aka.Models;

namespace Tip4Trip_aka.Controllers
{
    
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(string searching, string Address)
        {
            return View(db.Houses.Include(mmn => mmn.Location).Where(x => x.Location.NameCity.Contains(searching) && x.Address.Contains(Address) || searching == null).ToList());

            //return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}