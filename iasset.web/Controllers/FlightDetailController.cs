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
        public JsonResult<FlightScheduleResponse> Post([FromBody]FlightDetailPost input)
        {
            try
            {
                var response = _flightGateService.UpdateFlightDetail(input.flightDetailId, input.flightId, input.gateId, input.arrivalDateTime, input.departureDateTime);
                return Json(response);
            }
            catch (ArgumentException exception)
            {
                var response = new FlightScheduleResponse { IsSuccess = false, Message = exception.Message };
                return Json(response);
            }
            catch (FlightSchedulingException)
            {
                var response = new FlightScheduleResponse { IsSuccess = false, Message = "Unable to Add Flight due to scheduling conflict." };
                return Json(response);
            }
            catch
            {
                var response = new FlightScheduleResponse { IsSuccess = false, Message = "Update Failed." };
                return Json(response);
            }
            
        }

        // PUT: api/FlightDetail/5
        public JsonResult<FlightScheduleResponse> Put([FromBody]FlightDetailPut input)
        {
            try
            {
                var response = _flightGateService.AddFlightDetail(input.flightId, input.gateId, input.arrivalDateTime,
                    input.departureDateTime);
                return Json(response);
            }
            catch (ArgumentException exception)
            {
                var response = new FlightScheduleResponse { IsSuccess = false, Message = exception.Message};
                return Json(response);
            }
            catch (FlightSchedulingException)
            {
                var response = new FlightScheduleResponse {IsSuccess = false, Message = "Unable to Add Flight due to scheduling conflict."};
                return Json(response);
            }
            catch
            {
                var response = new FlightScheduleResponse { IsSuccess = false, Message = "Update Failed." };
                return Json(response);
            }
        }

        // DELETE: api/FlightDetail/5
        [Route("api/FlightDetail/Delete/{flightDetailId}")]
        public void Delete(string flightDetailId)
        {
            _flightGateService.CancelFlightDetail(new Guid(flightDetailId));
        }
    }
}
