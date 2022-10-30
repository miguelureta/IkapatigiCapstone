using EasyAbp.IkapatigiCapstone1.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace EasyAbp.IkapatigiCapstone1.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class IkapatigiCapstone1PageModel : AbpPageModel
{
    protected IkapatigiCapstone1PageModel()
    {
        LocalizationResourceType = typeof(IkapatigiCapstone1Resource);
    }
}
