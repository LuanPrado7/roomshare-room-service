using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using ZC.Service.AppEnviroment;

namespace Tests.Core;

public abstract class ZCStartupUnitTest : ZCStartupTestBase
{
    //https://docs.microsoft.com/pt-br/aspnet/core/test/middleware?view=aspnetcore-6.0
    public void ConfigureHost(IHostBuilder hostBuilder)
    {
        hostBuilder
            .ConfigureHostConfiguration(builder =>
            {
                AppEnviromentService.LoadLaunchSettingsIntoEnvVariables("Properties/launchSettings.json", EnvironmentName);
            })
            .ConfigureAppConfiguration((context, builder) =>
            {
                var env = context.HostingEnvironment;

                builder
                   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                   .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                   .AddEnvironmentVariables();

                ConfigureAppConfiguration(context, builder);
            })
            .ConfigureServices(services => ConfigureTestServices(services));
    }

    public abstract void ConfigureAppConfiguration(HostBuilderContext hostingContext, IConfigurationBuilder configurationBuilder);
}
