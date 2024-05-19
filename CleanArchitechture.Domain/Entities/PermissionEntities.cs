namespace CleanArchitechture.Domain.Entities
{
    /// <summary>
    /// Thực thể phân quyền trong hệ thống
    /// </summary>
    [Table("Permissions")]
    public class PermissionEntities : BaseEntity<string>
    {
        /// <summary>
        /// Tên phân quyền
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Claim
        /// </summary>
        public PermissionEnumTypes Claim { get; set; }

        /// <summary>
        /// Trạng thái kích hoạt
        /// </summary>
        public SystemActiveEnumTypes IsActive { get; set; } = SystemActiveEnumTypes.ACTIVE;
    }
}
