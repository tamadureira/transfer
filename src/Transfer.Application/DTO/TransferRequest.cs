using System;

namespace Transfer.Application.ViewModels
{
    public class TransferRequest
    {
        public Guid ExternalID { get; set; }
        public decimal Amount { get; set; }
        public DateTime ExpectedOn { get; set; }
    }
}
