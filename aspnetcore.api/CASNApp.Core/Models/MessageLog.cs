using System;
using System.Runtime.Serialization;

namespace CASNApp.Core.Models
{
	[DataContract]
	class MessageLog
	{
		/// <summary>
		/// Gets or Sets Id
		/// </summary>
		[DataMember(Name = "id")]
		public int Id { get; set; }

		/// <summary>
		/// Gets or Sets FromPhone
		/// </summary>
		[DataMember(Name = "fromPhone")]
		public string FromPhone { get; set; }

		/// <summary>
		/// Gets or Sets ToPhone
		/// </summary>
		[DataMember(Name = "toPhone")]
		public string ToPhone { get; set; }

		/// <summary>
		/// Gets or Sets ToPhone
		/// </summary>
		[DataMember(Name = "subject")]
		public string Subject { get; set; }

		/// <summary>
		/// Gets or Sets ToPhone
		/// </summary>
		[DataMember(Name = "body")]
		public string Body { get; set; }

		/// <summary>
		/// Gets or Sets ToPhone
		/// </summary>
		[DataMember(Name = "dateSent")]
		public DateTime DateSent { get; set; }
	}
}
