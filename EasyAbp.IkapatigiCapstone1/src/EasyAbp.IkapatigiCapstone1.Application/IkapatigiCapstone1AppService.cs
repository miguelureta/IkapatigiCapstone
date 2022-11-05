using System;
using System.Collections.Generic;
using System.Text;
using EasyAbp.IkapatigiCapstone1.Localization;
using Volo.Abp.Application.Services;

namespace EasyAbp.IkapatigiCapstone1;

/* Inherit your application services from this class.
 */
public abstract class IkapatigiCapstone1AppService : ApplicationService
{
    protected IkapatigiCapstone1AppService()
    {
        LocalizationResource = typeof(IkapatigiCapstone1Resource);
    }
}
