using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iasset.web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddNewFlightDetail()
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