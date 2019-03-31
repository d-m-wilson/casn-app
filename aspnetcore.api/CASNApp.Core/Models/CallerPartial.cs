namespace CASNApp.API.Models
{
    public partial class Caller
    {
        public Caller()
        {
        }

        public Caller(Entities.Caller e)
        {
            Id = e.Id;
            CiviContactId = e.CiviContactId;
            CallerIdentifier = e.CallerIdentifier;
            FirstName = e.FirstName;
            LastName = e.LastName;
            Phone = e.Phone;
            IsMinor = e.IsMinor;
            PreferredLanguage = e.PreferredLanguage;
            PreferredContactMethod = e.PreferredContactMethod;
            Note = e.Note;
            Created = e.Created;
            Updated = e.Updated;
        }

    }
}
