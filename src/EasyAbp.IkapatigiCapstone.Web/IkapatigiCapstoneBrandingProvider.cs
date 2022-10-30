using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.IkapatigiCapstone.Web;

[Dependency(ReplaceServices = true)]
public class IkapatigiCapstoneBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "IkapatigiCapstone";
}
