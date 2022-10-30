using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EasyAbp.IkapatigiCapstone1.Data;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.IkapatigiCapstone1.EntityFrameworkCore;

public class EntityFrameworkCoreIkapatigiCapstone1DbSchemaMigrator
    : IIkapatigiCapstone1DbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreIkapatigiCapstone1DbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the IkapatigiCapstone1DbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<IkapatigiCapstone1DbContext>()
            .Database
            .MigrateAsync();
    }
}
