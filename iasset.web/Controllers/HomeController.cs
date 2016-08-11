﻿using System;
using System.Web.Mvc;

namespace iasset.web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EditFlightDetail(Guid id)
        {
            ViewBag.FlightDetailId = id;
            return View();
        }
    }
}