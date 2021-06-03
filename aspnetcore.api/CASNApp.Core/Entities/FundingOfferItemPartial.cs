using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CASNApp.Core.Interfaces;

namespace CASNApp.Core.Entities
{
    public partial class FundingOfferItem : ISoftDelete, ICreatedDate, IUpdatedDate, IValidatableObject
    {

        [NotMapped]
        public bool AllowsEdits
        {
            get
            {
                if (FundingOffer == null || FundingSource == null || !PaymentMethodId.HasValue)
                {
                    return false;
                }

                if (FundingOffer.AllowsEdits)
                {
                    return true;
                }

                if (FundingSource.IsExternal || PaymentMethodId.Value != PaymentMethod.Voucher)
                {
                    return true;
                }

                return false;
            }
        }

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
