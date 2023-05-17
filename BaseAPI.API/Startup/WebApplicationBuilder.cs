using System;
namespace BaseAPI.API.Startup
{
	public static class ConfigurationInitializer
	{
		public static IHostBuilder ConfigureAppSettings(this IHostBuilder host)
		{
			var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            host.ConfigureAppConfiguration((ctx, builder) =>
			{
				builder.AddJsonFile("appsettings.json", false, true);
				builder.AddJsonFile($"appsettings.{environment}.json",true,true);
				builder.AddEnvironmentVariables();
			});

			return host;
		}
	}
}

