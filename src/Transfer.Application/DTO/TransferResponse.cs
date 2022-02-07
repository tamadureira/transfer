using System;
using Transfer.Domain.Enuns;

namespace Transfer.Application.DTO
{
    public class TransferResponse
    {
        public Guid ID { get; set; }
        public Guid ExternalID { get; set; }
        public StatusTransfer? Status { get; set; }
        public decimal Amount { get; set; }
        public DateTime ExpectedOn { get; set; }
    }
}
