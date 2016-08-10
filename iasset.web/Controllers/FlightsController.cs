﻿using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using iasset.core;
using iasset.core.Services;

namespace iasset.web.Controllers
{
    [Authorize]
    public class FlightsController : ApiController
    {
        private readonly IFlightGateService _flightGateService;

        public FlightsController()
        {
            _flightGateService = new FlightGateService();
        }

        public JsonResult<IEnumerable<Flight>> GetAll()
        {
            return Json(_flightGateService.GetAllFlights());
        }
    }
}
