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
        private static readonly string userTimeZoneName;
		private static readonly string appUrl;

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
            userTimeZoneName = configuration[Core.Constants.UserTimeZoneName];
			appUrl = configuration[Core.Constants.CASNAppURL];
        }

        private static IServiceProvider BuildDi()
		{
			return new ServiceCollection()
				.AddDbContext<casn_appContext>(options =>
				{
					options.UseSqlServer(configuration.GetConnectionString(Core.Constants.DbConnectionString), sqlOptions =>
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
				.AddSingleton(configuration)
				.BuildServiceProvider();
		}

		static void Main(string[] args)
		{
			//set the mesage priority from the command line
			TwilioCommand.MessageType messageType = TwilioCommand.MessageType.Unknown;

			if (args.Length > 0)
			{
				var firstArgument = args[0];

				if (firstArgument.Contains("friendly", StringComparison.InvariantCultureIgnoreCase))
				{
					messageType = TwilioCommand.MessageType.FriendlyReminder;
				}
				else if (firstArgument.Contains("serious", StringComparison.InvariantCultureIgnoreCase))
				{
					messageType = TwilioCommand.MessageType.SeriousRequest;
				}
				else if (firstArgument.Contains("desperate", StringComparison.InvariantCultureIgnoreCase))
				{
					messageType = TwilioCommand.MessageType.DesperatePlea;
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
				loggerFactory = servicesProvider.GetRequiredService<ILoggerFactory>();

				if (messageType == TwilioCommand.MessageType.FriendlyReminder || messageType == TwilioCommand.MessageType.SeriousRequest || messageType == TwilioCommand.MessageType.DesperatePlea)
				{
					//send out a single reminder message for all open appointments
					TwilioCommand reminderSMS = new TwilioCommand(twilioAccountSID, twilioAuthKey, twilioPhoneNumber, loggerFactory.CreateLogger<TwilioCommand>(),
                        dbContext, userTimeZoneName, appUrl);
					reminderSMS.SendAppointmentReminderMessage(openAppointments, messageType);
				}
				else
				{
					//loop thru the appointments and send the assoicated text message
					if (openAppointments != null && openAppointments.Count > 0)
					{
						DriveQuery driveQuery = new DriveQuery(dbContext);
						TwilioCommand appointmentSMS = new TwilioCommand(twilioAccountSID, twilioAuthKey, twilioPhoneNumber, loggerFactory.CreateLogger<TwilioCommand>(),
                            dbContext, userTimeZoneName, appUrl);
						foreach (Appointment appointment in openAppointments)
						{
							//get each drive objetc (to and from) and send messages to appropriate drives
							Drive driveTo = appointment.Drives.FirstOrDefault(d => d.IsActive && d.Direction == Core.Models.Drive.DirectionToServiceProvider);
							Drive driveFrom = appointment.Drives.FirstOrDefault(d => d.IsActive && d.Direction == Core.Models.Drive.DirectionFromServiceProvider);
							if (driveTo != null || driveFrom != null)
								appointmentSMS.SendAppointmentMessage(appointment, driveTo, driveFrom, messageType, true);

							//update the appointment values in the database
							dbContext.Appointment.Update(appointment);
							dbContext.SaveChanges();
						}
					}
				}
			}
		}
	}
}
