using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp;

namespace EasyAbp.IkapatigiCapstone1;

public class IkapatigiCapstone1WebTestStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication<IkapatigiCapstone1WebTestModule>();
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
        app.InitializeApplication();
    }
}
