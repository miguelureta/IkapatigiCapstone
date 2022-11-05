using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.IkapatigiCapstone1.Web;

[Dependency(ReplaceServices = true)]
public class IkapatigiCapstone1BrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "IkapatigiCapstone1";
}
