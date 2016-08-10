using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using iasset.core;
using iasset.core.Services;

namespace iasset.web.Controllers
{
    [Authorize]
    public class FlightDetailController : ApiController
    {
        private readonly IFlightGateService _flightGateService;

        public FlightDetailController()
        {
            _flightGateService = new FlightGateService();
        }

        // GET: api/FlightDetail/5
        public JsonResult<IEnumerable<FlightDetail>> GetAll()
        {
            return Json(_flightGateService.GetAllFlightDetails());
        }

        // GET: api/FlightDetail/5
        public JsonResult<FlightDetail> Get(Guid id)
        {
            return Json(_flightGateService.GetFlightDetail(id));
        }

        // POST: api/FlightDetail
        public void Post([FromBody]Guid flightDetailId, Guid flightId, Guid gateId, DateTime arrivalDateTime, DateTime departureDateTime)
        {
            _flightGateService.UpdateFlightDetail(flightDetailId, flightId, gateId, arrivalDateTime, departureDateTime);
        }

        public class PutInput
        {
            public Guid flightId;
            public Guid gateId;
            public DateTime arrivalDateTime;
            public DateTime departureDateTime;
        }

        // PUT: api/FlightDetail/5
        public void Put([FromBody]PutInput input)
        {
            _flightGateService.AddFlightDetail(input.flightId, input.gateId, input.arrivalDateTime, input.departureDateTime);
        }

        // DELETE: api/FlightDetail/5
        public void Delete(Guid id)
        {
            _flightGateService.CancelFlightDetail(id);
        }
    }
}
