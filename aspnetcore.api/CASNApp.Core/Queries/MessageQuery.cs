using System;
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
			var result = (readOnly ? dbContext.Messages.AsNoTracking() : dbContext.Messages)
				.Where(m => m.MessageTypeId == messageType)
				.OrderBy(m => Guid.NewGuid())
				.FirstOrDefault();
			return result;
		}

	}
}
