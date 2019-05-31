using System.Linq;
using CASNApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CASNApp.Core.Queries
{
	class MessageQuery
	{
		private readonly casn_appContext dbContext;

		public MessageQuery(casn_appContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public Message GetMessageByType(int messageType, bool readOnly)
		{
			var result = (readOnly ? dbContext.Message.AsNoTracking() : dbContext.Message)
				.Where(m => m.MessageType == messageType)
				.SingleOrDefault();
			return result;
		}

	}
}
