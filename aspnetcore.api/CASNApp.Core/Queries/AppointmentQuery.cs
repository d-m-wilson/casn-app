using System;
using System.Collections.Generic;
using System.Linq;
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

		public List<Appointment> GetAllNextDayAppointmentsWithOpenDrives(bool readOnly)
		{
			var openAppointmentIds = (readOnly ? dbContext.Drive.AsNoTracking() : dbContext.Drive)
				.Where(d => d.IsActive && d.StatusId == Models.Drive.StatusOpen 
						&& d.StartLatitude != null && d.StartLongitude != null
						&& d.EndLatitude != null && d.EndLongitude != null)
				.Select(d => d.AppointmentId)
				.Distinct()
				.ToList();

			var tomorrowBeginsUTC = DateTime.Today.AddDays(1).ToUniversalTime();
			var tomorrowEndsUTC = DateTime.Today.AddDays(1).AddMilliseconds(-1).ToUniversalTime();

			var result = (readOnly ? dbContext.Appointment.AsNoTracking() : dbContext.Appointment)
				.Include(a => a.Drives)
				.Where(a => a.IsActive && a.AppointmentDate >= tomorrowBeginsUTC && a.AppointmentDate <= tomorrowEndsUTC
					&& openAppointmentIds.Contains(a.Id))
				.ToList();
			
			return result;
		}

		public Appointment GetAppointmentByIdWithCaller(int appointmentId, bool readOnly)
		{
			var result = (readOnly ? dbContext.Appointment.AsNoTracking() : dbContext.Appointment)
				.Include(a => a.Caller)
				.Where(a => a.Id == appointmentId)
				.SingleOrDefault();
			return result;
		}
	}
}
