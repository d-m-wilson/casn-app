using System.Linq;
using CASNApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CASNApp.Core.Queries
{
	class ServiceProviderQuery
	{
		private readonly casn_appContext dbContext;

		public ServiceProviderQuery(casn_appContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public ServiceProvider GetServiceProviderById(int serviceProviderId, bool readOnly)
		{
			var result = (readOnly ? dbContext.ServiceProviders.AsNoTracking() : dbContext.ServiceProviders)
                .Include(sp => sp.ServiceProviderType)
				.Where(m => m.Id == serviceProviderId)
				.SingleOrDefault();
			return result;
		}

	}
}
