using Microsoft.AspNetCore.Mvc;
using WeatherStation.Entities;
using WeatherStation.Services;

namespace WeatherStation.Controllers
{
    [Route("api/measurment")]
    [ApiController]
    public class MeasurmentController : ControllerBase
    {
        private readonly IMeasurmentService _measurmentService;

        public MeasurmentController(IMeasurmentService measurmentService)
        {
            _measurmentService = measurmentService;
        }

        [HttpGet]
        public ActionResult GetAllMeasurments()
        {
            return Ok(_measurmentService.GetMeasurements());
        }

        [HttpPost]
        public ActionResult SaveMeasurment([FromBody] Measurement measure)
        {
            _measurmentService.SaveMeasure(measure);
            return NoContent();
        }

        [HttpDelete]
        public ActionResult DropDatabase()
        {
            _measurmentService.DropDatabase();
            return NoContent();
        }
    }
}
