using static PlcBase.Shared.Enums.PermissionPolicy;

namespace PlcBase.Shared.Helpers;

public class PermissionHelper : IPermissionHelper
{
    public List<PermissionContent> GetAllPermissions()
    {
        return new List<PermissionContent>()
        {
            // new (UserPermission.GetAll, ""),
            // new (UserPermission.GetOne, ""),
            // new (UserPermission.Update, ""),
            // new (UserPermission.GetPersonal, ""),
            // new (UserPermission.UpdatePersonal, ""),
            // new (UserPermission.GetAnonymous, ""),
            //
            // new (RolePermission.GetAll, ""),
            //
            // new (ConfigSettingPermission.GetAll, ""),
            // new (ConfigSettingPermission.GetOne, ""),
            // new (ConfigSettingPermission.Update, ""),
            //
            new(EventPermission.GetAll, "Can view all event"),
            new(EventPermission.GetOne, "Can view event detail"),
            new(EventPermission.Create, "Can create event"),
            new(EventPermission.Update, "Can update event"),
            new(EventPermission.Delete, "Can delete event"),
            //
            new(InvitationPermission.GetForProject, "Can view all invitation"),
            // new (InvitationPermission.GetForUser, ""),
            new(InvitationPermission.Create, "Can create invitation"),
            new(InvitationPermission.Delete, "Can delete invitation"),
            // new (InvitationPermission.Accept, ""),
            // new (InvitationPermission.Decline, ""),
            //
            new(IssuePermission.GetForBoard, "Can get issues in board"),
            new(IssuePermission.UpdateForBoard, "Can update issue in board"),
            new(IssuePermission.MoveToBacklog, "Can move issues from sprint to backlog"),
            new(IssuePermission.GetForBacklog, "Cat get issues in backlog"),
            new(IssuePermission.UpdateForBacklog, "Can update issue in backlog"),
            new(IssuePermission.MoveToSprint, "Can move issues from backlog to sprint"),
            new(IssuePermission.GetOne, "Can view issue detail"),
            new(IssuePermission.Create, "Can create issue"),
            new(IssuePermission.Update, "Can update issue"),
            new(IssuePermission.Delete, "Can delete issue"),
            //
            // new (PaymentPermission.Create, ""),
            // new (PaymentPermission.Submit, ""),
            //
            // new (ProjectPermission.GetAll, ""),
            new(ProjectPermission.GetOne, "Can view project detail"),
            // new (ProjectPermission.Create, ""),
            new(ProjectPermission.Update, "Can update project"),
            new(ProjectPermission.Delete, "Can delete project"),
            //
            new(ProjectMemberPermission.GetAll, "Can view all project member"),
            new(ProjectMemberPermission.Delete, "Can delete project member"),
            //
            // new (ProjectStatusPermission.GetAll, "Can view all project status"),
            new(ProjectStatusPermission.Create, "Can create project status"),
            new(ProjectStatusPermission.Update, "Can update project status"),
            new(ProjectStatusPermission.Delete, "Can delete project status"),
            new(ProjectStatusPermission.UpdateForBoard, "Can update project status in board"),
            //
            // new (SprintPermission.GetAvailable, "Can view available sprint"),
            // new (SprintPermission.GetOne, "Can view sprint detail"),
            new(SprintPermission.Create, "Can create sprint"),
            new(SprintPermission.Update, "Can update sprint"),
            new(SprintPermission.Delete, "Can delete sprint"),
            new(SprintPermission.Start, "Can start sprint"),
            new(SprintPermission.Complete, "Can complete sprint"),
            //
            // new (ProjectRolePermission.GetAll, ""),
            // new (ProjectRolePermission.GetSelect, ""),
            // new (ProjectRolePermission.GetOne, ""),
            // new (ProjectRolePermission.Create, ""),
            // new (ProjectRolePermission.Update, ""),
            // new (ProjectRolePermission.Delete, ""),
            //
            new(MemberRolePermission.GetAll, "Can view all member role"),
            new(MemberRolePermission.Create, "Can create member role"),
            new(MemberRolePermission.Delete, "Can delete member role"),
        };
    }
}
