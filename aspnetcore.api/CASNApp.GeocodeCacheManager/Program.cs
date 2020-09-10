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
                .AddLogging(builder => {
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
            var servicesProvider = BuildDi();

            var googleApiKey = configuration["GoogleApiKey"];
            var geocoderLogger = servicesProvider.GetRequiredService<ILogger<GeocoderQuery>>();
            var geocoder = new GeocoderQuery(googleApiKey, geocoderLogger);

            RefreshServiceProviders(servicesProvider, geocoder);

            RefreshAndCleanDrives(servicesProvider);

            CleanAppointments(servicesProvider);

            RefreshVolunteers(servicesProvider, geocoder);

        }

        private static void RefreshServiceProviders(IServiceProvider servicesProvider, GeocoderQuery geocoder)
        {
            var logger = servicesProvider.GetRequiredService<ILogger<Program>>();

            string logMessage = $"Refreshing {nameof(Core.Entities.ServiceProvider)} records...";
            Console.WriteLine(logMessage);
            logger.LogInformation(logMessage);

            var serviceProviderCount = 0;

            using (var dbContext = servicesProvider.GetRequiredService<casn_appContext>())
            {
                var serviceProviders = dbContext.ServiceProviders;

                foreach (var serviceProvider in serviceProviders)
                {
                    if (serviceProvider.Latitude.HasValue &&
                        serviceProvider.Longitude.HasValue &&
                        (!serviceProvider.Geocoded.HasValue ||
                        serviceProvider.Geocoded.Value.AddDays(maxDays) > DateTime.UtcNow))
                    {
                        continue;
                    }

                    var serviceProviderAddress = serviceProvider.GetAddress();
                    var serviceProviderLocation = geocoder.ForwardLookupAsync(serviceProviderAddress).Result;

                    if (serviceProviderLocation == null)
                    {
                        logMessage = "The geocoder returned null. Please check the log for details.";
                        Console.WriteLine(logMessage);
                        logger.LogError(logMessage);
                        throw new Exception(logMessage);
                    }

                    serviceProvider.SetLocation(serviceProviderLocation);

                    serviceProviderCount++;
                }

                dbContext.SaveChanges();
            }

            logMessage = $"{serviceProviderCount} {nameof(Core.Entities.ServiceProvider)} records refreshed.";
            Console.WriteLine(logMessage);
            logger.LogInformation(logMessage);
        }

        private static void RefreshAndCleanDrives(IServiceProvider servicesProvider)
        {
            var logger = servicesProvider.GetRequiredService<ILogger<Program>>();

            var logMessage = $"Cleaning {nameof(Drive)} records...";
            Console.WriteLine(logMessage);
            logger.LogInformation(logMessage);

            var driveCount = 0;

            using (var dbContext = servicesProvider.GetRequiredService<casn_appContext>())
            {
                var drives = dbContext.Drives
                    .Include(d => d.Appointment)
                    .ThenInclude(a => a.ServiceProvider)
                    .Where(d => d.StartGeocoded.HasValue || d.EndGeocoded.HasValue);

                foreach (var drive in drives)
                {
                    bool updated = false;

                    if (drive.StartGeocoded.HasValue &&
                        drive.StartGeocoded.Value.AddDays(maxDays) < DateTime.UtcNow)
                    {
                        if (drive.Direction == Core.Models.Drive.DirectionToServiceProvider)
                        {
                            drive.StartLatitude = null;
                            drive.StartLongitude = null;
                            drive.StartGeocoded = null;
                        }
                        else
                        {
                            drive.StartLatitude = drive.Appointment.ServiceProvider.Latitude;
                            drive.StartLongitude = drive.Appointment.ServiceProvider.Longitude;
                            drive.StartGeocoded = drive.Appointment.ServiceProvider.Geocoded;
                        }

                        updated = true;
                    }

                    if (drive.EndGeocoded.HasValue &&
                        drive.EndGeocoded.Value.AddDays(maxDays) < DateTime.UtcNow)
                    {
                        if (drive.Direction == Core.Models.Drive.DirectionFromServiceProvider)
                        {
                            drive.EndLatitude = null;
                            drive.EndLongitude = null;
                            drive.EndGeocoded = null;
                        }
                        else
                        {
                            drive.EndLatitude = drive.Appointment.ServiceProvider.Latitude;
                            drive.EndLongitude = drive.Appointment.ServiceProvider.Longitude;
                            drive.EndGeocoded = drive.Appointment.ServiceProvider.Geocoded;
                        }

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

        }

        private static void CleanAppointments(IServiceProvider servicesProvider)
        {
            var logger = servicesProvider.GetRequiredService<ILogger<Program>>();

            string logMessage = $"Cleaning {nameof(Appointment)} records...";
            Console.WriteLine(logMessage);
            logger.LogInformation(logMessage);

            var appointmentCount = 0;

            using (var dbContext = servicesProvider.GetRequiredService<casn_appContext>())
            {
                var appointments = dbContext.Appointments
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
        }

        private static void RefreshVolunteers(IServiceProvider servicesProvider, GeocoderQuery geocoder)
        {
            var logger = servicesProvider.GetRequiredService<ILogger<Program>>();

            string logMessage = $"Refreshing {nameof(Volunteer)} records...";
            Console.WriteLine(logMessage);
            logger.LogInformation(logMessage);

            var volunteerCount = 0;

            using (var dbContext = servicesProvider.GetRequiredService<casn_appContext>())
            {
                var volunteers = dbContext.Volunteers;

                foreach (var volunteer in volunteers)
                {
                    if (volunteer.Latitude.HasValue &&
                        volunteer.Longitude.HasValue &&
                        (!volunteer.Geocoded.HasValue ||
                        volunteer.Geocoded.Value.AddDays(maxDays) > DateTime.UtcNow))
                    {
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(volunteer.Address) ||
                        string.IsNullOrWhiteSpace(volunteer.City) ||
                        string.IsNullOrWhiteSpace(volunteer.State) ||
                        string.IsNullOrWhiteSpace(volunteer.PostalCode))
                    {
                        logger.LogWarning($"Skipping volunteer {volunteer.Id} because their address is incomplete.");
                        continue;
                    }

                    var volunteerAddress = volunteer.GetAddress();
                    var volunteerLocation = geocoder.ForwardLookupAsync(volunteerAddress).Result;

                    if (volunteerLocation == null)
                    {
                        logMessage = "The geocoder returned null. Please check the log for details.";
                        Console.WriteLine(logMessage);
                        logger.LogError(logMessage);
                        throw new Exception(logMessage);
                    }

                    volunteer.SetLocation(volunteerLocation);

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
