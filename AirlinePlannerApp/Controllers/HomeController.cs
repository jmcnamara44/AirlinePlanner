using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AirlinePlannerApp.Models;
using System;

namespace AirlinePlannerApp.Controllers
{
    public class HomeController : Controller
    {
      [HttpGet("/")]
        public ActionResult Index()
        {
          List<Flights> newFlights = Flights.GetAll();
          Flights newFlight = newFlights[1];
          List<Cities> newCities = newFlight.GetDepartureCities();
          return View(newCities);
        }


    }
}
