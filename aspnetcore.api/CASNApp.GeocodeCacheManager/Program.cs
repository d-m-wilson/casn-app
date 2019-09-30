using System;
using System.IO;
using System.Linq;
using CASNApp.Core.Entities;
using CASNApp.Core.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace CASNApp.GeocodeCacheManager
{
    class Program
    {
        private static readonly IConfiguration configuration;
        private static readonly int maxDays;

        static Program()
        {
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, false)
                .AddUserSecrets<Program>(true)
                .AddEnvironmentVariables()
                .Build();

            maxDays = int.Parse(configuration["GeocodeCacheMaxDays"]);
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
                .AddLogging(builder => {
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
            var servicesProvider = BuildDi();
            var logger = servicesProvider.GetRequiredService<ILogger<Program>>();

            var logMessage = $"Cleaning {nameof(Drive)} records...";
            Console.WriteLine(logMessage);
            logger.LogInformation(logMessage);

            var driveCount = 0;

            using (var dbContext = servicesProvider.GetRequiredService<casn_appContext>())
            {
                var drives = dbContext.Drive
                    .Where(d => d.StartGeocoded.HasValue || d.EndGeocoded.HasValue);

                foreach (var drive in drives)
                {
                    bool updated = false;

                    if (drive.StartGeocoded.HasValue &&
                        drive.StartGeocoded.Value.AddDays(maxDays) < DateTime.UtcNow)
                    {
                        drive.StartLatitude = null;
                        drive.StartLongitude = null;
                        drive.StartGeocoded = null;
                        updated = true;
                    }

                    if (drive.EndGeocoded.HasValue &&
                        drive.EndGeocoded.Value.AddDays(maxDays) < DateTime.UtcNow)
                    {
                        drive.EndLatitude = null;
                        drive.EndLongitude = null;
                        drive.EndGeocoded = null;
                        updated = true;
                    }

                    if (updated)
                    {
                        driveCount++;
                    }
                }

                dbContext.SaveChanges();
            }

            logMessage = $"{driveCount} {nameof(Drive)} records cleaned.";
            Console.WriteLine(logMessage);
            logger.LogInformation(logMessage);

            logMessage = $"Cleaning {nameof(Appointment)} records...";
            Console.WriteLine(logMessage);
            logger.LogInformation(logMessage);

            var appointmentCount = 0;

            using (var dbContext = servicesProvider.GetRequiredService<casn_appContext>())
            {
                var appointments = dbContext.Appointment
                    .Where(a => a.PickupVagueGeocoded.HasValue || a.DropoffVagueGeocoded.HasValue);

                foreach (var appointment in appointments)
                {
                    bool updated = false;

                    if (appointment.PickupVagueGeocoded.HasValue &&
                        appointment.PickupVagueGeocoded.Value.AddDays(maxDays) < DateTime.UtcNow)
                    {
                        appointment.PickupVagueLatitude = null;
                        appointment.PickupVagueLongitude = null;
                        appointment.PickupVagueGeocoded = null;
                        updated = true;
                    }

                    if (appointment.DropoffVagueGeocoded.HasValue &&
                        appointment.DropoffVagueGeocoded.Value.AddDays(maxDays) < DateTime.UtcNow)
                    {
                        appointment.DropoffVagueLatitude = null;
                        appointment.DropoffVagueLongitude = null;
                        appointment.DropoffVagueGeocoded = null;
                        updated = true;
                    }

                    if (updated)
                    {
                        appointmentCount++;
                    }
                }

                dbContext.SaveChanges();
            }

            logMessage = $"{appointmentCount} {nameof(Appointment)} records cleaned.";
            Console.WriteLine(logMessage);
            logger.LogInformation(logMessage);

            logMessage = $"Refreshing {nameof(Clinic)} records...";
            Console.WriteLine(logMessage);
            logger.LogInformation(logMessage);

            var clinicCount = 0;

            var googleApiKey = configuration["GoogleApiKey"];
            var geocoderLogger = servicesProvider.GetRequiredService<ILogger<GeocoderQuery>>();
            var geocoder = new GeocoderQuery(googleApiKey, geocoderLogger);

            using (var dbContext = servicesProvider.GetRequiredService<casn_appContext>())
            {
                var clinics = dbContext.Clinic;

                foreach (var clinic in clinics)
                {
                    if (clinic.Latitude.HasValue &&
                        clinic.Longitude.HasValue &&
                        (!clinic.Geocoded.HasValue ||
                        clinic.Geocoded.Value.AddDays(maxDays) > DateTime.UtcNow))
                    {
                        continue;
                    }

                    var clinicAddress = clinic.GetAddress();
                    var clinicLocation = geocoder.ForwardLookupAsync(clinicAddress).Result;

                    if (clinicLocation == null)
                    {
                        logMessage = "The geocoder returned null. Please check the log for details.";
                        Console.WriteLine(logMessage);
                        logger.LogError(logMessage);
                        throw new Exception(logMessage);
                    }

                    clinic.SetLocation(clinicLocation);

                    clinicCount++;
                }

                dbContext.SaveChanges();
            }

            logMessage = $"{clinicCount} {nameof(Clinic)} records refreshed.";
            Console.WriteLine(logMessage);
            logger.LogInformation(logMessage);

            logMessage = $"Refreshing {nameof(Volunteer)} records...";
            Console.WriteLine(logMessage);
            logger.LogInformation(logMessage);

            var volunteerCount = 0;

            using (var dbContext = servicesProvider.GetRequiredService<casn_appContext>())
            {
                var volunteers = dbContext.Volunteer;

                foreach (var volunteer in volunteers)
                {
                    if (volunteer.Latitude.HasValue &&
                        volunteer.Longitude.HasValue &&
                        (!volunteer.Geocoded.HasValue ||
                        volunteer.Geocoded.Value.AddDays(maxDays) > DateTime.UtcNow))
                    {
                        continue;
                    }

                    var clinicAddress = volunteer.GetAddress();
                    var clinicLocation = geocoder.ForwardLookupAsync(clinicAddress).Result;

                    if (clinicLocation == null)
                    {
                        logMessage = "The geocoder returned null. Please check the log for details.";
                        Console.WriteLine(logMessage);
                        logger.LogError(logMessage);
                        throw new Exception(logMessage);
                    }

                    volunteer.SetLocation(clinicLocation);

                    volunteerCount++;
                }

                dbContext.SaveChanges();
            }

            logMessage = $"{volunteerCount} {nameof(Volunteer)} records refreshed.";
            Console.WriteLine(logMessage);
            logger.LogInformation(logMessage);

        }

    }
}
