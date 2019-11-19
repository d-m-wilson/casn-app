using CASNApp.Core.Interfaces;

namespace CASNApp.Core.Entities
{
    public partial class Caller : ICreatedDate, IUpdatedDate, ISoftDelete
    {
        public Caller(Models.Caller e)
        {
            if (e.Id.HasValue)
            {
                Id = (int)e.Id.Value;
            }

            CiviContactId = e.CiviContactId.HasValue ? (short)e.CiviContactId.Value : 0;
            CallerIdentifier = e.CallerIdentifier;
            FirstName = e.FirstName;
            LastName = e.LastName;
            Phone = e.Phone;

            if (e.IsMinor.HasValue)
            {
                IsMinor = e.IsMinor.Value;
            }

            PreferredLanguage = e.PreferredLanguage;
            PreferredContactMethod = e.PreferredContactMethod.HasValue ? (short)e.PreferredContactMethod.Value : (short)0;
            Note = e.Note;

            if (e.Created.HasValue)
            {
                Created = e.Created.Value;
            }

            Updated = e.Updated;
        }

        public void UpdateFromModel(Models.Caller callerModel)
        {
            this.CallerIdentifier = callerModel.CallerIdentifier;
            this.FirstName = callerModel.FirstName;
            this.IsMinor = callerModel.IsMinor.Value;
            this.LastName = callerModel.LastName;
            this.Note = callerModel.Note;
            this.Phone = callerModel.Phone;
            this.PreferredContactMethod = (short)callerModel.PreferredContactMethod.Value;
            this.PreferredLanguage = callerModel.PreferredLanguage;
        }

    }
}
