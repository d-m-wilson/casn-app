using System.Runtime.Serialization;

namespace CASNApp.Core.Models
{
	[DataContract]
	class Message
	{
		/// <summary>
		/// Gets or Sets Id
		/// </summary>
		[DataMember(Name = "id")]
		public int Id { get; set; }

		/// <summary>
		/// Gets or Sets MessageType
		/// </summary>
		[DataMember(Name = "messageType")]
		public int MessageType { get; set; }

		/// <summary>
		/// Gets or Sets MessageText
		/// </summary>
		[DataMember(Name = "messageText")]
		public string MessageText { get; set; }
	}
}
