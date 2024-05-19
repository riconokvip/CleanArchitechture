namespace CleanArchitechture.Domain.Enums.Users
{
    /// <summary>
    /// Kiểu dữ liệu trạng thái xác thực
    /// </summary>
    public enum UserVerifyEnumTypes
    {
        [Display(Name = "Chưa xác thực")]
        NO_VERIFY = 0,
        [Display(Name = "Xác thực")]
        VERIFY = 1
    }
}
