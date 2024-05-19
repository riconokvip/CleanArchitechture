namespace CleanArchitechture.Domain.Enums.Users
{
    /// <summary>
    /// Kiểu dữ liệu trạng thái kích hoạt xác thực hai bước
    /// </summary>
    public enum UserTwoFactorEnumTypes
    {
        [Display(Name = "Tắt")]
        DISABLED = 0,
        [Display(Name = "Bật")]
        ENABLED = 1
    }
}
