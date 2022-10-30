using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp;

namespace EasyAbp.IkapatigiCapstone;

public class IkapatigiCapstoneWebTestStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication<IkapatigiCapstoneWebTestModule>();
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
        app.InitializeApplication();
    }
}
