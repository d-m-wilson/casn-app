using System.Linq;
using System.Threading.Tasks;
using CASNApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CASNApp.Core.Queries
{
    public class VolunteerDriveLogQuery
    {
        private readonly casn_appContext dbContext;

        public VolunteerDriveLogQuery(casn_appContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<VolunteerDriveLog> GetByIdAsync(int driveLogId, bool readOnly)
        {
            var result = (readOnly ? dbContext.VolunteerDriveLogs.AsNoTracking() : dbContext.VolunteerDriveLogs)
                .Include(vdl => vdl.Drive)
                .Where(vdl => vdl.Id == driveLogId && vdl.IsActive)
                .SingleOrDefaultAsync();
            return result;
        }

        public Task<VolunteerDriveLog> GetByVolunteerAndDriveIdAsync(int volunteerId, int driveId, bool readOnly)
        {
            var result = (readOnly ? dbContext.VolunteerDriveLogs.AsNoTracking() : dbContext.VolunteerDriveLogs)
                .Include(vdl => vdl.Drive)
                .Where(vdl => vdl.VolunteerId == volunteerId && vdl.DriveId == driveId && vdl.IsActive)
                .SingleOrDefaultAsync();
            return result;
        }

    }
}
