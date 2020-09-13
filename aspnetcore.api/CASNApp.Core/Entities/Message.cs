using System;
using System.Collections.Generic;

namespace CASNApp.Core.Entities
{
	public partial class Message
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
