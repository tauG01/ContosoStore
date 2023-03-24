using ContosoStore.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace ContosoStore.Permissions;

public class ContosoStorePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(ContosoStorePermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(ContosoStorePermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ContosoStoreResource>(name);
    }
}
