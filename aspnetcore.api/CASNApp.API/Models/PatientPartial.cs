namespace CASNApp.API.Models
{
    public partial class Patient
    {
        public Patient()
        {
        }

        public Patient(Entities.Patient e)
        {
            Id = e.Id;
            CiviContactId = e.CiviContactId;
            PatientIdentifier = e.PatientIdentifier;
            FirstName = e.FirstName;
            LastName = e.LastName;
            Phone = e.Phone;
            IsMinor = e.IsMinor;
            PreferredLanguage = e.PreferredLanguage;
            PreferredContactMethod = e.PreferredContactMethod;
            Created = e.Created;
            Updated = e.Updated;
        }

    }
}
