using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CASNApp.Core.Entities;
using CASNApp.Core.Misc;
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
            var result = from b in (readOnly ? dbContext.Badges.AsNoTracking() : dbContext.Badges)
                         where b.IsActive
                         join vb in (readOnly ? dbContext.VolunteerBadges.AsNoTracking() : dbContext.VolunteerBadges)
                         on
                         new { Key1 = b.Id, Key2 = volunteerId }
                         equals
                         new { Key1 = vb.BadgeId, Key2 = vb.VolunteerId }
                         into vbgroup
                         from subvb in vbgroup.DefaultIfEmpty()
                         orderby b.DisplayOrdinal ascending, b.Id ascending
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

        public async Task<List<Badge>> GetUnearnedBadgesForVolunteerIdAsync(int volunteerId, bool readOnly)
        {
            var earnedBadgeIds = await (readOnly ? dbContext.VolunteerBadges.AsNoTracking() : dbContext.VolunteerBadges)
                .Where(vb => vb.VolunteerId == volunteerId)
                .Select(vb => vb.BadgeId)
                .ToListAsync();

            var unearnedBadges = (readOnly ? dbContext.Badges.AsNoTracking() : dbContext.Badges)
                .Where(b => !earnedBadgeIds.Contains(b.Id) && b.IsActive)
                .ToListAsync();

            return await unearnedBadges;
        }

        public async Task<List<Badge>> GetRelevantUnearnedBadgesForVolunteerIdAsync(int volunteerId, BadgeTriggerType badgeTriggerType, bool readOnly)
        {
            var earnedBadgeIds = await(readOnly ? dbContext.VolunteerBadges.AsNoTracking() : dbContext.VolunteerBadges)
                .Where(vb => vb.VolunteerId == volunteerId)
                .Select(vb => vb.BadgeId)
                .ToListAsync();

            var unearnedBadges = (readOnly ? dbContext.Badges.AsNoTracking() : dbContext.Badges)
                .Where(b => !earnedBadgeIds.Contains(b.Id) && b.TriggerType == (int)badgeTriggerType && b.IsActive)
                .ToListAsync();

            return await unearnedBadges;
        }

    }
}
