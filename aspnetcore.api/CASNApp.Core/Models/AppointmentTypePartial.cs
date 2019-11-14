namespace CASNApp.Core.Models
{
    public partial class AppointmentType
    {
        public AppointmentType()
        {
        }

        public AppointmentType(Entities.AppointmentType at)
        {
            Id = at.Id;
            Name = at.Name;
            Title = at.Title;
            Description = at.Description;
            EstimatedDurationMinutes = at.EstimatedDurationMinutes;
        }

    }
}
