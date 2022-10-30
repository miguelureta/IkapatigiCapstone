using Volo.Abp.Modularity;

namespace EasyAbp.IkapatigiCapstone;

[DependsOn(
    typeof(IkapatigiCapstoneApplicationModule),
    typeof(IkapatigiCapstoneDomainTestModule)
    )]
public class IkapatigiCapstoneApplicationTestModule : AbpModule
{

}
