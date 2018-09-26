using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CASNApp.API.Entities
{
    public partial class Patient
    {
        public Patient(Models.Patient e)
        {
            if (e.Id.HasValue)
            {
                Id = (uint)e.Id.Value;
            }

            CiviContactId = e.CiviContactId.HasValue ? (uint)e.CiviContactId.Value : 0;
            PatientIdentifier = e.PatientIdentifier;
            FirstName = e.FirstName;
            LastName = e.LastName;
            Phone = e.Phone;

            if (e.IsMinor.HasValue)
            {
                IsMinor = e.IsMinor.Value;
            }

            PreferredLanguage = e.PreferredLanguage;
            PreferredContactMethod = e.PreferredContactMethod.HasValue ? (sbyte)e.PreferredContactMethod.Value : (sbyte)0;

            if (e.Created.HasValue)
            {
                Created = e.Created.Value;
            }

            Updated = e.Updated;
        }

    }
}
