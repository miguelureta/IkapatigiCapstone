using EasyAbp.IkapatigiCapstone1.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace EasyAbp.IkapatigiCapstone1.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(IkapatigiCapstone1EntityFrameworkCoreModule),
    typeof(IkapatigiCapstone1ApplicationContractsModule)
    )]
public class IkapatigiCapstone1DbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}
