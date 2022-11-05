using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EasyAbp.IkapatigiCapstone.Data;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.IkapatigiCapstone.EntityFrameworkCore;

public class EntityFrameworkCoreIkapatigiCapstoneDbSchemaMigrator
    : IIkapatigiCapstoneDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreIkapatigiCapstoneDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the IkapatigiCapstoneDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<IkapatigiCapstoneDbContext>()
            .Database
            .MigrateAsync();
    }
}
