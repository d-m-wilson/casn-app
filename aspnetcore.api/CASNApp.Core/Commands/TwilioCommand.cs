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
			FriendlyReminder = 5,
			SeriousRequest = 6,
			DesperatePlea = 7,
		}

		public TwilioCommand(string accountSid, string authTokey, string accountPhoneNumber, ILogger<TwilioCommand> logger, casn_appContext dbContext)
		{
			this.accountSid = accountSid;
			this.authToken = authTokey;
			this.accountPhoneNumber = accountPhoneNumber;
			this.logger = logger;
			this.dbContext = dbContext;
		}

		public void SendAppointmentMessage(Appointment appointment, Drive driveTo, Drive driveFrom, MessageType messageType)
		{


			//determine type of meesage that would be displayed
			if (messageType == MessageType.Unknown)
			{
				if (driveTo == null && driveFrom != null)
					messageType = MessageType.ApptAddedOneWayFromClinic;
				else if (driveTo != null && driveFrom == null)
					messageType = MessageType.ApptAddedOneWayToClinic;
				else if (driveTo.StartAddress == driveFrom.EndAddress)
					messageType = MessageType.ApptAddedRoundTripSameAddress;
				else if (driveTo.StartAddress != driveFrom.EndAddress)
					messageType = MessageType.ApptAddedRoundTripDiffAddress;
				else
					messageType = MessageType.Unknown;
			}

			//get the list of drivers for the message based on message type, elapsed time, and length of the drive drive
			double driveDistance = 0;
			double initialLatitude = 0;
			double initialLongitude = 0;
			if (messageType == MessageType.FriendlyReminder || messageType == MessageType.FriendlyReminder || messageType == MessageType.DesperatePlea)
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
			string messageText = message.MessageText.Replace("{clinic}", clinic.Name).Replace("{vagueTo}", appointment.PickupLocationVague).Replace("{vagueFrom}", appointment.DropoffLocationVague);

			//select the drivers 
			VolunteerQuery volunteerQuery = new VolunteerQuery(dbContext);
			var drivers = volunteerQuery.GetAllActiveDriversWithTextConsent(true);

			//send message to appropriate drivers
			foreach (Volunteer driver in drivers)
			{
				if (driver.MobilePhone != null)
				{
					//send message to all drivers is appointment outside 30 miles or and appointment made for today
					if (messageType == MessageType.FriendlyReminder || messageType == MessageType.FriendlyReminder || messageType == MessageType.DesperatePlea 
						|| driveDistance >= 30 || appointment.AppointmentDate.Date == DateTime.Now.Date)
						SMSMessage(messageText, accountPhoneNumber, driver.MobilePhone);
					else
					{
						//calculate hours difference
						int hours = 0;
						if (((TimeSpan)(DateTime.Now - appointment.Created)).Days != 0)
							hours = ((int)((TimeSpan)(DateTime.Now - appointment.Created)).Days * 24) + (int)((TimeSpan)(DateTime.Now - appointment.Created)).Hours;
						else
							hours = (int)((TimeSpan)(DateTime.Now - appointment.Created)).Hours;
						
						double radius = GeocoderQuery.LatLng.GetDistance(initialLatitude, initialLongitude, (double)driver.Latitude, (double)driver.Longitude, GeocoderQuery.LatLng.UnitType.Miles);
						if (hours < 2 && radius <= 5)
							SMSMessage(messageText, accountPhoneNumber, driver.MobilePhone);
						else if (hours < 3 && radius > 5 && radius <= 15)
							SMSMessage(messageText, accountPhoneNumber, driver.MobilePhone);
						else if (hours >= 3 && radius > 15)
							SMSMessage(messageText, accountPhoneNumber, driver.MobilePhone);
					}
				}
			}
		}

		private void SMSMessage(string messageText, string fromPhone, string toPhone)
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
			messageLogEntity.DateSent = DateTime.Now;
			dbContext.MessageLog.Add(messageLogEntity);
			dbContext.SaveChanges();
		}
	}
}
