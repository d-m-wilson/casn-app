﻿using System;

namespace CASNApp.Core.Entities
{
    public class AppointmentType
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

    }
}
