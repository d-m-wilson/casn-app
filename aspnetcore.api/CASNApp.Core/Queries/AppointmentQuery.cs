using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CASNApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CASNApp.Core.Queries
{
	public class AppointmentQuery
	{
		private readonly casn_appContext dbContext;

		public AppointmentQuery(casn_appContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public List<Appointment> GetAllAppointmentsWithOpenDrives(bool readOnly)
		{
			var openAppointmentIds = (readOnly ? dbContext.Drive.AsNoTracking() : dbContext.Drive)
				.Where(d => d.IsActive && d.StatusId == Models.Drive.StatusOpen)
				.Select(d => d.AppointmentId)
				.Distinct()
				.ToList();

			var result = (readOnly ? dbContext.Appointment.AsNoTracking() : dbContext.Appointment)
				.Include(a => a.Drives)
				.Where(a => a.IsActive && openAppointmentIds.Contains(a.Id))
				.ToList();
			
			//var result = (readOnly ? dbContext.Appointment.AsNoTracking() : dbContext.Appointment)
			//	.Join((readOnly ? dbContext.Drive.AsNoTracking() : dbContext.Drive), appt => appt.Id, drive => drive.AppointmentId, (appt, drive) => new { Appointment = appt, Drive = drive })
			//	.Where(apptAnddrive => apptAnddrive.Drive.Status.Name == "Open");

			return result;
		}

	}
}
