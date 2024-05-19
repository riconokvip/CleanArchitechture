namespace CleanArchitechture.Domain.Entities
{
    /// <summary>
    /// Thực thể tệp dữ liệu trong hệ thống
    /// </summary>
    [Table("Files")]
    public class FileEntities : BaseEntity<string>
    {
        /// <summary>
        /// Đường dẫn đến thư mục lưu trữ trực tiếp
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Tên tệp
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Kích thước tệp
        /// </summary>
        public int FileSize { get; set; }

        /// <summary>
        /// Loại tệp
        /// </summary>
        public FileEnumTypes FileType { get; set; }

        /// <summary>
        /// Tệp
        /// </summary>
        public string File { get; set; }

        /// <summary>
        /// Trạng thái sử dụng
        /// </summary>
        public FileUseEnumTypes IsUsed { get; set; } = FileUseEnumTypes.UNSED;

        /// <summary>
        /// Loại extension của tệp
        /// </summary>
        public string FileExtension { get; set; }

        /// <summary>
        /// Kiểu tệp
        /// </summary>
        public string ContentType { get; set; }
    }
}
