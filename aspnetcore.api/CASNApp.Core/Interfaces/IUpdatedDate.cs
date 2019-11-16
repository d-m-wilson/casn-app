using System;

namespace CASNApp.Core.Interfaces
{
    public interface IUpdatedDate : ICreatedDate
    {
        DateTime? Updated { get; set; }
    }
}
