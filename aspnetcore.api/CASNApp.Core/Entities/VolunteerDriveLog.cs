using System;
using System.Collections.Generic;

namespace CASNApp.Core.Entities
{
    public partial class VolunteerDriveLog
    {
        public VolunteerDriveLog()
        {
            VolunteerBadges = new HashSet<VolunteerBadge>();
        }

        public int Id { get; set; }
        public int VolunteerId { get; set; }
        public int DriveId { get; set; }
        public int DriveLogStatusId { get; set; }
        public DateTime? Canceled { get; set; }
        public DateTime? Approved { get; set; }
        public DateTime? Reassigned { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public virtual Drive Drive { get; set; }
        public virtual DriveLogStatus DriveLogStatus { get; set; }
        public virtual Volunteer Volunteer { get; set; }
        public virtual ICollection<VolunteerBadge> VolunteerBadges { get; set; }
    }
}
