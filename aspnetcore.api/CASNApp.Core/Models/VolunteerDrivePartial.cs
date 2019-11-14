namespace CASNApp.Core.Models
{
    public partial class VolunteerDrive
    {
        public VolunteerDrive()
        {
        }

        public VolunteerDrive(Entities.VolunteerDriveLog vdl)
        {
            Created = vdl.Created;
            DriveId = vdl.DriveId;
            Id = vdl.Id;
            Updated = vdl.Updated;
            VolunteerId = vdl.VolunteerId;

            if (vdl.Volunteer != null)
            {
                FirstName = vdl.Volunteer?.FirstName;
                LastName = vdl.Volunteer?.LastName;
                MobilePhone = vdl.Volunteer?.MobilePhone;
            }
        }

    }
}
