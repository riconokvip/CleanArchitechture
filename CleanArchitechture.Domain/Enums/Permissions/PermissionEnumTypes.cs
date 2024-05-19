namespace CleanArchitechture.Domain.Enums.Permissions
{
    public enum PermissionEnumTypes
    {
        [Display(Name = "Quản trị viên cấp cao")]
        SUPERADMIN = 0,
        [Display(Name = "Quản trị viên")]
        ADMIN = 1
    }
}
