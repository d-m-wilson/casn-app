using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CASNApp.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CASNApp.API.Queries
{
    public class VolunteerQuery
    {
        private readonly casn_appContext dbContext;

        public VolunteerQuery(casn_appContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Volunteer GetActiveVolunteerByEmail(string email, bool readOnly)
        {
            var result = (readOnly ? dbContext.Volunteer.AsNoTracking() : dbContext.Volunteer)
                .Where(v => v.GoogleAccount == email && v.IsActive && (v.IsDriver || v.IsDispatcher))
                .SingleOrDefault();
            return result;
        }

        public Volunteer GetActiveDispatcherByEmail(string email, bool readOnly)
        {
            var volunteer = GetActiveVolunteerByEmail(email, readOnly);

            if (volunteer != null && volunteer.IsDispatcher)
            {
                return volunteer;
            }

            return null;
        }

        public Volunteer GetActiveDriverByEmail(string email, bool readOnly)
        {
            var volunteer = GetActiveVolunteerByEmail(email, readOnly);

            if (volunteer != null && volunteer.IsDriver)
            {
                return volunteer;
            }

            return null;
        }

    }
}
