using System;
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
				.Where(d => d.IsActive && d.StatusId == Models.Drive.StatusOpen 
						&& d.StartLatitude != null && d.StartLongitude != null
						&& d.EndLatitude != null && d.EndLongitude != null)
				.Select(d => d.AppointmentId)
				.Distinct()
				.ToList();

			var result = (readOnly ? dbContext.Appointment.AsNoTracking() : dbContext.Appointment)
				.Include(a => a.Drives)
				.Where(a => a.IsActive && DateTime.Compare(a.AppointmentDate, DateTime.UtcNow) >= 0 
					&& a.AppointmentDate.Subtract(DateTime.UtcNow).Days >= 1 
					&& openAppointmentIds.Contains(a.Id))
				.ToList();
			
			return result;
		}

		public Appointment GetAppointmentById(int appointmentId, bool readOnly)
		{
			var result = (readOnly ? dbContext.Appointment.AsNoTracking() : dbContext.Appointment)
				.Where(a => a.Id == appointmentId)
				.SingleOrDefault();
			return result;
		}
	}
}
