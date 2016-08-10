using System.Collections.Generic;
using iasset.core.Services;
using System.Web.Http;
using System.Web.Http.Results;
using iasset.core;

namespace iasset.web.Controllers
{
    [Authorize]
    public class GatesController : ApiController
    {
        private readonly IFlightGateService _flightGateService;

        public GatesController()
        {
            _flightGateService = new FlightGateService();
        }

        public JsonResult<IEnumerable<Gate>> GetAll()
        {
            return Json(_flightGateService.GetAllGates());
        }
    }
}
