using System.Runtime.Serialization;

namespace CASNApp.Core.Models
{
    [DataContract]
    public partial class Link
    {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name = "id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Title
        /// </summary>
        [DataMember(Name = "title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or Sets Title
        /// </summary>
        [DataMember(Name = "url")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or Sets Title
        /// </summary>
        [DataMember(Name = "target")]
        public string Target { get; set; }

        /// <summary>
        /// Gets or Sets DisplayOrdinal
        /// </summary>
        [DataMember(Name = "displayOrdinal")]
        public int DisplayOrdinal { get; set; }

    }
}
