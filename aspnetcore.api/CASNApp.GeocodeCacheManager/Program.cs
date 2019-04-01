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
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

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
                .Build();

            maxDays = int.Parse(configuration["GeocodeCacheMaxDays"]);
        }

        private static ServiceProvider BuildDi()
        {
            return new ServiceCollection()
                .AddDbContext<casn_appContext>(options =>
                {
                    options.UseMySql(configuration[Core.Constants.DbConnectionString], mysqlOptions =>
                    {
                        mysqlOptions.ServerVersion(Version.Parse(configuration[Core.Constants.MySQLVersionString]), ServerType.MySql)
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

            Console.WriteLine($"Cleaning {nameof(Drive)} records...");
            var driveCount = 0;

            using (var dbContext = servicesProvider.GetRequiredService<casn_appContext>())
            {
                var drives = dbContext.Drive
                    .Where(d => d.StartGeocoded.HasValue || d.EndGeocoded.HasValue);

                foreach (var drive in drives)
                {
                    if (drive.StartGeocoded.HasValue &&
                        drive.StartGeocoded.Value.AddDays(maxDays) < DateTime.UtcNow)
                    {
                        drive.StartLatitude = null;
                        drive.StartLongitude = null;
                        drive.StartGeocoded = null;
                    }

                    if (drive.EndGeocoded.HasValue &&
                        drive.EndGeocoded.Value.AddDays(maxDays) < DateTime.UtcNow)
                    {
                        drive.EndLatitude = null;
                        drive.EndLongitude = null;
                        drive.EndGeocoded = null;
                    }

                    driveCount++;
                }

                dbContext.SaveChanges();
            }

            Console.WriteLine($"{driveCount} {nameof(Drive)} records cleaned.");

            Console.WriteLine($"Cleaning {nameof(Appointment)} records...");
            var appointmentCount = 0;

            using (var dbContext = servicesProvider.GetRequiredService<casn_appContext>())
            {
                var appointments = dbContext.Appointment
                    .Where(a => a.PickupVagueGeocoded.HasValue || a.DropoffVagueGeocoded.HasValue);

                foreach (var appointment in appointments)
                {
                    if (appointment.PickupVagueGeocoded.HasValue &&
                        appointment.PickupVagueGeocoded.Value.AddDays(maxDays) < DateTime.UtcNow)
                    {
                        appointment.PickupVagueLatitude = null;
                        appointment.PickupVagueLongitude = null;
                        appointment.PickupVagueGeocoded = null;
                    }

                    if (appointment.DropoffVagueGeocoded.HasValue &&
                        appointment.DropoffVagueGeocoded.Value.AddDays(maxDays) < DateTime.UtcNow)
                    {
                        appointment.DropoffVagueLatitude = null;
                        appointment.DropoffVagueLongitude = null;
                        appointment.DropoffVagueGeocoded = null;
                    }

                    appointmentCount++;
                }

                dbContext.SaveChanges();
            }

            Console.WriteLine($"{appointmentCount} {nameof(Appointment)} records cleaned.");

            Console.WriteLine($"Refreshing {nameof(Clinic)} records...");
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
                    clinic.SetLocation(clinicLocation);

                    clinicCount++;
                }

                dbContext.SaveChanges();
            }

            Console.WriteLine($"{clinicCount} {nameof(Clinic)} records refreshed.");

        }

    }
}
