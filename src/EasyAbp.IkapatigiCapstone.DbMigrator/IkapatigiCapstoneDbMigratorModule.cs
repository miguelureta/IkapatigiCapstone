using EasyAbp.IkapatigiCapstone.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace EasyAbp.IkapatigiCapstone.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(IkapatigiCapstoneEntityFrameworkCoreModule),
    typeof(IkapatigiCapstoneApplicationContractsModule)
    )]
public class IkapatigiCapstoneDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}
