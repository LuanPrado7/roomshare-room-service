using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using ZC.Service.AppEnviroment;

namespace Tests.Core;

public abstract class ZCStartupIntegrationTest<T> : ZCStartupTestBase where T : class
{
    public void ConfigureHost(IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureWebHost(webHostBuilder =>
        {
            AppEnviromentService.LoadLaunchSettingsIntoEnvVariables("Properties/launchSettings.json", EnvironmentName);

            webHostBuilder
            .UseTestServer()
            .ConfigureAppConfiguration((hostingContext, configurationBuilder) =>
            {
                var env = hostingContext.HostingEnvironment;

                configurationBuilder
                   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                   .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                   .AddEnvironmentVariables();

                ConfigureAppConfiguration(hostingContext, configurationBuilder);
            })
            .ConfigureTestServices(services => ConfigureTestServices(services))
            .Configure(app => Configure(app))
            .UseStartup<T>();
        });
    }
    public abstract void ConfigureAppConfiguration(WebHostBuilderContext hostingContext, IConfigurationBuilder configurationBuilder);
}
