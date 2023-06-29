using static PlcBase.Shared.Enums.PermissionPolicy;

namespace PlcBase.Shared.Helpers;

public class PermissionHelper : IPermissionHelper
{
    public List<PermissionContent> GetAllPermissions()
    {
        return new List<PermissionContent>()
        {
            // new PermissionContent(UserPermission.GetAll, ""),
            // new PermissionContent(UserPermission.GetOne, ""),
            // new PermissionContent(UserPermission.Update, ""),
            // new PermissionContent(UserPermission.GetPersonal, ""),
            // new PermissionContent(UserPermission.UpdatePersonal, ""),
            // new PermissionContent(UserPermission.GetAnonymous, ""),
            //
            // new PermissionContent(RolePermission.GetAll, ""),
            //
            // new PermissionContent(ConfigSettingPermission.GetAll, ""),
            // new PermissionContent(ConfigSettingPermission.GetOne, ""),
            // new PermissionContent(ConfigSettingPermission.Update, ""),
            //
            new PermissionContent(EventPermission.GetAll, ""),
            new PermissionContent(EventPermission.GetOne, ""),
            new PermissionContent(EventPermission.Create, ""),
            new PermissionContent(EventPermission.Update, ""),
            new PermissionContent(EventPermission.Delete, ""),
            //
            new PermissionContent(InvitationPermission.GetForProject, ""),
            // new PermissionContent(InvitationPermission.GetForUser, ""),
            new PermissionContent(InvitationPermission.Create, ""),
            new PermissionContent(InvitationPermission.Delete, ""),
            // new PermissionContent(InvitationPermission.Accept, ""),
            // new PermissionContent(InvitationPermission.Decline, ""),
            //
            new PermissionContent(IssuePermission.GetForBoard, ""),
            new PermissionContent(IssuePermission.UpdateForBoard, ""),
            new PermissionContent(IssuePermission.MoveToBacklog, ""),
            new PermissionContent(IssuePermission.GetForBacklog, ""),
            new PermissionContent(IssuePermission.UpdateForBacklog, ""),
            new PermissionContent(IssuePermission.MoveToSprint, ""),
            new PermissionContent(IssuePermission.GetOne, ""),
            new PermissionContent(IssuePermission.Create, ""),
            new PermissionContent(IssuePermission.Update, ""),
            new PermissionContent(IssuePermission.Delete, ""),
            //
            // new PermissionContent(PaymentPermission.Create, ""),
            // new PermissionContent(PaymentPermission.Submit, ""),
            //
            // new PermissionContent(ProjectPermission.GetAll, ""),
            new PermissionContent(ProjectPermission.GetOne, ""),
            // new PermissionContent(ProjectPermission.Create, ""),
            new PermissionContent(ProjectPermission.Update, ""),
            new PermissionContent(ProjectPermission.Delete, ""),
            //
            new PermissionContent(ProjectMemberPermission.GetAll, ""),
            new PermissionContent(ProjectMemberPermission.GetSelect, ""),
            new PermissionContent(ProjectMemberPermission.Delete, ""),
            //
            new PermissionContent(ProjectStatusPermission.GetAll, ""),
            new PermissionContent(ProjectStatusPermission.Create, ""),
            new PermissionContent(ProjectStatusPermission.Update, ""),
            new PermissionContent(ProjectStatusPermission.Delete, ""),
            //
            new PermissionContent(SprintPermission.GetAvailable, ""),
            new PermissionContent(SprintPermission.GetOne, ""),
            new PermissionContent(SprintPermission.Create, ""),
            new PermissionContent(SprintPermission.Update, ""),
            new PermissionContent(SprintPermission.Delete, ""),
            new PermissionContent(SprintPermission.Start, ""),
            new PermissionContent(SprintPermission.Complete, ""),
            //
            // new PermissionContent(ProjectRolePermission.GetAll, ""),
            // new PermissionContent(ProjectRolePermission.GetSelect, ""),
            // new PermissionContent(ProjectRolePermission.GetOne, ""),
            // new PermissionContent(ProjectRolePermission.Create, ""),
            // new PermissionContent(ProjectRolePermission.Update, ""),
            // new PermissionContent(ProjectRolePermission.Delete, ""),
            //
            new PermissionContent(MemberRolePermission.GetAll, ""),
            new PermissionContent(MemberRolePermission.Create, ""),
            new PermissionContent(MemberRolePermission.Delete, ""),
        };
    }
}
