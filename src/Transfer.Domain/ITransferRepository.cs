using System;
using System.Threading.Tasks;
using Transfer.Core.Data;

namespace Transfer.Domain
{
    public interface ITransferRepository : IRepository<Transfer>
    {
        Task<Transfer> GetByInternalId(Guid internalId);
        Task<Transfer> GetByExternalId(Guid externalId);
        void AddTransfer(Transfer transfer);
    }
}
