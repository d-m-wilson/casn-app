using System;
using System.IO;
using System.Linq;
using CASNApp.Core.Commands;
using CASNApp.Core.Entities;
using CASNApp.Core.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace CASNApp.TextMessageManager
{
    class Program
	{
		private static readonly IConfiguration configuration;
		private static ILoggerFactory loggerFactory;
		private static readonly string twilioAccountSID;
		private static readonly string twilioAuthKey;
		private static readonly string twilioPhoneNumber;

		static Program()
		{
			configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", false, false)
				.AddUserSecrets<Program>(true)
				.AddEnvironmentVariables()
				.Build();

			twilioAccountSID = configuration[Core.Constants.TwilioAccountSID];
			twilioAuthKey = configuration[Core.Constants.TwilioAuthKey];
			twilioPhoneNumber = configuration[Core.Constants.TwilioPhoneNumber];
		}

		private static ServiceProvider BuildDi()
		{
			return new ServiceCollection()
				.AddDbContext<casn_appContext>(options =>
				{
					options.UseSqlServer(configuration[Core.Constants.DbConnectionString], sqlOptions =>
					{
						sqlOptions
							.EnableRetryOnFailure(2);
					});
				}, ServiceLifetime.Transient, ServiceLifetime.Transient)
				.AddLogging(builder =>
				{
					builder.SetMinimumLevel(LogLevel.Trace);
					builder.AddNLog(new NLogProviderOptions
					{
						CaptureMessageTemplates = true,
						CaptureMessageProperties = true
					});
				})
				.BuildServiceProvider();
		}

		static void Main(string[] args)
		{
			//set the mesage priority from the command line
			TwilioCommand.MessageType messageType = TwilioCommand.MessageType.Unknown;
			if (args.Length > 0)
			{
				int value;
				if (int.TryParse(args[0], out value))
				{
					switch (value)
					{
						case 1:
							messageType = TwilioCommand.MessageType.FriendlyReminder;
							break;
						case 2:
							messageType = TwilioCommand.MessageType.SeriousRequest;
							break;
						case 3:
							messageType = TwilioCommand.MessageType.DesperatePlea;
							break;
					}
				}
			}

			var servicesProvider = BuildDi();
			var logger = servicesProvider.GetRequiredService<ILogger<Program>>();

			var logMessage = $"Sending text notifcations for appointments open drives...";
			Console.WriteLine(logMessage);
			logger.LogInformation(logMessage);

			//connect to the database
			using (var dbContext = servicesProvider.GetRequiredService<casn_appContext>())
			{
				//get a list of all the appointments with open drives
				AppointmentQuery appointmentQuery = new AppointmentQuery(dbContext);
				var openAppointments = appointmentQuery.GetAllAppointmentsWithOpenDrives(true);

				//loop thru the appointments and send the assoicated text message
				if (openAppointments != null && openAppointments.Count > 0)
				{
					loggerFactory = servicesProvider.GetRequiredService<ILoggerFactory>();
					DriveQuery driveQuery = new DriveQuery(dbContext);
					TwilioCommand newSMS = new TwilioCommand(twilioAccountSID, twilioAuthKey, twilioPhoneNumber, loggerFactory.CreateLogger<TwilioCommand>(), dbContext);
					foreach (Appointment appointment in openAppointments)
					{
						Drive driveTo = appointment.Drives.FirstOrDefault(d => d.IsActive && d.Direction == Core.Models.Drive.DirectionToClinic);
						Drive driveFrom = appointment.Drives.FirstOrDefault(d => d.IsActive && d.Direction == Core.Models.Drive.DirectionFromClinic);
						newSMS.SendAppointmentMessage(appointment, driveTo, driveFrom, messageType);
					}
				}
			}
		}
	}
}
