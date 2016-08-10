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

        public class FlightDetailPost
        {
            public Guid flightDetailId;
            public Guid flightId;
            public Guid gateId;
            public DateTime arrivalDateTime;
            public DateTime departureDateTime;
        }

        // POST: api/FlightDetail
        public void Post([FromBody]FlightDetailPost input)
        {
            _flightGateService.UpdateFlightDetail(input.flightDetailId, input.flightId, input.gateId, input.arrivalDateTime, input.departureDateTime);
        }

        public class FlightDetailPut
        {
            public Guid flightId;
            public Guid gateId;
            public DateTime arrivalDateTime;
            public DateTime departureDateTime;
        }

        // PUT: api/FlightDetail/5
        public void Put([FromBody]FlightDetailPut input)
        {
            _flightGateService.AddFlightDetail(input.flightId, input.gateId, input.arrivalDateTime, input.departureDateTime);
        }

        // DELETE: api/FlightDetail/5
        [Route("api/FlightDetail/Delete/{flightDetailId}")]
        public void Delete(string flightDetailId)
        {
            _flightGateService.CancelFlightDetail(new Guid(flightDetailId));
        }
    }
}
