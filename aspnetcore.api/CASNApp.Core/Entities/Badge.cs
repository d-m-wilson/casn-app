using System;
using System.Collections.Generic;

namespace CASNApp.Core.Entities
{
    public class Badge
    {
        public Badge()
        {
            VolunteerBadges = new HashSet<VolunteerBadge>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        public bool IsActive { get; set; }
        public bool IsHidden { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public ICollection<VolunteerBadge> VolunteerBadges { get; set; }

    }
}
