using System;
using System.Threading.Tasks;
using Transfer.Application.DTO;
using Transfer.Domain;

namespace Transfer.Application.Queries
{
    public class TransferQueries : ITransferQueries
    {
        private readonly ITransferRepository _transferRepository;

        public TransferQueries(ITransferRepository transferRepository)
        {
            _transferRepository = transferRepository;
        }

        public async Task<TransferResponse> ConsultTransferByInternalId(Guid internalId)
        {
            return MapTransferToTransferResponse(await _transferRepository.GetByInternalId(internalId));
        }

        public async Task<TransferResponse> ConsultTransferByExternalId(Guid externalId)
        {
            return MapTransferToTransferResponse(await _transferRepository.GetByExternalId(externalId));
        }

        private static TransferResponse MapTransferToTransferResponse(Domain.Transfer transfer)
        {
            if (transfer == null)
                return null;

            return new TransferResponse()
            {
                Status = transfer.Status ?? null,
                Amount = transfer.Amount,
                ExpectedOn = transfer.ExpectedOn,
                ExternalID = transfer.ExternalID,
                ID = transfer.Id
            };
        }


    }
}
