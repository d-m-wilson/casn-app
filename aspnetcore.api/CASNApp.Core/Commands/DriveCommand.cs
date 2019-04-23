using System.Threading.Tasks;
using CASNApp.Core.Entities;
using CASNApp.Core.Misc;
using CASNApp.Core.Queries;

namespace CASNApp.Core.Commands
{
    public partial class DriveCommand
    {
        private readonly casn_appContext dbContext;

        public DriveCommand(casn_appContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<CommandResult<Models.Drive>> CancelDriveAsync(uint driveId, uint cancelReasonId)
        {
            var driveQuery = new DriveQuery(dbContext);
            var drive = await driveQuery.GetDriveAsync(driveId);

            if (drive == null)
            {
                return new CommandResult<Models.Drive>(ErrorCode.NotFound, null);
            }

            if (drive.StatusId != Models.Drive.StatusOpen &&
                drive.StatusId != Models.Drive.StatusPending &&
                drive.StatusId != Models.Drive.StatusApproved)
            {
                return new CommandResult<Models.Drive>(ErrorCode.InvalidOperation, null);
            }

            var driveCancelReasonQuery = new DriveCancelReasonQuery(dbContext);
            var driveCancelReason = await driveCancelReasonQuery.GetActiveCancelReasonAsync(cancelReasonId, true);

            if (driveCancelReason == null)
            {
                return new CommandResult<Models.Drive>(ErrorCode.ChildEntityNotFound, null);
            }

            drive.StatusId = Models.Drive.StatusCanceled;
            drive.CancelReasonId = cancelReasonId;

            var driveModel = new Models.Drive(drive);

            return new CommandResult<Models.Drive>(ErrorCode.None, driveModel);
        }

    }
}
