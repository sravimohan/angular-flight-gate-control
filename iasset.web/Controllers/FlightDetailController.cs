using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using iasset.core;
using iasset.core.Services;
using iasset.web.Models;

namespace iasset.web.Controllers
{
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
        public void Post([FromBody]FlightDetailPost input)
        {
            _flightGateService.UpdateFlightDetail(input.flightDetailId, input.flightId, input.gateId, input.arrivalDateTime, input.departureDateTime);
        }

        // PUT: api/FlightDetail/5
        public JsonResult<FlightScheduleResponse> Put([FromBody]FlightDetailPut input)
        {
            var response = _flightGateService.AddFlightDetail(input.flightId, input.gateId, input.arrivalDateTime, input.departureDateTime);
            return Json(response);
        }

        // DELETE: api/FlightDetail/5
        [Route("api/FlightDetail/Delete/{flightDetailId}")]
        public void Delete(string flightDetailId)
        {
            _flightGateService.CancelFlightDetail(new Guid(flightDetailId));
        }
    }
}
