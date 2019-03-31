namespace CASNApp.API.Models
{
    public partial class VolunteerDrive
    {
        public VolunteerDrive()
        {
        }

        public VolunteerDrive(Entities.VolunteerDrive vd)
        {
            Created = vd.Created;
            DriveId = vd.DriveId;
            Id = vd.Id;
            Updated = vd.Updated;
            VolunteerId = vd.VolunteerId;

            if (vd.Volunteer != null)
            {
                FirstName = vd.Volunteer?.FirstName;
                LastName = vd.Volunteer?.LastName;
                MobilePhone = vd.Volunteer?.MobilePhone;
            }
        }

    }
}
