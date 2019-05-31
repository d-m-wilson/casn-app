using System;
using System.Collections.Generic;

namespace CASNApp.Core.Entities
{
	public class Message
	{
		public uint Id { get; set; }
		public int MessageType { get; set; }
		public string MessageText { get; set; }
		public DateTime Created { get; set; }
		public DateTime? Updated { get; set; }
	}
}
