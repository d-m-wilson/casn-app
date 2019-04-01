using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CASNApp.Core.Entities
{
    public partial class Caller
    {
        public Caller(Models.Caller e)
        {
            if (e.Id.HasValue)
            {
                Id = (uint)e.Id.Value;
            }

            CiviContactId = e.CiviContactId.HasValue ? (uint)e.CiviContactId.Value : 0;
            CallerIdentifier = e.CallerIdentifier;
            FirstName = e.FirstName;
            LastName = e.LastName;
            Phone = e.Phone;

            if (e.IsMinor.HasValue)
            {
                IsMinor = e.IsMinor.Value;
            }

            PreferredLanguage = e.PreferredLanguage;
            PreferredContactMethod = e.PreferredContactMethod.HasValue ? (sbyte)e.PreferredContactMethod.Value : (sbyte)0;
            Note = e.Note;

            if (e.Created.HasValue)
            {
                Created = e.Created.Value;
            }

            Updated = e.Updated;
        }

    }
}
