using System.Runtime.Serialization;

namespace CASNApp.Core.Models
{
    [DataContract]
    public class DriveCancelReason
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
        /// Gets or Sets Description
        /// </summary>
        [DataMember(Name = "description")]
        public string Description { get; set; }

    }
}
