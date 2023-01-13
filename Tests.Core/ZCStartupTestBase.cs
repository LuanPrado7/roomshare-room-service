using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.Core;

public abstract class ZCStartupTestBase
{
    /// <summary>
    /// EnvironmentName 
    /// Default: Microsoft.Extensions.Hosting.Environments.Staging
    /// </summary>
    public virtual string EnvironmentName
    {
        get
        {
            return Microsoft.Extensions.Hosting.Environments.Staging;
        }
    }
    public abstract void Configure(IApplicationBuilder app);
    public abstract void ConfigureTestServices(IServiceCollection services);

}
