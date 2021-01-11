using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CASNApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CASNApp.Core.Queries
{
    public class VolunteerQuery
    {
        private readonly casn_appContext dbContext;

        public VolunteerQuery(casn_appContext dbContext)
        {
            this.dbContext = dbContext;
        }

		public List<Volunteer> GetAllActiveDispatcherssWithTextConsent(bool readOnly)
		{
			var result = (readOnly ? dbContext.Volunteers.AsNoTracking() : dbContext.Volunteers)
				.Where(v => v.IsActive && v.IsDispatcher && v.HasTextConsent)
				.ToList();
			return result;
		}

		public List<Volunteer> GetAllActiveDriversWithTextConsent(bool readOnly)
		{
			var result = (readOnly ? dbContext.Volunteers.AsNoTracking() : dbContext.Volunteers)
				.Where(v => v.IsActive && v.IsDriver && v.HasTextConsent)
				.ToList();
			return result;
		}

		public Volunteer GetActiveVolunteerByEmail(string email, bool readOnly)
        {
            var result = (readOnly ? dbContext.Volunteers.AsNoTracking() : dbContext.Volunteers)
                .Where(v => v.GoogleAccount == email && v.IsActive && (v.IsDriver || v.IsDispatcher))
                .SingleOrDefault();
            return result;
        }

        public Task<Volunteer> GetVolunteerByEmailAsync(string email, bool readOnly)
        {
            var result = (readOnly ? dbContext.Volunteers.AsNoTracking() : dbContext.Volunteers)
                .Where(v => v.GoogleAccount == email)
                .SingleOrDefaultAsync();
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

        public Task<Volunteer> GetActiveDispatcherByEmailAsync(string email, bool readOnly)
        {
            var result = (readOnly ? dbContext.Volunteers.AsNoTracking() : dbContext.Volunteers)
                .Where(v => v.GoogleAccount == email && v.IsActive && v.IsDispatcher)
                .SingleOrDefaultAsync();
            return result;
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

        public Volunteer GetActiveDriverById(int volunteerId, bool readOnly)
        {
            var result = (readOnly ? dbContext.Volunteers.AsNoTracking() : dbContext.Volunteers)
                .Where(v => v.Id == volunteerId && v.IsActive && (v.IsDriver || v.IsDispatcher))
                .SingleOrDefault();

            return result;
        }

        public Task<Volunteer> GetByIdAsync(int volunteerId, bool readOnly)
        {
            var result = (readOnly ? dbContext.Volunteers.AsNoTracking() : dbContext.Volunteers)
                .Where(v => v.Id == volunteerId)
                .SingleOrDefaultAsync();
            return result;
        }

    }
}
