using EasyAbp.IkapatigiCapstone.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.IkapatigiCapstone.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class IkapatigiCapstoneController : AbpControllerBase
{
    protected IkapatigiCapstoneController()
    {
        LocalizationResource = typeof(IkapatigiCapstoneResource);
    }
}
