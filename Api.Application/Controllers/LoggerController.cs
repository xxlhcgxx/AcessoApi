using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.Log;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [ApiController]
    public class LoggerController : ControllerBase
    {
        private readonly ILoggerService _loggerService;
        public LoggerController(ILoggerService loggerService)
        {
            _loggerService = loggerService;
        }

        [HttpPost]
        [Route("info/")]
        public async Task<IActionResult> LogInformation([FromBody] LoggerRequestEntity request)
        {
            if (request == null)
                return BadRequest();

            await _loggerService.LogInformation(request);
            return Ok();
        }

        [HttpPost]
        [Route("warning/")]
        public async Task<IActionResult> LogWarning([FromBody] LoggerRequestEntity request)
        {
            if (request == null)
                return BadRequest();

            await _loggerService.LogWarning(request);
            return Ok();
        }

        [HttpPost]
        [Route("error/")]
        public async Task<IActionResult> LogError([FromBody] LoggerRequestEntity request)
        {
            if (request == null)
                return BadRequest();

            await _loggerService.LogError(request);
            return Ok();
        }
    }
}
