using Microsoft.AspNetCore.Mvc;
using UnitConverstion.Commands;
using UnitConverstion.CommandHandlers;
using UnitConverstion.Models;

namespace UnitConverstion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UnitConversionController : ControllerBase
    {
        private readonly ConvertUnitCommandHandler _handler;

        public UnitConversionController(
            ConvertUnitCommandHandler handler)
        {
            _handler = handler;
        }

        [HttpPost]
        public ActionResult<ConversionResult> Convert(
            [FromBody] ConvertUnitCommand command)
        {
            var result = _handler.Handle(command);

            if (!string.IsNullOrEmpty(result.Message))
            {
                return BadRequest(new { error = result.Message });
            }

            return Ok(result);
        }

        [HttpGet("health")]
        public IActionResult Health()
        {
            return Ok(new
            {
                status = "Unit Conversion API is running."
            });
        }
    }
}