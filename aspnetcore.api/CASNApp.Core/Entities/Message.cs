using System;
using CASNApp.Core.Interfaces;

namespace CASNApp.Core.Entities
{
	public class Message : ICreatedDate, IUpdatedDate, ISoftDelete
    {
		public int Id { get; set; }
		public int MessageTypeId { get; set; }
		public string MessageText { get; set; }
        public bool IsActive { get; set; }
		public DateTime Created { get; set; }
		public DateTime? Updated { get; set; }

		public MessageType MessageType { get; set; }
	}
}
