namespace CleanArchitechture.Domain.Entities
{
    /// <summary>
    /// Thực thể phân quyền người dùng trong hệ thống
    /// </summary>
    [Table("UserPermissions")]
    public class UserPermissionEntities : BaseEntity<string>
    {
        /// <summary>
        /// Id người dùng
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Id phân quyền
        /// </summary>
        public string PermissionId { get; set; }
    }
}
