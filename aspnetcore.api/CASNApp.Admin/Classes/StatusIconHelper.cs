using CASNApp.Core.Entities;

namespace CASNApp.Admin.Classes
{
    public static class StatusIconHelper
    {
        public static string GetHtml(int statusId)
        {
            string statusName;
            string iconName;

            switch (statusId)
            {
                case FundingOfferStatus.Draft:
                    statusName = nameof(FundingOfferStatus.Draft);
                    iconName = "file-earmark-medical";
                    break;
                case FundingOfferStatus.Issued:
                    statusName = nameof(FundingOfferStatus.Issued);
                    iconName = "file-earmark-text";
                    break;
                case FundingOfferStatus.Redeemed:
                    statusName = nameof(FundingOfferStatus.Redeemed);
                    iconName = "check2-circle";
                    break;
                case FundingOfferStatus.Paid:
                    statusName = nameof(FundingOfferStatus.Paid);
                    iconName = "receipt-cutoff";
                    break;
                case FundingOfferStatus.Voided:
                    statusName = nameof(FundingOfferStatus.Voided);
                    iconName = "x-circle";
                    break;
                default:
                    return "";
            }

            statusName = statusName.ToLowerInvariant();

            var result = $"<i class=\"bi-{iconName} status-{statusName}\"></i>";

            return result;
        }

    }
}
