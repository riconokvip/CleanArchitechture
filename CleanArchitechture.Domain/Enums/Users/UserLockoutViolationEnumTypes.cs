namespace CleanArchitechture.Domain.Enums.Users
{
    /// <summary>
    /// Kiểu dữ liệu trạng thái khóa tài khoản
    /// </summary>
    public enum UserLockoutViolationEnumTypes
    {
        [Display(Name = "Khóa")]
        LOCK = 0,
        [Display(Name = "Không khóa")]
        UNLOCK = 1
    }
}
