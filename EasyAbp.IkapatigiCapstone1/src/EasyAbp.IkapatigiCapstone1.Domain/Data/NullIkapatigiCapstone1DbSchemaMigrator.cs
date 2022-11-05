using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.IkapatigiCapstone1.Data;

/* This is used if database provider does't define
 * IIkapatigiCapstone1DbSchemaMigrator implementation.
 */
public class NullIkapatigiCapstone1DbSchemaMigrator : IIkapatigiCapstone1DbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
