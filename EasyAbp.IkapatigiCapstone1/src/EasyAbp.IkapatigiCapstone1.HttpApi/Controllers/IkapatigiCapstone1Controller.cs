using EasyAbp.IkapatigiCapstone1.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.IkapatigiCapstone1.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class IkapatigiCapstone1Controller : AbpControllerBase
{
    protected IkapatigiCapstone1Controller()
    {
        LocalizationResource = typeof(IkapatigiCapstone1Resource);
    }
}
