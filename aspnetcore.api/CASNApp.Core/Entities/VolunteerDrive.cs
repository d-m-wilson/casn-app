using System;
using System.Collections.Generic;

namespace CASNApp.Core.Entities
{
    public partial class VolunteerDrive
    {
        public int Id { get; set; }
        public int VolunteerId { get; set; }
        public int DriveId { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public bool IsActive { get; set; }

        public Drive Drive { get; set; }
        public Volunteer Volunteer { get; set; }
    }
}
