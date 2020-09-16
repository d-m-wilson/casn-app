using System;
using System.Collections.Generic;

namespace CASNApp.Core.Entities
{
    public partial class VolunteerBadge
    {
        public int Id { get; set; }
        public int VolunteerId { get; set; }
        public int BadgeId { get; set; }
        public int VolunteerDriveLogId { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public virtual Badge Badge { get; set; }
        public virtual Volunteer Volunteer { get; set; }
        public virtual VolunteerDriveLog VolunteerDriveLog { get; set; }
    }
}
