using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CASNApp.Core.Interfaces;

namespace CASNApp.Core.Entities
{
    public partial class FundingOfferItem : ISoftDelete, ICreatedDate, IUpdatedDate, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!NeedAmount.HasValue && !NeedAmountNullReasonId.HasValue)
            {
                yield return new ValidationResult("Need Amount cannot be empty unless a reason is selected.", new[] { nameof(NeedAmountNullReasonId) });
            }

            if (!FundingAmount.HasValue && !FundingAmountNullReasonId.HasValue)
            {
                yield return new ValidationResult("Funding Amount cannot be empty unless a reason is selected.", new[] { nameof(FundingAmountNullReasonId) });
            }
        }

    }
}
