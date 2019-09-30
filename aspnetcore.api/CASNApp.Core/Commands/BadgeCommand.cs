using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using CASNApp.Core.Entities;
using CASNApp.Core.Extensions;
using CASNApp.Core.Misc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CASNApp.Core.Commands
{
    public class BadgeCommand
    {
        private readonly casn_appContext dbContext;
        private readonly ILogger<BadgeCommand> logger;

        public BadgeCommand(casn_appContext dbContext, ILogger<BadgeCommand> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public async Task<bool> CheckAndAwardBadgeAsync(Badge badge, Volunteer volunteer, VolunteerDriveLog volunteerDriveLog)
        {
            var sqlConnection = dbContext.GetSqlConnection();

            var sqlParameters = new List<SqlParameter>();

            sqlParameters.Add(new SqlParameter("VolunteerId", volunteer.Id));

            if (badge.ClinicId.HasValue)
            {
                sqlParameters.Add(new SqlParameter("ClinicId", badge.ClinicId.Value));
            }

            if (badge.CountTarget.HasValue)
            {
                sqlParameters.Add(new SqlParameter("CountTarget", badge.CountTarget.Value));
            }

            int returnCode = await sqlConnection.ExecuteStoredProcedureAsync(badge.ProcedureName, sqlParameters.ToArray());

            var awardBadge = (returnCode == 1);

            if (awardBadge)
            {
                var volunteerBadge = new VolunteerBadge
                {
                    BadgeId = badge.Id,
                    Created = volunteerDriveLog.Created,
                    VolunteerDriveLogId = volunteerDriveLog.Id,
                    VolunteerId = volunteer.Id,
                };

                dbContext.VolunteerBadge.Add(volunteerBadge);
            }

            return awardBadge;
        }


    }
}
