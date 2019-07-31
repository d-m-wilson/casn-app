using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CASNApp.Core.Entities;
using CASNApp.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CASNApp.Core.Queries
{
    public class BadgeQuery
    {
        private readonly casn_appContext dbContext;

        public BadgeQuery(casn_appContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<List<BadgeDTO>> GetBadgesForVolunteerIdAsync(int volunteerId, bool readOnly)
        {
            var result = from b in (readOnly ? dbContext.Badge.AsNoTracking() : dbContext.Badge)
                         where b.IsActive
                         join vb in (readOnly ? dbContext.VolunteerBadge.AsNoTracking() : dbContext.VolunteerBadge)
                         on
                         new { Key1 = b.Id, Key2 = volunteerId }
                         equals
                         new { Key1 = vb.BadgeId, Key2 = vb.VolunteerId }
                         into vbgroup
                         from subvb in vbgroup.DefaultIfEmpty()
                         select new BadgeDTO
                         {
                             Id = b.Id,
                             Title = b.Title,
                             Description = b.Description,
                             Path = b.Path,
                             IsHidden = b.IsHidden,
                             IsEarned = subvb != null,
                             DateEarned = subvb != null ? subvb.Created : (DateTime?)null,
                         };

            return result.ToListAsync();
        }

    }
}
