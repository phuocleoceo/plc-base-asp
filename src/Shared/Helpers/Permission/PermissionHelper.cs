using static PlcBase.Shared.Enums.PermissionPolicy;

namespace PlcBase.Shared.Helpers;

public class PermissionHelper : IPermissionHelper
{
    public List<PermissionContent> GetAllPermissions()
    {
        return new List<PermissionContent>()
        {
            new PermissionContent(CommonPermission.Basic, "Basic Permisison"),
        };
    }
}
