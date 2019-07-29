using System.Runtime.Serialization;

namespace CASNApp.Core.Models
{
    [DataContract]
    public partial class AppointmentType
    {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name = "id")]
        public uint Id { get; set; }

        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

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
        /// Gets or Sets EstimatedDurationMinutes
        /// </summary>
        [DataMember(Name = "estimatedDurationMinutes")]
        public int EstimatedDurationMinutes { get; set; }

    }
}
