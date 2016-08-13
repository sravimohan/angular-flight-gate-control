using System.Web.Mvc;
using iasset.core.Repository;

namespace iasset.web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Reset()
        {
            new FlightGateRepository().InitData(true);
            return View("Index");
        }
    }
}