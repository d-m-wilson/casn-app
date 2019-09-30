using System;

namespace CASNApp.Core.Entities
{
    public class VolunteerBadge
    {
        public int Id { get; set; }
        public int VolunteerId { get; set; }
        public int BadgeId { get; set; }
        public int VolunteerDriveLogId { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public Volunteer Volunteer { get; set; }
        public Badge Badge { get; set; }
        public VolunteerDriveLog VolunteerDriveLog { get; set; }

    }
}
