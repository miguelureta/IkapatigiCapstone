using EasyAbp.IkapatigiCapstone1.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EasyAbp.IkapatigiCapstone1;

[DependsOn(
    typeof(IkapatigiCapstone1EntityFrameworkCoreTestModule)
    )]
public class IkapatigiCapstone1DomainTestModule : AbpModule
{

}
