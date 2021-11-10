using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace ConfigurationExtended
{
    public static class ConfigurationUtility
    {
        public static IConfigurationRoot Build()
        {
            var args = Environment.GetCommandLineArgs();
            var envArg = args.ToList().IndexOf("--environment");
            var envFromArgs = envArg >= 0 ? args[envArg + 1] : null;

            var aspnetcore = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var dotnetcore = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");

            var environment = envFromArgs ?? (string.IsNullOrWhiteSpace(aspnetcore)
                ? dotnetcore
                : aspnetcore);

            return new ConfigurationBuilder()
                .AddCommandLine(Environment.GetCommandLineArgs())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile(
                    $"appsettings.{environment}.json",
                    optional: true)
                .Build();
        }
    }
}
