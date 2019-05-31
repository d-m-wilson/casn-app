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
		public uint Id { get; set; }

		/// <summary>
		/// Gets or Sets Name
		/// </summary>
		[DataMember(Name = "messageType")]
		public int MessageType { get; set; }

		/// <summary>
		/// Gets or Sets Title
		/// </summary>
		[DataMember(Name = "messageText")]
		public string MessageText { get; set; }
	}
}
