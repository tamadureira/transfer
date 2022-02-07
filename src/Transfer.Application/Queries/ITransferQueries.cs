using System;
using System.Threading.Tasks;
using Transfer.Application.DTO;

namespace Transfer.Application.Queries
{
    public interface ITransferQueries
    {
        Task<TransferResponse> ConsultTransferByInternalId(Guid internalId);
        Task<TransferResponse> ConsultTransferByExternalId(Guid externalId);
    }
}
