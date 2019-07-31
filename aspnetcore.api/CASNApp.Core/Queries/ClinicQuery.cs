using System.Linq;
using CASNApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CASNApp.Core.Queries
{
	class ClinicQuery
	{
		private readonly casn_appContext dbContext;

		public ClinicQuery(casn_appContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public Clinic GetClinicByID(int clinicId, bool readOnly)
		{
			var result = (readOnly ? dbContext.Clinic.AsNoTracking() : dbContext.Clinic)
				.Where(m => m.Id == clinicId)
				.SingleOrDefault();
			return result;
		}

	}
}
