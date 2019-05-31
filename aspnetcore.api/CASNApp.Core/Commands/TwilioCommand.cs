using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using CASNApp.Core.Entities;
using CASNApp.Core.Queries;
using System.Globalization;

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
		}

		public TwilioCommand(string accountSid, string authTokey, string accountPhoneNumber, ILogger<TwilioCommand> logger, casn_appContext dbContext)
		{
			this.accountSid = accountSid;
			this.authToken = authTokey;
			this.accountPhoneNumber = accountPhoneNumber;
			this.logger = logger;
			this.dbContext = dbContext;
		}

		public void SendAppointmentMessage(Appointment appointment, Drive driveTo, Drive driveFrom, int distance)
		{
			//determine type of meesage that would be displayed
			MessageType messageType = new MessageType();
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

			//get the message by message type
			MessageQuery messageQuery = new MessageQuery(dbContext);
			Message message = messageQuery.GetMessageByType(Convert.ToInt32(messageType), true);
			string messageText = message.MessageText.Replace("{clinic}", appointment.Clinic.Name).Replace("{vagueTo}", appointment.PickupLocationVague).Replace("{vagueFrom}", appointment.DropoffLocationVague);
			
			//get the list of drivers for the message based on message type, elapsed time, and length of the drive drive
			double driveDistance = 0;
			double initialLatitude;
			double initialLongitude;
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

			//select the drivers 
			VolunteerQuery volunteerQuery = new VolunteerQuery(dbContext);
			Volunteer[] drivers = null;
			if (driveDistance >= 30) 
				drivers = volunteerQuery.GetAllActiveDrivers(true); //all drivers if length of drive is greater than 30 miles
			else 
			{
				//calculate the latitude and longitude range for the specified radius
			}

			//send message
			foreach (Volunteer driver in drivers)
			{
				if (driver.MobilePhone != null)
					SMSMessage(messageText, accountPhoneNumber, driver.MobilePhone);
			}
		}

		public void SMSMessage(string messageText, string fromPhone, string toPhone)
		{
			TwilioClient.Init(accountSid, authToken);

			var message = MessageResource.Create(body: messageText,
												 from: new Twilio.Types.PhoneNumber(fromPhone),
												 to: new Twilio.Types.PhoneNumber(toPhone));
		}
	}
}
