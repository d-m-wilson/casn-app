using System;
using System.Threading.Tasks;
using CASNApp.Core.Entities;
using CASNApp.Core.Queries;
using Microsoft.Extensions.Logging;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using System.Collections;
using System.Reflection;
using System.ComponentModel;
using System.Collections.Generic;

namespace CASNApp.Core.Commands
{

	public class TwilioCommand
	{
		private readonly Core.Entities.casn_appContext dbContext;
		private string accountSid;
		private string authToken;
		private string accountPhoneNumber;
		private ILogger<TwilioCommand> logger;

		public enum MessageType
		{
			Unknown = 0,
			ApptAddedOneWayToClinic = 1,
			ApptAddedOneWayFromClinic = 2,
			ApptAddedRoundTripSameAddress = 3,
			ApptAddedRoundTripDiffAddress = 4,
			ApptAddedToday = 5,
			FriendlyReminder = 6,
			SeriousRequest = 7,
			DesperatePlea = 8,
			DriverAppliedForDrive = 9,
			DriveCanceled = 10,
			DriverApprovedForDrive = 11,
		}

		public TwilioCommand(string accountSid, string authTokey, string accountPhoneNumber, ILogger<TwilioCommand> logger, casn_appContext dbContext)
		{
			this.accountSid = accountSid;
			this.authToken = authTokey;
			this.accountPhoneNumber = accountPhoneNumber;
			this.logger = logger;
			this.dbContext = dbContext;
		}

		public void SendDispatcherMessage(Drive drive, Volunteer driver, MessageType messageType)
		{
			//get the message by message type
			MessageQuery messageQuery = new MessageQuery(dbContext);
			Message message = messageQuery.GetMessageByType(Convert.ToInt32(messageType), true);

			//get the appointment by id
			AppointmentQuery appointmentQuery = new AppointmentQuery(dbContext);
			var appointment = appointmentQuery.GetAppointmentById(drive.AppointmentId, true);

			//check the message type to see if the message is being sent to the driver or the dispatchers
			if (messageType == MessageType.DriverAppliedForDrive)
			{
				//select the dispatchers
				VolunteerQuery volunteerQuery = new VolunteerQuery(dbContext);
				var dispatchers = volunteerQuery.GetAllActiveDispatcherssWithTextConsent(true);

				//send message to appropriate dispatchers
				foreach (Volunteer dispatcher in dispatchers)
				{
					if (!String.IsNullOrEmpty(dispatcher.MobilePhone))
					{
						//build the outbound message text
						string messageText = BuildMessage(message.MessageText, null, appointment, driver, 0, drive.Id);

						//send message to all dispatchers
						SMSMessage(messageText, accountPhoneNumber, dispatcher.MobilePhone, dispatcher.Id, appointment.Id);
					}
				}
			}
			else
			{
				//send message to driver
				if (!String.IsNullOrEmpty(driver.MobilePhone))
				{
					//build the appintment listing
					string messageText = BuildMessage(message.MessageText, null, appointment, driver, 0, drive.Id);

					//send text message to driver
					SMSMessage(messageText, accountPhoneNumber, driver.MobilePhone, driver.Id, drive.AppointmentId);
				}
			}
		}

		public void SendAppointmentReminderMessage(List<Appointment> appointments, MessageType messageType)
		{
			//get the message by message type
			MessageQuery messageQuery = new MessageQuery(dbContext);
			Message message = messageQuery.GetMessageByType(Convert.ToInt32(messageType), true);

			//build the appintment listing
			string messageText = BuildMessage(message.MessageText, null, null, null, appointments.Count, 0);
			Dictionary<int, string> driveList = new Dictionary<int, string>();
			if (appointments.Count <= 3)
			{
				ClinicQuery clinicQuery = new ClinicQuery(dbContext);
				foreach (Appointment appointment in appointments)
				{
					Clinic clinic = clinicQuery.GetClinicByID(appointment.ClinicId, true);
					driveList.Add(appointment.Id, clinic.Name + " at " + appointment.AppointmentDate.ToString());
				}
			}

			//select the drivers 
			VolunteerQuery volunteerQuery = new VolunteerQuery(dbContext);
			var drivers = volunteerQuery.GetAllActiveDriversWithTextConsent(true);

			//send the message to all drivers
			foreach (Volunteer driver in drivers)
			{
				if (!String.IsNullOrEmpty(driver.MobilePhone))
				{
					//send message to all drivers is appointment outside 30 miles or and appointment made for today
					SMSMessage(messageText, accountPhoneNumber, driver.MobilePhone, driver.Id, null);
					foreach (KeyValuePair<int, string> driveDetail in driveList)
						SMSMessage(driveDetail.Value, accountPhoneNumber, driver.MobilePhone, driver.Id, driveDetail.Key);
				}
			}
		}

		public void SendAppointmentMessage(Appointment appointment, Drive driveTo, Drive driveFrom, MessageType messageType)
		{
			//determine type of meesage that would be displayed
			if (appointment.AppointmentDate.Date == DateTime.UtcNow.Date)
				messageType = MessageType.ApptAddedToday;
			else if (driveTo == null && driveFrom != null)
				messageType = MessageType.ApptAddedOneWayFromClinic;
			else if (driveTo != null && driveFrom == null)
				messageType = MessageType.ApptAddedOneWayToClinic;
			else if (driveTo.StartAddress == driveFrom.EndAddress)
				messageType = MessageType.ApptAddedRoundTripSameAddress;
			else if (driveTo.StartAddress != driveFrom.EndAddress)
				messageType = MessageType.ApptAddedRoundTripDiffAddress;
			else
				messageType = MessageType.Unknown;

			//skip unknown message types
			if (messageType != MessageType.Unknown)
			{
				//get the list of drivers for the message based on message type, elapsed time, and length of the drive drive
				double driveDistance = 0;
				double initialLatitude = 0;
				double initialLongitude = 0;
				if (messageType != MessageType.ApptAddedToday)
				{
					if (messageType == MessageType.ApptAddedRoundTripSameAddress || messageType == MessageType.ApptAddedOneWayToClinic)
					{
						driveDistance = GeocoderQuery.LatLng.GetDistance((double)driveTo.StartLatitude, (double)driveTo.StartLongitude, (double)driveTo.EndLatitude, (double)driveTo.EndLongitude, GeocoderQuery.LatLng.UnitType.Miles);
						initialLatitude = (double)driveTo.StartLatitude;
						initialLongitude = (double)driveTo.StartLongitude;
					}
					else if (messageType == MessageType.ApptAddedOneWayFromClinic)
					{
						driveDistance = GeocoderQuery.LatLng.GetDistance((double)driveFrom.StartLatitude, (double)driveFrom.StartLongitude, (double)driveFrom.EndLatitude, (double)driveFrom.EndLongitude, GeocoderQuery.LatLng.UnitType.Miles);
						initialLatitude = (double)driveFrom.StartLatitude;
						initialLongitude = (double)driveFrom.StartLongitude;
					}
					else if (messageType == MessageType.ApptAddedRoundTripDiffAddress)
					{
						double distanceTo = GeocoderQuery.LatLng.GetDistance((double)driveTo.StartLatitude, (double)driveTo.StartLongitude, (double)driveTo.EndLatitude, (double)driveTo.EndLongitude, GeocoderQuery.LatLng.UnitType.Miles);
						double distanceFrom = GeocoderQuery.LatLng.GetDistance((double)driveFrom.StartLatitude, (double)driveFrom.StartLongitude, (double)driveFrom.EndLatitude, (double)driveFrom.EndLongitude, GeocoderQuery.LatLng.UnitType.Miles);
						driveDistance = (distanceTo > distanceFrom ? distanceTo : distanceFrom);
						initialLatitude = (double)driveTo.StartLatitude;
						initialLongitude = (double)driveTo.StartLongitude;
					}
				}

				//get the message by message type
				MessageQuery messageQuery = new MessageQuery(dbContext);
				Message message = messageQuery.GetMessageByType(Convert.ToInt32(messageType), true);
				ClinicQuery clinicQuery = new ClinicQuery(dbContext);
				Clinic clinic = clinicQuery.GetClinicByID(appointment.ClinicId, true);

				//select the drivers 
				VolunteerQuery volunteerQuery = new VolunteerQuery(dbContext);
				var drivers = volunteerQuery.GetAllActiveDriversWithTextConsent(true);

				//send message to appropriate drivers
				foreach (Volunteer driver in drivers)
				{
					if (!String.IsNullOrEmpty(driver.MobilePhone) && driver.Latitude.HasValue && driver.Longitude.HasValue)
					{
						//build the outbound message text
						string messageText = BuildMessage(message.MessageText, clinic, appointment, driver, 0, 0);

						//send message to all drivers is appointment outside 30 miles or and appointment made for today
						if (driveDistance >= 30 || messageType == MessageType.ApptAddedToday)
							SMSMessage(messageText, accountPhoneNumber, driver.MobilePhone, driver.Id, appointment.Id);
						else
						{
							//calculate hours difference
							int hours = 0;
							if (((TimeSpan)(DateTime.UtcNow - appointment.Created)).Days != 0)
								hours = ((int)((TimeSpan)(DateTime.UtcNow - appointment.Created)).Days * 24) + (int)((TimeSpan)(DateTime.UtcNow - appointment.Created)).Hours;
							else
								hours = (int)((TimeSpan)(DateTime.UtcNow - appointment.Created)).Hours;

							//end message to driver based on time since appointment creation and distnace from driver
							double radius = GeocoderQuery.LatLng.GetDistance(initialLatitude, initialLongitude, (double)driver.Latitude, (double)driver.Longitude, GeocoderQuery.LatLng.UnitType.Miles);
							if (hours < 2 && radius <= 5)
								SMSMessage(messageText, accountPhoneNumber, driver.MobilePhone, driver.Id, appointment.Id);
							else if (hours < 3 && radius > 5 && radius <= 15)
								SMSMessage(messageText, accountPhoneNumber, driver.MobilePhone, driver.Id, appointment.Id);
							else if (hours >= 3 && radius > 15)
								SMSMessage(messageText, accountPhoneNumber, driver.MobilePhone, driver.Id, appointment.Id);
						}
					}
				}
			}
		}

		private string BuildMessage(string messageText, Clinic clinic, Appointment appointment, Volunteer driver, int driveCount, int driveId)
		{
			return messageText.Replace("{clinic}", clinic?.Name ?? "")
				.Replace("{vagueTo}", appointment?.PickupLocationVague ?? "")
				.Replace("{vagueFrom}", appointment?.DropoffLocationVague ?? "")
				.Replace("{timeDate}", appointment?.AppointmentDate.ToString() ?? "")
				.Replace("{dayOfTheWeek}", appointment?.AppointmentDate.DayOfWeek.ToString() ?? "")
				.Replace("{volunteerFirstName}", driver?.FirstName ?? "")
				.Replace("{driveCount}", driveCount.ToString())
				.Replace("{driveId}", driveId.ToString()); 
		}
		private void SMSMessage(string messageText, string fromPhone, string toPhone, int? driverId, int? appointmentId)
		{
			//initialize twilio client
			TwilioClient.Init(accountSid, authToken);

			//send requested message
			var message = MessageResource.Create(body: messageText,
												 from: new Twilio.Types.PhoneNumber(fromPhone),
												 to: new Twilio.Types.PhoneNumber(toPhone));

			//log message sent to database (from number, to number, message text, date sent)
			var messageLogEntity = new Core.Entities.MessageLog();
			messageLogEntity.FromPhone = fromPhone;
			messageLogEntity.ToPhone = toPhone;
			messageLogEntity.Body = messageText;
			messageLogEntity.DateSent = DateTime.UtcNow;
			messageLogEntity.AppointmentId = appointmentId;
			messageLogEntity.VolunteerId = driverId;
			dbContext.MessageLog.Add(messageLogEntity);
			dbContext.SaveChanges();
		}
	}
}
