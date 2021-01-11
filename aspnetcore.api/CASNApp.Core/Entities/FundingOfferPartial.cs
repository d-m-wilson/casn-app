using System;
using System.ComponentModel.DataAnnotations.Schema;
using CASNApp.Core.Interfaces;

namespace CASNApp.Core.Entities
{
    public partial class FundingOffer : ISoftDelete, ICreatedDate, IUpdatedDate
    {
        private int _FundingOfferStatusId;

        public int FundingOfferStatusId { get { return _FundingOfferStatusId; } set { UpdateStatus(value); } }

        [NotMapped]
        public bool AllowsEdits => _FundingOfferStatusId == FundingOfferStatus.Draft;

        protected void UpdateStatus(int newFundingOfferStatusId)
        {
            // if the current value is zero then assume that we're deserializing
            // i.e. just do the assignment and nothing else
            if (_FundingOfferStatusId == 0)
            {
                _FundingOfferStatusId = newFundingOfferStatusId;
                return;
            }

            if (!FundingOfferStatus.IsValidStateTransition(_FundingOfferStatusId, newFundingOfferStatusId))
            {
                throw new InvalidOperationException();
            }

            switch (newFundingOfferStatusId)
            {
                case FundingOfferStatus.Draft:
                    break;
                case FundingOfferStatus.Issued:
                    Issued = DateTime.UtcNow;
                    break;
                case FundingOfferStatus.Redeemed:
                    Redeemed = DateTime.UtcNow;
                    break;
                case FundingOfferStatus.Paid:
                    Paid = DateTime.UtcNow;
                    break;
                case FundingOfferStatus.Voided:
                    Voided = DateTime.UtcNow;
                    break;
                default:
                    throw new InvalidOperationException();
            }

            _FundingOfferStatusId = newFundingOfferStatusId;
        }

        public void SetVolunteerId(int newFundingOfferStatusId, int volunteerId)
        {
            switch (newFundingOfferStatusId)
            {
                case FundingOfferStatus.Draft:
                    CreatedById = volunteerId;
                    break;
                case FundingOfferStatus.Issued:
                    IssuedById = volunteerId;
                    break;
                case FundingOfferStatus.Redeemed:
                    break;
                case FundingOfferStatus.Paid:
                    break;
                case FundingOfferStatus.Voided:
                    VoidedById = volunteerId;
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

    }
}
