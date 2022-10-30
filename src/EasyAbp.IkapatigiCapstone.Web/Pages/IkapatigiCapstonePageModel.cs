using EasyAbp.IkapatigiCapstone.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace EasyAbp.IkapatigiCapstone.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class IkapatigiCapstonePageModel : AbpPageModel
{
    protected IkapatigiCapstonePageModel()
    {
        LocalizationResourceType = typeof(IkapatigiCapstoneResource);
    }
}
