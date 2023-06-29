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
            new PermissionContent(EventPermission.GetAll, "Can view all event"),
            new PermissionContent(EventPermission.GetOne, "Can view event detail"),
            new PermissionContent(EventPermission.Create, "Can create event"),
            new PermissionContent(EventPermission.Update, "Can update event"),
            new PermissionContent(EventPermission.Delete, "Can delete event"),
            //
            new PermissionContent(InvitationPermission.GetForProject, "Can view all invitation"),
            // new PermissionContent(InvitationPermission.GetForUser, ""),
            new PermissionContent(InvitationPermission.Create, "Can create invitation"),
            new PermissionContent(InvitationPermission.Delete, "Can delete invitation"),
            // new PermissionContent(InvitationPermission.Accept, ""),
            // new PermissionContent(InvitationPermission.Decline, ""),
            //
            new PermissionContent(IssuePermission.GetForBoard, "Can get issues in board"),
            new PermissionContent(IssuePermission.UpdateForBoard, "Can update issue in board"),
            new PermissionContent(
                IssuePermission.MoveToBacklog,
                "Can move issues from sprint to backlog"
            ),
            new PermissionContent(IssuePermission.GetForBacklog, "Cat get issues in backlog"),
            new PermissionContent(IssuePermission.UpdateForBacklog, "Can update issue in backlog"),
            new PermissionContent(
                IssuePermission.MoveToSprint,
                "Can move issues from backlog to sprint"
            ),
            new PermissionContent(IssuePermission.GetOne, "Can view issue detail"),
            new PermissionContent(IssuePermission.Create, "Can create issue"),
            new PermissionContent(IssuePermission.Update, "Can update issue"),
            new PermissionContent(IssuePermission.Delete, "Can delete issue"),
            //
            // new PermissionContent(PaymentPermission.Create, ""),
            // new PermissionContent(PaymentPermission.Submit, ""),
            //
            // new PermissionContent(ProjectPermission.GetAll, ""),
            new PermissionContent(ProjectPermission.GetOne, "Can view project detail"),
            // new PermissionContent(ProjectPermission.Create, ""),
            new PermissionContent(ProjectPermission.Update, "Can update project"),
            new PermissionContent(ProjectPermission.Delete, "Can delete project"),
            //
            new PermissionContent(ProjectMemberPermission.GetAll, "Can view all project member"),
            new PermissionContent(ProjectMemberPermission.Delete, "Can delete project member"),
            //
            new PermissionContent(ProjectStatusPermission.GetAll, "Can view all project status"),
            new PermissionContent(ProjectStatusPermission.Create, "Can create project status"),
            new PermissionContent(ProjectStatusPermission.Update, "Can update project status"),
            new PermissionContent(ProjectStatusPermission.Delete, "Can delete project status"),
            //
            new PermissionContent(SprintPermission.GetAvailable, "Can view available sprint"),
            new PermissionContent(SprintPermission.GetOne, "Can view sprint detail"),
            new PermissionContent(SprintPermission.Create, "Can create sprint"),
            new PermissionContent(SprintPermission.Update, "Can update sprint"),
            new PermissionContent(SprintPermission.Delete, "Can delete sprint"),
            new PermissionContent(SprintPermission.Start, "Can start sprint"),
            new PermissionContent(SprintPermission.Complete, "Can complete sprint"),
            //
            // new PermissionContent(ProjectRolePermission.GetAll, ""),
            // new PermissionContent(ProjectRolePermission.GetSelect, ""),
            // new PermissionContent(ProjectRolePermission.GetOne, ""),
            // new PermissionContent(ProjectRolePermission.Create, ""),
            // new PermissionContent(ProjectRolePermission.Update, ""),
            // new PermissionContent(ProjectRolePermission.Delete, ""),
            //
            new PermissionContent(MemberRolePermission.GetAll, "Can view all member role"),
            new PermissionContent(MemberRolePermission.Create, "Can create member role"),
            new PermissionContent(MemberRolePermission.Delete, "Can delete member role"),
        };
    }
}
