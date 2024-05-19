namespace CleanArchitechture.Domain.Enums.Users
{
    /// <summary>
    /// Kiểu dữ liệu giới tính
    /// </summary>
    public enum SexEnumTypes
    {
        [Display(Name = "Không xác định")]
        UNKNOWN = 0,
        [Display(Name = "Nam")]
        MALE = 1,
        [Display(Name = "Nữ")]
        FEMALE = 2
    }
}
