using System;
using Transfer.Core.DomainObjects;
using Transfer.Domain.Enuns;

namespace Transfer.Domain
{
    public class Transfer : Entity, IAggregateRoot
    {
        public Guid ExternalID { get; set; }
        public decimal Amount { get; set; }
        public DateTime ExpectedOn { get; set; }
        public StatusTransfer? Status { get; set; }
    }
}
