namespace BaselineAPIProjectController.Permissions;

public static partial class Permission
{
    public static class Project
    {
        public const string Create = "project:create";
        public const string Read = "project:read";
        public const string Update = "project:update";
        public const string Delete = "project:delete";
        public const string ManageBudget = "project:manage_budget";
    }
}