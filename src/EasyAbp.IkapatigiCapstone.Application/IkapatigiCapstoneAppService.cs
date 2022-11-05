using System;
using System.Collections.Generic;
using System.Text;
using EasyAbp.IkapatigiCapstone.Localization;
using Volo.Abp.Application.Services;

namespace EasyAbp.IkapatigiCapstone;

/* Inherit your application services from this class.
 */
public abstract class IkapatigiCapstoneAppService : ApplicationService
{
    protected IkapatigiCapstoneAppService()
    {
        LocalizationResource = typeof(IkapatigiCapstoneResource);
    }
}
