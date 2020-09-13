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
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public bool IsActive { get; set; }

        public Drive Drive { get; set; }
        public DriveLogStatus DriveLogStatus { get; set; }
        public Volunteer Volunteer { get; set; }
        public ICollection<VolunteerBadge> VolunteerBadges { get; set; }

    }
}
