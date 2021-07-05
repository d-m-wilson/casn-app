using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CASNApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CASNApp.Core.Queries
{
    public class LinkQuery
    {
        private readonly casn_appContext dbContext;

        public LinkQuery(casn_appContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<List<Link>> GetLinksAsync(bool readOnly, bool includeInactive)
        {
            var result = (readOnly ? dbContext.Links.AsNoTracking() : dbContext.Links)
                .Where(e => includeInactive || e.IsActive)
                .OrderBy(e => e.DisplayOrdinal)
                .ThenBy(e => e.Title)
                .ToListAsync();
            return result;
        }

    }
}
