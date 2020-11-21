using CarInsurance.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Xml.Linq;

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

        public partial class Insuree
        {
            public int Id { get; set; }
            [Display(Name = "First Name")]
            [Required]
            public string FirstName { get; set; }
            [Display(Name = "Last Name")]
            [Required]
            public string LastName { get; set; }
            [Display(Name = "Email")]
            [Required]
            public string EmailAddress { get; set; }
            [Display(Name = "Date of Birth")]
            [Required]
            public System.DateTime DateOfBirth { get; set; }
            [Display(Name = "Year")]
            [Required]
            public int CarYear { get; set; }
            [Display(Name = "Make")]
            [Required]
            public string CarMake { get; set; }
            [Display(Name = "Model")]
            [Required]
            public string CarModel { get; set; }
            [Display(Name = "DUI")]
            public bool DUI { get; set; }
            [Display(Name = "Number of Speeding Tickets")]
            [Required]
            public int SpeedingTickets { get; set; }
            [Display(Name = "Add Collision")]
            public bool CoverageType { get; set; }
            public decimal? Quote
            {
                get
                {
                    Insuree insuree = new Insuree();
                    insuree.DateOfBirth = DateOfBirth;
                    insuree.CarYear = CarYear;
                    insuree.CarMake = CarMake.ToLower();
                    insuree.CarModel = CarModel.ToLower();
                    insuree.DUI = DUI;
                    insuree.SpeedingTickets = SpeedingTickets;
                    insuree.CoverageType = CoverageType;

                    float quote = 0f;
                    int monthlyQuote = 50;
                    int extras = 0;
                    int age = Convert.ToInt32(DateTime.Now.Year - insuree.DateOfBirth.Year);
                    if (age <= 18)
                    {
                        monthlyQuote += 100;
                    }
                    else if (age >= 19 && age <= 25)
                    {
                        monthlyQuote += 50;
                    }
                    else
                    {
                        monthlyQuote += 25;
                    }

                    if (insuree.CarYear >= 2000 && insuree.CarYear <= 2015)
                    {
                        monthlyQuote += 0;
                    }
                    else
                    {
                        monthlyQuote += 25;
                    }

                    if (insuree.CarMake == "Porsche")
                    {
                        extras += 25;

                        if (insuree.CarModel == "911 carrera")
                        {
                            extras += 50;
                        }
                    }

                    monthlyQuote += insuree.SpeedingTickets * 10;

                    quote = (monthlyQuote) + extras;

                    if (insuree.DUI)
                    {
                        quote = (float)(quote * 1.25);
                    }

                    if (insuree.CoverageType)
                    {
                        quote = (float)(quote * 1.5);
                    }
                    return (decimal)quote;
                }
                set
                {
                    // A dynamically computed field needs an empty set to fulfill prop requirements
                }
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,CoverageType,Quote")] Insuree insuree)
        {
            if (ModelState.IsValid)
            {
                Convert.ToInt32();
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
                return RedirectToAction("Index");
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