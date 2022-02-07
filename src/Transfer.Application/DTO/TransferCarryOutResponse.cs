using System;
using Transfer.Domain.Enuns;

namespace Transfer.Application.DTO
{
    public class TransferCarryOutResponse
    {
        public Guid ExternalID { get; set; }
        public StatusTransfer Status { get; set; }
    }
}
