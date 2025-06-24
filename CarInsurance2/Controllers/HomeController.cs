using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarInsurance2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
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
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Create(Insuree insuree)
{
    if (ModelState.IsValid)
    {
        // Base quote
        decimal quote = 50m;

        // Calculate Age
        int age = DateTime.Now.Year - insuree.DateOfBirth.Year;
        if (insuree.DateOfBirth > DateTime.Now.AddYears(-age)) age--;

        // Age-based cost
        if (age <= 18)
        {
            quote += 100;
        }
        else if (age >= 19 && age <= 25)
        {
            quote += 50;
        }
        else if (age >= 26)
        {
            quote += 25;
        }

        // Car Year check
        if (insuree.CarYear < 2000)
        {
            quote += 25;
        }
        if (insuree.CarYear > 2015)
        {
            quote += 25;
        }

        // Car Make and Model
        if (!string.IsNullOrEmpty(insuree.CarMake) && insuree.CarMake.ToLower() == "porsche")
        {
            quote += 25;

            if (!string.IsNullOrEmpty(insuree.CarModel) && insuree.CarModel.ToLower() == "911 carrera")
            {
                quote += 25;
            }
        }

        // Speeding Tickets
        quote += insuree.SpeedingTickets * 10;

        // DUI - Add 25% to the total
        if (insuree.DUI)
        {
            quote *= 1.25m;
        }

        // Full Coverage - Add 50% to the total
        if (insuree.CoverageType)
        {
            quote *= 1.5m;
        }

        // Save the calculated quote
        insuree.Quote = Math.Round(quote, 2); // optional rounding

        // Save to database
        db.Insurees.Add(insuree);
        db.SaveChanges();
        return RedirectToAction("Index");
    }

    return View(insuree);
}

        }
    }
}
