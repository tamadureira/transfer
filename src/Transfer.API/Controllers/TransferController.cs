using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Transfer.Application.Commands;
using Transfer.Application.DTO;
using Transfer.Application.Queries;
using Transfer.Application.ViewModels;
using Transfer.Core.Communication.Mediator;
using Transfer.Core.Messages.CommonMessages.Notifications;
using Transfer.Domain.Enuns;

namespace Transfer.API.Controllers
{
    [Route("api/transfer")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        private readonly ITransferQueries _transferQueries;
        private readonly IMediatorHandler _mediatorHandler;

        public TransferController(INotificationHandler<DomainNotification> notifications,
                                  IMediatorHandler mediatorHandler,
                                  ITransferQueries transferQueries
            ) : base(notifications, mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
            _transferQueries = transferQueries;
        }

        /// <summary>
        /// Consult the transfer settlement.
        /// </summary>
        /// <param name="internalId">Internal transfer ID</param>
        /// <returns>
        /// Status code: 200 => Return object TransferResponse with data Transfer
        /// Status code: 500 => internal server error
        /// Status code: 404 => Transfer not found  with especific message
        /// Object </returns>
        [HttpPatch]
        [Route("paymentOrders/{internalId}")]
        public async Task<IActionResult> ConsultTransfer(Guid internalId)
        {
            var transferResponse = await _transferQueries.ConsultTransferByInternalId(internalId);
            if (transferResponse != null)
                return Ok(transferResponse);

            return NotFound("Transfer not found");

            //TODO: Tratar erros 500
        }

        /// <summary>
        /// Settle the transfer.
        /// </summary>
        /// <param name="transferRequest">Part of the data transfer</param>
        /// <returns>
        /// Status code: 201 => Created transfer with TransferCarryOutResponse object
        /// Status code: 500 => Internal server error
        /// Status code: 405 => Business logic error
        /// Status code: 404 => Transfer not found  with especific message
        /// Status code: 400 => Service bad request with especific message
        /// </returns>
        [HttpPatch]
        [Route("paymentOrders")]
        public async Task<IActionResult> TransferCarryOut([FromBody] TransferRequest transferRequest)
        {
            if (transferRequest.ExternalID == Guid.Empty)
                return BadRequest("Invalid or nulable ExternalID");

            var transferResponse = await _transferQueries.ConsultTransferByExternalId(transferRequest.ExternalID);
            if (transferResponse == null)
                return NotFound("Transfer not found");

            var command = new TransferCarryOutCommand(transferRequest.ExternalID, transferRequest.Amount, transferRequest.ExpectedOn);
            bool comandSuccessfully = await _mediatorHandler.EnviarComando(command);

            if (OperacaoValida())
            {
                if (comandSuccessfully)
                {
                    transferResponse = await _transferQueries.ConsultTransferByExternalId(transferRequest.ExternalID);
                    if (transferResponse != null && transferResponse.Status == Domain.Enuns.StatusTransfer.CREATED)
                        return Created("paymentOrders", new TransferCarryOutResponse() { ExternalID = transferResponse.ExternalID, Status = (StatusTransfer)transferResponse.Status });
                    else
                        return new StatusCodeResult(405);
                }
                else
                    return new StatusCodeResult(405);
            }

            var erros = ObterMensagensErro();
            return BadRequest(erros);

            //TODO: Tratar erros 500
        }
    }
}
