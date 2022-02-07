using FluentValidation;
using System;
using Transfer.Core.Messages;

namespace Transfer.Application.Commands
{
    public class TransferCarryOutCommand : Command
    {
        public Guid ExternalID { get; set; }
        public decimal Amount { get; set; }
        public DateTime ExpectedOn { get; set; }

        public TransferCarryOutCommand(Guid externalID, decimal amount, DateTime expectedOn)
        {
            ExternalID = externalID;
            Amount = amount;
            ExpectedOn = expectedOn;
        }

        public override bool EhValido()
        {
            ValidationResult = new TransferCarryOutValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class TransferCarryOutValidation : AbstractValidator<TransferCarryOutCommand>
        {
            public TransferCarryOutValidation()
            {
                RuleFor(c => c.ExternalID)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Invalid ExternalID");

                RuleFor(c => c.Amount)
                    .GreaterThan(0)
                    .WithMessage("Invalid Amount");

                RuleFor(s => s.ExpectedOn)
                    .Must(IsvalidDate)
                    .WithMessage("Invalid ExpectedOn");

            }

            private bool IsvalidDate(DateTime value)
            {
                return DateTime.TryParse(value.ToString(), out DateTime date);
            }
        }
    }
}
