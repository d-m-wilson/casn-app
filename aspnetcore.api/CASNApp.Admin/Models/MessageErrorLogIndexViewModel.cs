using System;

namespace CASNApp.Admin.Models
{
    public class MessageErrorLogIndexViewModel
    {
		public int Id { get; set; }
		public string FromPhone { get; set; }
		public string ToPhone { get; set; }
		public string Subject { get; set; }
		public string Body { get; set; }
		public DateTime DateSent { get; set; }
		public int? AppointmentId { get; set; }
		public int? VolunteerId { get; set; }
		public string VolunteerName { get; set; }
		public string ErrorCode { get; set; }
		public string ErrorMessage { get; set; }
		public string ErrorDetails { get; set; }
	}
}
