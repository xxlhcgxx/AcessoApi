using System;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.Acesso;
using Api.Domain.Interfaces.Services.Log;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        private readonly ITransfer _service;
        private readonly ILoggerService _loggerService;
        private const string transaction = "Transaction";
        private const string confirmed = "Confirmed";


        public TransferController(ITransfer service, ILoggerService loggerService)
        {
            _service = service;
            _loggerService = loggerService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> Get(Guid Id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var getLog = await _service.GetTransfer(Id);
                dynamic retorno = new { };
                retorno = new { Status = getLog.Status };

                if (getLog.Status != confirmed)
                {
                    retorno = new { Status = getLog.Status, Message = getLog.Message };
                    await _loggerService.LogInformation(new LoggerRequestEntity() { ApplicationName = transaction, Message = Id.ToString() + " - " + getLog.Message });
                }
                else {
                    retorno = new { Status = getLog.Status };
                    await _loggerService.LogInformation(new LoggerRequestEntity() { ApplicationName = transaction, Message = Id.ToString() + " - " + getLog.Status });
                }

                return Ok(retorno);
            }
            catch (ArgumentException ex)
            {
                await _loggerService.LogError(new LoggerRequestEntity() { ApplicationName = transaction, Message = ex.Message });
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(TransferEntity transfer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var getLog = await _service.Transaction(transfer);
                await _loggerService.LogInformation(new LoggerRequestEntity() { ApplicationName = transaction, Message = getLog.transactionId.ToString() });

                return Ok(getLog);
            }
            catch (ArgumentException ex)
            {
                await _loggerService.LogError(new LoggerRequestEntity() { ApplicationName = transaction, Message = ex.Message });
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
