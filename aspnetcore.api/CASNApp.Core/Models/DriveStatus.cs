using System.Runtime.Serialization;

namespace CASNApp.Core.Models
{
    [DataContract]
    public class DriveStatus
    {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name = "id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

    }
}
