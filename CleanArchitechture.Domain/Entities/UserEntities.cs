namespace CleanArchitechture.Domain.Entities
{
    /// <summary>
    /// Thực thể người dùng trong hệ thống
    /// </summary>
    [Table("Users")]
    public class UserEntities : BaseEntity<string>
    {
        /// <summary>
        /// Tên hiển thị
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Tên công việc/ tên đầy đủ
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Mã người dùng
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Tài khoản email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Ảnh đại diện
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// Id ảnh đại diện
        /// </summary>
        public string AvatarId { get; set; }

        /// <summary>
        /// Mô tả bản thân
        /// </summary>
        public string Bio { get; set; }

        /// <summary>
        /// Ngày bắt đầu làm việc
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public SexEnumTypes Sex { get; set; } = SexEnumTypes.UNKNOWN;

        /* -- verify -- */
        /// <summary>
        /// Trạng thái xác thực tài khoản email
        /// </summary>
        public UserVerifyEnumTypes EmailConfirmed { get; set; } = UserVerifyEnumTypes.NO_VERIFY;

        /// <summary>
        /// Trạng thái xác thực số điện thoại
        /// </summary>
        public UserVerifyEnumTypes PhoneConfirmed { get; set; } = UserVerifyEnumTypes.NO_VERIFY;

        /// <summary>
        /// Trạng thái bật/tắt xác thực hai bước
        /// </summary>
        public UserTwoFactorEnumTypes TwoFactorEnabled { get; set; } = UserTwoFactorEnumTypes.DISABLED;

        /* -- security -- */
        /// <summary>
        /// Mã bảo mật
        /// </summary>
        public string SecurityStamp { get; set; }

        /// <summary>
        /// Mật khẩu
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Số cần kết nối thất bại
        /// </summary>
        public int AccessFailedCount { get; set; } = 0;

        /* -- Violation -- */
        /// <summary>
        /// Trạng thái kích hoạt khóa khi vi phạm
        /// </summary>
        public UserLockoutViolationEnumTypes LockoutViolationEnabled { get; set; } = UserLockoutViolationEnumTypes.UNLOCK;

        /// <summary>
        /// Lý do khóa khi vi phạm
        /// </summary>
        public string LockoutViolationReason { get; set; }

        /// <summary>
        /// Thời hạn khóa khi vi phạm
        /// </summary>
        public DateTime? LockoutViolationEnd { get; set; }
    }
}
