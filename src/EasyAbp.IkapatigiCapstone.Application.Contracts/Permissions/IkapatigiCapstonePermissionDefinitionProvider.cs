using EasyAbp.IkapatigiCapstone.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace EasyAbp.IkapatigiCapstone.Permissions;

public class IkapatigiCapstonePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(IkapatigiCapstonePermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(IkapatigiCapstonePermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<IkapatigiCapstoneResource>(name);
    }
}
