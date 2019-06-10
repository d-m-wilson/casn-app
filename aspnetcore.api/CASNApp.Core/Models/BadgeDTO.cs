using System;
using System.Runtime.Serialization;

namespace CASNApp.Core.Models
{
    [DataContract]
    public class BadgeDTO
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
        /// Gets or Sets Description
        /// </summary>
        [DataMember(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or Sets Path
        /// </summary>
        [DataMember(Name = "path")]
        public string Path { get; set; }

        /// <summary>
        /// Gets or Sets IsHidden
        /// </summary>
        [DataMember(Name = "isHidden")]
        public bool IsHidden { get; set; }

        /// <summary>
        /// Gets or Sets IsEarned
        /// </summary>
        [DataMember(Name = "isEarned")]
        public bool IsEarned { get; set; }

        /// <summary>
        /// Gets or Sets DateEarned
        /// </summary>
        [DataMember(Name = "dateEarned")]
        public DateTime? DateEarned { get; set; }

    }
}
