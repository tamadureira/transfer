using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Transfer.Core.Communication.Mediator;
using Transfer.Core.Messages;
using Transfer.Core.Messages.CommonMessages.Notifications;
using Transfer.Domain;
using Transfer.Domain.Enuns;

namespace Transfer.Application.Commands
{
    public class TransferCarryOutCommandHandler : IRequestHandler<TransferCarryOutCommand, bool>
    {
        private readonly ITransferRepository _transferRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public TransferCarryOutCommandHandler(ITransferRepository transferRepository,
                                              IMediatorHandler mediatorHandler)
        {
            _transferRepository = transferRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<bool> Handle(TransferCarryOutCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message)) return false;

            var transfer = ExecuteBusinessRule(message);
            _transferRepository.AddTransfer(transfer);

            return await _transferRepository.UnitOfWork.Commit();
        }

        private static Domain.Transfer ExecuteBusinessRule(TransferCarryOutCommand message)
        {
            var validReturn = new Domain.Transfer();
            var mookRule = new Random(40);

            if (mookRule.Next() >= 0 && mookRule.Next() <= 10) //APPROVED
            {
                validReturn.Id = message.ExternalID;
                validReturn.ExternalID = message.ExternalID;
                validReturn.Amount = message.Amount;
                validReturn.ExpectedOn = message.ExpectedOn;
                validReturn.Status = StatusTransfer.APPROVED;
            }
            else if (mookRule.Next() > 10 && mookRule.Next() <= 20) //CREATED
            {
                validReturn.Id = message.ExternalID;
                validReturn.ExternalID = message.ExternalID;
                validReturn.Amount = message.Amount;
                validReturn.ExpectedOn = message.ExpectedOn;
                validReturn.Status = StatusTransfer.CREATED;
            }
            else if (mookRule.Next() > 20 && mookRule.Next() <= 30) //REJECTED
            {
                validReturn.Id = message.ExternalID;
                validReturn.ExternalID = message.ExternalID;
                validReturn.Amount = message.Amount;
                validReturn.ExpectedOn = message.ExpectedOn;
                validReturn.Status = StatusTransfer.REJECTED;
            }
            else if (mookRule.Next() >= 30) //SCHEDULED
            {
                validReturn.Id = message.ExternalID;
                validReturn.ExternalID = message.ExternalID;
                validReturn.Amount = message.Amount;
                validReturn.ExpectedOn = message.ExpectedOn;
                validReturn.Status = StatusTransfer.SCHEDULED;
            }

            return validReturn;
        }

        private bool ValidarComando(Command message)
        {
            if (message.EhValido()) return true;

            foreach (var error in message.ValidationResult.Errors)
                _mediatorHandler.PublicarNotificacao(new DomainNotification(message.MessageType, error.ErrorMessage));

            return false;
        }
    }
}
