using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CASNApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CASNApp.Core.Queries
{
    public class DriveCancelReasonQuery
    {
        private readonly casn_appContext dbContext;

        public DriveCancelReasonQuery(casn_appContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<List<DriveCancelReason>> GetActiveCancelReasonsAsync(bool readOnly)
        {
            var result = (readOnly ? dbContext.DriveCancelReason.AsNoTracking() : dbContext.DriveCancelReason)
                .Where(e => e.IsActive)
                .OrderBy(e => e.Id)
                .ToListAsync();
            return result;
        }

        public Task<DriveCancelReason> GetActiveCancelReasonAsync(uint id, bool readOnly)
        {
            var result = (readOnly ? dbContext.DriveCancelReason.AsNoTracking() : dbContext.DriveCancelReason)
                .SingleOrDefaultAsync(e => e.Id == id && e.IsActive);
            return result;
        }

    }
}
