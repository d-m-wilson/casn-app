using System;
using System.Collections.Generic;
using CASNApp.Core.Entities;
using CASNApp.Core.Queries;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace CASNApp.Core.Commands
{

    public class TwilioCommand
	{
		private readonly ILogger<TwilioCommand> logger;
		private readonly casn_appContext dbContext;
		private readonly IConfiguration configuration;
		private readonly string accountSid;
		private readonly string authToken;
		private readonly string accountPhoneNumber;
		private readonly TimeZoneInfo timeZone;
		private readonly string appUrl;

		public enum MessageType
		{
			Unknown = 0,
			ApptAddedOneWayToServiceProvider = 1,
            ApptAddedOneWayFromServiceProvider = 2,
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

		public TwilioCommand(ILogger<TwilioCommand> logger, casn_appContext dbContext, IConfiguration configuration)
		{
			this.logger = logger;
			this.dbContext = dbContext;
			this.configuration = configuration;

			accountSid = configuration[Constants.TwilioAccountSID];
			authToken = configuration[Constants.TwilioAuthKey];
			accountPhoneNumber = configuration[Constants.TwilioPhoneNumber];
			appUrl = configuration[Constants.CASNAppURL];

			var timeZoneName = configuration[Constants.UserTimeZoneName];
			timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneName) ??
                throw new ArgumentException($"Time zone not found: \"{timeZoneName}\"", nameof(timeZoneName));
		}

		public void SendDispatcherMessage(Drive drive, Volunteer driver, MessageType messageType)
		{
			//get the message by message type
			MessageQuery messageQuery = new MessageQuery(dbContext);
			Message message = messageQuery.GetMessageByType(Convert.ToInt32(messageType), true);

			//get the appointment by id
			AppointmentQuery appointmentQuery = new AppointmentQuery(dbContext);
			var appointment = appointmentQuery.GetAppointmentByIdWithCaller(drive.AppointmentId, true);

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
						string messageText = BuildMessage(message.MessageText, null, appointment, driver);

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
					string messageText = BuildMessage(message.MessageText, null, appointment, driver);

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

			//build the reminder message text
			string messageText = BuildMessage(message.MessageText, null, null, null);

			//build the appoint list message text
			string appointmentListText = "";
			ServiceProviderQuery serviceProviderQuery = new ServiceProviderQuery(dbContext);
			foreach (Appointment appointment in appointments)
			{
				ServiceProvider serviceProvider = serviceProviderQuery.GetServiceProviderById(appointment.ServiceProviderId, true);
				if (appointmentListText != "")
					appointmentListText += "\n";
				appointmentListText += serviceProvider.Name + " on " + TimeZoneInfo.ConvertTimeFromUtc(appointment.AppointmentDate, timeZone).ToString("MM/dd/yyyy hh:mm tt");
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
					SMSMessage(appointmentListText, accountPhoneNumber, driver.MobilePhone, driver.Id, null);
				}
			}
		}

		public void SendAppointmentMessage(Appointment appointment, Drive driveTo, Drive driveFrom, MessageType messageType, bool isCronJob)
		{
			int tier2MessageDelayMinutes = int.Parse(configuration["Tier2MessageDelayMinutes"]);
			int tier3MessageDelayMinutes = int.Parse(configuration["Tier3MessageDelayMinutes"]);

			//determine type of meesage that would be displayed
			//if (appointment.AppointmentDate.Date == DateTime.UtcNow.Date)
			if (TimeZoneInfo.ConvertTimeFromUtc(appointment.AppointmentDate, timeZone).Date == TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone).Date)
				messageType = MessageType.ApptAddedToday;
			else if (driveTo == null && driveFrom != null)
				messageType = MessageType.ApptAddedOneWayFromServiceProvider;
			else if (driveTo != null && driveFrom == null)
				messageType = MessageType.ApptAddedOneWayToServiceProvider;
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
					if (messageType == MessageType.ApptAddedRoundTripSameAddress || messageType == MessageType.ApptAddedOneWayToServiceProvider)
					{
						driveDistance = GeocoderQuery.LatLng.GetDistance((double)driveTo.StartLatitude, (double)driveTo.StartLongitude, (double)driveTo.EndLatitude, (double)driveTo.EndLongitude, GeocoderQuery.LatLng.UnitType.Miles);
						initialLatitude = (double)driveTo.StartLatitude;
						initialLongitude = (double)driveTo.StartLongitude;
					}
					else if (messageType == MessageType.ApptAddedOneWayFromServiceProvider)
					{
						driveDistance = GeocoderQuery.LatLng.GetDistance((double)driveFrom.StartLatitude, (double)driveFrom.StartLongitude, (double)driveFrom.EndLatitude, (double)driveFrom.EndLongitude, GeocoderQuery.LatLng.UnitType.Miles);
						initialLatitude = (double)driveFrom.EndLatitude;
						initialLongitude = (double)driveFrom.EndLongitude;
					}
					else if (messageType == MessageType.ApptAddedRoundTripDiffAddress)
					{
						double distanceTo = GeocoderQuery.LatLng.GetDistance((double)driveTo.StartLatitude, (double)driveTo.StartLongitude, (double)driveTo.EndLatitude, (double)driveTo.EndLongitude, GeocoderQuery.LatLng.UnitType.Miles);
						double distanceFrom = GeocoderQuery.LatLng.GetDistance((double)driveFrom.StartLatitude, (double)driveFrom.StartLongitude, (double)driveFrom.EndLatitude, (double)driveFrom.EndLongitude, GeocoderQuery.LatLng.UnitType.Miles);
						driveDistance = distanceTo > distanceFrom ? distanceTo : distanceFrom;
						initialLatitude = (double)driveTo.StartLatitude;
						initialLongitude = (double)driveTo.StartLongitude;
					}
				}

				//get the message by message type
				MessageQuery messageQuery = new MessageQuery(dbContext);
				Message message = messageQuery.GetMessageByType(Convert.ToInt32(messageType), true);
				ServiceProviderQuery serviceProviderQuery = new ServiceProviderQuery(dbContext);
				ServiceProvider serviceProvider = serviceProviderQuery.GetServiceProviderById(appointment.ServiceProviderId, true);

				//select the drivers 
				VolunteerQuery volunteerQuery = new VolunteerQuery(dbContext);
				var drivers = volunteerQuery.GetAllActiveDriversWithTextConsent(true);

				//calculate hours difference and initialize mesage count
				int messageCount = 0;

				int minutesSinceAppointmentCreated = (int)(DateTime.UtcNow - appointment.Created).TotalMinutes;

				int minutesSinceTier2Message;

				if (appointment.Tier2MessageDate.HasValue)
				{
					minutesSinceTier2Message = (int)(DateTime.UtcNow - appointment.Tier2MessageDate.Value).TotalMinutes;
				}
				else
				{
					minutesSinceTier2Message = -1;
				}

				//send message to appropriate drivers
				foreach (Volunteer driver in drivers)
				{
					if (!String.IsNullOrEmpty(driver.MobilePhone) && driver.Latitude.HasValue && driver.Longitude.HasValue)
					{
						//build the outbound message text
						string messageText = BuildMessage(message.MessageText, serviceProvider, appointment, driver);

						//send message to all drivers is appointment outside 30 miles or and appointment made for today
						if (driveDistance >= 30 || messageType == MessageType.ApptAddedToday)
						{
							if (!appointment.BroadcastMessageCount.HasValue)
							{
								SMSMessage(messageText, accountPhoneNumber, driver.MobilePhone, driver.Id, appointment.Id);
								messageCount++;
							}
						}
						else
						{
							//send message to driver based on time since appointment creation and distnace from driver
							double radius = GeocoderQuery.LatLng.GetDistance(initialLatitude, initialLongitude, (double)driver.Latitude, (double)driver.Longitude, GeocoderQuery.LatLng.UnitType.Miles);

							if (!isCronJob && radius <= 5 && !appointment.Tier1MessageCount.HasValue)
							{
								SMSMessage(messageText, accountPhoneNumber, driver.MobilePhone, driver.Id, appointment.Id);
								messageCount++;
							}
							else if (minutesSinceAppointmentCreated >= tier2MessageDelayMinutes &&
								radius > 5 &&
								radius <= 15 &&
								!appointment.Tier2MessageCount.HasValue)
							{
								SMSMessage(messageText, accountPhoneNumber, driver.MobilePhone, driver.Id, appointment.Id);
								messageCount++;
							}
							else if (minutesSinceTier2Message >= tier3MessageDelayMinutes &&
								radius > 15 &&
								!appointment.Tier3MessageCount.HasValue)
							{ 
								SMSMessage(messageText, accountPhoneNumber, driver.MobilePhone, driver.Id, appointment.Id);
								messageCount++;
							}
						}
					}
				}

				//set the proper tier message count value on the appointment
				if (driveDistance >= 30 || messageType == MessageType.ApptAddedToday && !appointment.BroadcastMessageCount.HasValue)
				{
					appointment.BroadcastMessageCount = messageCount;
					appointment.BroadcastMessageDate = DateTime.UtcNow;
				}
				else if (!isCronJob && !appointment.Tier1MessageCount.HasValue)
				{
					appointment.Tier1MessageCount = messageCount;
					appointment.Tier1MessageDate = DateTime.UtcNow;
				}
				else if (minutesSinceAppointmentCreated >= tier2MessageDelayMinutes &&
					!appointment.Tier2MessageCount.HasValue)
				{
					appointment.Tier2MessageCount = messageCount;
					appointment.Tier2MessageDate = DateTime.UtcNow;
				}
				else if (minutesSinceTier2Message >= tier3MessageDelayMinutes &&
					!appointment.Tier3MessageCount.HasValue)
				{
					appointment.Tier3MessageCount = messageCount;
					appointment.Tier3MessageDate = DateTime.UtcNow;
				}
			}
		}

		public void SendBadgeMessage(Volunteer driver, Badge badge)
		{
			if (!driver.HasTextConsent || string.IsNullOrWhiteSpace(badge.MessageText))
			{
				return;
			}

			//send text message to driver
			SMSMessage(badge.MessageText, accountPhoneNumber, driver.MobilePhone, driver.Id, null);
		}

		private string BuildMessage(string messageText, ServiceProvider serviceProvider, Appointment appointment, Volunteer driver)
		{
			return messageText.Replace("{clinic}", serviceProvider?.Name ?? "")
				.Replace("{vagueTo}", appointment?.PickupLocationVague ?? "")
				.Replace("{vagueFrom}", appointment?.DropoffLocationVague ?? "")
				.Replace("{timeDate}", appointment != null ? TimeZoneInfo.ConvertTimeFromUtc(appointment.AppointmentDate, timeZone).ToString("MM/dd/yyyy hh:mm tt") : "")
				.Replace("{dayOfTheWeek}", TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone).DayOfWeek.ToString() ?? "")
				.Replace("{volunteerFirstName}", driver?.FirstName ?? "")
				.Replace("{callerIdentifier}", appointment?.Caller?.CallerIdentifier)
				.Replace("{appUrl}", appUrl ?? "");
		}

		private void SMSMessage(string messageText, string fromPhone, string toPhone, int? driverId, int? appointmentId)
		{
			//initialize twilio client
			TwilioClient.Init(accountSid, authToken);
			MessageResource messageResource;

			try
			{
				//send requested message
				messageResource = MessageResource.Create(body: messageText,
					from: new Twilio.Types.PhoneNumber(fromPhone),
					to: new Twilio.Types.PhoneNumber(toPhone));

				//log message sent to database (from number, to number, message text, date sent)
				var messageLogEntity = new MessageLog
				{
					FromPhone = fromPhone,
					ToPhone = toPhone,
					Body = messageText,
					DateSent = DateTime.UtcNow,
					AppointmentId = appointmentId,
					VolunteerId = driverId
				};

				dbContext.MessageLog.Add(messageLogEntity);
				dbContext.SaveChanges();
			}
			catch (Twilio.Exceptions.ApiException apiException)
			{
				var messageErrorLogEntity = new MessageErrorLog
				{
					FromPhone = fromPhone,
					ToPhone = toPhone,
					Body = messageText,
					DateSent = DateTime.UtcNow,
					AppointmentId = appointmentId,
					VolunteerId = driverId,
					ErrorCode = apiException.Code.ToString(),
					ErrorMessage = apiException.Message,
					ErrorDetails = apiException.ToString()
				};

				dbContext.MessageErrorLog.Add(messageErrorLogEntity);
				dbContext.SaveChanges();
			}
		}

	}
}
