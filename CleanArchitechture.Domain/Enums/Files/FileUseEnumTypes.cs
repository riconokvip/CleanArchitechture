namespace CleanArchitechture.Domain.Enums.Files
{
    /// <summary>
    /// Kiểu dữ liệu trạng thái sử dụng
    /// </summary>
    public enum FileUseEnumTypes
    {
        [Display(Name = "Chưa sử dụng")]
        UNSED = 0,
        [Display(Name = "Đã sử dụng")]
        USED = 1
    }
}
