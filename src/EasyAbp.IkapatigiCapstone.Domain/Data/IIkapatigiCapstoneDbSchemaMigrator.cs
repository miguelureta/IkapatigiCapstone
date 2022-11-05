using System.Threading.Tasks;

namespace EasyAbp.IkapatigiCapstone.Data;

public interface IIkapatigiCapstoneDbSchemaMigrator
{
    Task MigrateAsync();
}
