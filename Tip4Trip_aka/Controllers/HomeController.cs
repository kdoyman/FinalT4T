using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Tip4Trip_aka.Models;
using Tip4Trip_aka.ViewModels;

namespace Tip4Trip_aka.Controllers
{
    
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
       
        public ActionResult Index(string searching, string Address,DateTime? Sstartdate,DateTime? Enddate)
        {
            var Hous = db.Houses.Include(xxx => xxx.Reservations).Include(mmn => mmn.Location).Where(x => x.Location.NameCity.Contains(searching) && x.Address.Contains(Address));
           
            var res = db.Reservations.Where(c=>(c.StartDate <= Sstartdate )&& (c.EndDate<=Sstartdate));
            var res2 = db.Reservations.Where(f => (f.EndDate >= Enddate )&& (f.StartDate >= Enddate));
            var res3 = res.Concat(res2);
            if (Sstartdate == null && Enddate == null) {  res3 = db.Reservations; }
            
            
           HousesDates to_search_mas = new HousesDates { houses = Hous.ToList(), reservations = res3.ToList() };
            if (to_search_mas == null) { return View(); }
            return View(to_search_mas );

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