using System.Threading.Tasks;
using CASNApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CASNApp.Core.Queries
{
    public class DriveQuery
    {
        private readonly casn_appContext dbContext;

        public DriveQuery(casn_appContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<Drive> GetDriveAsync(int driveId)
        {
            var result = dbContext.Drives.SingleOrDefaultAsync(d => d.Id == driveId);
            return result;
        }

    }
}
