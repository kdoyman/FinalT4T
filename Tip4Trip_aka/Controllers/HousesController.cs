using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tip4Trip_aka.Models;
using Tip4Trip_aka.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.IO;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Tip4Trip_aka.Controllers
{
    [Authorize]
    public class HousesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUser du = new ApplicationUser();

        // GET: Houses
        //[Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var houses = db.Houses.Include(h => h.Location).Include(xxx => xxx.Reservations);
            return View(houses.ToList());
        }
        [HttpGet]
        public ActionResult Imageget(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            House house = db.Houses.Find(id);
            if (house == null)
            {
                return HttpNotFound();
            }
            HouseImage hsimg = new HouseImage() { housId = house.Id };
            return View(hsimg);
        }
        [HttpPost]
        public ActionResult Imageget(HouseImage membervalues)
        {
            //Use Namespace called :  System.IO  
            string FileName = Path.GetFileNameWithoutExtension(membervalues.ImageFile.FileName);

            //To Get File Extension  
            string FileExtension = Path.GetExtension(membervalues.ImageFile.FileName);

            //Add Current Date To Attached File Name  
            FileName =  FileName.Trim() + FileExtension;

            //Get Upload path from Web.Config file AppSettings.  
            string UploadPath = @"C:\T4T1105eva\Tip4Trip_aka\UserImages\";

            //Its Create complete path to store in server.  
            membervalues.ImagePath = UploadPath + FileName;

            //To copy and save file into server.  
            membervalues.ImageFile.SaveAs(membervalues.ImagePath);
          
              HouseImage _member = new HouseImage();
              _member.ImagePath = membervalues.ImagePath;
            _member.housId = membervalues.housId;
            //   _member.Name = membervalues.Name;
            //    _member.PhoneNumber = membervalues.PhoneNumber;
            //  db.tblMembers.Add(_member);
            //    db.SaveChanges();
            if (ModelState.IsValid)
            {
                db.HouseImages.Add(_member);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult mazi(int idilicious)
        {
            
            List<House> housesList3 = db.Houses.ToList();
            // var housesViewModelList = housesList.Select(x => new HouseReservationViewModel { House = x, Reservations1 = x.Reservations }).ToList();
            var housesList = db.Houses.Include(c => c.Location).Include(y => y.Reservations).Where(xs => xs.Id.Equals(idilicious)).ToList();
            var ReservationsList = db.Reservations.Where(xc=>xc.HouseId.Equals(idilicious)).ToList();
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            if (idilicious >= db.Houses.Min(n=>n.Id) && idilicious <= db.Houses.Max(x=>x.Id))
            {
                HouseReservationViewModel ViewModel = new HouseReservationViewModel
                {
                    House = housesList.First(),
                    Reservations1 = ReservationsList,
                    Name=user.FullName
                    
                    

                };

                return View(ViewModel);
            }
            else
            { return Content("Wrong Id , Try again"); }
        }

        // GET: Houses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            House house = db.Houses.Find(id);
            if (house == null)
            {
                return HttpNotFound();
            }
            return View(house);
        }

        // GET: Houses/Create
        public ActionResult Create()
        {
            
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            House ase = new House() ;
            List<House> list = new List<House> {  };
            list.Add(ase)
;            UserHousesViewModel ViewModel = new UserHousesViewModel { User = user, Houses = list };
            ViewBag.LocationId = new SelectList(db.Locations, "Id", "NameCity");
            return View(ViewModel);
        }

        // POST: Houses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,OwnerId,Address,Description,LocationId,MaxOccupancy,PriceperNight")] House house)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var UserManagers = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var AuthManager = HttpContext.GetOwinContext().Authentication;
            if (User.IsInRole("Renter"))
            {

                UserManagers.RemoveFromRole(user.Id, "Renter");

                UserManagers.AddToRole(user.Id, "Owner");

                AuthManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie, DefaultAuthenticationTypes.ExternalCookie);
            }
            // house.Owner = user;
            // house.OwnerId = user.Id;
            if (ModelState.IsValid)
            {
                db.Houses.Add(house);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            List<House> list = new List<House> { };
            list.Add(house);
            ViewBag.LocationId = new SelectList(db.Locations, "Id", "NameCity", house.LocationId);
            UserHousesViewModel ViewModel = new UserHousesViewModel { User = user, Houses = list };
            return View(ViewModel);
        }

        // GET: Houses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            House house = db.Houses.Find(id);
            if (house == null)
            {
                return HttpNotFound();
            }
            ViewBag.LocationId = new SelectList(db.Locations, "Id", "NameCity", house.LocationId);
            return View(house);
        }

        // POST: Houses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Owner,Address,Description,LocationId,MaxOccupancy,PriceperNight")] House house)
        {
            if (ModelState.IsValid)
            {
                db.Entry(house).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LocationId = new SelectList(db.Locations, "Id", "NameCity", house.LocationId);
            return View(house);
        }

        // GET: Houses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            House house = db.Houses.Find(id);
            if (house == null)
            {
                return HttpNotFound();
            }
            return View(house);
        }

        // POST: Houses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)

        {

            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            var UserManagers = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            if (User.IsInRole("Owner"))

            {

                if (db.Houses.Where(g => g.OwnerId == user.Id).Count() == 1)
                {

                    UserManagers.RemoveFromRole(user.Id, "Owner");

                    UserManagers.AddToRole(user.Id, "Renter");

                }

            }
            House house = db.Houses.Find(id);

            db.Houses.Remove(house);

            db.SaveChanges();

            return RedirectToAction("Index");

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
