using Volo.Abp.Modularity;

namespace EasyAbp.IkapatigiCapstone1;

[DependsOn(
    typeof(IkapatigiCapstone1ApplicationModule),
    typeof(IkapatigiCapstone1DomainTestModule)
    )]
public class IkapatigiCapstone1ApplicationTestModule : AbpModule
{

}
