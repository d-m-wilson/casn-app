using CASNApp.Core.Interfaces;

namespace CASNApp.Core.Entities
{
    public partial class DriveLogStatus : ICreatedDate, IUpdatedDate, ISoftDelete
    {
        public const int APPLIED = 1;
        public const int CANCELED = 2;
        public const int APPROVED = 3;
        public const int REASSIGNED = 4;

    }
}
