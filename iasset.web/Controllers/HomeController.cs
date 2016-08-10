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

        public ActionResult AddNewFlight()
        {
            return View();
        }

        public ActionResult EditFlight(Guid id)
        {
            ViewBag.Id = id;
            return View();
        }
    }
}