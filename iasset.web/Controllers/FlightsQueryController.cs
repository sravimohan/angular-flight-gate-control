using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using iasset.core;
using iasset.core.Services;

namespace iasset.web.Controllers
{
    public class FlightsQueryController : ApiController
    {
        private readonly IFlightGateService _flightGateService;

        public FlightsQueryController()
        {
            _flightGateService = new FlightGateService();
        }

        // GET: api/FlightDetail
        public JsonResult<IEnumerable<FlightDetail>> Get(Guid gateId, DateTime date)
        {
            return Json(_flightGateService.GetFlightDetails(gateId, date));
        }
    }
}
