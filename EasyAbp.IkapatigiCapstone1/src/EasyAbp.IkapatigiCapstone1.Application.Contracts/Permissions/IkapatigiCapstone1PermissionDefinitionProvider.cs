﻿using EasyAbp.IkapatigiCapstone1.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace EasyAbp.IkapatigiCapstone1.Permissions;

public class IkapatigiCapstone1PermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(IkapatigiCapstone1Permissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(IkapatigiCapstone1Permissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<IkapatigiCapstone1Resource>(name);
    }
}
