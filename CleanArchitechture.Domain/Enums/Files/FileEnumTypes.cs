namespace CleanArchitechture.Domain.Enums.Files
{
    /// <summary>
    /// Kiểu dữ liệu tệp
    /// </summary>
    public enum FileEnumTypes
    {
        [Display(Name = "Hình ảnh")]
        IMAGE = 0,
        [Display(Name = "Video")]
        VIDEO = 1,
        [Display(Name = "Tệp")]
        FILE = 2
    }
}
