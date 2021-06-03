using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CASNApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CASNApp.Core.Queries
{
    public class DriveStatusQuery
    {
        private readonly casn_appContext dbContext;

        public DriveStatusQuery(casn_appContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<List<DriveStatus>> GetActiveDriveStatusesAsync(bool readOnly)
        {
            var result = (readOnly ? dbContext.DriveStatuses.AsNoTracking() : dbContext.DriveStatuses)
                .Where(e => e.IsActive)
                .OrderBy(e => e.Id)
                .ToListAsync();
            return result;
        }

    }
}
