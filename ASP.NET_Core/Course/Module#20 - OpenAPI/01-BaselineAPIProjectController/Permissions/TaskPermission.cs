namespace BaselineAPIProjectController.Permissions;

public static partial class Permission
{
    public static class Task
    {
        public const string Create = "task:create";
        public const string Read = "task:read";
        public const string Update = "task:update";
        public const string Delete = "task:delete";
        public const string AssignUser = "task:assign_user";
        public const string UpdateStatus = "task:update_status";
        public const string Comment = "task:comment";
    }
}