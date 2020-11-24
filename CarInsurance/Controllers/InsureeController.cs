using CarInsurance.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CarInsurance.Controllers
{
    public class InsureeController : Controller
    {
       private InsuranceEntities db = new InsuranceEntities();
       
        // GET: Insuree Offer
        public ActionResult Index()
        {
            return View(db.Insurees.ToList());
        }
        public ActionResult Admin()
        {
            return View(db.Insurees.ToList());
        }

        // GET: Insuree/Details/?<int:id>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // GET: Insuree/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: Insuree/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

      [HttpPost]
      [ValidateAntiForgeryToken]
            public ActionResult Create([Bind(Include = "Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,CoverageType,Quote")] Insuree insuree)
        {
            if (ModelState.IsValid)
            {
                //set up the variables
                int age = Convert.ToInt32(DateTime.Now.Year - insuree.DateOfBirth.Year);

                //base cost $50/month
                insuree.Quote = 50;
                if (age <= 18)
                {
                    insuree.Quote += 100;
                }
                else if (age >= 19 && age <= 25)
                {
                    insuree.Quote += 50;
                }
                else
                {
                    insuree.Quote += 25;
                }

                if (insuree.CarYear < 2000 || insuree.CarYear > 2015)
                {
                    insuree.Quote += 25;
                }

                if (insuree.CarMake == "Porsche")
                {
                    insuree.Quote += 25;

                    if (insuree.CarModel == "911 carrera")
                    {
                        insuree.Quote += 25;
                    }
                }

                if (insuree.SpeedingTickets > 0)
                {
                    insuree.Quote += insuree.SpeedingTickets * 10;
                }

                if (insuree.DUI)
                {
                    insuree.Quote *= 1.25m;
                }

                if (insuree.CoverageType)
                {
                    insuree.Quote *= 1.5m;
                }

                 db.Insurees.Add(insuree);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(insuree);
        }

        // GET: Insuree/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // POST: Insuree/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,CoverageType,Quote")] Insuree insuree)
        {
            if (ModelState.IsValid)
            {
                db.Entry(insuree).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", insuree);
            }
            return View(insuree);
        }

        // GET: Insuree/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // POST: Insuree/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Insuree insuree = db.Insurees.Find(id);
            Insuree insuree1 = db.Insurees.Remove(insuree);
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