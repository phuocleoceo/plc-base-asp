using static PlcBase.Common.Enums.PermissionPolicy;

namespace PlcBase.Helpers;

public class PermissionHelper : IPermissionHelper
{
    public List<PermissionContent> GetAllPermissions()
    {
        return new List<PermissionContent>()
        {
            new PermissionContent(CommonPermission.Basic, "Basic permisison"),
        };
    }
}