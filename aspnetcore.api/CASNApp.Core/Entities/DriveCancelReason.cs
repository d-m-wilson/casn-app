using System;
using CASNApp.Core.Interfaces;

namespace CASNApp.Core.Entities
{
    public class DriveCancelReason : ICreatedDate, IUpdatedDate, ISoftDelete
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

    }
}
