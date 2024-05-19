namespace CleanArchitechture.Domain.Enums
{
    /// <summary>
    /// Kiểu dữ liệu trường kích hoạt
    /// </summary>
    public enum SystemActiveEnumTypes
    {
        [Display(Name = "Không kích hoạt")]
        INACTIVE = 0,
        [Display(Name = "Kích hoạt")]
        ACTIVE = 1
    }
}
