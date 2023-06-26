namespace PlcBase.Shared.Enums;

public static class PermissionPolicy
{
    public static class UserPermission
    {
        public const string GetAll = "User.GetAll";
        public const string GetOne = "User.GetOne";
        public const string Update = "User.Update";
        public const string GetPersonal = "User.GetPersonal";
        public const string UpdatePersonal = "User.UpdatePersonal";
        public const string GetAnonymous = "User.GetAnonymous";
    }

    public static class RolePermission
    {
        public const string GetAll = "Role.GetAll";
    }

    public static class ConfigSettingPermission
    {
        public const string GetAll = "ConfigSetting.GetAll";
        public const string GetOne = "ConfigSetting.GetOne";
        public const string Update = "ConfigSetting.Update";
    }

    public static class EventPermission
    {
        public const string GetAll = "Event.GetAll";
        public const string GetOne = "Event.GetOne";
        public const string Create = "Event.Create";
        public const string Update = "Event.Update";
        public const string Delete = "Event.Delete";
    }

    public static class InvitationPermission
    {
        public const string GetForProject = "Invitation.GetForProject";
        public const string GetForUser = "Invitation.GetForUser";
        public const string Create = "Invitation.Create";
        public const string Delete = "Invitation.Delete";
        public const string Accept = "Invitation.Accept";
        public const string Decline = "Invitation.Decline";
    }

    public static class IssuePermission
    {
        public const string GetForBoard = "Issue.GetForBoard";
        public const string UpdateForBoard = "Issue.UpdateForBoard";
        public const string MoveToBacklog = "Issue.MoveToBacklog";
        public const string GetForBacklog = "Issue.GetForBacklog";
        public const string UpdateForBacklog = "Issue.UpdateForBacklog";
        public const string MoveToSprint = "Issue.MoveToSprint";
        public const string GetOne = "Issue.GetOne";
        public const string Create = "Issue.Create";
        public const string Update = "Issue.Update";
        public const string Delete = "Issue.Delete";
    }

    public static class PaymentPermission
    {
        public const string Create = "Payment.Create";
        public const string Submit = "Payment.Submit";
    }

    public static class ProjectPermission
    {
        public const string GetAll = "Project.GetAll";
        public const string GetOne = "Project.GetOne";
        public const string Create = "Project.Create";
        public const string Update = "Project.Update";
        public const string Delete = "Project.Delete";
    }

    public static class ProjectMemberPermission
    {
        public const string GetAll = "ProjectMember.GetAll";
        public const string GetSelect = "ProjectMember.GetSelect";
        public const string Delete = "ProjectMember.Delete";
    }

    public static class ProjectStatusPermission
    {
        public const string GetAll = "ProjectStatus.GetAll";
        public const string Create = "ProjectStatus.Create";
        public const string Update = "ProjectStatus.Update";
        public const string Delete = "ProjectStatus.Delete";
    }

    public static class SprintPermission
    {
        public const string GetAvailable = "Sprint.GetAvailable";
        public const string GetOne = "Sprint.GetOne";
        public const string Create = "Sprint.Create";
        public const string Update = "Sprint.Update";
        public const string Delete = "Sprint.Delete";
        public const string Start = "Sprint.Start";
        public const string Complete = "Sprint.Complete";
    }

    public static class ProjectRolePermission
    {
        public const string GetAll = "ProjectRole.GetAll";
        public const string GetSelect = "ProjectRole.GetSelect";
        public const string GetOne = "ProjectRole.GetOne";
        public const string Create = "ProjectRole.Create";
        public const string Update = "ProjectRole.Update";
        public const string Delete = "ProjectRole.Delete";
    }

    public static class MemberRolePermission
    {
        public const string GetAll = "MemberRole.GetAll";
        public const string Create = "MemberRole.Create";
        public const string Delete = "MemberRole.Delete";
    }
}
