using System.Runtime.Serialization;

namespace CASNApp.Core.Models
{
    [DataContract]
    public class DriveBuddy
    {
        [DataMember(Name = "firstName")]
        public string FirstName { get; set; }

        [DataMember(Name = "lastName")]
        public string LastName { get; set; }

        [DataMember(Name = "mobilePhone")]
        public string MobilePhone { get; set; }

    }
}
