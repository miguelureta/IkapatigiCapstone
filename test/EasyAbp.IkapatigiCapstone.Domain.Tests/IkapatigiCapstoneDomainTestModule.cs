using EasyAbp.IkapatigiCapstone.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EasyAbp.IkapatigiCapstone;

[DependsOn(
    typeof(IkapatigiCapstoneEntityFrameworkCoreTestModule)
    )]
public class IkapatigiCapstoneDomainTestModule : AbpModule
{

}
