using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Transfer.Core.Data;
using Transfer.Domain;
using Transfer.Infra.Data.Context;

namespace Transfer.Infra.Repository
{
    public class TransferRepository : ITransferRepository
    {
        private readonly TransferContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public TransferRepository(TransferContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<Domain.Transfer> GetByInternalId(Guid internalId)
        {
            var transfer = await _context.FindAsync<Domain.Transfer>(internalId);
            if (transfer == null) return null;

            return transfer;
        }

        public async Task<Domain.Transfer> GetByExternalId(Guid externalId)
        {
            var transfer = await _context.HistoryTransfers.FromSqlRaw("SELECT * FROM HistoryTransfers where ExternalID = {0}", externalId).FirstOrDefaultAsync();
            if (transfer == null) return null;

            return transfer;
        }

        public void AddTransfer(Domain.Transfer transfer)
        {
            _context.Add(transfer);
        }
    }
}
