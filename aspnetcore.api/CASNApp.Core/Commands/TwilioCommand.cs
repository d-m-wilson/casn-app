using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace CASNApp.Core.Commands
{
	public class TwilioCommand
	{
		private string accountSid;
		private string authToken;
		private ILogger<TwilioCommand> logger;

		public TwilioCommand(string accountSid, string authTokey, ILogger<TwilioCommand> logger)
		{
			this.accountSid = accountSid;
			this.accountSid = authTokey;
			this.logger = logger;
		}

		public void SendAppointmentMessage()
		{
			SMSMessage("We’ve got a round-trip drive from Beaumont to Houston Women's Clinic. Can you help?", "+17133227795", "+12817829558");
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
