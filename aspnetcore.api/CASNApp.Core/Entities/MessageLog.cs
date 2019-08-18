using System;
using System.Collections.Generic;

namespace CASNApp.Core.Entities
{
	public class MessageLog
	{
		public int Id { get; set; }
		public string FromPhone { get; set; }
		public string ToPhone { get; set; }
		public string Subject { get; set; }
		public string Body { get; set; }
		public DateTime DateSent { get; set; }
		public int? AppointmentId { get; set; }
		public int? VolunteerId { get; set; }
	}
}
