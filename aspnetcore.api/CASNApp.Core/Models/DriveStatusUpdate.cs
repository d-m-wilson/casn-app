using System.Runtime.Serialization;

namespace CASNApp.Core.Models
{
    [DataContract]
    public class DriveStatusUpdate
    {
        [DataMember(Name = "driveStatusId")]
        public int DriveStatusId { get; set; }

    }
}
