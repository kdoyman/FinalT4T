﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tip4Trip_aka.Models;

namespace Tip4Trip_aka.Controllers
{
    [Authorize]
    public class ReservationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reservations
        public ActionResult Index()
        {
            var reservations = db.Reservations.Include(r => r.HouseRes);
            return View(reservations.ToList());
        }

        // GET: Reservations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            var poso = reservation.PricePerNightCharged;
            //ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var house = db.Houses.Where(c => c.Id == reservation.HouseId).First();
            if (reservation.RenterId == house.OwnerId) { poso = 0; }
            if (reservation.EndDate.Year == reservation.StartDate.Year)
            {
                double dx = (double)(reservation.EndDate.DayOfYear - reservation.StartDate.DayOfYear) * poso;
                ViewBag.Message = "Your Reservation Total is :" + dx;
                return View(reservation);
            }
            if (reservation.EndDate.Year - reservation.StartDate.Year == 1)
            {
                double dx = (double)((reservation.EndDate.DayOfYear * poso) + ((365 - reservation.StartDate.DayOfYear) * poso));
                ViewBag.Message = "Your Reservation Total is :" + dx;
                return View(reservation);
            }
            if (reservation.EndDate.Year - reservation.StartDate.Year > 1)
            {
                double dx = (double)((reservation.EndDate.DayOfYear * poso) + ((365 - reservation.StartDate.DayOfYear) * poso) + ((reservation.EndDate.Year - reservation.StartDate.Year - 1) * poso));
                ViewBag.Message = "Your Reservation Total is :" + dx;
                return View(reservation);
            }
            return View();
        }

        // GET: Reservations/Create
        public ActionResult Create()
        {
            ViewBag.HouseId = new SelectList(db.Houses, "Id", "Title");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,HouseId,Renter,StartDate,EndDate,Occupants,DateOfBooking,CustommerComments,PricePerNightCharged")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                foreach (Reservation item in db.Reservations.Where(x => x.HouseId == reservation.HouseId))
                {
                    if ((reservation.StartDate >= item.StartDate && reservation.StartDate < item.EndDate) || (reservation.EndDate > item.StartDate && reservation.EndDate <= item.EndDate))
                    {
                        //return Content(" The Dates You want to rent the House are already closed , Sorry ! Please check the list and try again ");
                        ViewBag.Message = "The Dates you want to rent the House are already booked , Sorry ! Please check the list and try again";
                        return View();
                    }

                }
                db.Reservations.Add(reservation);
                db.SaveChanges();
                return RedirectToAction("MyReservations", "Home");
                //return RedirectToAction("mazi","Houses",new { idilicious=reservation.HouseId });
            }

            ViewBag.HouseId = new SelectList(db.Houses, "Id", "Title", reservation.HouseId);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            ViewBag.HouseId = new SelectList(db.Houses, "Id", "Title", reservation.HouseId);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,HouseId,renter,StartDate,EndDate,Occupants,DateOfBooking,CustommerComments,PricePerNightCharged")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HouseId = new SelectList(db.Houses, "Id", "Title", reservation.HouseId);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            db.Reservations.Remove(reservation);
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
