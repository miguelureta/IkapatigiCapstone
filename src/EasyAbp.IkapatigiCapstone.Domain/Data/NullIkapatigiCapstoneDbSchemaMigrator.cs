using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.IkapatigiCapstone.Data;

/* This is used if database provider does't define
 * IIkapatigiCapstoneDbSchemaMigrator implementation.
 */
public class NullIkapatigiCapstoneDbSchemaMigrator : IIkapatigiCapstoneDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
