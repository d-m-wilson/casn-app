using System.Collections.Generic;
using System.Linq;
using CASNApp.Core.Interfaces;

namespace CASNApp.Core.Entities
{
    public partial class FundingOfferStatus : ISoftDelete, ICreatedDate, IUpdatedDate
    {
        public const int Draft = 1;
        public const int Issued = 2;
        public const int Redeemed = 3;
        public const int Paid = 4;
        public const int Voided = 5;

        public static readonly IReadOnlyDictionary<int, IReadOnlyCollection<int>> ValidStateTransitions;

        static FundingOfferStatus()
        {
            ValidStateTransitions = new Dictionary<int, IReadOnlyCollection<int>>
            {
                { Draft, new List<int>() { Issued, Voided } },
                { Issued, new List<int>() { Redeemed, Voided } },
                { Redeemed, new List<int>() { Paid, Voided } },
                { Paid, new List<int>() { Voided } },
                { Voided, new List<int>() { } }
            };
        }

        public static bool IsValidStateTransition(int currentStatusId, int newStatusId)
        {
            if (!ValidStateTransitions.ContainsKey(currentStatusId))
            {
                return false;
            }

            var validStates = ValidStateTransitions[currentStatusId].ToList();

            if (!validStates.Contains(newStatusId))
            {
                return false;
            }

            return true;
        }

    }
}
